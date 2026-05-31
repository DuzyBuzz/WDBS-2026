using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Dashboard;

internal static class BillerDashboardDataService
{
    public static async Task<BillerDashboardSnapshot> GetSnapshotAsync(UserRole role, DateTime month)
    {
        DateTime periodStart = new(month.Year, month.Month, 1);
        DateTime periodEnd = periodStart.AddMonths(1);

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        (decimal totalBilledAmount, int totalBillCount, int unpaidBillCount) = await GetBillingTotalsAsync(connection, periodStart, periodEnd);
        int activeConcessionaireCount = await GetActiveConcessionaireCountAsync(connection);

        DateTime trendStart = periodStart.AddMonths(-11);
        IReadOnlyList<BillerDashboardPoint> billedTrend = await GetMonthlyTrendAsync(connection, trendStart, periodEnd);
        IReadOnlyList<BillerBillingStatusMonthly> billingStatusMonthly = await GetBillingStatusMonthlyAsync(connection, month);
        IReadOnlyList<BillerDashboardPoint> concessionairesByZone = await GetConcessionairesByZoneAsync(connection);
        IReadOnlyList<BillerDashboardPoint> concessionaireStatusMix = await GetConcessionaireStatusMixAsync(connection);
        IReadOnlyList<BillerDashboardPoint> topConcessionairesByAmount = await GetTopConcessionairesByAmountAsync(connection, periodStart, periodEnd);
        IReadOnlyList<BillerServiceBillingMonthly> serviceBillingMonthly = await GetServiceBillingMonthlyAsync(connection, month);

        return new BillerDashboardSnapshot(
            periodStart,
            periodEnd,
            totalBilledAmount,
            totalBillCount,
            unpaidBillCount,
            activeConcessionaireCount,
            billedTrend,
            billingStatusMonthly,
            concessionairesByZone,
            concessionaireStatusMix,
            topConcessionairesByAmount,
            serviceBillingMonthly);
    }

    private static async Task<(decimal TotalBilledAmount, int TotalBillCount, int UnpaidBillCount)> GetBillingTotalsAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT
    COALESCE(SUM(b.total_amount), 0.00) AS total_billed,
    COUNT(*) AS bill_count,
    SUM(CASE WHEN LOWER(TRIM(b.status)) IN ('unpaid', 'overdue', 'partially_paid') THEN 1 ELSE 0 END) AS unpaid_count
FROM billing b
WHERE b.billing_date >= @periodStart
  AND b.billing_date < @periodEnd;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@periodStart", periodStart);
        command.Parameters.AddWithValue("@periodEnd", periodEnd);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return (0M, 0, 0);
        }

        int totalBilledOrdinal = reader.GetOrdinal("total_billed");
        int billCountOrdinal = reader.GetOrdinal("bill_count");
        int unpaidCountOrdinal = reader.GetOrdinal("unpaid_count");

        decimal totalBilled = reader.IsDBNull(totalBilledOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(totalBilledOrdinal));
        int billCount = reader.IsDBNull(billCountOrdinal) ? 0 : Convert.ToInt32(reader.GetValue(billCountOrdinal));
        int unpaidCount = reader.IsDBNull(unpaidCountOrdinal) ? 0 : Convert.ToInt32(reader.GetValue(unpaidCountOrdinal));

        return (totalBilled, billCount, unpaidCount);
    }

    private static async Task<int> GetActiveConcessionaireCountAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT COUNT(*)
