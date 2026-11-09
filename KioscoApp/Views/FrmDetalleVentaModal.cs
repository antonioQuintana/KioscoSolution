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
        private readonly VentaService _ventaService;
        private readonly ConfiguracionService _configService;
        private readonly Venta _venta;
        private List<DetalleVenta> _detalles = new List<DetalleVenta>();

        private DataGridView dgvDetalles = null!;
        private Label lblTotales = null!;
        private Button btnCopiarTicket = null!;
        private Button btnCerrar = null!;

        public FrmDetalleVentaModal(Venta venta)
        {
            _venta = venta ?? throw new ArgumentNullException(nameof(venta));
            _ventaService = new VentaService();
            _configService = new ConfiguracionService();

            InitializeCustomComponents();
            CargarDetalles();
        }

        private void InitializeCustomComponents()
        {
            Text = $"Detalle del Ticket de Venta N° {_venta.Id}";
            Size = new Size(620, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);

            var lblTitulo = new Label
            {
                Text = $"Comprobante de Venta #{_venta.Id}",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 15),
                AutoSize = true
            };

            var pnlCabecera = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(565, 80),
                BackColor = Color.FromArgb(45, 45, 48)
            };

            var lblFecha = new Label
            {
                Text = $"Fecha: {_venta.Fecha:dd/MM/yyyy HH:mm:ss}",
                Location = new Point(15, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White
            };

            var lblCliente = new Label
            {
                Text = $"Cliente: {_venta.NombreCliente}",
                Location = new Point(15, 42),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(220, 220, 220)
            };

            var lblMetodo = new Label
            {
                Text = $"Método de Pago: {_venta.MetodoPago}",
                Location = new Point(320, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113)
            };

            var lblEstado = new Label
            {
                Text = $"Estado: {_venta.Estado}",
                Location = new Point(320, 42),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(241, 196, 15)
            };

            pnlCabecera.Controls.Add(lblFecha);
            pnlCabecera.Controls.Add(lblCliente);
            pnlCabecera.Controls.Add(lblMetodo);
            pnlCabecera.Controls.Add(lblEstado);

            dgvDetalles = new DataGridView
            {
                Location = new Point(20, 140),
                Size = new Size(565, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.Black
            };

            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cant.", FillWeight = 15 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Codigo", HeaderText = "Código / SKU", FillWeight = 25 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descripcion", HeaderText = "Descripción", FillWeight = 35 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioUnitarioStr", HeaderText = "P. Unit", FillWeight = 20 });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubtotalStr", HeaderText = "Subtotal", FillWeight = 20 });

            lblTotales = new Label
            {
                Location = new Point(20, 400),
                Size = new Size(565, 40),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.FromArgb(46, 204, 113),
                Text = $"Total Cobrado: $ {_venta.Total:N2}"
            };

            btnCopiarTicket = new Button
            {
                Text = "Copiar / Imprimir Ticket",
                Location = new Point(20, 455),
                Size = new Size(330, 45),
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
                var config = _configService.Obtener();
                string tituloNegocio = string.IsNullOrWhiteSpace(config.NombreFantasia) ? "KIOSCO EXPRESS" : config.NombreFantasia.Trim();
                string pie = string.IsNullOrWhiteSpace(config.MensajePieTicket) ? "¡GRACIAS POR SU COMPRA!" : config.MensajePieTicket.Trim();

                var sb = new StringBuilder();
                sb.AppendLine("========================================");
                sb.AppendLine(CentrarTexto(tituloNegocio, 40));
                if (!string.IsNullOrEmpty(config.RazonSocial)) sb.AppendLine(CentrarTexto(config.RazonSocial, 40));
                if (!string.IsNullOrEmpty(config.Cuit)) sb.AppendLine(CentrarTexto($"CUIT: {config.Cuit}", 40));
                if (!string.IsNullOrEmpty(config.Direccion)) sb.AppendLine(CentrarTexto(config.Direccion, 40));
                if (!string.IsNullOrEmpty(config.Telefono)) sb.AppendLine(CentrarTexto($"Tel: {config.Telefono}", 40));
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
                sb.AppendLine(CentrarTexto(pie, 40));
                sb.AppendLine("========================================");

                Clipboard.SetText(sb.ToString());
                MessageBox.Show("¡Ticket copiado al portapapeles!\n\nPuede pegarlo o enviarlo a la impresora térmica directamente.", "Ticket Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CentrarTexto(string texto, int ancho)
        {
            if (texto.Length >= ancho) return texto;
            int espacios = (ancho - texto.Length) / 2;
            return new string(' ', espacios) + texto;
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
