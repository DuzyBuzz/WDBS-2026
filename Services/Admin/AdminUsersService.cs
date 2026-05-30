using System.Data;
using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;

namespace WDBS_2026.Services.Admin;

internal sealed class AdminUserWriteRequest
{
    public string Username { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public UserRole Role { get; init; }
    public bool IsActive { get; init; }
}

internal static class AdminUsersService
{
    public static async Task<DataTable> GetUsersAsync(UserRole role, string search)
    {
        const string sql = @"
SELECT
    user_id,
    username,
    full_name,
    role,
    is_active,
    created_at
FROM users
WHERE (@search = '' OR username LIKE CONCAT('%', @search, '%') OR full_name LIKE CONCAT('%', @search, '%'))
ORDER BY user_id DESC;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@search", search.Trim());

        using var adapter = new MySqlDataAdapter(command);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    public static async Task CreateUserAsync(UserRole role, AdminUserWriteRequest request)
    {
        const string sql = @"
INSERT INTO users
(
    username,
    password,
    full_name,
    role,
    is_active
)
VALUES
(
    @username,
    @password,
    @fullName,
    @role,
    @isActive
);";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", request.Username.Trim());
        command.Parameters.AddWithValue("@password", request.Password);
        command.Parameters.AddWithValue("@fullName", request.FullName.Trim());
        command.Parameters.AddWithValue("@role", request.Role.ToString());
        command.Parameters.AddWithValue("@isActive", request.IsActive);

        await command.ExecuteNonQueryAsync();
    }

    public static async Task UpdateUserAsync(UserRole role, int userId, AdminUserWriteRequest request)
    {
        string sql = string.IsNullOrWhiteSpace(request.Password)
            ? @"
UPDATE users
SET
    username = @username,
    full_name = @fullName,
    role = @role,
    is_active = @isActive
WHERE user_id = @userId;"
            : @"
UPDATE users
SET
    username = @username,
    password = @password,
    full_name = @fullName,
    role = @role,
    is_active = @isActive
WHERE user_id = @userId;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", request.Username.Trim());
        command.Parameters.AddWithValue("@fullName", request.FullName.Trim());
        command.Parameters.AddWithValue("@role", request.Role.ToString());
        command.Parameters.AddWithValue("@isActive", request.IsActive);
        command.Parameters.AddWithValue("@userId", userId);

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            command.Parameters.AddWithValue("@password", request.Password);
        }

        int affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows == 0)
        {
            throw new InvalidOperationException("User account was not updated.");
        }
    }

    public static async Task DeleteUserAsync(UserRole role, int userId)
    {
        const string sql = @"
DELETE FROM users
WHERE user_id = @userId;";

        await using MySqlConnection connection = await OpenConnectionAsync(role);
        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", userId);

        int affectedRows = await command.ExecuteNonQueryAsync();
        if (affectedRows == 0)
        {
            throw new InvalidOperationException("User account was not deleted.");
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