using System;

namespace KioscoApp.Models
{
    public class DescuadreEmpleadoItem
    {
        public string Empleado { get; set; } = string.Empty;
        public int TotalCierres { get; set; }
        public decimal TotalFaltantes { get; set; }
        public decimal TotalSobrantes { get; set; }
        public decimal DiferenciaNeta { get; set; }
        public string Estado => DiferenciaNeta < 0 ? "Faltante Neto" : (DiferenciaNeta > 0 ? "Sobrante Neto" : "Balanceado");
    }

    public class VentaPorHoraItem
    {
        public int Hora { get; set; }
        public string FranjaHoraria => $"{Hora:00}:00 - {Hora + 1:00}:00";
        public int CantidadVentas { get; set; }
        public decimal TotalRecaudado { get; set; }
        public decimal TicketPromedio { get; set; }
    }

    public class VentaAnuladaItem
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Cajero { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }

    public class InventarioValorizadoItem
    {
        public string SKU { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal ValorCostoTotal => StockActual * PrecioCosto;
        public decimal ValorVentaTotal => StockActual * PrecioVenta;
        public decimal GananciaPotencial => ValorVentaTotal - ValorCostoTotal;
    }
}
