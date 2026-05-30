using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Reports;

internal readonly record struct AgingOfAccountsQueryOptions(
    DateTime AgingDate,
    string SearchTerm);

internal static class AgingOfAccountsReportService
{
    public static async Task<DateTime> GetAgingDateAsync(UserRole role)
    {
        const string sql = @"
SELECT settings_value
FROM system_settings
WHERE settings_key = 'aging_date'
LIMIT 1;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        object? scalar = await command.ExecuteScalarAsync();

        string rawValue = Convert.ToString(scalar, CultureInfo.InvariantCulture) ?? string.Empty;
        if (DateTime.TryParseExact(rawValue, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed))
        {
            return parsed.Date;
        }

        return DateTime.Today;
    }

    public static async Task SetAgingDateAsync(UserRole role, DateTime agingDate)
    {
        const string sql = @"
INSERT INTO system_settings (settings_key, settings_value)
VALUES ('aging_date', @agingDate)
ON DUPLICATE KEY UPDATE settings_value = @agingDate;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@agingDate", agingDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
        await command.ExecuteNonQueryAsync();
    }

    public static async Task<DataTable> GetSummaryRowsAsync(UserRole role, AgingOfAccountsQueryOptions options)
    {
        const string sql = @"
SELECT
    Account_No,
    Concessionaire_Name,
    Current,
    `1-30_Days`,
    `31-60_Days`,
    `61-90_Days`,
    `91-120_Days`,
    Over_120_Days,
    Total
FROM v_aging_of_accounts_summary
WHERE (@search = ''
    OR Account_No LIKE @searchLike
    OR Concessionaire_Name LIKE @searchLike)
ORDER BY Account_No ASC;";

        await SetAgingDateAsync(role, options.AgingDate.Date);

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@search", options.SearchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{options.SearchTerm}%");

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    private static async Task<MySqlConnection> OpenConnectionAsync(UserRole role)
    {
        DBConfig.SetConnectionString(role);
        MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();
        return connection;
    }
}
