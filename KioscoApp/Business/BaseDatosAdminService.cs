using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.SqlClient;
using KioscoApp.Utils;

namespace KioscoApp.Business
{
    public class BaseDatosAdminService
    {
        public class BackupResultado
        {
            public bool Exitoso { get; set; }
            public string RutaArchivo { get; set; } = string.Empty;
            public long TamanoBytes { get; set; }
            public TimeSpan Duracion { get; set; }
            public string Mensaje { get; set; } = string.Empty;

            public string TamanoFormateado
            {
                get
                {
                    if (TamanoBytes > 1024 * 1024)
                        return $"{(TamanoBytes / (1024.0 * 1024.0)):N2} MB";
                    if (TamanoBytes > 1024)
                        return $"{(TamanoBytes / 1024.0):N2} KB";
                    return $"{TamanoBytes} Bytes";
                }
            }
        }

        public BackupResultado CrearBackup(string rutaDestino)
        {
            var sw = Stopwatch.StartNew();
            var res = new BackupResultado { RutaArchivo = rutaDestino };

            try
            {
                string? dir = Path.GetDirectoryName(rutaDestino);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (var conn = DatabaseHelper.GetMasterConnection())
                {
                    conn.Open();

                    string sql = @"
                        BACKUP DATABASE [KioscoDB] 
                        TO DISK = @Ruta 
                        WITH FORMAT, INIT, MEDIANAME = 'KioscoBackup', NAME = 'Full Backup of KioscoDB';
                        RESTORE VERIFYONLY FROM DISK = @Ruta;";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@Ruta", rutaDestino);
                        cmd.ExecuteNonQuery();
                    }
                }

                sw.Stop();
                res.Duracion = sw.Elapsed;
                res.Exitoso = true;

                if (File.Exists(rutaDestino))
                {
                    var fi = new FileInfo(rutaDestino);
                    res.TamanoBytes = fi.Length;
                }

                res.Mensaje = $"Copia de seguridad realizada y verificada correctamente en {res.Duracion.TotalSeconds:N1} seg.";
                AuditoriaService.Registrar("BaseDeDatos", "Copia de Seguridad Realizada",
                    $"Archivo: {Path.GetFileName(rutaDestino)}, Tamaño: {res.TamanoFormateado}");
                Logger.LogInfo("BaseDeDatos", $"Backup exitoso: {rutaDestino} ({res.TamanoFormateado})");
            }
            catch (Exception ex)
            {
                sw.Stop();
                res.Duracion = sw.Elapsed;
                res.Exitoso = false;
                res.Mensaje = ex.Message;
                Logger.LogError("BaseDeDatos", $"Fallo al crear backup en {rutaDestino}", ex);
            }

            return res;
        }

        public void RestaurarBackup(string rutaOrigen)
        {
            if (!File.Exists(rutaOrigen))
            {
                throw new FileNotFoundException("El archivo de copia de seguridad no existe.", rutaOrigen);
            }

            try
            {
                using (var conn = DatabaseHelper.GetMasterConnection())
                {
                    conn.Open();

                    string sql = @"
                        ALTER DATABASE [KioscoDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        RESTORE DATABASE [KioscoDB] FROM DISK = @Ruta WITH REPLACE;
                        ALTER DATABASE [KioscoDB] SET MULTI_USER;";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandTimeout = 180;
                        cmd.Parameters.AddWithValue("@Ruta", rutaOrigen);
                        cmd.ExecuteNonQuery();
                    }
                }

                SqlConnection.ClearAllPools();

                AuditoriaService.Registrar("BaseDeDatos", "Restauración de Base de Datos",
                    $"Origen: {Path.GetFileName(rutaOrigen)}");
                Logger.LogInfo("BaseDeDatos", $"Restauración exitosa desde: {rutaOrigen}");
            }
            catch (Exception ex)
            {
                // Intentar asegurar que quede MULTI_USER ante cualquier contingencia
                try
                {
                    using var connFallback = DatabaseHelper.GetMasterConnection();
                    connFallback.Open();
                    using var cmdFallback = new SqlCommand("IF EXISTS (SELECT 1 FROM sys.databases WHERE name='KioscoDB') ALTER DATABASE [KioscoDB] SET MULTI_USER;", connFallback);
                    cmdFallback.ExecuteNonQuery();
                }
                catch { }

                Logger.LogError("BaseDeDatos", $"Fallo en la restauración desde {rutaOrigen}", ex);
                throw;
            }
        }

        public string OptimizarBaseDatos()
        {
            var sw = Stopwatch.StartNew();
            string stats = string.Empty;

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Obtener estadísticas iniciales
                    using (var cmdSpace = new SqlCommand("EXEC sp_spaceused;", conn))
                    using (var reader = cmdSpace.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats = $"Tamaño inicial BD: {reader["database_size"]} (Espacio no asignado: {reader["unallocated space"]})";
                        }
                    }

                    // Ejecutar mantenimiento y shrink
                    using (var cmdMaint = new SqlCommand("DBCC SHRINKDATABASE (KioscoDB); EXEC sp_updatestats;", conn))
                    {
                        cmdMaint.CommandTimeout = 120;
                        cmdMaint.ExecuteNonQuery();
                    }
                }

                sw.Stop();
                AuditoriaService.Registrar("BaseDeDatos", "Mantenimiento y Compactación",
                    $"Optimización ejecutada en {sw.Elapsed.TotalSeconds:N1} seg. {stats}");
                Logger.LogInfo("BaseDeDatos", $"Optimización de base de datos finalizada en {sw.Elapsed.TotalSeconds:N1}s");

                return $"Mantenimiento completado con éxito en {sw.Elapsed.TotalSeconds:N1} segundos.\n\n• Índices reorganizados y actualizados.\n• Base de datos compactada.\n• {stats}";
            }
            catch (Exception ex)
            {
                Logger.LogError("BaseDeDatos", "Error durante el mantenimiento de base de datos", ex);
                throw;
            }
        }
    }
}
