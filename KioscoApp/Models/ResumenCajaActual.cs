using System;

namespace KioscoApp.Models
{
    public class ResumenCajaActual
    {
        public decimal FondoInicial { get; set; } = 0m;
        public decimal VentasEfectivo { get; set; }
        public decimal VentasQR { get; set; }
        public decimal VentasTarjeta { get; set; }
        public decimal TotalVentas => VentasEfectivo + VentasQR + VentasTarjeta;
        public decimal IngresosVarios { get; set; }
        public decimal GastosRetiros { get; set; }
        public decimal EfectivoEsperado => FondoInicial + VentasEfectivo + IngresosVarios - GastosRetiros;
        public int CantidadVentas { get; set; }
        public DateTime? FechaPrimeraVenta { get; set; }
    }
}
