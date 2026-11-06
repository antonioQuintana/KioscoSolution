using System;
using System.Collections.Generic;
using KioscoApp.Data;
using KioscoApp.Models;
using KioscoApp.Utils;

namespace KioscoApp.Business
{
    public class AuditoriaService
    {
        private static readonly AuditoriaRepository _repo = new AuditoriaRepository();

        public static void Registrar(string modulo, string accion, string detalle = "")
        {
            try
            {
                var r = new RegistroAuditoria
                {
                    Fecha = DateTime.Now,
                    IdUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null,
                    Usuario = string.IsNullOrWhiteSpace(UserSession.Usuario) ? "Sistema" : UserSession.Usuario,
                    Rol = string.IsNullOrWhiteSpace(UserSession.Rol) ? "Sistema" : UserSession.Rol,
                    Modulo = modulo,
                    Accion = accion,
                    Detalle = detalle,
                    IpEquipo = Environment.MachineName
                };

                _repo.Registrar(r);
                Logger.LogInfo(modulo, $"{accion} - {detalle}");
            }
            catch (Exception ex)
            {
                Logger.LogError("Auditoria", "Error al registrar evento de auditoría", ex);
            }
        }

        public List<RegistroAuditoria> Obtener(DateTime? desde = null, DateTime? hasta = null, string? modulo = null, string? filtro = null)
        {
            return _repo.Obtener(desde, hasta, modulo, filtro);
        }
    }
}
