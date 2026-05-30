using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Dashboard;

internal static class CashierDashboardDataService
{
    public static async Task<CashierDashboardSnapshot> GetSnapshotAsync(UserRole role, DateTime referenceDate)
    {
        DateTime periodStart = new(referenceDate.Year, referenceDate.Month, 1);
        DateTime periodEnd = periodStart.AddMonths(1);

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        (decimal totalCollectedAmount, int officialReceiptCount, decimal scfCollectedAmount) =
            await GetMonthlyTotalsAsync(connection, periodStart, periodEnd);

        decimal billedAmount = await GetMonthlyBilledAmountAsync(connection, periodStart, periodEnd);
        decimal monthlyCollectionRate = billedAmount <= 0M ? 0M : (totalCollectedAmount / billedAmount) * 100M;

        int outstandingAccountsCount = await GetOutstandingAccountsCountAsync(connection);
        (IReadOnlyList<CashierDashboardPoint> collectionTrend, IReadOnlyList<CashierDashboardPoint> uncollectedTrend) =
            await GetDailyCollectionTrendAsync(connection, periodStart, periodEnd);
        (IReadOnlyList<CashierDashboardPoint> monthlyCollectionTrend, IReadOnlyList<CashierDashboardPoint> monthlyUncollectedTrend) =
            await GetMonthlyCollectionTrendAsync(connection, periodStart, periodEnd);
        IReadOnlyList<CashierDashboardPoint> paymentTypeBreakdown = await GetPaymentTypeBreakdownAsync(connection, periodStart, periodEnd);
        IReadOnlyList<CashierDashboardPoint> topCollectedAccounts = await GetTopCollectedAccountsAsync(connection, periodStart, periodEnd);
        IReadOnlyList<CashierRecentCollection> recentCollections = await GetRecentCollectionsAsync(connection);

        return new CashierDashboardSnapshot(
            periodStart,
            periodEnd,
            totalCollectedAmount,
            officialReceiptCount,
            scfCollectedAmount,
            outstandingAccountsCount,
            monthlyCollectionRate,
            collectionTrend,
            uncollectedTrend,
            monthlyCollectionTrend,
            monthlyUncollectedTrend,
            paymentTypeBreakdown,
            topCollectedAccounts,
            recentCollections);
    }

    private static async Task<(decimal TotalCollectedAmount, int OfficialReceiptCount, decimal ScfCollectedAmount)> GetMonthlyTotalsAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT
    COALESCE(SUM(COALESCE(v.total_paid_amount, 0.00)), 0.00) AS total_collected,
    COUNT(DISTINCT NULLIF(TRIM(v.or_number), '')) AS receipt_count,
    COALESCE(SUM(COALESCE(v.scf_paid, 0.00)), 0.00) AS scf_collected
FROM v_collection_with_users v
WHERE v.collection_date >= @periodStart
    AND v.collection_date < @periodEnd
  AND UPPER(TRIM(COALESCE(v.status, 'POSTED'))) <> 'VOIDED';";

        await using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@periodStart", periodStart);
                command.Parameters.AddWithValue("@periodEnd", periodEnd);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return (0M, 0, 0M);
        }

        int totalOrdinal = reader.GetOrdinal("total_collected");
        int receiptOrdinal = reader.GetOrdinal("receipt_count");
        int scfOrdinal = reader.GetOrdinal("scf_collected");

        decimal totalCollected = reader.IsDBNull(totalOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(totalOrdinal));
        int receiptCount = reader.IsDBNull(receiptOrdinal) ? 0 : Convert.ToInt32(reader.GetValue(receiptOrdinal));
        decimal scfCollected = reader.IsDBNull(scfOrdinal) ? 0M : Convert.ToDecimal(reader.GetValue(scfOrdinal));

        return (totalCollected, receiptCount, scfCollected);
    }

    private static async Task<decimal> GetMonthlyBilledAmountAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT COALESCE(SUM(COALESCE(b.total_amount, 0.00)), 0.00) AS billed_amount
