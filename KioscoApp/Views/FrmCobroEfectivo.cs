using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using KioscoApp.Utils;

namespace KioscoApp.Views
{
    public class FrmCobroEfectivo : Form
    {
        private readonly decimal _totalAPagar;
        public decimal MontoAbonado { get; private set; }
        public decimal Vuelto => MontoAbonado - _totalAPagar;

        private Label lblTitulo;
        private Label lblTotalTexto;
        private Label lblTotalMonto;
        private Label lblPromptAbonado;
        private TextBox txtAbonado;
        private FlowLayoutPanel pnlBilletes;
        private Panel pnlResultado;
        private Label lblResultadoTexto;
        private Label lblResultadoMonto;
        private Button btnConfirmar;
        private Button btnCancelar;

        public FrmCobroEfectivo(decimal totalAPagar)
        {
            _totalAPagar = totalAPagar;
            InitializeComponent();
            ConfigurarAyuda();
            CalcularVuelto();
        }

        private void ConfigurarAyuda()
        {
            HelpTooltipHelper.AttachFormHelp(
                this,
                "Calculadora de Cobro en Efectivo",
                "En este formulario se calcula automáticamente el vuelto a entregar al cliente:\n" +
                "1. Escriba con cuánto dinero abona el cliente o pulse uno de los botones rápidos de billetes ($1.000 a $20.000 o Exacto).\n" +
                "2. Verifique el 'VUELTO A ENTREGAR' en verde.\n" +
                "3. Presione [Enter] o 'CONFIRMAR COBRO' para registrar la venta.",
                new Point(438, 18));

            HelpTooltipHelper.AttachHelp(
                txtAbonado,
                "Importe con el que Abona el Cliente",
                "Ingrese aquí el monto de dinero en efectivo entregado por el cliente (debe ser igual o mayor al total a cobrar). También puede usar los botones de billetes rápidos debajo.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnConfirmar,
                "Confirmar Cobro y Registrar Venta [Enter]",
                "Asienta la venta en efectivo en la caja del turno actual y cierra esta ventana informando el vuelto a entregar.",
                shrinkControl: true);
        }

