using System;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Utils;

namespace KioscoApp.Views
{
    public class FrmMovimientoCaja : Form
    {
        public decimal Monto { get; private set; }
        public string Motivo { get; private set; } = string.Empty;

        private readonly string _tipoMovimiento;
        private readonly TextBox txtMonto;
        private readonly TextBox txtMotivo;
        private readonly Button btnConfirmar;
        private readonly Button btnCancelar;
        private readonly Label lblTitulo;
        private readonly Label lblMonto;
        private readonly Label lblMotivo;

        public FrmMovimientoCaja(string tipoMovimiento) // "Ingreso", "Retiro" o "FondoInicial"
        {
            _tipoMovimiento = tipoMovimiento;

            string tituloVentana = "Movimiento de Caja";
            string tituloLabel = "Movimiento de Caja";
            Color colorTema = Color.SeaGreen;

            if (tipoMovimiento == "FondoInicial")
            {
                tituloVentana = "Declarar Fondo Inicial de Caja (Apertura)";
                tituloLabel = "💵 Declarar Fondo Inicial de Turno (Cambio)";
                colorTema = Color.FromArgb(41, 128, 185);
            }
            else if (tipoMovimiento == "Ingreso")
            {
                tituloVentana = "Ingreso de Efectivo a Caja";
                tituloLabel = "📥 Registrar Ingreso a Caja";
                colorTema = Color.SeaGreen;
            }
            else // Retiro
            {
                tituloVentana = "Retiro de Efectivo / Gasto de Caja";
                tituloLabel = "📤 Registrar Retiro de Caja / Gasto";
                colorTema = Color.IndianRed;
            }

            this.Text = tituloVentana;
            this.Size = new Size(420, tipoMovimiento == "FondoInicial" ? 360 : 310);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;

            lblTitulo = new Label
            {
                Text = tituloLabel,
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = colorTema,
                Location = new Point(20, 18),
                AutoSize = true
            };

            lblMonto = new Label
            {
                Text = tipoMovimiento == "FondoInicial" ? "Efectivo inicial en cajón ($):" : "Monto ($):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 55),
                AutoSize = true
            };

            txtMonto = new TextBox
            {
                Location = new Point(20, 80),
                Size = new Size(360, 28),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.PaleGreen,
                TextAlign = HorizontalAlignment.Right
            };

            txtMonto.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                    e.Handled = true;
            };

            int nextY = 120;

            // Si es Fondo Inicial, agregamos botones de billetes rápidos
            if (tipoMovimiento == "FondoInicial")
            {
                var pnlRapidos = new FlowLayoutPanel
                {
                    Location = new Point(20, 115),
                    Size = new Size(365, 38),
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false
                };

                int[] importes = { 1000, 2000, 5000, 10000, 20000 };
                foreach (int imp in importes)
                {
                    var btn = new Button
                    {
                        Text = $"${imp / 1000}k",
                        Size = new Size(58, 30),
                        BackColor = Color.FromArgb(60, 60, 65),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += (s, e) =>
                    {
                        decimal.TryParse(txtMonto.Text.Replace('.', ','), out decimal actual);
                        txtMonto.Text = (actual + imp).ToString("0.##");
                        txtMonto.Focus();
                        txtMonto.SelectionStart = txtMonto.Text.Length;
                    };
                    pnlRapidos.Controls.Add(btn);
                }

                var btnReset = new Button
                {
                    Text = "$0",
                    Size = new Size(48, 30),
                    BackColor = Color.FromArgb(127, 140, 141),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
                };
                btnReset.FlatAppearance.BorderSize = 0;
                btnReset.Click += (s, e) => { txtMonto.Text = "0"; txtMonto.Focus(); };
                pnlRapidos.Controls.Add(btnReset);

                this.Controls.Add(pnlRapidos);
                nextY = 160;
            }

            lblMotivo = new Label
            {
                Text = "Motivo / Concepto:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, nextY),
                AutoSize = true
            };

            txtMotivo = new TextBox
            {
                Location = new Point(20, nextY + 25),
                Size = new Size(360, 26),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                MaxLength = 200,
                Text = tipoMovimiento == "FondoInicial" ? "Fondo Inicial de Turno (Apertura de Caja)" : ""
            };

            int btnY = nextY + 65;

            btnConfirmar = new Button
            {
                Text = "Confirmar",
                DialogResult = DialogResult.OK,
                Location = new Point(140, btnY),
                Size = new Size(115, 36),
                BackColor = colorTema,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.Click += BtnConfirmar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(265, btnY),
                Size = new Size(115, 36),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblMonto);
            this.Controls.Add(txtMonto);
            this.Controls.Add(lblMotivo);
            this.Controls.Add(txtMotivo);
            this.Controls.Add(btnConfirmar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnConfirmar;
            this.CancelButton = btnCancelar;

            HelpTooltipHelper.AttachHelp(
                lblTitulo,
                "Registro de Movimiento de Caja",
                "Complete el monto en pesos ($) y una breve descripción del motivo (ej. 'Pago proveedor panadería', 'Cambio inicial', 'Retiro parcial') y presione Confirmar.\n" +
                "Todo movimiento queda registrado con usuario y hora para el arqueo de caja.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                txtMonto,
                "Importe del Movimiento ($)",
                "Ingrese el monto numérico del dinero que entra o sale del cajón físico.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                txtMotivo,
                "Motivo / Concepto Obligatorio",
                "Especifique el concepto del movimiento para que el Supervisor pueda identificarlo en la Auditoría de Cajas.",
                shrinkControl: true);
        }

        private void BtnConfirmar_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text.Replace('.', ','), out decimal monto) || (_tipoMovimiento != "FondoInicial" && monto <= 0) || (_tipoMovimiento == "FondoInicial" && monto < 0))
            {
                MessageBox.Show("Por favor, ingrese un monto válido.", "Monto inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                txtMonto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Por favor, detalle el motivo o concepto del movimiento.", "Motivo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                txtMotivo.Focus();
                return;
            }

            Monto = monto;
            Motivo = txtMotivo.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
