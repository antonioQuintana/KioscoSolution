using System;
using System.Windows.Forms;
using KioscoApp.Business;

namespace KioscoApp
{
    public partial class UcHistorialVentas : UserControl
    {
        private readonly VentaService _ventaService;

        public UcHistorialVentas()
        {
            InitializeComponent();
            _ventaService = new VentaService();
            ConfigurarUI();
        }

        private void ConfigurarUI()
        {
            dgvHistorial.AutoGenerateColumns = false;
            if (dgvHistorial.Columns.Count == 0)
            {
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "Fecha", Width = 150 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cliente", DataPropertyName = "NombreCliente", Width = 250 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", Width = 100 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Método", DataPropertyName = "MetodoPago", Width = 150 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", Width = 100 });
            }
            CargarVentas();
        }

        public void CargarVentas()
        {
            try
            {
                var ventas = _ventaService.ObtenerTodas();
                dgvHistorial.DataSource = ventas;
                
                decimal sumaTotal = 0;
                foreach(var v in ventas) sumaTotal += v.Total;
                
                if (lblInfo != null)
                {
                    lblInfo.Text = $"Total Ventas (50 más recientes): $ {sumaTotal:N2}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas: {ex.Message}");
            }
        }
    }
}
