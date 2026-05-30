using System.Data;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;
using WDBS_2026.Services.Printing;

namespace WDBS_2026.Services.Reports;

internal readonly record struct CollectionReportQueryOptions(
    DateTime DateFrom,
    DateTime DateTo,
    string SearchTerm);

internal static class CollectionReportService
{
    private const string ReportSelectColumns = @"
    DATE(v.`Date`) AS `Date`,
    v.`OR_Number` AS `OR Number`,
    COALESCE(c.payor_name, '') AS `Payor`,
    v.`Invoice_Number` AS `Invoice Number`,
    v.`Remarks` AS `Remarks`,
    v.`Water_Charge` AS `Water Charge`,
    v.`Tax` AS `Tax`,
    v.`Arrears` AS `Arrears`,
    v.`Penalty` AS `Penalty`,
    v.`SCF` AS `SCF`,
    v.`Collected` AS `Collected`,
    v.`Uncollected` AS `Uncollected`";

    private const string ReportFilterClause = @"
FROM v_collection_report v
LEFT JOIN collection c ON c.collection_id = v.`Collection_ID`
WHERE DATE(v.`Date`) >= @dateFrom
  AND DATE(v.`Date`) < @dateToExclusive
  AND (
      @search = ''
      OR CAST(v.`OR_Number` AS CHAR) LIKE @searchLike
      OR v.`Invoice_Number` LIKE @searchLike
      OR v.`Concessionaire_Code` LIKE @searchLike
      OR COALESCE(c.payor_name, '') LIKE @searchLike
      OR COALESCE(v.`Remarks`, '') LIKE @searchLike
  )";

    private const string TableSelectColumns = @"
        v.collection_id,
        v.or_number,
        v.collection_date,
        v.bill_numbers,
        v.concessionaire_code,
        v.concessionaire_name,
        v.address,
        v.total_current_bill,
        v.total_arrears,
        v.total_penalty,
        v.total_tax,
        v.total_scf,
        v.total_others,
        v.grand_total,
        v.amount_received,
        v.change_amount,
        v.total_paid_amount,
        v.uncollected,
        v.status,
        v.payment_type,
        v.payment_reference,
        v.payor_name,
        v.remarks,
        v.created_by_full_name,
        v.created_at,
        v.updated_at,
        v.voided_at";

    private const string TableFilterClause = @"
FROM v_collection_with_users v
WHERE DATE(v.collection_date) >= @dateFrom
    AND DATE(v.collection_date) < @dateToExclusive
  AND (
      @search = ''
            OR CAST(v.or_number AS CHAR) LIKE @searchLike
            OR v.bill_numbers LIKE @searchLike
            OR v.concessionaire_code LIKE @searchLike
            OR v.concessionaire_name LIKE @searchLike
            OR COALESCE(v.payor_name, '') LIKE @searchLike
            OR COALESCE(v.remarks, '') LIKE @searchLike
  )";

    public static async Task<int> GetTableTotalRecordsAsync(UserRole role, CollectionReportQueryOptions options)
    {
        const string sql = $@"
SELECT COUNT(*)
{TableFilterClause};";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        ApplyParameters(command, options);

        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0);
    }

    public static async Task<DataTable> GetTablePagedRowsAsync(UserRole role, CollectionReportQueryOptions options, int offset, int limit)
    {
        string sql = $@"
SELECT
{TableSelectColumns}
{TableFilterClause}
ORDER BY v.collection_date DESC, CAST(v.or_number AS UNSIGNED) DESC, v.or_number DESC
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

    public static async Task<int> GetTotalRecordsAsync(UserRole role, CollectionReportQueryOptions options)
    {
        const string sql = $@"
SELECT COUNT(*)
{ReportFilterClause};";

        DBConfig.SetConnectionString(role);
        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        ApplyParameters(command, options);

        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0);
    }

    public static async Task<DataTable> GetReportRowsWithProgressAsync(
        UserRole role,
        CollectionReportQueryOptions options,
        int totalRecords,
        IProgress<ReportOperationProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        string sql = $@"
SELECT
    {ReportSelectColumns}
    {ReportFilterClause}
ORDER BY v.`Date` DESC, CAST(v.`OR_Number` AS UNSIGNED) DESC, v.`OR_Number` DESC;";

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

    private static void ApplyParameters(MySqlCommand command, CollectionReportQueryOptions options)
    {
        command.Parameters.AddWithValue("@dateFrom", options.DateFrom.Date);
        command.Parameters.AddWithValue("@dateToExclusive", options.DateTo.Date.AddDays(1));
        command.Parameters.AddWithValue("@search", options.SearchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{options.SearchTerm}%");
    }
}
