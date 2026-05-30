using System.Data;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Reports;

internal readonly record struct SCFAgingOfAccountsQueryOptions(string SearchTerm);

internal static class SCFAgingOfAccountsReportService
{
    public static async Task<DataTable> GetSummaryRowsAsync(UserRole role, SCFAgingOfAccountsQueryOptions options)
    {
        const string sql = @"
SELECT
    Account_No,
    Concessionaire_Name,
    Current,
    `1_30_Days`,
    `31_60_Days`,
    `61_90_Days`,
    Over_90_Days,
    Total
FROM v_aging_of_scf_summary
WHERE (@search = ''
    OR Account_No LIKE @searchLike
    OR Concessionaire_Name LIKE @searchLike)
ORDER BY Account_No ASC;";

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