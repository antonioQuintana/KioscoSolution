using System;
using KioscoApp.Business;

namespace KioscoApp
{
    /// <summary>
    /// Gestiona el estado de la sesión del usuario autenticado en la aplicación.
    /// Accesible globalmente en todas las capas del sistema.
    /// </summary>
    public static class UserSession
    {
        public static int IdUsuario { get; private set; }
        public static string Nombre { get; private set; } = string.Empty;
        public static string Usuario { get; private set; } = string.Empty;
        public static string Rol { get; private set; } = string.Empty;

        public static void IniciarSesion(int id, string nombre, string usuario, string rol)
        {
            IdUsuario = id;
            Nombre = nombre ?? string.Empty;
            Usuario = usuario ?? string.Empty;
            Rol = rol ?? string.Empty;
        }

        public static void CerrarSesion()
        {
            if (IdUsuario > 0)
            {
                AuditoriaService.Registrar("Seguridad", "Cierre de Sesión", $"Usuario: {Usuario}");
            }

            IdUsuario = 0;
            Nombre = string.Empty;
            Usuario = string.Empty;
            Rol = string.Empty;
        }
    }
}
