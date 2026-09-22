using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;
using System.Data;

namespace KioscoApp.Data
{
    public class UsuarioRepository
    {
        public List<Usuario> ObtenerTodos()
        {
            var usuarios = new List<Usuario>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT Id, Nombre, Apellido, Usuario, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento FROM Usuarios";
            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(MapUsuario(reader));
            }
            return usuarios;
        }

        public Usuario ObtenerPorUsuario(string nombreUsuario)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT Id, Nombre, Apellido, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento FROM Usuarios WHERE Usuario = @Usuario";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Usuario", nombreUsuario);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapUsuario(reader, true);
            }
            return null;
        }

        public bool ExisteDatoUnico(string campo, string valor, int idExcluir)
        {
            // Validar el campo para prevenir inyección SQL en el nombre de la columna
            var allowedColumns = new HashSet<string> { "Usuario", "Email", "Telefono" };
            if (!allowedColumns.Contains(campo)) throw new ArgumentException("Campo inválido");

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = $"SELECT COUNT(1) FROM Usuarios WHERE {campo} = @Valor AND Id != @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Valor", valor);
            cmd.Parameters.AddWithValue("@Id", idExcluir);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        public void Insertar(Usuario u)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"INSERT INTO Usuarios 
                (Nombre, Apellido, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
                VALUES 
                (@Nombre, @Apellido, @Usuario, @Contrasena, @Rol, @Email, @Telefono, @Calle, @Numero, @Ciudad, @Provincia, @Sexo, @Nacimiento)";
                
            using var cmd = new SqlCommand(query, connection);
            SetCommandParameters(cmd, u, true);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Usuario u)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"UPDATE Usuarios SET 
                Nombre=@Nombre, Apellido=@Apellido, Usuario=@Usuario, Rol=@Rol, 
                Email=@Email, Telefono=@Telefono, Calle=@Calle, Numero=@Numero, 
                Ciudad=@Ciudad, Provincia=@Provincia, Sexo=@Sexo, Nacimiento=@Nacimiento " + 
                (string.IsNullOrEmpty(u.Contrasena) ? "" : ", Contrasena=@Contrasena ") + 
                "WHERE Id=@Id";
                
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", u.Id);
            SetCommandParameters(cmd, u, !string.IsNullOrEmpty(u.Contrasena));
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "DELETE FROM Usuarios WHERE Id=@Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private void SetCommandParameters(SqlCommand cmd, Usuario u, bool includePassword)
        {
            cmd.Parameters.AddWithValue("@Nombre", (object)u.Nombre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Apellido", (object)u.Apellido ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Usuario", (object)u.NombreUsuario ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Rol", (object)u.Rol ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Email", (object)u.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object)u.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Calle", (object)u.Calle ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Numero", (object)u.Numero ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", (object)u.Ciudad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Provincia", (object)u.Provincia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Sexo", (object)u.Sexo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Nacimiento", (object)u.Nacimiento ?? DBNull.Value);

            if (includePassword)
            {
                cmd.Parameters.AddWithValue("@Contrasena", (object)u.Contrasena ?? DBNull.Value);
            }
        }

        private Usuario MapUsuario(SqlDataReader reader, bool includePassword = false)
        {
            var u = new Usuario
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"] != DBNull.Value ? reader["Apellido"].ToString() : null,
                NombreUsuario = reader["Usuario"].ToString(),
                Rol = reader["Rol"].ToString(),
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                Calle = reader["Calle"] != DBNull.Value ? reader["Calle"].ToString() : null,
                Numero = reader["Numero"] != DBNull.Value ? reader["Numero"].ToString() : null,
                Ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : null,
                Provincia = reader["Provincia"] != DBNull.Value ? reader["Provincia"].ToString() : null,
                Sexo = reader["Sexo"] != DBNull.Value ? reader["Sexo"].ToString() : null,
                Nacimiento = reader["Nacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["Nacimiento"]) : (DateTime?)null
            };

            if (includePassword)
            {
                u.Contrasena = reader["Contrasena"].ToString();
            }

            return u;
        }
    }
}
