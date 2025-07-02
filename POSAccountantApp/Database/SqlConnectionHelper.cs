using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace POSAccountantApp.Database
{
    public static class SqlConnectionHelper
    {
        private static readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=POSAccountingDB;Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True";

        public static Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            try
            {
                var connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                throw new Exception($"Database connection error: {ex.Message}", ex);
            }
        }

        public static void ExecuteNonQuery(string query, Microsoft.Data.SqlClient.SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new Microsoft.Data.SqlClient.SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    throw new Exception($"Database query error: {ex.Message}", ex);
                }
            }
        }

        public static DataTable ExecuteQuery(string query, Microsoft.Data.SqlClient.SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new Microsoft.Data.SqlClient.SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    var dataTable = new DataTable();
                    using (var adapter = new Microsoft.Data.SqlClient.SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    throw new Exception($"Database query error: {ex.Message}", ex);
                }
            }
        }

        public static object ExecuteScalar(string query, Microsoft.Data.SqlClient.SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            using (var command = new Microsoft.Data.SqlClient.SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    return command.ExecuteScalar();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    throw new Exception($"Database query error: {ex.Message}", ex);
                }
            }
        }

        public static void InitializeDatabase()
        {
            // Create database if it doesn't exist
            string createDbQuery = @"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'POSAccountingDB')
                BEGIN
                    CREATE DATABASE POSAccountingDB;
                END";

            // Create tables
            string createTablesQuery = @"
                USE POSAccountingDB;

                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                BEGIN
                    CREATE TABLE Users (
                        UserId INT PRIMARY KEY IDENTITY(1,1),
                        Username NVARCHAR(50) UNIQUE NOT NULL,
                        PasswordHash NVARCHAR(256) NOT NULL,
                        Role NVARCHAR(20) NOT NULL,
                        FullName NVARCHAR(100) NOT NULL,
                        CreatedDate DATETIME NOT NULL,
                        IsActive BIT NOT NULL
                    );

                    -- Insert default admin user
                    INSERT INTO Users (Username, PasswordHash, Role, FullName, CreatedDate, IsActive)
                    VALUES ('admin', '" + Models.User.HashPassword("admin") + @"', 'Admin', 'System Administrator', GETDATE(), 1);
                END";

            try
            {
                // Create database
                using (var connection = new Microsoft.Data.SqlClient.SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;TrustServerCertificate=True"))
                {
                    connection.Open();
                    using (var command = new Microsoft.Data.SqlClient.SqlCommand(createDbQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Create tables
                ExecuteNonQuery(createTablesQuery);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database initialization error: {ex.Message}", ex);
            }
        }
    }
}
