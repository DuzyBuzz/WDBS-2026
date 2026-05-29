using System;
using System.IO;
using MySql.Data.MySqlClient;
using WDBS_2026.Models;

namespace WDBS_2026.Database
{
    internal static class DBConfig
    {
        private const string LoginConnectionKey = "AdminConnection";
        private static readonly string DbConfigFile = Path.Combine(AppContext.BaseDirectory, "db_connection.txt");

        public static string ConnectionString { get; private set; } = string.Empty;

        public static void SetConnectionStringForLogin()
        {
            ConnectionString = LoadRequiredConnectionString(LoginConnectionKey);
        }

        public static void SetConnectionString(string role)
        {
            ConnectionString = LoadRequiredConnectionString(GetConnectionKey(role));
        }

        public static void SetConnectionString(UserRole role)
        {
            SetConnectionString(role.ToString());
        }

        private static string GetConnectionKey(string role)
        {
            return role.Trim().ToUpperInvariant() switch
            {
                "ADMIN" => "AdminConnection",
                "BILLER" => "BillerConnection",
                "CASHIER" => "CashierConnection",
                _ => throw new ArgumentException("Invalid role specified.", nameof(role))
            };
        }

        private static string LoadRequiredConnectionString(string keyName)
        {
            string? connectionString = LoadConnectionStringFromFile(keyName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Connection string for '{keyName}' was not found in db_connection.txt.");
            }

            return connectionString;
        }

        private static string? LoadConnectionStringFromFile(string keyName)
        {
            if (!File.Exists(DbConfigFile))
            {
                throw new FileNotFoundException(
                    $"Database connection config file not found: {DbConfigFile}. " +
                    "A default db_connection.txt template is included with the project. Build or run the app once so it is copied beside the executable, then update it if your MySQL credentials differ.");
            }

            foreach (string line in File.ReadAllLines(DbConfigFile))
            {
                string trimmed = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                string[] parts = trimmed.Split(new[] { '=' }, 2);
                if (parts.Length == 2 && string.Equals(parts[0].Trim(), keyName, StringComparison.Ordinal))
                {
                    return parts[1].Trim();
                }
            }

            return null;
        }

        public static MySqlConnection GetConnection()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new InvalidOperationException("Connection string not set. Prepare the connection context before requesting a database connection.");
            }

            return new MySqlConnection(ConnectionString);
        }

        public static bool TestConnection(out string message)
        {
            try
            {
                using MySqlConnection connection = GetConnection();
                connection.Open();
                message = "Connection successful.";
                return true;
            }
            catch (Exception ex)
            {
                message = "Connection failed: " + ex.Message;
                return false;
            }
        }
    }
}
