using System;
using Microsoft.Data.SqlClient;

class Program {
    static void Main() {
        string password = "antonio";
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        
        string connectionString = "Server=.\SQLEXPRESS;Database=KioscoDB;Integrated Security=True;TrustServerCertificate=True;";
        using (SqlConnection conn = new SqlConnection(connectionString)) {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Contrasena = @Hash WHERE Id = 3", conn)) {
                cmd.Parameters.AddWithValue("@Hash", hash);
                cmd.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Updated hash successfully.");
    }
}