FROM billing b
WHERE b.billing_date >= @periodStart
  AND b.billing_date < @periodEnd;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@periodStart", periodStart);
        command.Parameters.AddWithValue("@periodEnd", periodEnd);

        object? scalar = await command.ExecuteScalarAsync();
        return scalar is null or DBNull ? 0M : Convert.ToDecimal(scalar);
    }

    private static async Task<int> GetOutstandingAccountsCountAsync(MySqlConnection connection)
    {
        const string sql = @"
SELECT COUNT(*)
FROM v_concessionaire_collection_lookup v
WHERE COALESCE(v.unpaid_bill_count, 0) > 0;";

        await using var command = new MySqlCommand(sql, connection);
        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0);
    }

    private static async Task<(IReadOnlyList<CashierDashboardPoint> CollectedTrend, IReadOnlyList<CashierDashboardPoint> UncollectedTrend)> GetDailyCollectionTrendAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        const string sql = @"
SELECT
    DATE_FORMAT(v.collection_date, '%Y-%m-%d') AS period_key,
    COALESCE(SUM(COALESCE(v.total_paid_amount, 0.00)), 0.00) AS total_collected,
    COALESCE(SUM(COALESCE(v.uncollected, 0.00)), 0.00) AS total_uncollected
FROM v_collection_with_users v
WHERE v.collection_date >= @periodStart
  AND v.collection_date < @periodEnd
  AND UPPER(TRIM(COALESCE(v.status, 'POSTED'))) <> 'VOIDED'
