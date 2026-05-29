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
    private const string ReportSelectColumns = @"
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

        private const string ReportFilterClause = @"
FROM v_billing_report v
WHERE v.`Date` >= @dateFrom
    AND v.`Date` < @dateToExclusive
  AND (
      @search = ''
      OR v.`Concessionaire_Name` LIKE @searchLike
      OR v.`Account_No` LIKE @searchLike
      OR CAST(v.`Invoice_Number` AS CHAR) LIKE @searchLike
  )";

    private const string TableSelectColumns = @"
        v.billing_date,
        c.concessionaire_code,
        c.concessionaire_name,
        COALESCE(z.zone_name, CAST(c.zone_id AS CHAR)) AS zone,
        v.bill_number,
        c.address,
        COALESCE(
                (
                        SELECT r2.reading_date
                        FROM reading r2
                        WHERE r2.concessionaire_id = v.concessionaire_id
                            AND r2.reading_id < COALESCE(r.reading_id, 0)
                        ORDER BY r2.reading_id DESC
                        LIMIT 1
                ),
                c.first_reading_date
        ) AS previous_reading_date,
        r.reading_date AS current_reading_date,
        COALESCE(r.previous_reading, 0) AS previous_reading,
        COALESCE(r.present_reading, 0) AS present_reading,
        v.billing_id,
        v.concessionaire_id,
        v.reading_id,
        v.due_date,
        v.consumption,
        v.free_water,
        v.water_charge,
        v.discount_amount,
        v.tax_amount,
        v.total_water_bill,
        v.scf_amount,
        v.arrears_amount,
        v.penalty_amount,
        v.total_amount,
        v.remaining_water_charge,
        v.remaining_tax_amount,
        v.remaining_penalty_amount,
        v.remaining_scf_amount,
        v.remaining_balance,
        v.status,
        v.scf_status,
        v.updated_at,
        v.last_payment_date,
        v.is_initial,
        v.payment_count,
        v.last_collection_id,
        v.created_by_user_id,
        v.updated_by_user_id,
        v.tax_percent_used,
        v.discount_percent_used,
        v.penalty_percent_used,
        v.is_penalty_applied,
        v.penalty_applied_at,
        v.request_id,
        v.created_at,
        v.scf_monthly_used,
        v.scf_total_cap_used,
        v.paid_at,
        v.created_by_username,
        v.created_by_full_name,
        v.created_by_role,
        v.updated_by_username,
        v.updated_by_full_name,
        v.updated_by_role";

    private const string TableFilterClause = @"
FROM v_billing_with_users_full v
LEFT JOIN concessionaire c ON c.concessionaire_id = v.concessionaire_id
LEFT JOIN zone z ON z.zone_id = c.zone_id
LEFT JOIN reading r ON r.reading_id = v.reading_id
WHERE v.billing_date >= @dateFrom
    AND v.billing_date < @dateToExclusive
  AND (
      @search = ''
            OR c.concessionaire_name LIKE @searchLike
            OR c.concessionaire_code LIKE @searchLike
      OR CAST(v.bill_number AS CHAR) LIKE @searchLike
  )";

    public static async Task<int> GetTableTotalRecordsAsync(UserRole role, BillingReportQueryOptions options)
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

    public static async Task<DataTable> GetTablePagedRowsAsync(UserRole role, BillingReportQueryOptions options, int offset, int limit)
    {
        string sql = $@"
SELECT
{TableSelectColumns}
{TableFilterClause}
ORDER BY v.billing_date DESC, CAST(v.bill_number AS UNSIGNED) DESC, v.bill_number DESC
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

    public static async Task<int> GetTotalRecordsAsync(UserRole role, BillingReportQueryOptions options)
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

    public static async Task<DataTable> GetPagedRowsAsync(UserRole role, BillingReportQueryOptions options, int offset, int limit)
    {
        string sql = $@"
SELECT
{ReportSelectColumns}
{ReportFilterClause}
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
{ReportSelectColumns}
{ReportFilterClause}
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
    {ReportSelectColumns}
    {ReportFilterClause}
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
        command.Parameters.AddWithValue("@dateToExclusive", options.DateTo.Date.AddDays(1));
        command.Parameters.AddWithValue("@search", options.SearchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{options.SearchTerm}%");
    }
}