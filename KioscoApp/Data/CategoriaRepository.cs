using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class CategoriaRepository
    {
        public List<Categoria> ObtenerTodas()
        {
            var categorias = new List<Categoria>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT Id, Nombre, Descripcion FROM Categorias ORDER BY Nombre";
            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                categorias.Add(MapCategoria(reader));
            }
            return categorias;
        }

        public Categoria? ObtenerPorId(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT Id, Nombre, Descripcion FROM Categorias WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapCategoria(reader);
            }
            return null;
        }

        public bool ExisteNombre(string nombre, int idExcluir)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT COUNT(1) FROM Categorias WHERE LOWER(Nombre) = LOWER(@Nombre) AND Id != @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nombre", nombre.Trim());
            cmd.Parameters.AddWithValue("@Id", idExcluir);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        public bool TieneProductosAsociados(int idCategoria)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT COUNT(1) FROM Productos WHERE IdCategoria = @IdCategoria";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        public void Insertar(Categoria c)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "INSERT INTO Categorias (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre.Trim());
            cmd.Parameters.AddWithValue("@Descripcion", (object?)c.Descripcion ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Categoria c)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "UPDATE Categorias SET Nombre = @Nombre, Descripcion = @Descripcion WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", c.Id);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre.Trim());
            cmd.Parameters.AddWithValue("@Descripcion", (object?)c.Descripcion ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "DELETE FROM Categorias WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private Categoria MapCategoria(SqlDataReader reader)
        {
            return new Categoria
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null
            };
        }
    }
}
