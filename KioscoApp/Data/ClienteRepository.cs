using System;
using System.Collections.Generic;
using System.Data;
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
            using var cmd = new SqlCommand("sp_Clientes_ObtenerPorDni", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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
            using var cmd = new SqlCommand("sp_Clientes_ObtenerPorId", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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
                Dni = reader["Dni"]?.ToString() ?? string.Empty,
                Nombre = reader["Nombre"]?.ToString() ?? string.Empty,
                Apellido = reader["Apellido"]?.ToString() ?? string.Empty,
                Telefono = HasColumn(reader, "Telefono") && reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
                Email = HasColumn(reader, "Email") && reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                Calle = HasColumn(reader, "Calle") && reader["Calle"] != DBNull.Value ? reader["Calle"].ToString() : null,
                Numero = HasColumn(reader, "Numero") && reader["Numero"] != DBNull.Value ? reader["Numero"].ToString() : null,
                Ciudad = HasColumn(reader, "Ciudad") && reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : null,
                Provincia = HasColumn(reader, "Provincia") && reader["Provincia"] != DBNull.Value ? reader["Provincia"].ToString() : null,
                Nacimiento = HasColumn(reader, "Nacimiento") && reader["Nacimiento"] != DBNull.Value ? Convert.ToDateTime(reader["Nacimiento"]) : (DateTime?)null,
                FechaRegistro = HasColumn(reader, "FechaRegistro") && reader["FechaRegistro"] != DBNull.Value ? Convert.ToDateTime(reader["FechaRegistro"]) : DateTime.Now,
                SaldoDeudor = HasColumn(reader, "SaldoDeudor") && reader["SaldoDeudor"] != DBNull.Value ? Convert.ToDecimal(reader["SaldoDeudor"]) : 0m,
                LimiteCredito = HasColumn(reader, "LimiteCredito") && reader["LimiteCredito"] != DBNull.Value ? Convert.ToDecimal(reader["LimiteCredito"]) : 15000m,
                Activo = HasColumn(reader, "Activo") && reader["Activo"] != DBNull.Value ? Convert.ToBoolean(reader["Activo"]) : true
            };
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public List<Cliente> ObtenerTodos()
        {
            var lista = new List<Cliente>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Clientes_ObtenerTodos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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
            using var cmd = new SqlCommand("sp_Clientes_Insertar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            AsignarParametros(cmd, cliente);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Cliente cliente)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Clientes_Actualizar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            AsignarParametros(cmd, cliente);
            cmd.Parameters.AddWithValue("@Id", cliente.Id);
            cmd.ExecuteNonQuery();
        }

        public void EliminarLogico(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var cmd = new SqlCommand("sp_Clientes_EliminarLogico", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public void RegistrarAbonoCuentaCorriente(int idCliente, decimal monto, string metodoPago, int? idUsuario)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Reducir SaldoDeudor del cliente
                string sqlCliente = "UPDATE Clientes SET SaldoDeudor = CASE WHEN SaldoDeudor - @Monto < 0 THEN 0 ELSE SaldoDeudor - @Monto END WHERE Id = @Id";
                using var cmdCliente = new SqlCommand(sqlCliente, connection, transaction);
                cmdCliente.Parameters.AddWithValue("@Monto", monto);
                cmdCliente.Parameters.AddWithValue("@Id", idCliente);
                cmdCliente.ExecuteNonQuery();

                // 2. Si es en efectivo, impactar en la caja actual como Ingreso de fondos
                if (string.Equals(metodoPago, "Efectivo", StringComparison.OrdinalIgnoreCase))
                {
                    string sqlMov = @"
                        INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo)
                        VALUES (@IdUsuario, GETDATE(), 'Ingreso', @Monto, @Motivo)";
                    using var cmdMov = new SqlCommand(sqlMov, connection, transaction);
                    cmdMov.Parameters.AddWithValue("@IdUsuario", idUsuario.HasValue && idUsuario.Value > 0 ? (object)idUsuario.Value : DBNull.Value);
                    cmdMov.Parameters.AddWithValue("@Monto", monto);
                    cmdMov.Parameters.AddWithValue("@Motivo", $"Cobranza Cta. Cte. - Cliente #{idCliente}");
                    cmdMov.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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
