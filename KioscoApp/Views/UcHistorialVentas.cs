using System;
using System.Collections.Generic;
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
        private List<Venta> _ventasActuales = new List<Venta>();

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

            dgvHistorial.Columns.Clear();
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID Ticket", DataPropertyName = "Id", Width = 90 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha / Hora", DataPropertyName = "FechaStr", Width = 150 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cliente", DataPropertyName = "NombreCliente", Width = 280 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Medio de Pago", DataPropertyName = "MetodoPago", Width = 150 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", DataPropertyName = "TotalStr", Width = 120 });
            dgvHistorial.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Estado", DataPropertyName = "Estado", Width = 110 });

            dgvHistorial.CellFormatting += DgvHistorial_CellFormatting;
            dgvHistorial.DoubleClick += DgvHistorial_DoubleClick;
            dgvHistorial.KeyDown += DgvHistorial_KeyDown;

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

            CargarVentas();
        }

        private void DgvHistorial_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHistorial.Rows.Count) return;

            var row = dgvHistorial.Rows[e.RowIndex];
            string estado = row.Cells[5].Value?.ToString() ?? "";

            if (string.Equals(estado, "Anulada", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(estado, "Cancelada", StringComparison.OrdinalIgnoreCase))
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(60, 25, 25);
                row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 160, 160);
                row.DefaultCellStyle.Font = new Font(dgvHistorial.Font, FontStyle.Strikeout);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
                row.DefaultCellStyle.ForeColor = Color.White;
                row.DefaultCellStyle.Font = new Font(dgvHistorial.Font, FontStyle.Regular);
            }
        }

        public void CargarVentas()
        {
            try
            {
                bool soloTurno = chkSoloTurnoActual.Checked;
                DateTime? desde = soloTurno ? null : dtpDesde.Value.Date;
                DateTime? hasta = soloTurno ? null : dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);
                string filtro = txtBuscar.Text.Trim();

                _ventasActuales = _ventaService.FiltrarVentas(desde, hasta, string.IsNullOrEmpty(filtro) ? null : filtro, soloTurno);

                var displayList = new List<object>();
                decimal sumaCompletadas = 0;
                int anuladas = 0;

                foreach (var v in _ventasActuales)
                {
                    if (string.Equals(v.Estado, "Completada", StringComparison.OrdinalIgnoreCase))
                    {
                        sumaCompletadas += v.Total;
                    }
                    else
                    {
                        anuladas++;
                    }

                    displayList.Add(new
                    {
                        v.Id,
                        FechaStr = v.Fecha.ToString("dd/MM/yyyy HH:mm:ss"),
                        v.NombreCliente,
                        v.MetodoPago,
                        TotalStr = $"$ {v.Total:N2}",
                        v.Estado
                    });
                }

                dgvHistorial.DataSource = displayList;

                string turnoTexto = soloTurno ? "Turno Actual" : $"Período {dtpDesde.Value:dd/MM/yy} al {dtpHasta.Value:dd/MM/yy}";
                lblResumenTotales.Text = $"[{turnoTexto}]  Ventas Totales: {_ventasActuales.Count} | Total Facturado: $ {sumaCompletadas:N2} | Anuladas: {anuladas}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkSoloTurnoActual_CheckedChanged(object? sender, EventArgs e)
        {
            bool habilitarFechas = !chkSoloTurnoActual.Checked;
            dtpDesde.Enabled = habilitarFechas;
            dtpHasta.Enabled = habilitarFechas;
            CargarVentas();
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            CargarVentas();
        }

        private void btnFiltrar_Click(object? sender, EventArgs e)
        {
            CargarVentas();
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            chkSoloTurnoActual.Checked = true;
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            txtBuscar.Clear();
            CargarVentas();
        }

        private void btnAnular_Click(object? sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow == null || dgvHistorial.CurrentRow.Index >= _ventasActuales.Count)
            {
                MessageBox.Show("Seleccione una venta de la grilla para anular.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var venta = _ventasActuales[dgvHistorial.CurrentRow.Index];

            if (string.Equals(venta.Estado, "Anulada", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta venta ya se encuentra anulada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Está seguro de que desea ANULAR la Venta Ticket #{venta.Id}?\n\n" +
                $"• Cliente: {venta.NombreCliente}\n" +
                $"• Total a Reintegrar: $ {venta.Total:N2}\n" +
                $"• Método de Pago: {venta.MetodoPago}\n\n" +
                "Consecuencias automáticas:\n" +
                "1. El stock de todos los artículos vendidos volverá al inventario.\n" +
                "2. Si fue pagada en efectivo en el turno abierto, se generará la salida de caja correspondiente.\n" +
                "3. Si fue en cuenta corriente, se descontará de la deuda del cliente.",
                "Confirmar Anulación de Venta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;
                _ventaService.AnularVenta(venta.Id, "Anulada por usuario desde Historial de Ventas", idUsuario);

                MessageBox.Show($"¡Venta #{venta.Id} anulada con éxito!\n\nSe ha regularizado el stock y los saldos de caja/cliente.", "Anulación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al anular la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHistorial_DoubleClick(object? sender, EventArgs e)
        {
            AbrirDetalleVenta();
        }

        private void DgvHistorial_KeyDown(object? sender, KeyEventArgs e)
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
            if (dgvHistorial.CurrentRow != null && dgvHistorial.CurrentRow.Index < _ventasActuales.Count)
            {
                var ventaSeleccionada = _ventasActuales[dgvHistorial.CurrentRow.Index];
                using var modal = new FrmDetalleVentaModal(ventaSeleccionada);
                modal.ShowDialog(this);
            }
        }
    }
}
