using System;
using Microsoft.Data.SqlClient;

namespace KioscoApp
{
    public static class DatabaseHelper
    {
        // Cambiar si el servidor tiene otro nombre o usar autenticación de Windows (Integrated Security=True)
        private static readonly string connectionString = @"Server=.\SQLEXPRESS;Database=KioscoDB;Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString => connectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlConnection GetMasterConnection()
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };
            return new SqlConnection(builder.ConnectionString);
        }
    }
}
