using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class ProductoRepository
    {
        public List<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria, 
                                    c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta, 
                                    p.StockActual, p.StockMinimo
                             FROM Productos p
                             INNER JOIN Categorias c ON p.IdCategoria = c.Id
                             ORDER BY p.Nombre";
            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                productos.Add(MapProducto(reader));
            }
            return productos;
        }

        public Producto? ObtenerPorId(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria, 
                                    c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta, 
                                    p.StockActual, p.StockMinimo
                             FROM Productos p
                             INNER JOIN Categorias c ON p.IdCategoria = c.Id
                             WHERE p.Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapProducto(reader);
            }
            return null;
        }

        public bool ExisteSKU(string sku, int idExcluir)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT COUNT(1) FROM Productos WHERE LOWER(SKU) = LOWER(@SKU) AND Id != @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SKU", sku.Trim());
            cmd.Parameters.AddWithValue("@Id", idExcluir);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        public void Insertar(Producto p)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"INSERT INTO Productos 
                (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo) 
                VALUES 
                (@SKU, @Nombre, @Descripcion, @IdCategoria, @PrecioCosto, @PrecioVenta, @StockActual, @StockMinimo)";
                
            using var cmd = new SqlCommand(query, connection);
            SetCommandParameters(cmd, p);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Producto p)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = @"UPDATE Productos SET 
                SKU = @SKU, Nombre = @Nombre, Descripcion = @Descripcion, 
                IdCategoria = @IdCategoria, PrecioCosto = @PrecioCosto, PrecioVenta = @PrecioVenta, 
                StockActual = @StockActual, StockMinimo = @StockMinimo 
                WHERE Id = @Id";
                
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            SetCommandParameters(cmd, p);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "DELETE FROM Productos WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private void SetCommandParameters(SqlCommand cmd, Producto p)
        {
            cmd.Parameters.AddWithValue("@SKU", p.SKU.Trim());
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre.Trim());
            cmd.Parameters.AddWithValue("@Descripcion", (object?)p.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IdCategoria", p.IdCategoria);
            cmd.Parameters.AddWithValue("@PrecioCosto", p.PrecioCosto);
            cmd.Parameters.AddWithValue("@PrecioVenta", p.PrecioVenta);
            cmd.Parameters.AddWithValue("@StockActual", p.StockActual);
            cmd.Parameters.AddWithValue("@StockMinimo", p.StockMinimo);
        }

        private Producto MapProducto(SqlDataReader reader)
        {
            return new Producto
            {
                Id = Convert.ToInt32(reader["Id"]),
                SKU = reader["SKU"].ToString() ?? string.Empty,
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null,
                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                CategoriaNombre = reader["CategoriaNombre"].ToString() ?? string.Empty,
                PrecioCosto = Convert.ToDecimal(reader["PrecioCosto"]),
                PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                StockActual = Convert.ToInt32(reader["StockActual"]),
                StockMinimo = Convert.ToInt32(reader["StockMinimo"])
            };
        }
    }
}
