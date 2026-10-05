using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class ClienteRepository
    {
        public Cliente ObtenerPorDni(string dni)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT * FROM Clientes WHERE Dni = @Dni AND Activo = 1";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Dni", dni);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapRow(reader);
            }
            return null;
        }

        public Cliente ObtenerPorId(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT * FROM Clientes WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapRow(reader);
            }
            return null;
        }

        private Cliente MapRow(SqlDataReader reader)
        {
            return new Cliente
            {
                Id = Convert.ToInt32(reader["Id"]),
                Dni = reader["Dni"].ToString(),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                Calle = reader["Calle"] != DBNull.Value ? reader["Calle"].ToString() : null,
                Numero = reader["Numero"] != DBNull.Value ? reader["Numero"].ToString() : null,
                Ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : null,
                Provincia = reader["Provincia"] != DBNull.Value ? reader["Provincia"].ToString() : null,
                Nacimiento = reader["Nacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["Nacimiento"]) : (DateTime?)null,
                FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                Activo = Convert.ToBoolean(reader["Activo"])
            };
        }

        public List<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT * FROM Clientes WHERE Activo = 1 AND Dni != '00000000'";
            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(MapRow(reader));
            }
            return lista;
        }

        public void Insertar(Cliente cliente)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento) 
                             VALUES (@Dni, @Nombre, @Apellido, @Telefono, @Email, @Calle, @Numero, @Ciudad, @Provincia, @Nacimiento)";
            using var cmd = new SqlCommand(query, connection);
            AsignarParametros(cmd, cliente);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Cliente cliente)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"UPDATE Clientes SET Dni = @Dni, Nombre = @Nombre, Apellido = @Apellido, 
                             Telefono = @Telefono, Email = @Email, Calle = @Calle, Numero = @Numero, 
                             Ciudad = @Ciudad, Provincia = @Provincia, Nacimiento = @Nacimiento 
                             WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            AsignarParametros(cmd, cliente);
            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            cmd.ExecuteNonQuery();
        }

        public void EliminarLogico(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "UPDATE Clientes SET Activo = 0 WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private void AsignarParametros(SqlCommand cmd, Cliente cliente)
        {
            cmd.Parameters.AddWithValue("@Dni", cliente.Dni);
            cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Calle", cliente.Calle ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Numero", cliente.Numero ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", cliente.Ciudad ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Nacimiento", cliente.Nacimiento ?? (object)DBNull.Value);
        }
    }
}
