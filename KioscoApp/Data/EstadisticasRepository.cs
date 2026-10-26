using System;
using System.Collections.Generic;
using System.Data;
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

            using var cmd = new SqlCommand("sp_Estadisticas_ListarMasVendidos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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

            using var cmd = new SqlCommand("sp_Estadisticas_ListarVentasPorDias", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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

            using var cmd = new SqlCommand("sp_Estadisticas_ListarVentasPorHora", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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

            using var cmd = new SqlCommand("sp_Estadisticas_ListarVentasPorCategoria", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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

            using var cmd = new SqlCommand("sp_Estadisticas_ListarMetodosMasUsados", connection);
            cmd.CommandType = CommandType.StoredProcedure;
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
