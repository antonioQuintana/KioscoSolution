using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class CajaRepository
    {
        public ResumenCajaActual ObtenerResumenCajaActual(decimal fondoInicial = 0m)
        {
            var resumen = new ResumenCajaActual { FondoInicial = fondoInicial };

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            // 1. Obtener totales de ventas del turno actual (sin cerrar)
            string sqlVentas = @"
                SELECT 
                    ISNULL(SUM(CASE WHEN MetodoPago LIKE '%Efectivo%' THEN Total ELSE 0 END), 0) AS VentasEfectivo,
                    ISNULL(SUM(CASE WHEN MetodoPago LIKE '%QR%' OR MetodoPago LIKE '%MercadoPago%' THEN Total ELSE 0 END), 0) AS VentasQR,
                    ISNULL(SUM(CASE WHEN MetodoPago LIKE '%Tarjeta%' THEN Total ELSE 0 END), 0) AS VentasTarjeta,
                    COUNT(*) AS CantidadVentas,
                    MIN(Fecha) AS PrimerVenta
                FROM Ventas
                WHERE IdCierreCaja IS NULL";

            using (var cmd = new SqlCommand(sqlVentas, connection))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    resumen.VentasEfectivo = Convert.ToDecimal(reader["VentasEfectivo"]);
                    resumen.VentasQR = Convert.ToDecimal(reader["VentasQR"]);
                    resumen.VentasTarjeta = Convert.ToDecimal(reader["VentasTarjeta"]);
                    resumen.CantidadVentas = Convert.ToInt32(reader["CantidadVentas"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("PrimerVenta")))
                    {
                        resumen.FechaPrimeraVenta = Convert.ToDateTime(reader["PrimerVenta"]);
                    }
                }
            }

            // 2. Obtener movimientos de caja (ingresos / retiros) del turno actual
            string sqlMovimientos = @"
                SELECT 
                    ISNULL(SUM(CASE WHEN Tipo = 'Ingreso' THEN Monto ELSE 0 END), 0) AS Ingresos,
                    ISNULL(SUM(CASE WHEN Tipo = 'Retiro' THEN Monto ELSE 0 END), 0) AS Gastos
                FROM MovimientosCaja
                WHERE IdCierreCaja IS NULL";

            using (var cmd = new SqlCommand(sqlMovimientos, connection))
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    resumen.IngresosVarios = Convert.ToDecimal(reader["Ingresos"]);
                    resumen.GastosRetiros = Convert.ToDecimal(reader["Gastos"]);
                }
            }

            return resumen;
        }

        public int RegistrarMovimiento(MovimientoCaja mov)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo)
                OUTPUT INSERTED.Id
                VALUES (@IdUsuario, GETDATE(), @Tipo, @Monto, @Motivo)";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@IdUsuario", mov.IdUsuario.HasValue && mov.IdUsuario.Value > 0 ? mov.IdUsuario.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Tipo", mov.Tipo);
            cmd.Parameters.AddWithValue("@Monto", mov.Monto);
            cmd.Parameters.AddWithValue("@Motivo", mov.Motivo);

            return (int)cmd.ExecuteScalar();
        }

        public int RegistrarCierreCaja(CierreCaja cierre)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Guardar registro en CierresCaja
                string queryCierre = @"
                    INSERT INTO CierresCaja 
                    (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, 
                     IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
                    OUTPUT INSERTED.Id
                    VALUES 
                    (@IdUsuario, @FechaApertura, GETDATE(), @FondoInicial, @VentasEfectivo, @VentasQR, @VentasTarjeta, 
                     @IngresosVarios, @GastosRetiros, @TotalEsperado, @TotalDeclarado, @Diferencia, @Observaciones, @Estado)";

                using var cmdCierre = new SqlCommand(queryCierre, connection, transaction);
                cmdCierre.Parameters.AddWithValue("@IdUsuario", cierre.IdUsuario.HasValue && cierre.IdUsuario.Value > 0 ? cierre.IdUsuario.Value : (object)DBNull.Value);
                cmdCierre.Parameters.AddWithValue("@FechaApertura", cierre.FechaApertura);
                cmdCierre.Parameters.AddWithValue("@FondoInicial", cierre.FondoInicial);
                cmdCierre.Parameters.AddWithValue("@VentasEfectivo", cierre.VentasEfectivo);
                cmdCierre.Parameters.AddWithValue("@VentasQR", cierre.VentasQR);
                cmdCierre.Parameters.AddWithValue("@VentasTarjeta", cierre.VentasTarjeta);
                cmdCierre.Parameters.AddWithValue("@IngresosVarios", cierre.IngresosVarios);
                cmdCierre.Parameters.AddWithValue("@GastosRetiros", cierre.GastosRetiros);
                cmdCierre.Parameters.AddWithValue("@TotalEsperado", cierre.TotalEsperado);
                cmdCierre.Parameters.AddWithValue("@TotalDeclarado", cierre.TotalDeclarado);
                cmdCierre.Parameters.AddWithValue("@Diferencia", cierre.Diferencia);
                cmdCierre.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(cierre.Observaciones) ? (object)DBNull.Value : cierre.Observaciones);
                cmdCierre.Parameters.AddWithValue("@Estado", cierre.Estado);

                int idCierre = (int)cmdCierre.ExecuteScalar();
                cierre.Id = idCierre;

                // 2. Asociar todas las ventas abiertas al nuevo cierre
                string queryUpdateVentas = "UPDATE Ventas SET IdCierreCaja = @IdCierre WHERE IdCierreCaja IS NULL";
                using var cmdUpdateVentas = new SqlCommand(queryUpdateVentas, connection, transaction);
                cmdUpdateVentas.Parameters.AddWithValue("@IdCierre", idCierre);
                cmdUpdateVentas.ExecuteNonQuery();

                // 3. Asociar todos los movimientos abiertos al nuevo cierre
                string queryUpdateMovs = "UPDATE MovimientosCaja SET IdCierreCaja = @IdCierre WHERE IdCierreCaja IS NULL";
                using var cmdUpdateMovs = new SqlCommand(queryUpdateMovs, connection, transaction);
                cmdUpdateMovs.Parameters.AddWithValue("@IdCierre", idCierre);
                cmdUpdateMovs.ExecuteNonQuery();

                transaction.Commit();
                return idCierre;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public List<CierreCaja> ObtenerHistorialCierres()
        {
            var lista = new List<CierreCaja>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                SELECT c.*, ISNULL(u.NombreUsuario, 'Desconocido') AS NombreUsuario
                FROM CierresCaja c
                LEFT JOIN Usuarios u ON c.IdUsuario = u.Id
                ORDER BY c.FechaCierre DESC";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new CierreCaja
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? null : Convert.ToInt32(reader["IdUsuario"]),
                    NombreUsuario = reader["NombreUsuario"].ToString(),
                    FechaApertura = Convert.ToDateTime(reader["FechaApertura"]),
                    FechaCierre = Convert.ToDateTime(reader["FechaCierre"]),
                    FondoInicial = Convert.ToDecimal(reader["FondoInicial"]),
                    VentasEfectivo = Convert.ToDecimal(reader["VentasEfectivo"]),
                    VentasQR = Convert.ToDecimal(reader["VentasQR"]),
                    VentasTarjeta = Convert.ToDecimal(reader["VentasTarjeta"]),
                    IngresosVarios = Convert.ToDecimal(reader["IngresosVarios"]),
                    GastosRetiros = Convert.ToDecimal(reader["GastosRetiros"]),
                    TotalEsperado = Convert.ToDecimal(reader["TotalEsperado"]),
                    TotalDeclarado = Convert.ToDecimal(reader["TotalDeclarado"]),
                    Diferencia = Convert.ToDecimal(reader["Diferencia"]),
                    Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader["Observaciones"].ToString(),
                    Estado = reader["Estado"].ToString() ?? "Cerrado"
                });
            }

            return lista;
        }
    }
}
