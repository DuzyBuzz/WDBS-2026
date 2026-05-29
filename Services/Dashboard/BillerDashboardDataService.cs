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

        IReadOnlyList<BillerDashboardPoint> billedTrend = await GetMonthlyTrendAsync(connection, periodStart, periodEnd);
        IReadOnlyList<BillerDashboardPoint> billingStatusBreakdown = await GetBillingStatusBreakdownAsync(connection, periodStart, periodEnd);
        IReadOnlyList<BillerDashboardPoint> concessionairesByZone = await GetConcessionairesByZoneAsync(connection);
        IReadOnlyList<BillerDashboardPoint> concessionaireStatusMix = await GetConcessionaireStatusMixAsync(connection);
        IReadOnlyList<BillerDashboardPoint> topConcessionairesByAmount = await GetTopConcessionairesByAmountAsync(connection, periodStart, periodEnd);

        return new BillerDashboardSnapshot(
            periodStart,
            periodEnd,
            totalBilledAmount,
            totalBillCount,
            unpaidBillCount,
            activeConcessionaireCount,
            billedTrend,
            billingStatusBreakdown,
            concessionairesByZone,
            concessionaireStatusMix,
            topConcessionairesByAmount);
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
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT
    DATE_FORMAT(b.billing_date, '%Y-%m') AS month_key,
    SUM(b.total_amount) AS total_billed
FROM billing b
WHERE b.billing_date >= @periodStart
  AND b.billing_date < @periodEnd
GROUP BY DATE_FORMAT(b.billing_date, '%Y-%m')
ORDER BY month_key ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@periodStart", periodStart);
        command.Parameters.AddWithValue("@periodEnd", periodEnd);

        await using var reader = await command.ExecuteReaderAsync();

        var points = new List<BillerDashboardPoint>();
        while (await reader.ReadAsync())
        {
            int labelOrdinal = reader.GetOrdinal("month_key");
            int valueOrdinal = reader.GetOrdinal("total_billed");

            string label = reader.IsDBNull(labelOrdinal) ? string.Empty : Convert.ToString(reader.GetValue(labelOrdinal)) ?? string.Empty;
            decimal value = reader.IsDBNull(valueOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(valueOrdinal));
            points.Add(new BillerDashboardPoint(label, value));
        }

        return points;
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
    COALESCE(NULLIF(TRIM(v.Zone_Name), ''), 'UNASSIGNED') AS zone_name,
    COUNT(*) AS zone_count
FROM v_concessionaire_details v
GROUP BY COALESCE(NULLIF(TRIM(v.Zone_Name), ''), 'UNASSIGNED')
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
}
