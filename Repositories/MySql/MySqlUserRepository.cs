using MySql.Data.MySqlClient;
using WDBS_2026.Database;
using WDBS_2026.Models;
using WDBS_2026.Repositories.Interfaces;
using WDBS_2026.Services.Interfaces;

namespace WDBS_2026.Repositories.MySql;

public sealed class MySqlUserRepository : IUserRepository
{
    private readonly IConnectionContext _connectionContext;

    public MySqlUserRepository(IConnectionContext connectionContext)
    {
        _connectionContext = connectionContext;
    }

    public async Task<UserAccount?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        _connectionContext.UseLoginConnection();

        const string sql = @"
SELECT
    user_id,
    username,
    password,
    full_name,
    role,
    is_active,
    created_at
FROM users
WHERE username = @username
LIMIT 1;";

        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new UserAccount
        {
            UserId = reader.GetInt32("user_id"),
            Username = reader.GetString("username"),
            Password = reader.GetString("password"),
            FullName = reader.GetString("full_name"),
            Role = ParseRole(reader.GetString("role")),
            IsActive = reader.GetBoolean("is_active"),
            CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at"))
                ? DateTime.MinValue
                : reader.GetDateTime("created_at")
        };
    }

    public async Task<UserAccount?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        _connectionContext.UseLoginConnection();

        const string sql = @"
SELECT
    user_id,
    username,
    password,
    full_name,
    role,
    is_active,
    created_at
FROM users
WHERE user_id = @userId
LIMIT 1;";

        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", userId);

        await using var reader = (MySqlDataReader)await command.ExecuteReaderAsync(cancellationToken);
        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new UserAccount
        {
            UserId = reader.GetInt32("user_id"),
            Username = reader.GetString("username"),
            Password = reader.GetString("password"),
            FullName = reader.GetString("full_name"),
            Role = ParseRole(reader.GetString("role")),
            IsActive = reader.GetBoolean("is_active"),
            CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at"))
                ? DateTime.MinValue
                : reader.GetDateTime("created_at")
        };
    }

    public async Task<bool> UsernameExistsAsync(string username, int excludeUserId, CancellationToken cancellationToken = default)
    {
        _connectionContext.UseLoginConnection();

        const string sql = @"
SELECT COUNT(*)
FROM users
WHERE username = @username
  AND user_id <> @excludeUserId;";

        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@excludeUserId", excludeUserId);

        object? scalar = await command.ExecuteScalarAsync(cancellationToken);
        int count = Convert.ToInt32(scalar ?? 0);
        return count > 0;
    }

    public async Task UpdateCredentialsAsync(
        int userId,
        string username,
        string fullName,
        string? newPassword,
        CancellationToken cancellationToken = default)
    {
        _connectionContext.UseLoginConnection();

        string sql = string.IsNullOrWhiteSpace(newPassword)
            ? @"
UPDATE users
SET username = @username,
    full_name = @fullName
WHERE user_id = @userId;"
            : @"
UPDATE users
SET username = @username,
    full_name = @fullName,
    password = @newPassword
WHERE user_id = @userId;";

        await using MySqlConnection connection = DBConfig.GetConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@fullName", fullName);
        command.Parameters.AddWithValue("@userId", userId);

        if (!string.IsNullOrWhiteSpace(newPassword))
        {
            command.Parameters.AddWithValue("@newPassword", newPassword);
        }

        int affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affectedRows == 0)
        {
            throw new InvalidOperationException("No user record was updated.");
        }
    }

    private static UserRole ParseRole(string databaseValue)
    {
        if (Enum.TryParse(databaseValue, true, out UserRole parsedRole))
        {
            return parsedRole;
        }

        throw new InvalidOperationException($"Unsupported user role '{databaseValue}' was returned by the database.");
    }
}