using System;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Views;

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
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.ReadOnly = true;

            if (dgvHistorial.Columns.Count == 0)
            {
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha", DataPropertyName = "Fecha", Width = 150 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cliente", DataPropertyName = "NombreCliente", Width = 250 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "Total", Width = 110 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Método", DataPropertyName = "MetodoPago", Width = 150 });
                dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", Width = 100 });
            }

            dgvHistorial.DoubleClick += DgvHistorial_DoubleClick;
            dgvHistorial.KeyDown += DgvHistorial_KeyDown;

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
                    lblInfo.Text = $"Total Ventas (50 más recientes): $ {sumaTotal:N2}  |  (Doble clic en una venta para ver o reimprimir el ticket)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHistorial_DoubleClick(object sender, EventArgs e)
        {
            AbrirDetalleVenta();
        }

        private void DgvHistorial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                AbrirDetalleVenta();
            }
        }

        private void AbrirDetalleVenta()
        {
            if (dgvHistorial.CurrentRow?.DataBoundItem is Venta ventaSeleccionada)
            {
                using var modal = new FrmDetalleVentaModal(ventaSeleccionada);
                modal.ShowDialog(this);
            }
        }
    }
}
