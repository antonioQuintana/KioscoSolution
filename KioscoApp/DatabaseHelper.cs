using System;
using Microsoft.Data.SqlClient;

namespace KioscoApp
{
    public static class DatabaseHelper
    {
        // Cambiar si el servidor tiene otro nombre o usar autenticación de Windows (Integrated Security=True)
        private static readonly string connectionString = @"Server=.\SQLEXPRESS;Database=KioscoDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
