using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class UbicacionRepository
    {
        public List<Provincia> ObtenerProvincias()
        {
            var provincias = new List<Provincia>();
            try
            {
                using var connection = DatabaseHelper.GetConnection();
                connection.Open();
                string query = "SELECT Id, Nombre FROM Provincias ORDER BY Nombre";
                using var cmd = new SqlCommand(query, connection);
                using var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    provincias.Add(new Provincia
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener provincias: " + ex.Message);
            }
            return provincias;
        }

        public List<Ciudad> ObtenerCiudadesPorProvincia(int provinciaId)
        {
            var ciudades = new List<Ciudad>();
            try
            {
                using var connection = DatabaseHelper.GetConnection();
                connection.Open();
                string query = "SELECT Id, ProvinciaId, Nombre FROM Ciudades WHERE ProvinciaId = @ProvinciaId ORDER BY Nombre";
                using var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ProvinciaId", provinciaId);
                
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ciudades.Add(new Ciudad
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProvinciaId = Convert.ToInt32(reader["ProvinciaId"]),
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ciudades: " + ex.Message);
            }
            return ciudades;
        }
    }
}
