using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KioscoApp.Models;
using Microsoft.Data.SqlClient;

namespace KioscoApp.Data
{
    public class EstadisticasRepository
    {
        /// ///////////////////////////////////////////////////////////////////////
        /// Clases para graficar los productos más vendidos
        public class ProductoMasVendido
        {
            public string NombreCategoria { get; set; }
            public string NombreProducto { get; set; }
            public int CantidadTotalVendida { get; set; }
        }

        public static List<ProductoMasVendido> ListarMasVendidos()
        {
            var productos = new List<ProductoMasVendido>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            const string query = @" -- Lista el producto más vendido por categoría además de su cantidad vendida--
                                    SELECT 
                                        c.nombre AS nombre_categoria,
                                        p.nombre AS nombre_producto,
                                        SUM(dv.cantidad) AS cantidad_total_vendida
                                    FROM 
                                        categorias c
                                        INNER JOIN 
                                        productos p ON c.id = p.idcategoria
                                        INNER JOIN 
                                        detallesventa dv ON p.id = dv.idproducto
                                    GROUP BY 
                                        c.id,
                                        c.nombre,
                                        p.id,
                                        p.nombre
                                    HAVING 
                                        SUM(dv.cantidad) = (
                                        SELECT TOP 1 SUM(dv_sub.cantidad)
                                        FROM productos p_sub
                                            INNER JOIN detallesventa dv_sub ON p_sub.id = dv_sub.idproducto
                                        WHERE p_sub.idcategoria = c.id
                                        GROUP BY p_sub.id
                                        ORDER BY SUM(dv_sub.cantidad) DESC
                                        );";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new ProductoMasVendido
                {
                    NombreCategoria = reader["nombre_categoria"].ToString(),
                    NombreProducto = reader["nombre_producto"].ToString(),
                    CantidadTotalVendida = Convert.ToInt32(reader["cantidad_total_vendida"])
                };

                productos.Add(item);
            }
            return productos;
        }

        /// ///////////////////////////////////////////////////////////////////////
        /// Clases para graficar las ventas por Dias
        public class DiaMasVendido
        {
            public string Fecha { get; set; }
            public string NombreDia { get; set; }

            public int CantidadVentas { get; set; }

            public float TotalVendido { get; set; }
        }

