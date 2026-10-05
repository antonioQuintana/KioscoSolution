using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class ProductoRepository
    {
        public Producto ObtenerPorSKU(string sku)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT Id, SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo FROM Productos WHERE SKU = @SKU";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SKU", sku);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Producto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    SKU = reader["SKU"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null,
                    IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                    PrecioCosto = Convert.ToDecimal(reader["PrecioCosto"]),
                    PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                    StockActual = Convert.ToInt32(reader["StockActual"]),
                    StockMinimo = Convert.ToInt32(reader["StockMinimo"])
                };
            }
            return null;
        }

        public List<Producto> BuscarPorNombreOSKU(string term)
        {
            var productos = new List<Producto>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            string query = "SELECT TOP 10 Id, SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo FROM Productos WHERE Nombre LIKE @Term OR SKU LIKE @Term";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Term", "%" + term + "%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(new Producto
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    SKU = reader["SKU"].ToString(),
                    Nombre = reader["Nombre"].ToString(),
                    Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null,
                    IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                    PrecioCosto = Convert.ToDecimal(reader["PrecioCosto"]),
                    PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                    StockActual = Convert.ToInt32(reader["StockActual"]),
                    StockMinimo = Convert.ToInt32(reader["StockMinimo"])
                });
            }
            return productos;
        }
    }
}