FROM v_concessionaire_details v
WHERE UPPER(TRIM(v.Status)) = 'ACTIVE';";

        await using var command = new MySqlCommand(sql, connection);
        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0);
    }

    private static async Task<IReadOnlyList<BillerDashboardPoint>> GetMonthlyTrendAsync(
        MySqlConnection connection,
        DateTime trendStart,
        DateTime trendEnd)
    {
        const string sql = @"
SELECT
    DATE_FORMAT(b.billing_date, '%Y-%m') AS month_key,
    SUM(b.total_amount) AS total_billed
FROM billing b
WHERE b.billing_date >= @trendStart
  AND b.billing_date < @trendEnd
GROUP BY DATE_FORMAT(b.billing_date, '%Y-%m')
ORDER BY month_key ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@trendStart", trendStart);
        command.Parameters.AddWithValue("@trendEnd", trendEnd);

        await using var reader = await command.ExecuteReaderAsync();

        var valuesByMonth = new Dictionary<string, decimal>(StringComparer.Ordinal);
        while (await reader.ReadAsync())
        {
            int labelOrdinal = reader.GetOrdinal("month_key");
            int valueOrdinal = reader.GetOrdinal("total_billed");

            string label = reader.IsDBNull(labelOrdinal) ? string.Empty : Convert.ToString(reader.GetValue(labelOrdinal)) ?? string.Empty;
            decimal value = reader.IsDBNull(valueOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(valueOrdinal));
            if (!string.IsNullOrWhiteSpace(label))
            {
                valuesByMonth[label] = value;
            }
        }

        var points = new List<BillerDashboardPoint>(12);
        DateTime monthCursor = new DateTime(trendStart.Year, trendStart.Month, 1);
        DateTime trendLimit = new DateTime(trendEnd.Year, trendEnd.Month, 1);

        while (monthCursor < trendLimit)
        {
            string monthKey = monthCursor.ToString("yyyy-MM");
            decimal value = valuesByMonth.TryGetValue(monthKey, out decimal existing) ? existing : 0M;
            points.Add(new BillerDashboardPoint(monthKey, value));
            monthCursor = monthCursor.AddMonths(1);
        }

        return points;
    }

    private static async Task<IReadOnlyList<BillerBillingStatusMonthly>> GetBillingStatusMonthlyAsync(MySqlConnection connection, DateTime month)
    {
        string monthKey = month.ToString("yyyy-MM");
        
        const string sql = @"
SELECT
    billing_month,
    total_bills,
    paid_count,
    partially_paid_count,
    overdue_count,
    unpaid_count,
    paid_percent,
    partially_paid_percent,
    overdue_percent,
    unpaid_percent
FROM v_billing_status
WHERE billing_month = @monthKey
LIMIT 1;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@monthKey", monthKey);

        await using var reader = await command.ExecuteReaderAsync();

        var items = new List<BillerBillingStatusMonthly>();
        
        if (await reader.ReadAsync())
        {
            items.Add(new BillerBillingStatusMonthly(
                reader.IsDBNull(reader.GetOrdinal("billing_month")) ? monthKey : Convert.ToString(reader.GetValue(reader.GetOrdinal("billing_month"))) ?? monthKey,
                reader.IsDBNull(reader.GetOrdinal("total_bills")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total_bills"))),
                reader.IsDBNull(reader.GetOrdinal("paid_count")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("paid_count"))),
                reader.IsDBNull(reader.GetOrdinal("partially_paid_count")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("partially_paid_count"))),
                reader.IsDBNull(reader.GetOrdinal("overdue_count")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("overdue_count"))),
                reader.IsDBNull(reader.GetOrdinal("unpaid_count")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("unpaid_count"))),
                reader.IsDBNull(reader.GetOrdinal("paid_percent")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("paid_percent"))),
                reader.IsDBNull(reader.GetOrdinal("partially_paid_percent")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("partially_paid_percent"))),
                reader.IsDBNull(reader.GetOrdinal("overdue_percent")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("overdue_percent"))),
                reader.IsDBNull(reader.GetOrdinal("unpaid_percent")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("unpaid_percent")))));
        }

        return items;
    }

    private static async Task<IReadOnlyList<BillerDashboardPoint>> GetBillingStatusBreakdownAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT
    UPPER(TRIM(b.status)) AS status_name,
    COUNT(*) AS status_count
FROM billing b
WHERE b.billing_date >= @periodStart
  AND b.billing_date < @periodEnd
GROUP BY UPPER(TRIM(b.status))
ORDER BY status_count DESC, status_name ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@periodStart", periodStart);
        command.Parameters.AddWithValue("@periodEnd", periodEnd);

        await using var reader = await command.ExecuteReaderAsync();

        var points = new List<BillerDashboardPoint>();
        while (await reader.ReadAsync())
        {
            int labelOrdinal = reader.GetOrdinal("status_name");
            int valueOrdinal = reader.GetOrdinal("status_count");

            string label = reader.IsDBNull(labelOrdinal) ? "UNKNOWN" : Convert.ToString(reader.GetValue(labelOrdinal)) ?? "UNKNOWN";
            decimal value = reader.IsDBNull(valueOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(valueOrdinal));
            points.Add(new BillerDashboardPoint(label, value));
        }

        return points;
    }

    private static async Task<IReadOnlyList<BillerDashboardPoint>> GetConcessionairesByZoneAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT
    COALESCE(NULLIF(TRIM(v.Zone), ''), 'UNASSIGNED') AS zone_name,
    COUNT(*) AS zone_count
