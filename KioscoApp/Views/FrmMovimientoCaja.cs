using System;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp.Views
{
    public class FrmMovimientoCaja : Form
    {
        public decimal Monto { get; private set; }
        public string Motivo { get; private set; } = string.Empty;

        private readonly TextBox txtMonto;
        private readonly TextBox txtMotivo;
        private readonly Button btnConfirmar;
        private readonly Button btnCancelar;
        private readonly Label lblTitulo;
        private readonly Label lblMonto;
        private readonly Label lblMotivo;

        public FrmMovimientoCaja(string tipoMovimiento) // "Ingreso" o "Retiro"
        {
            this.Text = tipoMovimiento == "Ingreso" ? "Ingreso de Efectivo" : "Retiro de Efectivo / Gasto";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;

            lblTitulo = new Label
            {
                Text = tipoMovimiento == "Ingreso" ? "📥 Registrar Ingreso a Caja" : "📤 Registrar Retiro de Caja",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = tipoMovimiento == "Ingreso" ? Color.LightGreen : Color.LightCoral,
                Location = new Point(20, 20),
                AutoSize = true
            };

            lblMonto = new Label
            {
                Text = "Monto ($):",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 70),
                AutoSize = true
            };

            txtMonto = new TextBox
            {
                Location = new Point(20, 95),
                Size = new Size(340, 26),
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };

            txtMonto.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                    e.Handled = true;
            };

            lblMotivo = new Label
            {
                Text = "Motivo / Concepto:",
                Font = new Font("Segoe UI", 10F),
                Location = new Point(20, 135),
                AutoSize = true
            };

            txtMotivo = new TextBox
            {
                Location = new Point(20, 160),
                Size = new Size(340, 26),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                MaxLength = 200
            };

            btnConfirmar = new Button
            {
                Text = "Confirmar",
                DialogResult = DialogResult.OK,
                Location = new Point(140, 210),
                Size = new Size(105, 35),
                BackColor = tipoMovimiento == "Ingreso" ? Color.SeaGreen : Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirmar.Click += BtnConfirmar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(255, 210),
                Size = new Size(105, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblMonto);
            this.Controls.Add(txtMonto);
            this.Controls.Add(lblMotivo);
            this.Controls.Add(txtMotivo);
            this.Controls.Add(btnConfirmar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnConfirmar;
            this.CancelButton = btnCancelar;
        }

        private void BtnConfirmar_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtMonto.Text.Replace('.', ','), out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Por favor, ingrese un monto válido mayor a 0.", "Monto inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
