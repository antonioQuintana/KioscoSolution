using System;
using System.Data;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class ConfiguracionRepository
    {
        public ConfiguracionEmpresa Obtener()
        {
            var config = new ConfiguracionEmpresa();

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand("sp_Configuracion_Obtener", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                config.Id = Convert.ToInt32(reader["Id"]);
                config.NombreFantasia = reader["NombreFantasia"]?.ToString() ?? "KIOSCO EXPRESS";
                config.RazonSocial = reader["RazonSocial"]?.ToString() ?? string.Empty;
                config.Cuit = reader["Cuit"]?.ToString() ?? string.Empty;
                config.Direccion = reader["Direccion"]?.ToString() ?? string.Empty;
                config.Telefono = reader["Telefono"]?.ToString() ?? string.Empty;
                config.MensajePieTicket = reader["MensajePieTicket"]?.ToString() ?? "¡Gracias por su compra!";
                config.ImpresoraPredeterminada = reader["ImpresoraPredeterminada"]?.ToString() ?? string.Empty;
                config.AnchoPapelMm = reader["AnchoPapelMm"] != DBNull.Value ? Convert.ToInt32(reader["AnchoPapelMm"]) : 80;
                config.ImpresionAutomatica = reader["ImpresionAutomatica"] != DBNull.Value && Convert.ToBoolean(reader["ImpresionAutomatica"]);
                config.CortarPapel = reader["CortarPapel"] == DBNull.Value || Convert.ToBoolean(reader["CortarPapel"]);
            }

            return config;
        }

        public void Guardar(ConfiguracionEmpresa config)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand("sp_Configuracion_Guardar", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreFantasia", config.NombreFantasia ?? string.Empty);
            cmd.Parameters.AddWithValue("@RazonSocial", (object?)config.RazonSocial ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Cuit", (object?)config.Cuit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object?)config.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)config.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MensajePieTicket", (object?)config.MensajePieTicket ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ImpresoraPredeterminada", (object?)config.ImpresoraPredeterminada ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AnchoPapelMm", config.AnchoPapelMm);
            cmd.Parameters.AddWithValue("@ImpresionAutomatica", config.ImpresionAutomatica);
            cmd.Parameters.AddWithValue("@CortarPapel", config.CortarPapel);

            cmd.ExecuteNonQuery();
        }
    }
}