        public static List<DiaMasVendido> ListarVentasPorDias()
        {
            var ventas_dias = new List<DiaMasVendido>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            const string query = @" -- obtiene los la cantidad de ventas y su total de la última semana --
                                    
                                    SELECT 
                                        CAST(Fecha AS DATE) AS Dia,
                                        DATENAME(WEEKDAY, Fecha) AS NombreDia,
                                        COUNT(Id) AS CantidadVentas,
                                        SUM(Total) AS TotalIngresos
                                    FROM Ventas
                                    WHERE Estado = 'Completada'
                                      AND Fecha >= DATEADD(DAY, -7, CAST(GETDATE() AS DATE))
                                    GROUP BY 
                                        CAST(Fecha AS DATE), 
                                        DATENAME(WEEKDAY, Fecha)
                                    ORDER BY 
                                        Dia ASC;";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new DiaMasVendido
                {
                    Fecha = reader["Dia"].ToString(),
                    NombreDia = reader["NombreDia"].ToString(),
                    CantidadVentas = Convert.ToInt32(reader["CantidadVentas"]),
                    TotalVendido = Convert.ToSingle(reader["TotalIngresos"])
                };

                ventas_dias.Add(item);
            }
            return ventas_dias;
        }

        /// ///////////////////////////////////////////////////////////////////////
        /// Clases para graficar las ventas por horarios
        public class HorarioMasVendido
        {
            public string Hora { get; set; }
            public int CantidadVentas { get; set; }
            public float TotalVendido { get; set; }
        }

        public static List<HorarioMasVendido> ListarVentasPorHora()
        {
            var horarios = new List<HorarioMasVendido>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            const string query = @" -- obtiene las ventas por horarios --
                                    WITH FranjasHorarias AS (
                                        SELECT 1 AS Orden, '00:00 - 06:00 hs' AS FranjaHoraria, 0 AS HoraInicio, 5 AS HoraFin
                                        UNION ALL
                                        SELECT 2, '06:00 - 12:00 hs', 6, 11
                                        UNION ALL
                                        SELECT 3, '12:00 - 18:00 hs', 12, 17
                                        UNION ALL
                                        SELECT 4, '18:00 - 00:00 hs', 18, 23
                                    )
                                    SELECT 
                                        f.FranjaHoraria,
                                        COUNT(v.Id) AS CantidadVentas,
                                        ISNULL(SUM(v.Total), 0) AS TotalFacturado
                                    FROM FranjasHorarias f
                                    LEFT JOIN Ventas v 
                                        ON DATEPART(HOUR, v.Fecha) BETWEEN f.HoraInicio AND f.HoraFin
                                        AND v.Estado = 'Completada'
                                    GROUP BY 
                                        f.Orden, 
                                        f.FranjaHoraria
                                    ORDER BY 
                                        f.Orden ASC;";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new HorarioMasVendido
                {
                    Hora = reader["FranjaHoraria"].ToString(),
                    CantidadVentas = Convert.ToInt32(reader["CantidadVentas"]),
                    TotalVendido = Convert.ToSingle(reader["TotalFacturado"])
                };

                horarios.Add(item);
            }
            return horarios;
        }

        /// ///////////////////////////////////////////////////////////////////////
        /// Clases para graficar los ingresos por categoría
        public class CategoriaMasVendida
        {
            public string Categoria { get; set; }
            public int TotalVentas { get; set; }

            public int UnidadesVendidads { get; set; }
            public float TotalFacturado { get; set; }
        }

        public static List<CategoriaMasVendida> ListarVentasPorCategoria()
        {
            var horarios = new List<CategoriaMasVendida>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            const string query = @" -- obtiene las ventas por categorías --
                                    SELECT 
                                        c.Nombre AS Categoria,
                                        COUNT(DISTINCT v.Id) AS TotalVentas,              -- Cantidad de tickets/operaciones distintas
                                        ISNULL(SUM(dv.Cantidad), 0) AS UnidadesVendidas,  -- Cantidad total de productos vendidos
                                        ISNULL(SUM(dv.Subtotal), 0) AS TotalFacturado     -- Total recaudado por la categoría
                                    FROM Categorias c
                                    LEFT JOIN Productos p 
                                        ON p.IdCategoria = c.Id
                                    LEFT JOIN DetallesVenta dv 
                                        ON dv.IdProducto = p.Id
                                    LEFT JOIN Ventas v 
                                        ON v.Id = dv.IdVenta 
                                        AND v.Estado = 'Completada'
                                    GROUP BY 
                                        c.Id, 
                                        c.Nombre
                                    ORDER BY 
                                        TotalFacturado DESC;";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new CategoriaMasVendida
                {
                    Categoria = reader["Categoria"].ToString(),
                    TotalVentas = Convert.ToInt32(reader["TotalVentas"]),
                    UnidadesVendidads = Convert.ToInt32(reader["UnidadesVendidas"]),
                    TotalFacturado = Convert.ToSingle(reader["TotalFacturado"])
                };

                horarios.Add(item);
            }
            return horarios;
        }

        /// ///////////////////////////////////////////////////////////////////////
        /// Clases para graficar los ingresos por categoría
        public class MetodoPagoMasUsado
        {
            public string Nombre { get; set; }
            public int CantidadVentas { get; set; }
            public int UnidadesVendidas { get; set; }
            public float TotalFacturado { get; set; }
            public float TicketPromedio { get; set; }
        }

        public static List<MetodoPagoMasUsado> listarMetodosMasUsados()
        {
            var pagos = new List<MetodoPagoMasUsado>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            const string query = @" -- obtiene las ventas por categorías --
                                    WITH ArticulosPorVenta AS (
                                        SELECT 
                                            IdVenta, 
                                            SUM(Cantidad) AS CantidadArticulos
                                        FROM DetallesVenta
                                        GROUP BY IdVenta
                                    )
                                    SELECT 
                                        LTRIM(RTRIM(v.MetodoPago)) AS MetodoPago,
                                        COUNT(v.Id) AS CantidadVentas,                         -- Total de tickets / transacciones
                                        ISNULL(SUM(apv.CantidadArticulos), 0) AS UnidadesVendidas, -- Total de artículos físicos despachados
                                        SUM(v.Total) AS TotalFacturado,                        -- Dinero total ingresado por ese medio
                                        ROUND(AVG(v.Total), 2) AS TicketPromedio               -- Promedio de gasto por operación
                                    FROM Ventas v
                                    LEFT JOIN ArticulosPorVenta apv 
                                        ON v.Id = apv.IdVenta
                                    WHERE v.Estado = 'Completada'
                                    GROUP BY 
                                        LTRIM(RTRIM(v.MetodoPago))
                                    ORDER BY 
                                        TotalFacturado DESC;";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var item = new MetodoPagoMasUsado
                {
                    Nombre = reader["MetodoPago"].ToString(),
                    CantidadVentas = Convert.ToInt32(reader["CantidadVentas"]),
                    UnidadesVendidas = Convert.ToInt32(reader["UnidadesVendidas"]),
                    TotalFacturado = Convert.ToSingle(reader["TotalFacturado"]),
                    TicketPromedio = Convert.ToSingle(reader["TicketPromedio"])
                };

                pagos.Add(item);
            }
            return pagos;

        }
    }
}