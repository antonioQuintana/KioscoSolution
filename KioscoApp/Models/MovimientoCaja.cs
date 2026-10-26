using System;

namespace KioscoApp.Models
{
    public class MovimientoCaja
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Tipo { get; set; } = "Ingreso"; // "Ingreso" o "Retiro"
        public decimal Monto { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public int? IdCierreCaja { get; set; }
    }
}
