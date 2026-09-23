using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    partial class UcCierreCaja
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            gbSistema = new GroupBox();
            lblFondoCaja = new Label();
            lblVentasEfectivo = new Label();
            lblVentasQR = new Label();
            lblVentasTarjeta = new Label();
            lblGastos = new Label();
            gbBilletes = new GroupBox();
            lblBilletes10000 = new Label();
            txtBilletes10000 = new TextBox();
            lblBilletes2000 = new Label();
            txtBilletes2000 = new TextBox();
            lblBilletes1000 = new Label();
            txtBilletes1000 = new TextBox();
            lblBilletes500 = new Label();
            txtBilletes500 = new TextBox();
            lblBilletes200 = new Label();
            txtBilletes200 = new TextBox();
            lblBilletes100 = new Label();
            txtBilletes100 = new TextBox();
            lblMonedas = new Label();
            txtMonedas = new TextBox();
            gbResultados = new GroupBox();
            lblEsperado = new Label();
            lblDeclarado = new Label();
            lblDiferencia = new Label();
            btnRegistrarGasto = new Button();
            btnIngresoCaja = new Button();
            btnImprimirZ = new Button();
            btnCerrar = new Button();
            gbSistema.SuspendLayout();
            gbBilletes.SuspendLayout();
            gbResultados.SuspendLayout();
            SuspendLayout();
            // 
            // gbSistema
            // 
            gbSistema.Controls.Add(lblFondoCaja);
            gbSistema.Controls.Add(lblVentasEfectivo);
            gbSistema.Controls.Add(lblVentasQR);
            gbSistema.Controls.Add(lblVentasTarjeta);
            gbSistema.Controls.Add(lblGastos);
            gbSistema.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            gbSistema.ForeColor = SystemColors.ControlLightLight;
            gbSistema.Location = new Point(30, 30);
            gbSistema.Name = "gbSistema";
            gbSistema.Size = new Size(320, 360);
            gbSistema.TabIndex = 6;
            gbSistema.TabStop = false;
            gbSistema.Text = "Registros del Sistema";
            // 
            // lblFondoCaja
            // 
            lblFondoCaja.AutoSize = true;
            lblFondoCaja.Font = new Font("Segoe UI", 11F);
            lblFondoCaja.Location = new Point(20, 50);
            lblFondoCaja.Name = "lblFondoCaja";
            lblFondoCaja.Size = new Size(225, 20);
            lblFondoCaja.TabIndex = 0;
            lblFondoCaja.Text = "Fondo de Caja (Inicio): $ 5000.00";
            // 
            // lblVentasEfectivo
            // 
            lblVentasEfectivo.AutoSize = true;
            lblVentasEfectivo.Font = new Font("Segoe UI", 11F);
            lblVentasEfectivo.ForeColor = Color.PaleGreen;
            lblVentasEfectivo.Location = new Point(20, 100);
            lblVentasEfectivo.Name = "lblVentasEfectivo";
            lblVentasEfectivo.Size = new Size(211, 20);
            lblVentasEfectivo.TabIndex = 1;
            lblVentasEfectivo.Text = "(+) Ventas Efectivo: $ 12500.00";
            // 
            // lblVentasQR
            // 
            lblVentasQR.AutoSize = true;
            lblVentasQR.Font = new Font("Segoe UI", 11F);
            lblVentasQR.Location = new Point(20, 150);
            lblVentasQR.Name = "lblVentasQR";
            lblVentasQR.Size = new Size(170, 20);
            lblVentasQR.TabIndex = 2;
            lblVentasQR.Text = "(+) Ventas QR: $ 8400.00";
            // 
            // lblVentasTarjeta
            // 
            lblVentasTarjeta.AutoSize = true;
            lblVentasTarjeta.Font = new Font("Segoe UI", 11F);
            lblVentasTarjeta.Location = new Point(20, 200);
            lblVentasTarjeta.Name = "lblVentasTarjeta";
            lblVentasTarjeta.Size = new Size(202, 20);
            lblVentasTarjeta.TabIndex = 3;
            lblVentasTarjeta.Text = "(+) Ventas Tarjeta: $ 15300.00";
            // 
            // lblGastos
            // 
            lblGastos.AutoSize = true;
            lblGastos.Font = new Font("Segoe UI", 11F);
            lblGastos.ForeColor = Color.LightCoral;
            lblGastos.Location = new Point(20, 250);
            lblGastos.Name = "lblGastos";
            lblGastos.Size = new Size(203, 20);
            lblGastos.TabIndex = 4;
            lblGastos.Text = "(-) Gastos / Retiros: $ 2000.00";
            // 
            // gbBilletes
            // 
            gbBilletes.Controls.Add(lblBilletes10000);
            gbBilletes.Controls.Add(txtBilletes10000);
            gbBilletes.Controls.Add(lblBilletes2000);
            gbBilletes.Controls.Add(txtBilletes2000);
            gbBilletes.Controls.Add(lblBilletes1000);
            gbBilletes.Controls.Add(txtBilletes1000);
            gbBilletes.Controls.Add(lblBilletes500);
            gbBilletes.Controls.Add(txtBilletes500);
            gbBilletes.Controls.Add(lblBilletes200);
            gbBilletes.Controls.Add(txtBilletes200);
            gbBilletes.Controls.Add(lblBilletes100);
            gbBilletes.Controls.Add(txtBilletes100);
            gbBilletes.Controls.Add(lblMonedas);
            gbBilletes.Controls.Add(txtMonedas);
            gbBilletes.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            gbBilletes.ForeColor = SystemColors.ControlLightLight;
            gbBilletes.Location = new Point(380, 30);
            gbBilletes.Name = "gbBilletes";
            gbBilletes.Size = new Size(260, 420);
            gbBilletes.TabIndex = 5;
            gbBilletes.TabStop = false;
            gbBilletes.Text = "Conteo Físico";
            // 
            // lblBilletes10000
            // 
            lblBilletes10000.AutoSize = true;
            lblBilletes10000.Location = new Point(20, 50);
            lblBilletes10000.Name = "lblBilletes10000";
            lblBilletes10000.Size = new Size(85, 21);
            lblBilletes10000.TabIndex = 0;
            lblBilletes10000.Text = "$ 10.000 x";
            // 
            // txtBilletes10000
            // 
            txtBilletes10000.Location = new Point(130, 47);
            txtBilletes10000.Name = "txtBilletes10000";
            txtBilletes10000.Size = new Size(80, 29);
            txtBilletes10000.TabIndex = 1;
            // 
            // lblBilletes2000
            // 
            lblBilletes2000.AutoSize = true;
            lblBilletes2000.Location = new Point(20, 95);
            lblBilletes2000.Name = "lblBilletes2000";
            lblBilletes2000.Size = new Size(76, 21);
            lblBilletes2000.TabIndex = 2;
            lblBilletes2000.Text = "$ 2.000 x";
            // 
            // txtBilletes2000
            // 
            txtBilletes2000.Location = new Point(130, 92);
            txtBilletes2000.Name = "txtBilletes2000";
            txtBilletes2000.Size = new Size(80, 29);
            txtBilletes2000.TabIndex = 3;
            // 
            // lblBilletes1000
            // 
            lblBilletes1000.AutoSize = true;
            lblBilletes1000.Location = new Point(20, 140);
            lblBilletes1000.Name = "lblBilletes1000";
            lblBilletes1000.Size = new Size(76, 21);
            lblBilletes1000.TabIndex = 4;
            lblBilletes1000.Text = "$ 1.000 x";
            // 
            // txtBilletes1000
            // 
            txtBilletes1000.Location = new Point(130, 137);
            txtBilletes1000.Name = "txtBilletes1000";
            txtBilletes1000.Size = new Size(80, 29);
            txtBilletes1000.TabIndex = 5;
            // 
            // lblBilletes500
            // 
            lblBilletes500.AutoSize = true;
            lblBilletes500.Location = new Point(20, 185);
            lblBilletes500.Name = "lblBilletes500";
            lblBilletes500.Size = new Size(63, 21);
            lblBilletes500.TabIndex = 6;
            lblBilletes500.Text = "$ 500 x";
            // 
            // txtBilletes500
            // 
            txtBilletes500.Location = new Point(130, 182);
            txtBilletes500.Name = "txtBilletes500";
            txtBilletes500.Size = new Size(80, 29);
            txtBilletes500.TabIndex = 7;
            // 
            // lblBilletes200
            // 
            lblBilletes200.AutoSize = true;
            lblBilletes200.Location = new Point(20, 230);
            lblBilletes200.Name = "lblBilletes200";
            lblBilletes200.Size = new Size(63, 21);
            lblBilletes200.TabIndex = 8;
            lblBilletes200.Text = "$ 200 x";
            // 
            // txtBilletes200
            // 
            txtBilletes200.Location = new Point(130, 227);
            txtBilletes200.Name = "txtBilletes200";
            txtBilletes200.Size = new Size(80, 29);
            txtBilletes200.TabIndex = 9;
            // 
            // lblBilletes100
            // 
            lblBilletes100.AutoSize = true;
            lblBilletes100.Location = new Point(20, 275);
            lblBilletes100.Name = "lblBilletes100";
            lblBilletes100.Size = new Size(63, 21);
            lblBilletes100.TabIndex = 10;
            lblBilletes100.Text = "$ 100 x";
            // 
            // txtBilletes100
            // 
            txtBilletes100.Location = new Point(130, 272);
            txtBilletes100.Name = "txtBilletes100";
            txtBilletes100.Size = new Size(80, 29);
            txtBilletes100.TabIndex = 11;
            // 
            // lblMonedas
            // 
            lblMonedas.AutoSize = true;
            lblMonedas.Location = new Point(20, 320);
            lblMonedas.Name = "lblMonedas";
            lblMonedas.Size = new Size(93, 21);
            lblMonedas.TabIndex = 12;
            lblMonedas.Text = "Monedas $";
            // 
            // txtMonedas
            // 
            txtMonedas.Location = new Point(130, 317);
            txtMonedas.Name = "txtMonedas";
            txtMonedas.Size = new Size(80, 29);
            txtMonedas.TabIndex = 13;
            // 
            // gbResultados
            // 
            gbResultados.Controls.Add(lblEsperado);
            gbResultados.Controls.Add(lblDeclarado);
            gbResultados.Controls.Add(lblDiferencia);
            gbResultados.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            gbResultados.ForeColor = SystemColors.ControlLightLight;
            gbResultados.Location = new Point(670, 30);
            gbResultados.Name = "gbResultados";
            gbResultados.Size = new Size(360, 220);
            gbResultados.TabIndex = 4;
            gbResultados.TabStop = false;
            gbResultados.Text = "Resultado del Cierre";
            // 
            // lblEsperado
            // 
            lblEsperado.AutoSize = true;
            lblEsperado.Font = new Font("Segoe UI", 12F);
            lblEsperado.Location = new Point(20, 50);
            lblEsperado.Name = "lblEsperado";
            lblEsperado.Size = new Size(218, 21);
            lblEsperado.TabIndex = 0;
            lblEsperado.Text = "Efectivo Esperado: $ 15500.00";
            // 
            // lblDeclarado
            // 
            lblDeclarado.AutoSize = true;
            lblDeclarado.Font = new Font("Segoe UI", 12F);
            lblDeclarado.Location = new Point(20, 100);
            lblDeclarado.Name = "lblDeclarado";
            lblDeclarado.Size = new Size(177, 21);
            lblDeclarado.TabIndex = 1;
            lblDeclarado.Text = "Efectivo Contado: $ 0.00";
            // 
            // lblDiferencia
            // 
            lblDiferencia.AutoSize = true;
            lblDiferencia.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblDiferencia.ForeColor = Color.LightCoral;
            lblDiferencia.Location = new Point(20, 150);
            lblDiferencia.Name = "lblDiferencia";
            lblDiferencia.Size = new Size(275, 30);
            lblDiferencia.TabIndex = 2;
            lblDiferencia.Text = "DIFERENCIA: $ -15500.00";
            // 
            // btnRegistrarGasto
            // 
            btnRegistrarGasto.BackColor = Color.LightCoral;
            btnRegistrarGasto.ForeColor = Color.White;
            btnRegistrarGasto.Location = new Point(200, 410);
            btnRegistrarGasto.Name = "btnRegistrarGasto";
            btnRegistrarGasto.Size = new Size(150, 40);
            btnRegistrarGasto.TabIndex = 1;
            btnRegistrarGasto.Text = "Retirar Efectivo";
            btnRegistrarGasto.UseVisualStyleBackColor = false;
            // 
            // btnIngresoCaja
            // 
            btnIngresoCaja.BackColor = Color.LightGreen;
            btnIngresoCaja.Location = new Point(30, 410);
            btnIngresoCaja.Name = "btnIngresoCaja";
            btnIngresoCaja.Size = new Size(150, 40);
            btnIngresoCaja.TabIndex = 0;
            btnIngresoCaja.Text = "Ingresar Efectivo";
            btnIngresoCaja.UseVisualStyleBackColor = false;
            // 
            // btnImprimirZ
            // 
            btnImprimirZ.BackColor = Color.LightSkyBlue;
            btnImprimirZ.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnImprimirZ.Location = new Point(670, 270);
            btnImprimirZ.Name = "btnImprimirZ";
            btnImprimirZ.Size = new Size(360, 50);
            btnImprimirZ.TabIndex = 2;
            btnImprimirZ.Text = "🖨 Imprimir Cierre (Ticket Z)";
            btnImprimirZ.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Gold;
            btnCerrar.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCerrar.Location = new Point(670, 340);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(360, 60);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "✅ CONFIRMAR CIERRE DE CAJA";
            btnCerrar.UseVisualStyleBackColor = false;
            // 
            // UcCierreCaja
            // 
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(btnIngresoCaja);
            Controls.Add(btnRegistrarGasto);
            Controls.Add(btnImprimirZ);
            Controls.Add(btnCerrar);
            Controls.Add(gbResultados);
            Controls.Add(gbBilletes);
            Controls.Add(gbSistema);
            Name = "UcCierreCaja";
            Size = new Size(1060, 680);
            gbSistema.ResumeLayout(false);
            gbSistema.PerformLayout();
            gbBilletes.ResumeLayout(false);
            gbBilletes.PerformLayout();
            gbResultados.ResumeLayout(false);
            gbResultados.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox gbSistema;
        private System.Windows.Forms.Label lblFondoCaja;
        private System.Windows.Forms.Label lblVentasEfectivo;
        private System.Windows.Forms.Label lblVentasQR;
        private System.Windows.Forms.Label lblVentasTarjeta;
        private System.Windows.Forms.Label lblGastos;

        private System.Windows.Forms.GroupBox gbBilletes;
        private System.Windows.Forms.Label lblBilletes10000;
        private System.Windows.Forms.TextBox txtBilletes10000;
        private System.Windows.Forms.Label lblBilletes2000;
        private System.Windows.Forms.TextBox txtBilletes2000;
        private System.Windows.Forms.Label lblBilletes1000;
        private System.Windows.Forms.TextBox txtBilletes1000;
        private System.Windows.Forms.Label lblBilletes500;
        private System.Windows.Forms.TextBox txtBilletes500;
        private System.Windows.Forms.Label lblBilletes200;
        private System.Windows.Forms.TextBox txtBilletes200;
        private System.Windows.Forms.Label lblBilletes100;
        private System.Windows.Forms.TextBox txtBilletes100;
        private System.Windows.Forms.Label lblMonedas;
        private System.Windows.Forms.TextBox txtMonedas;

        private System.Windows.Forms.GroupBox gbResultados;
        private System.Windows.Forms.Label lblEsperado;
        private System.Windows.Forms.Label lblDeclarado;
        private System.Windows.Forms.Label lblDiferencia;
        
        private System.Windows.Forms.Button btnRegistrarGasto;
        private System.Windows.Forms.Button btnIngresoCaja;
        private System.Windows.Forms.Button btnImprimirZ;
        private System.Windows.Forms.Button btnCerrar;
    }
}
