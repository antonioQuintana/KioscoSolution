using System;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmDatosEmpresaModal : Form
    {
        private readonly ConfiguracionService _configService;
        private ConfiguracionEmpresa _config;

        private TextBox txtNombreFantasia = null!;
        private TextBox txtRazonSocial = null!;
        private TextBox txtCuit = null!;
        private TextBox txtDireccion = null!;
        private TextBox txtTelefono = null!;
        private TextBox txtMensajePie = null!;
        private TextBox txtPreviewTicket = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        public FrmDatosEmpresaModal()
        {
            _configService = new ConfiguracionService();
            _config = _configService.Obtener();

            InitializeCustomComponents();
            CargarDatos();
            ActualizarPreview();
        }

        private void InitializeCustomComponents()
        {
            Text = "Configuración de Datos del Negocio e Información de Ticket";
            Size = new Size(820, 620);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(30, 30, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);

            var lblTitulo = new Label
            {
                Text = "🏢 Datos de la Empresa (Encabezado y Pie de Ticket)",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 168, 255),
                Location = new Point(20, 15),
                AutoSize = true
            };

            // Panel Izquierdo: Formulario
            var pnlForm = new GroupBox
            {
                Text = "Información Fiscal y Comercial",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 55),
                Size = new Size(420, 480)
            };

            int y = 30;
            pnlForm.Controls.Add(CrearLabel("Nombre de Fantasía (Título del Ticket):*", 15, y));
            txtNombreFantasia = CrearTextBox(15, y + 22, 385);
            txtNombreFantasia.TextChanged += (s, e) => ActualizarPreview();
            pnlForm.Controls.Add(txtNombreFantasia);

            y += 60;
            pnlForm.Controls.Add(CrearLabel("Razón Social:", 15, y));
            txtRazonSocial = CrearTextBox(15, y + 22, 385);
            txtRazonSocial.TextChanged += (s, e) => ActualizarPreview();
            pnlForm.Controls.Add(txtRazonSocial);

            y += 60;
            pnlForm.Controls.Add(CrearLabel("CUIT / Identificación Tributaria:", 15, y));
            txtCuit = CrearTextBox(15, y + 22, 385);
            txtCuit.TextChanged += (s, e) => ActualizarPreview();
            pnlForm.Controls.Add(txtCuit);

            y += 60;
            pnlForm.Controls.Add(CrearLabel("Dirección del Local / Sucursal:", 15, y));
            txtDireccion = CrearTextBox(15, y + 22, 385);
            txtDireccion.TextChanged += (s, e) => ActualizarPreview();
            pnlForm.Controls.Add(txtDireccion);

            y += 60;
            pnlForm.Controls.Add(CrearLabel("Teléfono de Contacto / WhatsApp:", 15, y));
            txtTelefono = CrearTextBox(15, y + 22, 385);
            txtTelefono.TextChanged += (s, e) => ActualizarPreview();
            pnlForm.Controls.Add(txtTelefono);

            y += 60;
            pnlForm.Controls.Add(CrearLabel("Mensaje o Agradecimiento en Pie de Ticket:", 15, y));
            txtMensajePie = CrearTextBox(15, y + 22, 385);
            txtMensajePie.Multiline = true;
            txtMensajePie.Height = 55;
            txtMensajePie.TextChanged += (s, e) => ActualizarPreview();
            pnlForm.Controls.Add(txtMensajePie);

            // Panel Derecho: Vista previa de ticket
            var pnlPreview = new GroupBox
            {
                Text = "Vista Previa de Ticket Térmico",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(455, 55),
                Size = new Size(335, 480)
            };

            txtPreviewTicket = new TextBox
            {
                Location = new Point(15, 25),
                Size = new Size(305, 440),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(245, 245, 240),
                ForeColor = Color.FromArgb(20, 20, 20),
                Font = new Font("Consolas", 8.5F)
            };
            pnlPreview.Controls.Add(txtPreviewTicket);

            // Botones inferiores
            btnGuardar = new Button
            {
                Text = "Guardar Cambios",
                Location = new Point(455, 545),
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button
            {
                Text = "Cerrar / Cancelar",
                Location = new Point(650, 545),
                Size = new Size(140, 40),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => Close();

            Controls.Add(lblTitulo);
            Controls.Add(pnlForm);
            Controls.Add(pnlPreview);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 220, 220),
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        private TextBox CrearTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 25),
                BackColor = Color.FromArgb(45, 45, 48),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5F)
            };
        }

        private void CargarDatos()
        {
            txtNombreFantasia.Text = _config.NombreFantasia;
            txtRazonSocial.Text = _config.RazonSocial;
            txtCuit.Text = _config.Cuit;
            txtDireccion.Text = _config.Direccion;
            txtTelefono.Text = _config.Telefono;
            txtMensajePie.Text = _config.MensajePieTicket;
        }

        private void ActualizarPreview()
        {
            string nombre = string.IsNullOrWhiteSpace(txtNombreFantasia.Text) ? "KIOSCO EXPRESS" : txtNombreFantasia.Text.Trim();
            string razon = txtRazonSocial.Text.Trim();
            string cuit = txtCuit.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string tel = txtTelefono.Text.Trim();
            string pie = string.IsNullOrWhiteSpace(txtMensajePie.Text) ? "¡GRACIAS POR SU COMPRA!" : txtMensajePie.Text.Trim();

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("========================================");
            sb.AppendLine(CentrarTexto(nombre, 40));
            if (!string.IsNullOrEmpty(razon)) sb.AppendLine(CentrarTexto(razon, 40));
            if (!string.IsNullOrEmpty(cuit)) sb.AppendLine(CentrarTexto($"CUIT: {cuit}", 40));
            if (!string.IsNullOrEmpty(direccion)) sb.AppendLine(CentrarTexto(direccion, 40));
            if (!string.IsNullOrEmpty(tel)) sb.AppendLine(CentrarTexto($"Tel: {tel}", 40));
            sb.AppendLine("========================================");
            sb.AppendLine("Ticket N°: 00012345");
            sb.AppendLine($"Fecha:    {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("Cajero:   vendedor1");
            sb.AppendLine("Cliente:  Consumidor Final");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("CANT  DESCRIPCIÓN          P.UNIT  SUBTOT");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("  2   Coca Cola 500ml      1500.00 3000.00");
            sb.AppendLine("  1   Alfajor Havanna      1800.00 1800.00");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("TOTAL A PAGAR:            $  4.800,00");
            sb.AppendLine("Pago: Efectivo  Abona: $5000  Vuelto: $200");
            sb.AppendLine("========================================");
            sb.AppendLine(CentrarTexto(pie, 40));
            sb.AppendLine("========================================");

            txtPreviewTicket.Text = sb.ToString();
        }

        private string CentrarTexto(string texto, int ancho)
        {
            if (texto.Length >= ancho) return texto;
            int espacios = (ancho - texto.Length) / 2;
            return new string(' ', espacios) + texto;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreFantasia.Text))
            {
                MessageBox.Show("El Nombre de Fantasía es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreFantasia.Focus();
                return;
            }

            try
            {
                _config.NombreFantasia = txtNombreFantasia.Text.Trim();
                _config.RazonSocial = txtRazonSocial.Text.Trim();
                _config.Cuit = txtCuit.Text.Trim();
                _config.Direccion = txtDireccion.Text.Trim();
                _config.Telefono = txtTelefono.Text.Trim();
                _config.MensajePieTicket = txtMensajePie.Text.Trim();

                _configService.Guardar(_config);

                MessageBox.Show("¡Información del negocio y plantilla de ticket actualizadas correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
