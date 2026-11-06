using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmDetalleVentaModal : Form
    {
        private readonly Venta _venta;
        private readonly VentaService _ventaService;
        private List<DetalleVenta> _detalles = new List<DetalleVenta>();

        private Label lblTitulo;
        private Label lblInfoVenta;
        private DataGridView dgvDetalles;
        private Label lblTotales;
        private Button btnCopiarTicket;
        private Button btnCerrar;

        public FrmDetalleVentaModal(Venta venta)
        {
            _venta = venta ?? throw new ArgumentNullException(nameof(venta));
            _ventaService = new VentaService();
            InitializeComponent();
            CargarDetalles();
        }

        private void InitializeComponent()
        {
            Text = $"Detalle del Ticket de Venta #{_venta.Id}";
            Size = new Size(620, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            KeyPreview = true;
            BackColor = Color.FromArgb(34, 47, 62);
            ForeColor = Color.White;

            lblTitulo = new Label
            {
                Text = $"COMPROBANTE DE VENTA #{_venta.Id}",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Location = new Point(20, 15),
                Size = new Size(565, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var pnlCabecera = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(565, 80),
                BackColor = Color.FromArgb(44, 62, 80),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblInfoVenta = new Label
            {
                Text = $"• Fecha y Hora: {_venta.Fecha:dd/MM/yyyy HH:mm:ss}\n" +
                       $"• Cliente: {_venta.NombreCliente}\n" +
                       $"• Método de Pago: {_venta.MetodoPago}  |  Estado: {_venta.Estado}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.White,
                Location = new Point(10, 10),
                Size = new Size(540, 60)
            };
            pnlCabecera.Controls.Add(lblInfoVenta);

            dgvDetalles = new DataGridView
            {
                Location = new Point(20, 140),
                Size = new Size(565, 250),
                BackgroundColor = Color.FromArgb(45, 52, 54),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvDetalles.DefaultCellStyle.BackColor = Color.FromArgb(53, 59, 72);
            dgvDetalles.DefaultCellStyle.ForeColor = Color.White;
            dgvDetalles.DefaultCellStyle.SelectionBackColor = Color.FromArgb(41, 128, 185);
            dgvDetalles.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 39, 46);
            dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetalles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvDetalles.EnableHeadersVisualStyles = false;

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cant.", DataPropertyName = "Cantidad", FillWeight = 30 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código / SKU", DataPropertyName = "Codigo", FillWeight = 50 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = "Descripcion", FillWeight = 110 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "P. Unitario", DataPropertyName = "PrecioUnitarioStr", FillWeight = 50 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "SubtotalStr", FillWeight = 50 });

            // Panel Totales
            lblTotales = new Label
            {
                Location = new Point(20, 400),
                Size = new Size(565, 40),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                BackColor = Color.FromArgb(30, 39, 46),
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = ContentAlignment.MiddleRight,
                Text = $"Descuento: $ {_venta.Descuento:N2}    TOTAL: $ {_venta.Total:N2}  "
            };

            btnCopiarTicket = new Button
            {
                Text = "Copiar / Imprimir Ticket Digital",
                Location = new Point(20, 455),
                Size = new Size(320, 45),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCopiarTicket.FlatAppearance.BorderSize = 0;
            btnCopiarTicket.Click += BtnCopiarTicket_Click;

            btnCerrar = new Button
            {
                Text = "Cerrar [Esc]",
                Location = new Point(360, 455),
                Size = new Size(225, 45),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) => Close();

            Controls.Add(lblTitulo);
            Controls.Add(pnlCabecera);
            Controls.Add(dgvDetalles);
            Controls.Add(lblTotales);
            Controls.Add(btnCopiarTicket);
            Controls.Add(btnCerrar);
        }

        private void CargarDetalles()
        {
            try
            {
                _detalles = _ventaService.ObtenerDetallesPorVenta(_venta.Id);
                var displayList = new List<object>();
                foreach (var d in _detalles)
                {
                    displayList.Add(new
                    {
                        d.Cantidad,
                        d.Codigo,
                        d.Descripcion,
                        PrecioUnitarioStr = $"$ {d.PrecioUnitario:N2}",
                        SubtotalStr = $"$ {d.Subtotal:N2}"
                    });
                }
                dgvDetalles.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos de la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCopiarTicket_Click(object sender, EventArgs e)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("========================================");
                sb.AppendLine("             KIOSCO EXPRESS             ");
                sb.AppendLine("        TICKET NO FISCAL DE VENTA       ");
                sb.AppendLine("========================================");
                sb.AppendLine($"Ticket N°: {_venta.Id}");
                sb.AppendLine($"Fecha:    {_venta.Fecha:dd/MM/yyyy HH:mm:ss}");
                sb.AppendLine($"Cliente:  {_venta.NombreCliente}");
                sb.AppendLine($"Pago:     {_venta.MetodoPago}");
                sb.AppendLine("----------------------------------------");
                sb.AppendLine("CANT  DESCRIPCIÓN          P.UNIT  SUBTOT");
                sb.AppendLine("----------------------------------------");

                foreach (var d in _detalles)
                {
                    string desc = d.Descripcion.Length > 18 ? d.Descripcion.Substring(0, 18) : d.Descripcion.PadRight(18);
                    sb.AppendLine($"{d.Cantidad,3}  {desc} {d.PrecioUnitario,7:F2} {d.Subtotal,7:F2}");
                }

                sb.AppendLine("----------------------------------------");
                if (_venta.Descuento > 0)
                {
                    sb.AppendLine($"Descuento aplicado:       $ {_venta.Descuento,9:N2}");
                }
                sb.AppendLine($"TOTAL A PAGAR:            $ {_venta.Total,9:N2}");
                sb.AppendLine("========================================");
                sb.AppendLine("      ¡GRACIAS POR SU COMPRA!           ");
                sb.AppendLine("========================================");

                Clipboard.SetText(sb.ToString());
                MessageBox.Show("¡Ticket copiado al portapapeles!\n\nPuede pegarlo o enviarlo a la impresora térmica directamente.", "Ticket Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
