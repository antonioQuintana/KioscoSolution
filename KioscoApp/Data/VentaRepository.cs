using System;
using System.Collections.Generic;
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
                string queryVenta = @"INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado) 
                                      OUTPUT INSERTED.Id 
                                      VALUES (@IdCliente, @IdUsuario, GETDATE(), @Total, @Descuento, @MetodoPago, @Estado)";
                using var cmdVenta = new SqlCommand(queryVenta, connection, transaction);
                cmdVenta.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario ?? (object)DBNull.Value);
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                cmdVenta.Parameters.AddWithValue("@Descuento", venta.Descuento);
                cmdVenta.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
                cmdVenta.Parameters.AddWithValue("@Estado", venta.Estado);

                int idVenta = (int)cmdVenta.ExecuteScalar();
                venta.Id = idVenta;

                // 2. Insertar los detalles
                foreach (var detalle in venta.Detalles)
                {
                    string queryDetalle = @"INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) 
                                            VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)";
                    using var cmdDetalle = new SqlCommand(queryDetalle, connection, transaction);
                    cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                    cmdDetalle.ExecuteNonQuery();

                    // 3. Descontar stock del producto
                    string queryStock = "UPDATE Productos SET StockActual = StockActual - @Cantidad WHERE Id = @IdProducto";
                    using var cmdStock = new SqlCommand(queryStock, connection, transaction);
                    cmdStock.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdStock.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmdStock.ExecuteNonQuery();
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
    }
}
