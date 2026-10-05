using System;
using System.Collections.Generic;

namespace KioscoApp.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Descuento { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }

        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
