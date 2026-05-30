using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Admin;

internal static class AdminSystemSettingsService
{
    public static readonly string[] ManagedSettingKeys =
    [
        "discount_percent",
        "discount_thresh_hold",
        "penalize_after_days",
        "penalty_percent",
        "tax_percent"
    ];

    public static async Task<Dictionary<string, string>> GetSystemSettingsAsync(UserRole role)
    {
        const string sql = @"
SELECT settings_key, settings_value
FROM system_settings
WHERE settings_key IN (
    'discount_percent',
    'discount_thresh_hold',
    'penalize_after_days',
    'penalty_percent',
    'tax_percent');";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        while (await reader.ReadAsync())
        {
            string key = Convert.ToString(reader["settings_key"], CultureInfo.InvariantCulture) ?? string.Empty;
            string value = Convert.ToString(reader["settings_value"], CultureInfo.InvariantCulture) ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                values[key] = value;
            }
        }

        foreach (string key in ManagedSettingKeys)
        {
            values.TryAdd(key, string.Empty);
        }

        return values;
    }

    public static async Task SaveSystemSettingsAsync(UserRole role, IReadOnlyDictionary<string, string> values)
    {
        const string sql = @"
INSERT INTO system_settings (settings_key, settings_value)
VALUES (@settingsKey, @settingsValue)
ON DUPLICATE KEY UPDATE settings_value = @settingsValue;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            foreach (string key in ManagedSettingKeys)
            {
                if (!values.TryGetValue(key, out string? value))
                {
                    continue;
                }

                await using var command = new MySqlCommand(sql, connection, transaction);
                command.Parameters.AddWithValue("@settingsKey", key);
                command.Parameters.AddWithValue("@settingsValue", value?.Trim() ?? string.Empty);
                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public static async Task<DataTable> GetServicesAsync(UserRole role)
    {
        const string sql = @"
SELECT
    service_id,
    service_type,
    pipe_size,
    min_rate,
    rate_11_20,
    rate_21_30,
    rate_31_40,
    rate_41_above
FROM services
ORDER BY service_id ASC;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task<DataTable> GetZonesAsync(UserRole role)
    {
        const string sql = @"
SELECT
    zone_id,
    zone_name
FROM zone
ORDER BY zone_id ASC;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task SaveServiceChangesAsync(UserRole role, DataTable servicesTable)
    {
        DataTable? changes = servicesTable.GetChanges(DataRowState.Added | DataRowState.Modified);
        if (changes is null || changes.Rows.Count == 0)
        {
            return;
        }

        const string updateSql = @"
UPDATE services
SET
    service_type = @serviceType,
    pipe_size = @pipeSize,
    min_rate = @minRate,
    rate_11_20 = @rate1120,
    rate_21_30 = @rate2130,
    rate_31_40 = @rate3140,
    rate_41_above = @rate41Above
WHERE service_id = @serviceId;";

        const string insertSql = @"
INSERT INTO services
(
    service_id,
    service_type,
    pipe_size,
    min_rate,
    rate_11_20,
    rate_21_30,
    rate_31_40,
    rate_41_above
)
VALUES
(
    @serviceId,
    @serviceType,
    @pipeSize,
    @minRate,
    @rate1120,
    @rate2130,
    @rate3140,
    @rate41Above
);";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            foreach (DataRow row in changes.Rows)
            {
                string sql = row.RowState == DataRowState.Added ? insertSql : updateSql;
                await using var command = new MySqlCommand(sql, connection, transaction);
                command.Parameters.AddWithValue("@serviceId", row["service_id"]);
                command.Parameters.AddWithValue("@serviceType", row["service_type"]);
                command.Parameters.AddWithValue("@pipeSize", row["pipe_size"]);
                command.Parameters.AddWithValue("@minRate", row["min_rate"]);
                command.Parameters.AddWithValue("@rate1120", row["rate_11_20"]);
                command.Parameters.AddWithValue("@rate2130", row["rate_21_30"]);
                command.Parameters.AddWithValue("@rate3140", row["rate_31_40"]);
                command.Parameters.AddWithValue("@rate41Above", row["rate_41_above"]);

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public static async Task SaveZoneChangesAsync(UserRole role, DataTable zonesTable)
    {
        DataTable? changes = zonesTable.GetChanges(DataRowState.Added | DataRowState.Modified);
        if (changes is null || changes.Rows.Count == 0)
        {
            return;
        }

        const string updateSql = @"
UPDATE zone
SET zone_name = @zoneName
WHERE zone_id = @zoneId;";

        const string insertSql = @"
INSERT INTO zone
(
    zone_id,
    zone_name
)
VALUES
(
    @zoneId,
    @zoneName
);";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using MySqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            foreach (DataRow row in changes.Rows)
            {
                string sql = row.RowState == DataRowState.Added ? insertSql : updateSql;
                await using var command = new MySqlCommand(sql, connection, transaction);
                command.Parameters.AddWithValue("@zoneId", row["zone_id"]);
                command.Parameters.AddWithValue("@zoneName", row["zone_name"]);

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private static async Task<MySqlConnection> OpenConnectionAsync(UserRole role)
    {
        DBConfig.SetConnectionString(role);
        MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync();
        return connection;
    }
}