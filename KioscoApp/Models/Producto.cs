using System;

namespace KioscoApp.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
    }
}