FROM v_concessionaire_details v
GROUP BY COALESCE(NULLIF(TRIM(v.Zone), ''), 'UNASSIGNED')
ORDER BY zone_count DESC, zone_name ASC;";

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        var points = new List<BillerDashboardPoint>();
        while (await reader.ReadAsync())
        {
            int labelOrdinal = reader.GetOrdinal("zone_name");
            int valueOrdinal = reader.GetOrdinal("zone_count");

            string label = reader.IsDBNull(labelOrdinal) ? "UNASSIGNED" : Convert.ToString(reader.GetValue(labelOrdinal)) ?? "UNASSIGNED";
            decimal value = reader.IsDBNull(valueOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(valueOrdinal));
            points.Add(new BillerDashboardPoint(label, value));
        }

        return points;
    }

    private static async Task<IReadOnlyList<BillerDashboardPoint>> GetConcessionaireStatusMixAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT
    UPPER(TRIM(v.Status)) AS status_name,
    COUNT(*) AS status_count
FROM v_concessionaire_details v
GROUP BY UPPER(TRIM(v.Status))
ORDER BY status_count DESC, status_name ASC;";

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        var points = new List<BillerDashboardPoint>();
        while (await reader.ReadAsync())
        {
            int labelOrdinal = reader.GetOrdinal("status_name");
            int valueOrdinal = reader.GetOrdinal("status_count");

            string label = reader.IsDBNull(labelOrdinal) ? "UNKNOWN" : Convert.ToString(reader.GetValue(labelOrdinal)) ?? "UNKNOWN";
            decimal value = reader.IsDBNull(valueOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(valueOrdinal));
            points.Add(new BillerDashboardPoint(label, value));
        }

        return points;
    }

    private static async Task<IReadOnlyList<BillerDashboardPoint>> GetTopConcessionairesByAmountAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT
    CONCAT(v.Account_No, ' - ', v.Concessionaire_Name) AS account_display,
    SUM(v.Total_Amount_Billed) AS total_billed
FROM v_billing_report v
WHERE v.Date >= @periodStart
  AND v.Date < @periodEnd
