using System;

namespace KioscoApp.Models
{
    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; } // SKU
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => PrecioUnitario * Cantidad;
    }
}
