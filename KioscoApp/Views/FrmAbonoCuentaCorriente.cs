using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmAbonoCuentaCorriente : Form
    {
        private readonly Cliente _cliente;
        private readonly ClienteService _clienteService;

        public decimal MontoAbonado { get; private set; }
        public string MetodoPagoSeleccionado { get; private set; } = "Efectivo";

        private Label lblTitulo;
        private Label lblInfoCliente;
        private Label lblDeudaMonto;
        private TextBox txtMonto;
        private Button btnPagarTotal;
        private ComboBox cmbMetodo;
        private Label lblNuevoSaldo;
        private Button btnConfirmar;
        private Button btnCancelar;

        public FrmAbonoCuentaCorriente(Cliente cliente)
        {
            _cliente = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _clienteService = new ClienteService();

            InitializeComponent();
            CalcularNuevoSaldo();
        }

        private void InitializeComponent()
        {
            Text = $"Abono a Cuenta Corriente - {_cliente.NombreCompleto}";
            Size = new Size(500, 520);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            KeyPreview = true;
            BackColor = Color.FromArgb(34, 47, 62);
            ForeColor = Color.White;

            lblTitulo = new Label
            {
                Text = "COBRO DE CUENTA CORRIENTE",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Location = new Point(20, 15),
                Size = new Size(440, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Panel Info Cliente y Deuda
            var pnlCliente = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(435, 95),
                BackColor = Color.FromArgb(44, 62, 80),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblInfoCliente = new Label
            {
                Text = $"• Cliente: {_cliente.NombreCompleto.ToUpper()}  (DNI: {_cliente.Dni})\n" +
                       $"• Límite de Crédito: $ {_cliente.LimiteCredito:N2}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(223, 230, 233),
                Location = new Point(15, 10),
                Size = new Size(405, 38)
            };

            lblDeudaMonto = new Label
            {
                Text = $"DEUDA ACTUAL: $ {_cliente.SaldoDeudor:N2}",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(231, 76, 60),
                Location = new Point(15, 52),
                Size = new Size(405, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlCliente.Controls.Add(lblInfoCliente);
            pnlCliente.Controls.Add(lblDeudaMonto);

            // Importe a abonar
            var lblPrompt = new Label
            {
                Text = "Importe a Cobrar / Abonar: ($)",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(25, 160),
                Size = new Size(435, 20)
            };

            txtMonto = new TextBox
            {
                Location = new Point(25, 185),
                Size = new Size(290, 38),
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.FromArgb(245, 246, 250),
                ForeColor = Color.FromArgb(44, 62, 80),
                Text = _cliente.SaldoDeudor.ToString("F2")
            };
            txtMonto.TextChanged += (s, e) => CalcularNuevoSaldo();
            txtMonto.KeyPress += TxtMonto_KeyPress;

            btnPagarTotal = new Button
            {
                Text = "Totalidad",
                Location = new Point(325, 185),
                Size = new Size(135, 38),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPagarTotal.FlatAppearance.BorderSize = 0;
            btnPagarTotal.Click += (s, e) =>
            {
                txtMonto.Text = _cliente.SaldoDeudor.ToString("F2");
                txtMonto.Focus();
                txtMonto.SelectAll();
            };

            // Método de Pago
            var lblMetodo = new Label
            {
                Text = "Medio de Pago Utilizado:",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(25, 235),
                Size = new Size(435, 20)
            };

            cmbMetodo = new ComboBox
            {
                Location = new Point(25, 260),
                Size = new Size(435, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular)
            };
            cmbMetodo.Items.Add("Efectivo");
            cmbMetodo.Items.Add("Mercado Pago / Transferencia");
            cmbMetodo.Items.Add("Tarjeta de Débito");
            cmbMetodo.SelectedIndex = 0;

            // Panel Resumen Resultante
            var pnlResumen = new Panel
            {
                Location = new Point(25, 305),
                Size = new Size(435, 60),
                BackColor = Color.FromArgb(30, 39, 46),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblNuevoSaldo = new Label
            {
                Text = "Nuevo Saldo Deudor: $ 0,00",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Location = new Point(10, 15),
                Size = new Size(415, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlResumen.Controls.Add(lblNuevoSaldo);

            // Botones
            btnConfirmar = new Button
            {
                Text = "CONFIRMAR COBRANZA [Enter]",
                Location = new Point(25, 380),
                Size = new Size(435, 48),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.Click += BtnConfirmar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar [Esc]",
                Location = new Point(25, 435),
                Size = new Size(435, 35),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(lblTitulo);
            Controls.Add(pnlCliente);
            Controls.Add(lblPrompt);
            Controls.Add(txtMonto);
            Controls.Add(btnPagarTotal);
            Controls.Add(lblMetodo);
            Controls.Add(cmbMetodo);
            Controls.Add(pnlResumen);
            Controls.Add(btnConfirmar);
            Controls.Add(btnCancelar);

            Shown += (s, e) =>
            {
                txtMonto.Focus();
                txtMonto.SelectAll();
            };
        }

        private void TxtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void CalcularNuevoSaldo()
        {
            string texto = txtMonto.Text.Trim().Replace(',', '.');
            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto) && monto > 0)
            {
                MontoAbonado = monto;
                decimal saldoRestante = _cliente.SaldoDeudor - MontoAbonado;

                if (saldoRestante >= 0)
                {
                    lblNuevoSaldo.Text = $"Nuevo Saldo Deudor: $ {saldoRestante:N2}";
                    lblNuevoSaldo.ForeColor = saldoRestante == 0 ? Color.FromArgb(46, 204, 113) : Color.FromArgb(241, 196, 15);
                    btnConfirmar.Enabled = true;
                    btnConfirmar.BackColor = Color.FromArgb(39, 174, 96);
                }
                else
                {
                    lblNuevoSaldo.Text = $"Excede la deuda por: $ {Math.Abs(saldoRestante):N2}";
                    lblNuevoSaldo.ForeColor = Color.FromArgb(231, 76, 60);
                    btnConfirmar.Enabled = false;
                    btnConfirmar.BackColor = Color.FromArgb(120, 60, 60);
                }
            }
            else
            {
                MontoAbonado = 0;
                lblNuevoSaldo.Text = "Ingrese un importe mayor a $ 0.00";
                lblNuevoSaldo.ForeColor = Color.FromArgb(231, 76, 60);
                btnConfirmar.Enabled = false;
                btnConfirmar.BackColor = Color.FromArgb(120, 60, 60);
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            CalcularNuevoSaldo();

            if (MontoAbonado <= 0)
            {
                MessageBox.Show("Ingrese un monto válido a cobrar.", "Monto inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            if (MontoAbonado > _cliente.SaldoDeudor)
            {
                MessageBox.Show($"El monto no puede superar la deuda pendiente de $ {_cliente.SaldoDeudor:N2}.", "Importe Excesivo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            string metodo = cmbMetodo.SelectedItem?.ToString() ?? "Efectivo";
            decimal saldoRestante = _cliente.SaldoDeudor - MontoAbonado;

            var confirm = MessageBox.Show(
                $"¿Confirma registrar la cobranza de $ {MontoAbonado:N2} ({metodo}) para {_cliente.NombreCompleto}?\n\n" +
                $"• Deuda Anterior: $ {_cliente.SaldoDeudor:N2}\n" +
                $"• Pago Recibido:  $ {MontoAbonado:N2}\n" +
                $"• Saldo Restante: $ {saldoRestante:N2}\n\n" +
                (metodo == "Efectivo" ? "(El dinero ingresará automáticamente a la caja del turno actual)" : "(Cobro bancario / digital)"),
                "Confirmar Cobranza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;
                _clienteService.RegistrarAbono(_cliente.Id, MontoAbonado, metodo, idUsuario);

                // Actualizar modelo en memoria
                _cliente.SaldoDeudor = saldoRestante;
                MetodoPagoSeleccionado = metodo;

                var recibo = new StringBuilder();
                recibo.AppendLine("========================================");
                recibo.AppendLine("             KIOSCO EXPRESS             ");
                recibo.AppendLine("     RECIBO DE PAGO CUENTA CORRIENTE    ");
                recibo.AppendLine("========================================");
                recibo.AppendLine($"Fecha:          {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                recibo.AppendLine($"Cliente:        {_cliente.NombreCompleto}");
                recibo.AppendLine($"DNI:            {_cliente.Dni}");
                recibo.AppendLine($"Medio de Pago:  {metodo}");
                recibo.AppendLine("----------------------------------------");
                recibo.AppendLine($"TOTAL ABONADO:  $ {MontoAbonado,10:N2}");
                recibo.AppendLine("----------------------------------------");
                recibo.AppendLine($"Saldo Restante: $ {saldoRestante,10:N2}");
                recibo.AppendLine("========================================");

                Clipboard.SetText(recibo.ToString());

                MessageBox.Show(
                    $"¡Cobro registrado exitosamente!\n\n" +
                    $"• Monto recibido: $ {MontoAbonado:N2}\n" +
                    $"• Saldo actual del cliente: $ {saldoRestante:N2}\n\n" +
                    "El recibo ha sido copiado al portapapeles por si desea imprimirlo.",
                    "Cobro Exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el cobro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            if (keyData == Keys.Enter && !btnCancelar.Focused)
            {
                BtnConfirmar_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