GROUP BY v.Account_No, v.Concessionaire_Name
ORDER BY total_billed DESC, account_display ASC
LIMIT 10;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@periodStart", periodStart);
        command.Parameters.AddWithValue("@periodEnd", periodEnd);

        await using var reader = await command.ExecuteReaderAsync();

        var points = new List<BillerDashboardPoint>();
        while (await reader.ReadAsync())
        {
            int labelOrdinal = reader.GetOrdinal("account_display");
            int valueOrdinal = reader.GetOrdinal("total_billed");

            string rawLabel = reader.IsDBNull(labelOrdinal) ? string.Empty : Convert.ToString(reader.GetValue(labelOrdinal)) ?? string.Empty;
            string label = rawLabel.Length > 52 ? rawLabel[..49] + "..." : rawLabel;
            decimal value = reader.IsDBNull(valueOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(valueOrdinal));
            points.Add(new BillerDashboardPoint(label, value));
        }

        return points;
    }

    private static async Task<IReadOnlyList<BillerServiceBillingMonthly>> GetServiceBillingMonthlyAsync(MySqlConnection connection, DateTime month)
    {
        string monthKey = month.ToString("yyyy-MM");
        
        const string sql = @"
SELECT
    billing_month,
    service_id,
    service_type,
    pipe_size,
    total_bills,
    total_consumption,
    total_water_charge,
    total_tax_amount,
    total_penalty_amount,
    total_billed_amount,
    total_remaining_balance,
    billing_share_percent
FROM v_service_billing_monthly
WHERE billing_month = @monthKey
ORDER BY billing_share_percent DESC, service_id ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@monthKey", monthKey);

        await using var reader = await command.ExecuteReaderAsync();

        var items = new List<BillerServiceBillingMonthly>();

        while (await reader.ReadAsync())
        {
            items.Add(new BillerServiceBillingMonthly(
                reader.IsDBNull(reader.GetOrdinal("billing_month")) ? monthKey : Convert.ToString(reader.GetValue(reader.GetOrdinal("billing_month"))) ?? monthKey,
                reader.IsDBNull(reader.GetOrdinal("service_id")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("service_id"))),
                reader.IsDBNull(reader.GetOrdinal("service_type")) ? "UNSPECIFIED" : Convert.ToString(reader.GetValue(reader.GetOrdinal("service_type"))) ?? "UNSPECIFIED",
                reader.IsDBNull(reader.GetOrdinal("pipe_size")) ? "N/A" : Convert.ToString(reader.GetValue(reader.GetOrdinal("pipe_size"))) ?? "N/A",
                reader.IsDBNull(reader.GetOrdinal("total_bills")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("total_bills"))),
                reader.IsDBNull(reader.GetOrdinal("total_consumption")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_consumption"))),
                reader.IsDBNull(reader.GetOrdinal("total_water_charge")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_water_charge"))),
                reader.IsDBNull(reader.GetOrdinal("total_tax_amount")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_tax_amount"))),
                reader.IsDBNull(reader.GetOrdinal("total_penalty_amount")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_penalty_amount"))),
                reader.IsDBNull(reader.GetOrdinal("total_billed_amount")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_billed_amount"))),
                reader.IsDBNull(reader.GetOrdinal("total_remaining_balance")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_remaining_balance"))),
                reader.IsDBNull(reader.GetOrdinal("billing_share_percent")) ? 0M : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("billing_share_percent")))));
        }

        return items;
    }

    private static async Task<IReadOnlyList<BillerServiceSummaryItem>> GetServiceSummaryAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT
    COALESCE(v.ServiceID, 0) AS ServiceID,
    COALESCE(NULLIF(TRIM(v.ServiceType), ''), 'UNSPECIFIED') AS ServiceType,
    COALESCE(NULLIF(TRIM(v.PipeSize), ''), 'N/A') AS PipeSize,
    COALESCE(v.TotalConcessionaires, 0) AS TotalConcessionaires,
    COALESCE(v.TaxExemptCount, 0) AS TaxExemptCount,
    COALESCE(v.DueExemptCount, 0) AS DueExemptCount,
    COALESCE(v.DiscountedCount, 0) AS DiscountedCount,
    COALESCE(v.ActiveCount, 0) AS ActiveCount,
    COALESCE(v.InactiveCount, 0) AS InactiveCount,
    COALESCE(v.MetersAssigned, 0) AS MetersAssigned,
    COALESCE(v.MetersUnassigned, 0) AS MetersUnassigned
FROM v_service_summary v
ORDER BY ServiceID ASC;";

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        var items = new List<BillerServiceSummaryItem>();

        while (await reader.ReadAsync())
        {
            items.Add(new BillerServiceSummaryItem(
                reader.IsDBNull(reader.GetOrdinal("ServiceID")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ServiceID"))),
                reader.IsDBNull(reader.GetOrdinal("ServiceType")) ? "UNSPECIFIED" : Convert.ToString(reader.GetValue(reader.GetOrdinal("ServiceType"))) ?? "UNSPECIFIED",
                reader.IsDBNull(reader.GetOrdinal("PipeSize")) ? "N/A" : Convert.ToString(reader.GetValue(reader.GetOrdinal("PipeSize"))) ?? "N/A",
                reader.IsDBNull(reader.GetOrdinal("TotalConcessionaires")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TotalConcessionaires"))),
                reader.IsDBNull(reader.GetOrdinal("TaxExemptCount")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("TaxExemptCount"))),
                reader.IsDBNull(reader.GetOrdinal("DueExemptCount")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("DueExemptCount"))),
                reader.IsDBNull(reader.GetOrdinal("DiscountedCount")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("DiscountedCount"))),
                reader.IsDBNull(reader.GetOrdinal("ActiveCount")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ActiveCount"))),
                reader.IsDBNull(reader.GetOrdinal("InactiveCount")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("InactiveCount"))),
                reader.IsDBNull(reader.GetOrdinal("MetersAssigned")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("MetersAssigned"))),
                reader.IsDBNull(reader.GetOrdinal("MetersUnassigned")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("MetersUnassigned")))));
        }

        return items;
    }
}
