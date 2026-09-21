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
            // Paneles
            this.gbSistema = new System.Windows.Forms.GroupBox();
            this.gbBilletes = new System.Windows.Forms.GroupBox();
            this.gbResultados = new System.Windows.Forms.GroupBox();
            
            // Sistema
            this.lblFondoCaja = new System.Windows.Forms.Label();
            this.lblVentasEfectivo = new System.Windows.Forms.Label();
            this.lblVentasQR = new System.Windows.Forms.Label();
            this.lblVentasTarjeta = new System.Windows.Forms.Label();
            this.lblGastos = new System.Windows.Forms.Label();
            
            // Billetes
            this.lblBilletes10000 = new System.Windows.Forms.Label();
            this.txtBilletes10000 = new System.Windows.Forms.TextBox();
            this.lblBilletes2000 = new System.Windows.Forms.Label();
            this.txtBilletes2000 = new System.Windows.Forms.TextBox();
            this.lblBilletes1000 = new System.Windows.Forms.Label();
            this.txtBilletes1000 = new System.Windows.Forms.TextBox();
            this.lblBilletes500 = new System.Windows.Forms.Label();
            this.txtBilletes500 = new System.Windows.Forms.TextBox();
            this.lblBilletes200 = new System.Windows.Forms.Label();
            this.txtBilletes200 = new System.Windows.Forms.TextBox();
            this.lblBilletes100 = new System.Windows.Forms.Label();
            this.txtBilletes100 = new System.Windows.Forms.TextBox();
            this.lblMonedas = new System.Windows.Forms.Label();
            this.txtMonedas = new System.Windows.Forms.TextBox();

            // Resultados y Botones
            this.lblEsperado = new System.Windows.Forms.Label();
            this.lblDeclarado = new System.Windows.Forms.Label();
            this.lblDiferencia = new System.Windows.Forms.Label();
            
            this.btnRegistrarGasto = new System.Windows.Forms.Button();
            this.btnIngresoCaja = new System.Windows.Forms.Button();
            this.btnImprimirZ = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();

            this.gbSistema.SuspendLayout();
            this.gbBilletes.SuspendLayout();
            this.gbResultados.SuspendLayout();
            this.SuspendLayout();

            // 
            // gbSistema (Totales Registrados en el Sistema)
            // 
            this.gbSistema.Controls.Add(this.lblFondoCaja);
            this.gbSistema.Controls.Add(this.lblVentasEfectivo);
            this.gbSistema.Controls.Add(this.lblVentasQR);
            this.gbSistema.Controls.Add(this.lblVentasTarjeta);
            this.gbSistema.Controls.Add(this.lblGastos);
            this.gbSistema.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.gbSistema.Location = new System.Drawing.Point(30, 30);
            this.gbSistema.Name = "gbSistema";
            this.gbSistema.Size = new System.Drawing.Size(320, 360);
            this.gbSistema.Text = "Registros del Sistema";

            this.lblFondoCaja.AutoSize = true;
            this.lblFondoCaja.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblFondoCaja.Location = new System.Drawing.Point(20, 50);
            this.lblFondoCaja.Text = "Fondo de Caja (Inicio): $ 5000.00";

            this.lblVentasEfectivo.AutoSize = true;
            this.lblVentasEfectivo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVentasEfectivo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblVentasEfectivo.Location = new System.Drawing.Point(20, 100);
            this.lblVentasEfectivo.Text = "(+) Ventas Efectivo: $ 12500.00";

            this.lblVentasQR.AutoSize = true;
            this.lblVentasQR.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVentasQR.Location = new System.Drawing.Point(20, 150);
            this.lblVentasQR.Text = "(+) Ventas QR: $ 8400.00";

            this.lblVentasTarjeta.AutoSize = true;
            this.lblVentasTarjeta.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVentasTarjeta.Location = new System.Drawing.Point(20, 200);
            this.lblVentasTarjeta.Text = "(+) Ventas Tarjeta: $ 15300.00";

            this.lblGastos.AutoSize = true;
            this.lblGastos.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblGastos.ForeColor = System.Drawing.Color.DarkRed;
            this.lblGastos.Location = new System.Drawing.Point(20, 250);
            this.lblGastos.Text = "(-) Gastos / Retiros: $ 2000.00";

            // 
            // gbBilletes (Arqueo de Billetes Físicos)
            // 
            this.gbBilletes.Controls.Add(this.lblBilletes10000);
            this.gbBilletes.Controls.Add(this.txtBilletes10000);
            this.gbBilletes.Controls.Add(this.lblBilletes2000);
            this.gbBilletes.Controls.Add(this.txtBilletes2000);
            this.gbBilletes.Controls.Add(this.lblBilletes1000);
            this.gbBilletes.Controls.Add(this.txtBilletes1000);
            this.gbBilletes.Controls.Add(this.lblBilletes500);
            this.gbBilletes.Controls.Add(this.txtBilletes500);
            this.gbBilletes.Controls.Add(this.lblBilletes200);
            this.gbBilletes.Controls.Add(this.txtBilletes200);
            this.gbBilletes.Controls.Add(this.lblBilletes100);
            this.gbBilletes.Controls.Add(this.txtBilletes100);
            this.gbBilletes.Controls.Add(this.lblMonedas);
            this.gbBilletes.Controls.Add(this.txtMonedas);
            this.gbBilletes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.gbBilletes.Location = new System.Drawing.Point(380, 30);
            this.gbBilletes.Name = "gbBilletes";
            this.gbBilletes.Size = new System.Drawing.Size(260, 420);
            this.gbBilletes.Text = "Conteo Físico";

            int yPos = 50;
            // $10000
            this.lblBilletes10000.Location = new System.Drawing.Point(20, yPos); this.lblBilletes10000.Text = "$ 10.000 x"; this.lblBilletes10000.AutoSize = true;
            this.txtBilletes10000.Location = new System.Drawing.Point(130, yPos - 3); this.txtBilletes10000.Size = new System.Drawing.Size(80, 29);
            yPos += 45;
            // $2000
            this.lblBilletes2000.Location = new System.Drawing.Point(20, yPos); this.lblBilletes2000.Text = "$ 2.000 x"; this.lblBilletes2000.AutoSize = true;
            this.txtBilletes2000.Location = new System.Drawing.Point(130, yPos - 3); this.txtBilletes2000.Size = new System.Drawing.Size(80, 29);
            yPos += 45;
            // $1000
            this.lblBilletes1000.Location = new System.Drawing.Point(20, yPos); this.lblBilletes1000.Text = "$ 1.000 x"; this.lblBilletes1000.AutoSize = true;
            this.txtBilletes1000.Location = new System.Drawing.Point(130, yPos - 3); this.txtBilletes1000.Size = new System.Drawing.Size(80, 29);
            yPos += 45;
            // $500
            this.lblBilletes500.Location = new System.Drawing.Point(20, yPos); this.lblBilletes500.Text = "$ 500 x"; this.lblBilletes500.AutoSize = true;
            this.txtBilletes500.Location = new System.Drawing.Point(130, yPos - 3); this.txtBilletes500.Size = new System.Drawing.Size(80, 29);
            yPos += 45;
            // $200
            this.lblBilletes200.Location = new System.Drawing.Point(20, yPos); this.lblBilletes200.Text = "$ 200 x"; this.lblBilletes200.AutoSize = true;
            this.txtBilletes200.Location = new System.Drawing.Point(130, yPos - 3); this.txtBilletes200.Size = new System.Drawing.Size(80, 29);
            yPos += 45;
            // $100
            this.lblBilletes100.Location = new System.Drawing.Point(20, yPos); this.lblBilletes100.Text = "$ 100 x"; this.lblBilletes100.AutoSize = true;
            this.txtBilletes100.Location = new System.Drawing.Point(130, yPos - 3); this.txtBilletes100.Size = new System.Drawing.Size(80, 29);
            yPos += 45;
            // Monedas
            this.lblMonedas.Location = new System.Drawing.Point(20, yPos); this.lblMonedas.Text = "Monedas $"; this.lblMonedas.AutoSize = true;
            this.txtMonedas.Location = new System.Drawing.Point(130, yPos - 3); this.txtMonedas.Size = new System.Drawing.Size(80, 29);

            // 
            // gbResultados
            // 
            this.gbResultados.Controls.Add(this.lblEsperado);
            this.gbResultados.Controls.Add(this.lblDeclarado);
            this.gbResultados.Controls.Add(this.lblDiferencia);
            this.gbResultados.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.gbResultados.Location = new System.Drawing.Point(670, 30);
            this.gbResultados.Name = "gbResultados";
            this.gbResultados.Size = new System.Drawing.Size(360, 220);
            this.gbResultados.Text = "Resultado del Cierre";

            this.lblEsperado.AutoSize = true;
            this.lblEsperado.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEsperado.Location = new System.Drawing.Point(20, 50);
            this.lblEsperado.Text = "Efectivo Esperado: $ 15500.00";

            this.lblDeclarado.AutoSize = true;
            this.lblDeclarado.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblDeclarado.Location = new System.Drawing.Point(20, 100);
            this.lblDeclarado.Text = "Efectivo Contado: $ 0.00";

            this.lblDiferencia.AutoSize = true;
            this.lblDiferencia.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDiferencia.ForeColor = System.Drawing.Color.DarkRed;
            this.lblDiferencia.Location = new System.Drawing.Point(20, 150);
            this.lblDiferencia.Text = "DIFERENCIA: $ -15500.00";

            // Botones Extra
            this.btnIngresoCaja.Location = new System.Drawing.Point(30, 410);
            this.btnIngresoCaja.Size = new System.Drawing.Size(150, 40);
            this.btnIngresoCaja.Text = "Ingresar Efectivo";
            this.btnIngresoCaja.BackColor = System.Drawing.Color.LightGreen;
            
            this.btnRegistrarGasto.Location = new System.Drawing.Point(200, 410);
            this.btnRegistrarGasto.Size = new System.Drawing.Size(150, 40);
            this.btnRegistrarGasto.Text = "Retirar Efectivo";
            this.btnRegistrarGasto.BackColor = System.Drawing.Color.LightCoral;
            this.btnRegistrarGasto.ForeColor = System.Drawing.Color.White;

            this.btnImprimirZ.Location = new System.Drawing.Point(670, 270);
            this.btnImprimirZ.Size = new System.Drawing.Size(360, 50);
            this.btnImprimirZ.Text = "🖨 Imprimir Cierre (Ticket Z)";
            this.btnImprimirZ.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnImprimirZ.BackColor = System.Drawing.Color.LightSkyBlue;

            this.btnCerrar.Location = new System.Drawing.Point(670, 340);
            this.btnCerrar.Size = new System.Drawing.Size(360, 60);
            this.btnCerrar.Text = "✅ CONFIRMAR CIERRE DE CAJA";
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.BackColor = System.Drawing.Color.Gold;

            this.Controls.Add(this.btnIngresoCaja);
            this.Controls.Add(this.btnRegistrarGasto);
            this.Controls.Add(this.btnImprimirZ);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.gbResultados);
            this.Controls.Add(this.gbBilletes);
            this.Controls.Add(this.gbSistema);
            
            this.Size = new System.Drawing.Size(1060, 680);

            this.gbSistema.ResumeLayout(false);
            this.gbSistema.PerformLayout();
            this.gbBilletes.ResumeLayout(false);
            this.gbBilletes.PerformLayout();
            this.gbResultados.ResumeLayout(false);
            this.gbResultados.PerformLayout();
            this.ResumeLayout(false);
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
