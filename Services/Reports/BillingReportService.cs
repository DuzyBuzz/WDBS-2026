using System.Data;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Services.Reports;

internal readonly record struct BillingReportQueryOptions(
    DateTime DateFrom,
    DateTime DateTo,
    string SearchTerm);

internal static class BillingReportService
{
    private const string SelectColumns = @"
    v.`Zone`,
    v.`Account_No`,
    v.`Concessionaire_Name`,
    v.`Invoice_Number`,
    v.`Cu_m³`,
    v.`Un_m³`,
    v.`Water_Bill`,
    v.`Arrears`,
    v.`Tax`,
    v.`Discount`,
    v.`Total_Water_Bill`,
    v.`SCF`,
    v.`Total_Amount_Billed`,
    v.`Date`";

    private const string FilterClause = @"
FROM v_billing_report v
WHERE v.`Date` >= @dateFrom
  AND v.`Date` <= @dateTo
  AND (
      @search = ''
      OR v.`Concessionaire_Name` LIKE @searchLike
      OR v.`Account_No` LIKE @searchLike
      OR CAST(v.`Invoice_Number` AS CHAR) LIKE @searchLike
  )";

    public static async Task<int> GetTotalRecordsAsync(UserRole role, BillingReportQueryOptions options)
    {
        const string sql = $@"
SELECT COUNT(*)
{FilterClause};";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        ApplyParameters(command, options);

        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0);
    }

    public static async Task<DataTable> GetPagedRowsAsync(UserRole role, BillingReportQueryOptions options, int offset, int limit)
    {
        string sql = $@"
SELECT
{SelectColumns}
{FilterClause}
ORDER BY CAST(v.`Invoice_Number` AS UNSIGNED) ASC, v.`Invoice_Number` ASC
LIMIT @limit OFFSET @offset;";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        ApplyParameters(command, options);
        command.Parameters.AddWithValue("@limit", limit);
        command.Parameters.AddWithValue("@offset", offset);

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<DataTable> GetReportRowsAsync(UserRole role, BillingReportQueryOptions options)
    {
        string sql = $@"
SELECT
{SelectColumns}
{FilterClause}
ORDER BY CAST(v.`Invoice_Number` AS UNSIGNED) ASC, v.`Invoice_Number` ASC;";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        ApplyParameters(command, options);

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<DataTable> GetReportRowsWithProgressAsync(
        UserRole role,
        BillingReportQueryOptions options,
        int totalRecords,
        IProgress<ReportOperationProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        string sql = $@"
SELECT
{SelectColumns}
{FilterClause}
ORDER BY CAST(v.`Invoice_Number` AS UNSIGNED) ASC, v.`Invoice_Number` ASC;";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new MySqlCommand(sql, connection);
        ApplyParameters(command, options);

        progress?.Report(new ReportOperationProgress(10, totalRecords <= 0
            ? "Loading report rows..."
            : $"Loading report rows (0 of {totalRecords})..."));

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync(cancellationToken);
        var table = new DataTable();

        for (int columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
        {
            table.Columns.Add(reader.GetName(columnIndex), typeof(object));
        }

        int rowsLoaded = 0;
        while (await reader.ReadAsync(cancellationToken))
        {
            object[] values = new object[reader.FieldCount];
            reader.GetValues(values);
            table.Rows.Add(values);
            rowsLoaded++;

            if (totalRecords <= 0 || rowsLoaded == totalRecords || totalRecords <= 100 || rowsLoaded % 10 == 0)
            {
                int percentage = totalRecords <= 0
                    ? 10
                    : 10 + (int)Math.Round(((double)rowsLoaded / totalRecords) * 90D, MidpointRounding.AwayFromZero);

                progress?.Report(new ReportOperationProgress(
                    Math.Clamp(percentage, 10, 100),
                    totalRecords <= 0
                        ? $"Loading report rows ({rowsLoaded} loaded)..."
                        : $"Loading report rows ({rowsLoaded} of {totalRecords})..."));
            }
        }

        progress?.Report(new ReportOperationProgress(100, totalRecords <= 0
            ? $"Loaded {rowsLoaded} report row(s)."
            : $"Loaded {rowsLoaded} of {Math.Max(totalRecords, rowsLoaded)} report row(s)."));

        return table;
    }

    private static void ApplyParameters(MySqlCommand command, BillingReportQueryOptions options)
    {
        command.Parameters.AddWithValue("@dateFrom", options.DateFrom.Date);
        command.Parameters.AddWithValue("@dateTo", options.DateTo.Date);
        command.Parameters.AddWithValue("@search", options.SearchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{options.SearchTerm}%");
    }
}