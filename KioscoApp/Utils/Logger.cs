using System;
using System.IO;
using System.Text;

namespace KioscoApp.Utils
{
    public static class Logger
    {
        private static readonly object _syncLock = new object();
        private static readonly string _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string _logFilePath = Path.Combine(_logDirectory, "kiosco_system.log");

        public static string LogFilePath => _logFilePath;

        static Logger()
        {
            try
            {
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
            }
            catch { }
        }

        public static void LogInfo(string modulo, string mensaje)
        {
            Escribir("INFO", modulo, mensaje);
        }

        public static void LogWarning(string modulo, string mensaje)
        {
            Escribir("WARN", modulo, mensaje);
        }

        public static void LogError(string modulo, string mensaje, Exception? ex = null)
        {
            string detalle = mensaje;
            if (ex != null)
            {
                detalle += $" | Excepción: {ex.GetType().Name}: {ex.Message}\nStackTrace:\n{ex.StackTrace}";
            }
            Escribir("ERROR", modulo, detalle);
        }

        private static void Escribir(string nivel, string modulo, string texto)
        {
            try
            {
                lock (_syncLock)
                {
                    if (!Directory.Exists(_logDirectory))
                    {
                        Directory.CreateDirectory(_logDirectory);
                    }

                    string usuario = string.IsNullOrWhiteSpace(UserSession.Usuario) ? "Sistema" : UserSession.Usuario;
                    string linea = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{nivel}] [{modulo}] [{usuario}] {texto}";

                    File.AppendAllText(_logFilePath, linea + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch
            {
                // No propagar errores de logging para no afectar transacciones
            }
        }

        public static string LeerLogs(int maxLineas = 500)
        {
            try
            {
                lock (_syncLock)
                {
                    if (!File.Exists(_logFilePath)) return "No hay registros de log generados aún.";

                    var lineas = File.ReadAllLines(_logFilePath, Encoding.UTF8);
                    if (lineas.Length <= maxLineas)
                    {
                        return string.Join(Environment.NewLine, lineas);
                    }

                    var ultimas = new string[maxLineas];
                    Array.Copy(lineas, lineas.Length - maxLineas, ultimas, 0, maxLineas);
                    return string.Join(Environment.NewLine, ultimas);
                }
            }
            catch (Exception ex)
            {
                return $"Error al leer el archivo de log: {ex.Message}";
            }
        }

        public static void LimpiarLogs()
        {
            try
            {
                lock (_syncLock)
                {
                    if (File.Exists(_logFilePath))
                    {
                        File.WriteAllText(_logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] [Sistema] Archivo de logs reiniciado por el Administrador.{Environment.NewLine}", Encoding.UTF8);
                    }
                }
            }
            catch { }
        }
    }
}
