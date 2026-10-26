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
            using var cmd = new SqlCommand("sp_Usuarios_ObtenerTodos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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
            using var cmd = new SqlCommand("sp_Usuarios_ObtenerPorUsuario", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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
            // Validar el campo para prevenir valores no esperados
            var allowedColumns = new HashSet<string> { "Usuario", "Email", "Telefono", "Dni" };
            if (!allowedColumns.Contains(campo)) throw new ArgumentException("Campo inválido");

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Usuarios_ExisteDatoUnico", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Campo", campo);
            cmd.Parameters.AddWithValue("@Valor", valor);
            cmd.Parameters.AddWithValue("@Id", idExcluir);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        public void Insertar(Usuario u)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Usuarios_Insertar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SetCommandParameters(cmd, u, true);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Usuario u)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Usuarios_Actualizar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", u.Id);
            SetCommandParameters(cmd, u, !string.IsNullOrEmpty(u.Contrasena));
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Usuarios_Eliminar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private void SetCommandParameters(SqlCommand cmd, Usuario u, bool includePassword)
        {
            cmd.Parameters.AddWithValue("@Nombre", (object)u.Nombre ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Apellido", (object)u.Apellido ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Dni", (object)u.Dni ?? DBNull.Value);
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
                Dni = reader["Dni"] != DBNull.Value ? reader["Dni"].ToString() : null,
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
