using System.Data;
using System.Globalization;
using System.Text.Json;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Cashier;

internal static class CollectionService
{
    public static async Task<DataTable> GetConcessionaireLookupAsync(UserRole role, string searchTerm)
    {
        const string sql = @"
SELECT
    concessionaire_id,
    concessionaire_code,
    concessionaire_name,
    address,
    zone_id,
    meter_no,
    is_discounted,
    is_tax_exempt,
    is_due_exempt,
    service_type,
    outstanding_balance,
    remaining_scf,
    unpaid_bill_count,
    last_billing_date
FROM v_concessionaire_collection_lookup
WHERE (@search = ''
    OR concessionaire_code LIKE @searchLike
    OR concessionaire_name LIKE @searchLike
    OR meter_no LIKE @searchLike
    OR address LIKE @searchLike)
ORDER BY concessionaire_id DESC;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@search", searchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{searchTerm}%");

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<DataTable> GetCollectionHistoryByDateAsync(UserRole role, DateTime collectionDate, string searchTerm)
    {
        const string sql = @"
SELECT
    collection_id,
    or_number,
    collection_date,
    bill_numbers,
    concessionaire_code,
    concessionaire_name,
    address,
    total_current_bill,
    total_arrears,
    total_penalty,
    total_tax,
    total_scf,
    total_water_bill_paid,
    scf_paid,
    total_others,
    grand_total,
    amount_received,
    change_amount,
    total_paid_amount,
    uncollected,
    total_discount,
    payment_type,
    payment_reference,
    status,
    remarks,
    payor_name,
    created_at,
    updated_at,
    voided_at,
    billing_count,
    payment_count,
    created_by_user_id,
    created_by_username,
    created_by_full_name,
    created_by_role,
    created_by_is_active,
    voided_by_user_id,
    voided_by_username,
    voided_by_full_name
FROM v_collection_with_users
WHERE DATE(collection_date) = @collectionDate
  AND (@search = ''
      OR or_number LIKE @searchLike
      OR concessionaire_code LIKE @searchLike
      OR concessionaire_name LIKE @searchLike
      OR bill_numbers LIKE @searchLike
      OR address LIKE @searchLike
      OR payor_name LIKE @searchLike)
ORDER BY collection_id DESC;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@collectionDate", collectionDate.Date);
        command.Parameters.AddWithValue("@search", searchTerm);
        command.Parameters.AddWithValue("@searchLike", $"%{searchTerm}%");

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<string> GetNextOrNumberAsync(UserRole role)
    {
        const string sql = @"
SELECT LPAD(COALESCE(MAX(CAST(or_number AS UNSIGNED)), 0) + 1, 7, '0')
FROM collection
WHERE or_number REGEXP '^[0-9]+$';";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        object? scalar = await command.ExecuteScalarAsync();
        string nextValue = Convert.ToString(scalar, CultureInfo.InvariantCulture) ?? "0000001";
        return string.IsNullOrWhiteSpace(nextValue) ? "0000001" : nextValue;
    }

    public static async Task<DataTable> PreviewCollectionAsync(UserRole role, CollectionPreviewRequest request)
    {
        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand("sp_preview_collection_v48", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        string jsonConcessionaires = BuildConcessionairesJson(request.ConcessionaireIds);

        command.Parameters.AddWithValue("p_or_number", request.OrNumber);
        command.Parameters.AddWithValue("p_payment_date", request.PaymentDate);
        command.Parameters.AddWithValue("p_payment_type", request.PaymentType);
        command.Parameters.AddWithValue("p_reference_no", request.ReferenceNumber);
        command.Parameters.AddWithValue("p_remarks", request.Remarks);
        command.Parameters.AddWithValue("p_payor_name", request.PayorName);
        command.Parameters.AddWithValue("p_others", request.Others);
        command.Parameters.AddWithValue("p_json_concessionaires", jsonConcessionaires);

        return await ExecuteToDataTableAsync(command);
    }

    public static async Task<DataTable> PostCollectionAsync(UserRole role, CollectionPaymentRequest request)
    {
        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand("sp_make_collection_v48_test", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        string jsonConcessionaires = BuildConcessionairesJson(request.ConcessionaireIds);

        command.Parameters.AddWithValue("p_or_number", request.OrNumber);
        command.Parameters.AddWithValue("p_payment_date", request.PaymentDate);
        command.Parameters.AddWithValue("p_payment_type", request.PaymentType);
        command.Parameters.AddWithValue("p_reference_no", request.ReferenceNumber);
        command.Parameters.AddWithValue("p_remarks", request.Remarks);
        command.Parameters.AddWithValue("p_payor_name", request.PayorName);
        command.Parameters.AddWithValue("p_amount_received", request.AmountReceived);
        command.Parameters.AddWithValue("p_others", request.Others);
        command.Parameters.AddWithValue("p_user_id", request.UserId);
        command.Parameters.AddWithValue("p_json_concessionaires", jsonConcessionaires);

        return await ExecuteToDataTableAsync(command);
    }

    public static async Task<DataTable> PostScfCollectionAsync(UserRole role, CollectionScfPaymentRequest request)
    {
        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand("sp_make_scf_collection_v48", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("p_or_number", request.OrNumber);
        command.Parameters.AddWithValue("p_payment_date", request.PaymentDate);
        command.Parameters.AddWithValue("p_payment_type", request.PaymentType);
        command.Parameters.AddWithValue("p_reference_no", request.ReferenceNumber);
        command.Parameters.AddWithValue("p_remarks", request.Remarks);
        command.Parameters.AddWithValue("p_payor_name", request.PayorName);
        command.Parameters.AddWithValue("p_scf_amount", request.ScfAmount);
        command.Parameters.AddWithValue("p_others", request.Others);
        command.Parameters.AddWithValue("p_user_id", request.UserId);
        command.Parameters.AddWithValue("p_concessionaire_id", request.ConcessionaireId);

        return await ExecuteToDataTableAsync(command);
    }

    public static async Task<DataTable> VoidCollectionAsync(UserRole role, int collectionId, int voidedByUserId, string remarks)
    {
        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand("sp_void_collection_v48", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("p_collection_id", collectionId);
        command.Parameters.AddWithValue("p_voided_by_user_id", voidedByUserId);
        command.Parameters.AddWithValue("p_void_remarks", remarks);

        return await ExecuteToDataTableAsync(command);
    }

    public static async Task<DataTable> GetCollectionsAsync(UserRole role, string? searchTerm = null)
    {
        const string sql = @"
SELECT
    collection_id,
    or_number,
    collection_date,
    bill_numbers,
    concessionaire_code,
    concessionaire_name,
    address,
    total_current_bill,
    total_arrears,
    total_penalty,
    total_tax,
    total_scf,
    total_others,
    grand_total,
    amount_received,
    change_amount,
    total_paid_amount,
    status,
    payment_type,
    payment_reference,
    payor_name,
    created_by_full_name,
    created_at,
    updated_at,
    voided_at
FROM v_collection_with_users
WHERE (@search = ''
    OR or_number LIKE @searchLike
    OR concessionaire_code LIKE @searchLike
    OR concessionaire_name LIKE @searchLike
    OR bill_numbers LIKE @searchLike
    OR address LIKE @searchLike
    OR payor_name LIKE @searchLike)
ORDER BY collection_id DESC
LIMIT 500;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        string search = searchTerm?.Trim() ?? string.Empty;
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@searchLike", $"%{search}%");

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<int> GetCollectionHistoryTotalRecordsAsync(UserRole role, DateTime dateFrom, DateTime dateTo, string? searchTerm = null)
    {
        const string sql = @"
SELECT COUNT(*)
FROM v_collection_with_users
WHERE DATE(collection_date) >= @dateFrom
  AND DATE(collection_date) < @dateToExclusive
  AND (@search = ''
      OR or_number LIKE @searchLike
      OR concessionaire_code LIKE @searchLike
      OR concessionaire_name LIKE @searchLike
      OR bill_numbers LIKE @searchLike
      OR address LIKE @searchLike
      OR payor_name LIKE @searchLike
      OR remarks LIKE @searchLike);";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        string search = searchTerm?.Trim() ?? string.Empty;
        command.Parameters.AddWithValue("@dateFrom", dateFrom.Date);
        command.Parameters.AddWithValue("@dateToExclusive", dateTo.Date.AddDays(1));
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@searchLike", $"%{search}%");

        object? scalar = await command.ExecuteScalarAsync();
        return Convert.ToInt32(scalar ?? 0, CultureInfo.InvariantCulture);
    }

    public static async Task<DataTable> GetCollectionHistoryPagedAsync(
        UserRole role,
        DateTime dateFrom,
        DateTime dateTo,
        int offset,
        int limit,
        string? searchTerm = null)
    {
        const string sql = @"
SELECT
    collection_id,
    or_number,
    collection_date,
    bill_numbers,
    concessionaire_code,
    concessionaire_name,
    address,
    total_current_bill,
    total_arrears,
    total_penalty,
    total_tax,
    total_scf,
    total_others,
    grand_total,
    amount_received,
    change_amount,
    total_paid_amount,
    uncollected,
    payment_type,
    payment_reference,
    remarks,
        payor_name,
    created_by_full_name,
        created_at,
        billing_count,
        payment_count
FROM v_collection_with_users
WHERE DATE(collection_date) >= @dateFrom
  AND DATE(collection_date) < @dateToExclusive
  AND (@search = ''
      OR or_number LIKE @searchLike
      OR concessionaire_code LIKE @searchLike
      OR concessionaire_name LIKE @searchLike
      OR bill_numbers LIKE @searchLike
      OR address LIKE @searchLike
      OR payor_name LIKE @searchLike
      OR remarks LIKE @searchLike)
ORDER BY collection_date DESC, CAST(or_number AS UNSIGNED) DESC, or_number DESC
LIMIT @limit OFFSET @offset;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        string search = searchTerm?.Trim() ?? string.Empty;
        command.Parameters.AddWithValue("@dateFrom", dateFrom.Date);
        command.Parameters.AddWithValue("@dateToExclusive", dateTo.Date.AddDays(1));
        command.Parameters.AddWithValue("@search", search);
        command.Parameters.AddWithValue("@searchLike", $"%{search}%");
        command.Parameters.AddWithValue("@limit", limit);
        command.Parameters.AddWithValue("@offset", offset);

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<DataTable> GetLedgerRowsAsync(UserRole role, string concessionaireCode)
    {
        const string sql = @"
SELECT
    ledger.concessionaire_id,
    ledger.concessionaire_code,
    ledger.concessionaire_name,
    ledger.address,
    ledger.zone_id,
    ledger.transaction_date,
    ledger.reference_no,
    ledger.remarks,
    ledger.debit,
    ledger.credit,
    ledger.water_charge,
    ledger.tax_amount,
    ledger.penalty_amount,
    ledger.scf_amount,
    ledger.arrears_amount,
    ledger.billing_status
FROM
(
    SELECT
        c.concessionaire_id,
        c.concessionaire_code,
        c.concessionaire_name,
        c.address,
        c.zone_id,
        b.billing_date AS transaction_date,
        CONCAT('BILL-', b.bill_number) AS reference_no,
        '' AS remarks,
        ROUND(COALESCE(b.total_amount, 0.00), 2) AS debit,
        0.00 AS credit,
        ROUND(COALESCE(b.water_charge, 0.00), 2) AS water_charge,
        ROUND(COALESCE(b.tax_amount, 0.00), 2) AS tax_amount,
        ROUND(COALESCE(b.penalty_amount, 0.00), 2) AS penalty_amount,
        ROUND(COALESCE(b.scf_amount, 0.00), 2) AS scf_amount,
        ROUND(COALESCE(b.arrears_amount, 0.00), 2) AS arrears_amount,
        b.status AS billing_status
    FROM billing b
    INNER JOIN concessionaire c
        ON c.concessionaire_id = b.concessionaire_id
    WHERE c.concessionaire_code = @concessionaireCode

    UNION ALL

    SELECT
        c.concessionaire_id,
        c.concessionaire_code,
        c.concessionaire_name,
        c.address,
        c.zone_id,
        col.collection_date AS transaction_date,
        CONCAT('OR-', col.or_number) AS reference_no,
        COALESCE(col.remarks, '') AS remarks,
        0.00 AS debit,
        ROUND(COALESCE(col.grand_total, 0.00), 2) AS credit,
        ROUND(COALESCE(col.total_current_bill, 0.00), 2) AS water_charge,
        ROUND(COALESCE(col.total_tax, 0.00), 2) AS tax_amount,
        ROUND(COALESCE(col.total_penalty, 0.00), 2) AS penalty_amount,
        ROUND(COALESCE(col.total_scf, 0.00), 2) AS scf_amount,
        ROUND(COALESCE(col.total_arrears, 0.00), 2) AS arrears_amount,
        NULL AS billing_status
    FROM collection col
    INNER JOIN concessionaire c
        ON (
            c.concessionaire_code = col.concessionaire_code
            OR FIND_IN_SET(c.concessionaire_code, REPLACE(COALESCE(col.concessionaire_code, ''), ' ', '')) > 0
        )
    WHERE c.concessionaire_code = @concessionaireCode
) AS ledger
ORDER BY
    ledger.transaction_date ASC,
    ledger.reference_no ASC;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@concessionaireCode", concessionaireCode.Trim());

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

    private static async Task<DataTable> ExecuteToDataTableAsync(MySqlCommand command)
    {
        await using var reader = await command.ExecuteReaderAsync();
        var table = new DataTable();
        table.Load(reader);
        return table;
    }

    private static string BuildConcessionairesJson(IReadOnlyList<int> concessionaireIds)
    {
        int[] normalizedIds = concessionaireIds
            .Where(id => id > 0)
            .Distinct()
            .ToArray();

        return JsonSerializer.Serialize(normalizedIds);
    }
}
