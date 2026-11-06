using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class AuditoriaRepository
    {
        public void Registrar(RegistroAuditoria r)
        {
            try
            {
                using var conn = DatabaseHelper.GetConnection();
                conn.Open();

                using var cmd = new SqlCommand("sp_Auditoria_Registrar", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", (object?)r.IdUsuario ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Usuario", r.Usuario ?? "Sistema");
                cmd.Parameters.AddWithValue("@Rol", r.Rol ?? "Sistema");
                cmd.Parameters.AddWithValue("@Modulo", r.Modulo ?? "General");
                cmd.Parameters.AddWithValue("@Accion", r.Accion ?? string.Empty);
                cmd.Parameters.AddWithValue("@Detalle", (object?)r.Detalle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IpEquipo", (object?)r.IpEquipo ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                // No propagar para no interrumpir el flujo del usuario
            }
        }

        public List<RegistroAuditoria> Obtener(DateTime? desde = null, DateTime? hasta = null, string? modulo = null, string? filtro = null)
        {
            var lista = new List<RegistroAuditoria>();

            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using var cmd = new SqlCommand("sp_Auditoria_Obtener", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FechaDesde", (object?)desde ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@FechaHasta", (object?)hasta ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Modulo", (object?)modulo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Filtro", (object?)filtro ?? DBNull.Value);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new RegistroAuditoria
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Usuario = reader["Usuario"]?.ToString() ?? string.Empty,
                    Rol = reader["Rol"]?.ToString() ?? string.Empty,
                    Modulo = reader["Modulo"]?.ToString() ?? string.Empty,
                    Accion = reader["Accion"]?.ToString() ?? string.Empty,
                    Detalle = reader["Detalle"]?.ToString() ?? string.Empty,
                    IpEquipo = reader["IpEquipo"]?.ToString() ?? string.Empty
                });
            }

            return lista;
        }
    }
}
