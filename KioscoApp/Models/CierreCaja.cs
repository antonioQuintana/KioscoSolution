using System;

namespace KioscoApp.Models
{
    public class CierreCaja
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime FechaApertura { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal FondoInicial { get; set; }
        public decimal VentasEfectivo { get; set; }
        public decimal VentasQR { get; set; }
        public decimal VentasTarjeta { get; set; }
        public decimal VentasTotales => VentasEfectivo + VentasQR + VentasTarjeta;
        public decimal IngresosVarios { get; set; }
        public decimal GastosRetiros { get; set; }
        public decimal TotalEsperado { get; set; }
        public decimal TotalDeclarado { get; set; }
        public decimal Diferencia { get; set; }
        public string? Observaciones { get; set; }
        public string Estado { get; set; } = "Cerrado";
    }
}