        private void InitializeComponent()
        {
            Text = "Cobro en Efectivo - Calculadora de Vuelto";
            Size = new Size(500, 560);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            KeyPreview = true;
            BackColor = Color.FromArgb(34, 47, 62);
            ForeColor = Color.White;

            // Encabezado
            lblTitulo = new Label
            {
                Text = "COBRO EN EFECTIVO",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                Location = new Point(20, 15),
                Size = new Size(440, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Panel Total
            var pnlTotal = new Panel
            {
                Location = new Point(25, 55),
                Size = new Size(435, 75),
                BackColor = Color.FromArgb(44, 62, 80),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblTotalTexto = new Label
            {
                Text = "TOTAL A COBRAR:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(10, 8),
                Size = new Size(415, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblTotalMonto = new Label
            {
                Text = $"$ {_totalAPagar:N2}",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Location = new Point(10, 28),
                Size = new Size(415, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlTotal.Controls.Add(lblTotalTexto);
            pnlTotal.Controls.Add(lblTotalMonto);

            // Input Abonado
            lblPromptAbonado = new Label
            {
                Text = "El cliente abona con: ($)",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(25, 140),
                Size = new Size(435, 22)
            };

            txtAbonado = new TextBox
            {
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                Location = new Point(25, 165),
                Size = new Size(435, 43),
                TextAlign = HorizontalAlignment.Center,
                BackColor = Color.FromArgb(245, 246, 250),
                ForeColor = Color.FromArgb(44, 62, 80),
                Text = _totalAPagar.ToString("F2")
            };

            txtAbonado.TextChanged += (s, e) => CalcularVuelto();
            txtAbonado.KeyPress += TxtAbonado_KeyPress;
            txtAbonado.KeyDown += TxtAbonado_KeyDown;

            // Botones de billetes rápidos
            pnlBilletes = new FlowLayoutPanel
            {
                Location = new Point(25, 215),
                Size = new Size(440, 75),
                FlowDirection = FlowDirection.LeftToRight
            };

            AgregarBotonBillete("Exacto", 0, esExacto: true);
            AgregarBotonBillete("$ 1.000", 1000);
            AgregarBotonBillete("$ 2.000", 2000);
            AgregarBotonBillete("$ 5.000", 5000);
            AgregarBotonBillete("$ 10.000", 10000);
            AgregarBotonBillete("$ 20.000", 20000);

            // Panel Vuelto / Faltante
            pnlResultado = new Panel
            {
                Location = new Point(25, 300),
                Size = new Size(435, 95),
                BackColor = Color.FromArgb(30, 39, 46),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblResultadoTexto = new Label
            {
                Text = "VUELTO A ENTREGAR:",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(189, 195, 199),
                Location = new Point(10, 10),
                Size = new Size(415, 22),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblResultadoMonto = new Label
            {
                Text = "$ 0,00",
                Font = new Font("Segoe UI", 26F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Location = new Point(10, 35),
                Size = new Size(415, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlResultado.Controls.Add(lblResultadoTexto);
            pnlResultado.Controls.Add(lblResultadoMonto);

            // Botón Confirmar
            btnConfirmar = new Button
            {
                Text = "CONFIRMAR COBRO [Enter]",
                Location = new Point(25, 410),
                Size = new Size(435, 50),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.Click += BtnConfirmar_Click;

            // Botón Cancelar
            btnCancelar = new Button
            {
                Text = "Volver al Carrito [Esc]",
                Location = new Point(25, 468),
                Size = new Size(435, 35),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            // Agregar Controles
            Controls.Add(lblTitulo);
            Controls.Add(pnlTotal);
            Controls.Add(lblPromptAbonado);
            Controls.Add(txtAbonado);
            Controls.Add(pnlBilletes);
            Controls.Add(pnlResultado);
            Controls.Add(btnConfirmar);
            Controls.Add(btnCancelar);

            Shown += (s, e) =>
            {
                txtAbonado.Focus();
                txtAbonado.SelectAll();
            };
        }

        private void AgregarBotonBillete(string texto, decimal monto, bool esExacto = false)
        {
            var btn = new Button
            {
                Text = texto,
                Size = new Size(68, 32),
                Margin = new Padding(2),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) =>
            {
                if (esExacto)
                {
                    txtAbonado.Text = _totalAPagar.ToString("F2");
                }
                else
                {
                    // Si el monto de billete es menor al total, o si el usuario quiere redondear
                    txtAbonado.Text = monto.ToString("F2");
                }
                txtAbonado.Focus();
                txtAbonado.SelectAll();
            };
            pnlBilletes.Controls.Add(btn);
        }

        private void TxtAbonado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TxtAbonado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                Confirmar();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void CalcularVuelto()
        {
            string texto = txtAbonado.Text.Trim().Replace(',', '.');
            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal abonado))
            {
                MontoAbonado = abonado;
                decimal diff = MontoAbonado - _totalAPagar;

                if (diff >= 0)
                {
                    lblResultadoTexto.Text = "VUELTO A ENTREGAR:";
                    lblResultadoTexto.ForeColor = Color.FromArgb(46, 204, 113);
                    lblResultadoMonto.Text = $"$ {diff:N2}";
                    lblResultadoMonto.ForeColor = Color.FromArgb(46, 204, 113);
                    pnlResultado.BackColor = Color.FromArgb(20, 60, 40);
                    btnConfirmar.Enabled = true;
                    btnConfirmar.BackColor = Color.FromArgb(39, 174, 96);
                }
                else
                {
                    lblResultadoTexto.Text = "MONTO INSUFICIENTE (FALTAN):";
                    lblResultadoTexto.ForeColor = Color.FromArgb(231, 76, 60);
                    lblResultadoMonto.Text = $"$ {Math.Abs(diff):N2}";
                    lblResultadoMonto.ForeColor = Color.FromArgb(231, 76, 60);
                    pnlResultado.BackColor = Color.FromArgb(70, 25, 25);
                    btnConfirmar.Enabled = false;
                    btnConfirmar.BackColor = Color.FromArgb(120, 60, 60);
                }
            }
            else
            {
                MontoAbonado = 0;
                lblResultadoTexto.Text = "INGRESE UN MONTO VÁLIDO";
                lblResultadoTexto.ForeColor = Color.FromArgb(231, 76, 60);
                lblResultadoMonto.Text = "$ ---";
                lblResultadoMonto.ForeColor = Color.FromArgb(231, 76, 60);
                pnlResultado.BackColor = Color.FromArgb(50, 50, 50);
                btnConfirmar.Enabled = false;
                btnConfirmar.BackColor = Color.FromArgb(120, 60, 60);
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            Confirmar();
        }

        private void Confirmar()
        {
            CalcularVuelto();
            if (MontoAbonado < _totalAPagar)
            {
                MessageBox.Show(
                    $"El monto abonado ($ {MontoAbonado:N2}) es menor al total a pagar ($ {_totalAPagar:N2}).\n\nIngrese un importe suficiente para completar la operación.",
                    "Monto insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtAbonado.Focus();
                txtAbonado.SelectAll();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
