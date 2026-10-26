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

        public List<CierreCaja> ObtenerHistorialCierres(DateTime? desde = null, DateTime? hasta = null, int? idUsuario = null)
        {
            var lista = new List<CierreCaja>();

            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                SELECT c.*, ISNULL(u.Usuario, 'Desconocido') AS NombreUsuario
                FROM CierresCaja c
                LEFT JOIN Usuarios u ON c.IdUsuario = u.Id
                WHERE (@Desde IS NULL OR c.FechaCierre >= @Desde)
                  AND (@Hasta IS NULL OR c.FechaCierre <= @Hasta)
                  AND (@IdUsuario IS NULL OR c.IdUsuario = @IdUsuario)
                ORDER BY c.FechaCierre DESC";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Desde", desde.HasValue ? (object)desde.Value.Date : DBNull.Value);
            cmd.Parameters.AddWithValue("@Hasta", hasta.HasValue ? (object)hasta.Value.Date.AddDays(1).AddTicks(-1) : DBNull.Value);
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario.HasValue && idUsuario.Value > 0 ? (object)idUsuario.Value : DBNull.Value);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new CierreCaja
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? null : Convert.ToInt32(reader["IdUsuario"]),
                    NombreUsuario = reader["NombreUsuario"]?.ToString() ?? "Desconocido",
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
                    Estado = reader["Estado"]?.ToString() ?? "Cerrado"
                });
            }

            return lista;
        }

        public List<DescuadreEmpleadoItem> ObtenerDescuadresPorEmpleado()
        {
            var lista = new List<DescuadreEmpleadoItem>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                SELECT 
                    ISNULL(u.Usuario, 'Desconocido') AS Empleado,
                    COUNT(*) AS TotalCierres,
                    ISNULL(SUM(CASE WHEN c.Diferencia < 0 THEN c.Diferencia ELSE 0 END), 0) AS TotalFaltantes,
                    ISNULL(SUM(CASE WHEN c.Diferencia > 0 THEN c.Diferencia ELSE 0 END), 0) AS TotalSobrantes,
                    ISNULL(SUM(c.Diferencia), 0) AS DiferenciaNeta
                FROM CierresCaja c
                LEFT JOIN Usuarios u ON c.IdUsuario = u.Id
                GROUP BY u.Usuario
                ORDER BY DiferenciaNeta ASC";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new DescuadreEmpleadoItem
                {
                    Empleado = reader["Empleado"]?.ToString() ?? "Desconocido",
                    TotalCierres = Convert.ToInt32(reader["TotalCierres"]),
                    TotalFaltantes = Convert.ToDecimal(reader["TotalFaltantes"]),
                    TotalSobrantes = Convert.ToDecimal(reader["TotalSobrantes"]),
                    DiferenciaNeta = Convert.ToDecimal(reader["DiferenciaNeta"])
                });
            }
            return lista;
        }

        public List<VentaPorHoraItem> ObtenerVentasPorHora()
        {
            var lista = new List<VentaPorHoraItem>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                SELECT 
                    DATEPART(HOUR, Fecha) AS Hora,
                    COUNT(*) AS CantidadVentas,
                    ISNULL(SUM(Total), 0) AS TotalRecaudado,
                    ISNULL(AVG(Total), 0) AS TicketPromedio
                FROM Ventas
                WHERE Estado <> 'Anulada'
                GROUP BY DATEPART(HOUR, Fecha)
                ORDER BY Hora ASC";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new VentaPorHoraItem
                {
                    Hora = Convert.ToInt32(reader["Hora"]),
                    CantidadVentas = Convert.ToInt32(reader["CantidadVentas"]),
                    TotalRecaudado = Convert.ToDecimal(reader["TotalRecaudado"]),
                    TicketPromedio = Convert.ToDecimal(reader["TicketPromedio"])
                });
            }
            return lista;
        }

        public List<VentaAnuladaItem> ObtenerVentasAnuladas()
        {
            var lista = new List<VentaAnuladaItem>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                SELECT v.Id, v.Fecha, ISNULL(u.Usuario, 'Desconocido') AS Cajero, v.Total, v.MetodoPago, v.Estado
                FROM Ventas v
                LEFT JOIN Usuarios u ON v.IdUsuario = u.Id
                WHERE v.Estado = 'Anulada' OR v.Estado = 'Cancelada'
                ORDER BY v.Fecha DESC";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new VentaAnuladaItem
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Cajero = reader["Cajero"]?.ToString() ?? "Desconocido",
                    Total = Convert.ToDecimal(reader["Total"]),
                    MetodoPago = reader["MetodoPago"]?.ToString() ?? "",
                    Estado = reader["Estado"]?.ToString() ?? "Anulada"
                });
            }
            return lista;
        }

        public List<InventarioValorizadoItem> ObtenerInventarioValorizado()
        {
            var lista = new List<InventarioValorizadoItem>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            string query = @"
                SELECT 
                    p.SKU,
                    p.Nombre,
                    ISNULL(c.Nombre, 'Sin categoría') AS Categoria,
                    p.StockActual,
                    p.PrecioCosto,
                    p.PrecioVenta
                FROM Productos p
                LEFT JOIN Categorias c ON p.IdCategoria = c.Id
                                ORDER BY (p.StockActual * p.PrecioVenta) DESC";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new InventarioValorizadoItem
                {
                    SKU = reader["SKU"]?.ToString() ?? "",
                    Nombre = reader["Nombre"]?.ToString() ?? "",
                    Categoria = reader["Categoria"]?.ToString() ?? "",
                    StockActual = Convert.ToInt32(reader["StockActual"]),
                    PrecioCosto = Convert.ToDecimal(reader["PrecioCosto"]),
                    PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"])
                });
            }
            return lista;
        }
    }
}