GROUP BY DATE_FORMAT(v.collection_date, '%Y-%m-%d')
ORDER BY period_key ASC;";

        var collectedByDay = new Dictionary<string, decimal>(StringComparer.Ordinal);
    var uncollectedByDay = new Dictionary<string, decimal>(StringComparer.Ordinal);
        await using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@periodStart", periodStart);
            command.Parameters.AddWithValue("@periodEnd", periodEnd);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string label = reader.IsDBNull(reader.GetOrdinal("period_key"))
                    ? string.Empty
                    : Convert.ToString(reader.GetValue(reader.GetOrdinal("period_key"))) ?? string.Empty;
                decimal value = reader.IsDBNull(reader.GetOrdinal("total_collected"))
                    ? 0M
                    : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_collected")));
                decimal uncollected = reader.IsDBNull(reader.GetOrdinal("total_uncollected"))
                    ? 0M
                    : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_uncollected")));

                if (!string.IsNullOrWhiteSpace(label))
                {
                    collectedByDay[label] = value;
                    uncollectedByDay[label] = uncollected;
                }
            }
        }

        int daysInMonth = DateTime.DaysInMonth(periodStart.Year, periodStart.Month);
        var collectedPoints = new List<CashierDashboardPoint>(daysInMonth);
        var uncollectedPoints = new List<CashierDashboardPoint>(daysInMonth);
        DateTime cursor = periodStart;
        while (cursor < periodEnd)
        {
            string key = cursor.ToString("yyyy-MM-dd");
            decimal collected = collectedByDay.TryGetValue(key, out decimal value) ? value : 0M;
            decimal uncollected = uncollectedByDay.TryGetValue(key, out decimal uncollectedValue) ? uncollectedValue : 0M;
            collectedPoints.Add(new CashierDashboardPoint(key, collected));
            uncollectedPoints.Add(new CashierDashboardPoint(key, uncollected));
            cursor = cursor.AddDays(1);
        }

        return (collectedPoints, uncollectedPoints);
    }

    private static async Task<(IReadOnlyList<CashierDashboardPoint> CollectedTrend, IReadOnlyList<CashierDashboardPoint> UncollectedTrend)> GetMonthlyCollectionTrendAsync(
        MySqlConnection connection,
        DateTime periodStart,
        DateTime periodEnd)
    {
        DateTime monthlyStart = periodStart.AddMonths(-11);

        const string sql = @"
SELECT
    DATE_FORMAT(v.collection_date, '%Y-%m') AS period_key,
    COALESCE(SUM(COALESCE(v.total_paid_amount, 0.00)), 0.00) AS total_collected,
    COALESCE(SUM(COALESCE(v.uncollected, 0.00)), 0.00) AS total_uncollected
FROM v_collection_with_users v
WHERE v.collection_date >= @monthlyStart
  AND v.collection_date < @periodEnd
  AND UPPER(TRIM(COALESCE(v.status, 'POSTED'))) <> 'VOIDED'
GROUP BY DATE_FORMAT(v.collection_date, '%Y-%m')
ORDER BY period_key ASC;";

        var collectedByMonth = new Dictionary<string, decimal>(StringComparer.Ordinal);
        var uncollectedByMonth = new Dictionary<string, decimal>(StringComparer.Ordinal);

        await using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@monthlyStart", monthlyStart);
            command.Parameters.AddWithValue("@periodEnd", periodEnd);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string label = reader.IsDBNull(reader.GetOrdinal("period_key"))
                    ? string.Empty
                    : Convert.ToString(reader.GetValue(reader.GetOrdinal("period_key"))) ?? string.Empty;
                decimal collected = reader.IsDBNull(reader.GetOrdinal("total_collected"))
                    ? 0M
                    : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_collected")));
                decimal uncollected = reader.IsDBNull(reader.GetOrdinal("total_uncollected"))
                    ? 0M
                    : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_uncollected")));

                if (!string.IsNullOrWhiteSpace(label))
                {
                    collectedByMonth[label] = collected;
                    uncollectedByMonth[label] = uncollected;
                }
            }
        }

        var collectedPoints = new List<CashierDashboardPoint>(12);
        var uncollectedPoints = new List<CashierDashboardPoint>(12);

        DateTime cursor = monthlyStart;
        while (cursor < periodEnd)
        {
            string key = cursor.ToString("yyyy-MM");
            decimal collected = collectedByMonth.TryGetValue(key, out decimal collectedValue) ? collectedValue : 0M;
            decimal uncollected = uncollectedByMonth.TryGetValue(key, out decimal uncollectedValue) ? uncollectedValue : 0M;
            collectedPoints.Add(new CashierDashboardPoint(key, collected));
            uncollectedPoints.Add(new CashierDashboardPoint(key, uncollected));
            cursor = cursor.AddMonths(1);
        }

        return (collectedPoints, uncollectedPoints);
    }

    private static async Task<IReadOnlyList<CashierDashboardPoint>> GetPaymentTypeBreakdownAsync(
        MySqlConnection connection,
        DateTime snapshotDate,
        DateTime nextDate)
    {
        const string sql = @"
SELECT
    UPPER(TRIM(COALESCE(NULLIF(v.payment_type, ''), 'UNKNOWN'))) AS payment_type,
    COUNT(*) AS payment_count
FROM v_collection_with_users v
WHERE v.collection_date >= @snapshotDate
  AND v.collection_date < @nextDate
  AND UPPER(TRIM(COALESCE(v.status, 'POSTED'))) <> 'VOIDED'
GROUP BY UPPER(TRIM(COALESCE(NULLIF(v.payment_type, ''), 'UNKNOWN')))
ORDER BY payment_count DESC, payment_type ASC;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@snapshotDate", snapshotDate);
        command.Parameters.AddWithValue("@nextDate", nextDate);

        await using var reader = await command.ExecuteReaderAsync();
        var points = new List<CashierDashboardPoint>();
        while (await reader.ReadAsync())
        {
            string label = reader.IsDBNull(reader.GetOrdinal("payment_type"))
                ? "UNKNOWN"
                : Convert.ToString(reader.GetValue(reader.GetOrdinal("payment_type"))) ?? "UNKNOWN";
            decimal value = reader.IsDBNull(reader.GetOrdinal("payment_count"))
                ? 0M
                : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("payment_count")));
            points.Add(new CashierDashboardPoint(label, value));
        }

        return points;
    }

    private static async Task<IReadOnlyList<CashierDashboardPoint>> GetTopCollectedAccountsAsync(
        MySqlConnection connection,
        DateTime snapshotDate,
        DateTime nextDate)
    {
        const string sql = @"
SELECT
    CONCAT(grouped.concessionaire_code, ' - ', grouped.concessionaire_name) AS account_label,
    grouped.total_collected
FROM
(
    SELECT
        COALESCE(v.concessionaire_code, 'N/A') AS concessionaire_code,
        COALESCE(v.concessionaire_name, 'Unknown') AS concessionaire_name,
        COALESCE(SUM(COALESCE(v.total_paid_amount, 0.00)), 0.00) AS total_collected
    FROM v_collection_with_users v
    WHERE v.collection_date >= @snapshotDate
      AND v.collection_date < @nextDate
      AND UPPER(TRIM(COALESCE(v.status, 'POSTED'))) <> 'VOIDED'
    GROUP BY
        COALESCE(v.concessionaire_code, 'N/A'),
        COALESCE(v.concessionaire_name, 'Unknown')
) AS grouped
ORDER BY grouped.total_collected DESC, account_label ASC
LIMIT 10;";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@snapshotDate", snapshotDate);
        command.Parameters.AddWithValue("@nextDate", nextDate);

        await using var reader = await command.ExecuteReaderAsync();
        var points = new List<CashierDashboardPoint>();
        while (await reader.ReadAsync())
        {
            string rawLabel = reader.IsDBNull(reader.GetOrdinal("account_label"))
                ? string.Empty
                : Convert.ToString(reader.GetValue(reader.GetOrdinal("account_label"))) ?? string.Empty;
            string label = rawLabel.Length > 52 ? rawLabel[..49] + "..." : rawLabel;
            decimal value = reader.IsDBNull(reader.GetOrdinal("total_collected"))
                ? 0M
                : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_collected")));
            points.Add(new CashierDashboardPoint(label, value));
        }

        return points;
    }

    private static async Task<IReadOnlyList<CashierRecentCollection>> GetRecentCollectionsAsync(
        MySqlConnection connection)
    {
        const string sql = @"
SELECT
    v.collection_date,
    COALESCE(v.or_number, '') AS or_number,
    CONCAT(COALESCE(v.concessionaire_code, 'N/A'), ' - ', COALESCE(v.concessionaire_name, 'Unknown')) AS concessionaire,
    COALESCE(NULLIF(v.payment_type, ''), 'Unknown') AS payment_type,
    COALESCE(v.total_paid_amount, 0.00) AS total_collected
FROM v_collection_with_users v
ORDER BY v.collection_date DESC, v.or_number DESC
LIMIT 12;";

        await using var command = new MySqlCommand(sql, connection);

        await using var reader = await command.ExecuteReaderAsync();
        var items = new List<CashierRecentCollection>();
        while (await reader.ReadAsync())
        {
            DateTime collectionDate = reader.IsDBNull(reader.GetOrdinal("collection_date"))
                                ? DateTime.Now
                : Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("collection_date")));
            string orNumber = reader.IsDBNull(reader.GetOrdinal("or_number"))
                ? string.Empty
                : Convert.ToString(reader.GetValue(reader.GetOrdinal("or_number"))) ?? string.Empty;
            string concessionaire = reader.IsDBNull(reader.GetOrdinal("concessionaire"))
                ? string.Empty
                : Convert.ToString(reader.GetValue(reader.GetOrdinal("concessionaire"))) ?? string.Empty;
            string paymentType = reader.IsDBNull(reader.GetOrdinal("payment_type"))
                ? "Unknown"
                : Convert.ToString(reader.GetValue(reader.GetOrdinal("payment_type"))) ?? "Unknown";
            decimal amount = reader.IsDBNull(reader.GetOrdinal("total_collected"))
                ? 0M
                : Convert.ToDecimal(reader.GetValue(reader.GetOrdinal("total_collected")));

            items.Add(new CashierRecentCollection(collectionDate, orNumber, concessionaire, paymentType, amount));
        }

        return items;
    }
}
