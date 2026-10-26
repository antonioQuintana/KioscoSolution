using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class VentaRepository
    {
        public int InsertarVenta(Venta venta)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Insertar la Venta principal
                using var cmdVenta = new SqlCommand("sp_Ventas_InsertarCabecera", connection, transaction);
                cmdVenta.CommandType = CommandType.StoredProcedure;
                cmdVenta.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario ?? (object)DBNull.Value);
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                cmdVenta.Parameters.AddWithValue("@Descuento", venta.Descuento);
                cmdVenta.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
                cmdVenta.Parameters.AddWithValue("@Estado", venta.Estado);

                int idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                venta.Id = idVenta;

                // 2. Insertar los detalles y descontar stock
                foreach (var detalle in venta.Detalles)
                {
                    using var cmdDetalle = new SqlCommand("sp_Ventas_InsertarDetalleYDescontarStock", connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                    cmdDetalle.ExecuteNonQuery();
                }

                transaction.Commit();
                return idVenta;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al registrar la venta en la base de datos.", ex);
            }
        }

        public List<Venta> ObtenerTodas()
        {
            var ventas = new List<Venta>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Ventas_ObtenerTodas", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ventas.Add(new Venta
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    IdCliente = Convert.ToInt32(reader["IdCliente"]),
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Total = Convert.ToDecimal(reader["Total"]),
                    Descuento = Convert.ToDecimal(reader["Descuento"]),
                    MetodoPago = reader["MetodoPago"].ToString(),
                    Estado = reader["Estado"].ToString(),
                    NombreCliente = $"{reader["DniCliente"]} - {reader["NombreCliente"]} {reader["ApellidoCliente"]}"
                });
            }
            return ventas;
        }
    }
}
