using System;
using System.Data;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class UcHistorialVentas : UserControl
    {
        public UcHistorialVentas()
        {
            InitializeComponent();
            CargarVentasSimuladas();
        }

        private void CargarVentasSimuladas()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Hora", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Productos", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("Medio de Pago", typeof(string));

            dt.Rows.Add("14:15", "CONSUMIDOR FINAL", "1x Coca-Cola 2L, 2x Alfajor Jorgito", "$ 4500.00", "Efectivo");
            dt.Rows.Add("14:28", "Juan Pérez", "3x Cerveza Quilmes, 1x Papas Lays", "$ 7200.00", "QR MercadoPago");
            dt.Rows.Add("15:05", "CONSUMIDOR FINAL", "1x Atado Marlboro 20, 1x Encendedor", "$ 2800.00", "Efectivo");
            dt.Rows.Add("15:42", "María Gómez", "2x Jugo Baggio, 5x Caramelos", "$ 1150.00", "Tarjeta Débito");
            dt.Rows.Add("16:10", "CONSUMIDOR FINAL", "1x Yerba Playadito 500g", "$ 1900.00", "Efectivo");

            dgvHistorial.DataSource = dt;
        }
    }
}
