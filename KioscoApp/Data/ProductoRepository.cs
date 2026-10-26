using System;
using System.Collections.Generic;
using System.Data;
using KioscoApp.Models;
using Microsoft.Data.SqlClient;

namespace KioscoApp.Data
{
    public class ProductoRepository
    {
        public List<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_ObtenerTodos", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(MapProducto(reader));
            }

            return productos;
        }

        public Producto? ObtenerPorSKU(string sku)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_ObtenerPorSKU", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SKU", sku.Trim());
            using var reader = cmd.ExecuteReader();

            return reader.Read() ? MapProducto(reader) : null;
        }

        public Producto? ObtenerPorId(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_ObtenerPorId", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = cmd.ExecuteReader();

            return reader.Read() ? MapProducto(reader) : null;
        }

        public bool ExisteSKU(string sku, int idExcluir)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_ExisteSKU", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SKU", sku.Trim());
            cmd.Parameters.AddWithValue("@Id", idExcluir);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void Insertar(Producto p)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_Insertar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SetCommandParameters(cmd, p);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Producto p)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_Actualizar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", p.Id);
            SetCommandParameters(cmd, p);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_Eliminar", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Producto> BuscarPorNombreOSKU(string term)
        {
            var productos = new List<Producto>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_BuscarPorNombreOSKU", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", "%" + term.Trim() + "%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productos.Add(MapProducto(reader));
            }

            return productos;
        }

        public DataTable FiltrarStock(string filtro = "")
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_FiltrarStock", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public void ActualizarStock(int id, int actual, int minimo)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();

            using var cmd = new SqlCommand("sp_Productos_ActualizarStock", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Actual", actual);
            cmd.Parameters.AddWithValue("@Minimo", minimo);
            cmd.ExecuteNonQuery();
        }

        private static void SetCommandParameters(SqlCommand cmd, Producto p)
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

        private static Producto MapProducto(SqlDataReader reader)
        {
            return new Producto
            {
                Id = Convert.ToInt32(reader["Id"]),
                SKU = reader["SKU"].ToString() ?? string.Empty,
                Nombre = reader["Nombre"].ToString() ?? string.Empty,
                Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null,
                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                CategoriaNombre = reader["CategoriaNombre"] != DBNull.Value ? 
                    reader["CategoriaNombre"].ToString() ?? string.Empty : string.Empty,
                PrecioCosto = Convert.ToDecimal(reader["PrecioCosto"]),
                PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]),
                StockActual = Convert.ToInt32(reader["StockActual"]),
                StockMinimo = Convert.ToInt32(reader["StockMinimo"])
            };
        }
    }
}
