using System;

namespace KioscoApp.Models
{
    public class ConfiguracionEmpresa
    {
        public int Id { get; set; } = 1;
        public string NombreFantasia { get; set; } = "KIOSCO EXPRESS";
        public string RazonSocial { get; set; } = "Kiosco Express S.A.";
        public string Cuit { get; set; } = "20-12345678-9";
        public string Direccion { get; set; } = "Av. Principal 1234";
        public string Telefono { get; set; } = "(011) 4567-8900";
        public string MensajePieTicket { get; set; } = "¡Gracias por su compra! Conserve su ticket.";
        public string ImpresoraPredeterminada { get; set; } = string.Empty;
        public int AnchoPapelMm { get; set; } = 80;
        public bool ImpresionAutomatica { get; set; } = false;
        public bool CortarPapel { get; set; } = true;
    }
}
