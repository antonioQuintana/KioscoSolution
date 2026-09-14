namespace KioscoApp
{
    partial class UcCierreCaja
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblBilletes1000 = new System.Windows.Forms.Label();
            this.txtBilletes1000 = new System.Windows.Forms.TextBox();
            this.lblBilletes500 = new System.Windows.Forms.Label();
            this.txtBilletes500 = new System.Windows.Forms.TextBox();
            this.lblBilletes100 = new System.Windows.Forms.Label();
            this.txtBilletes100 = new System.Windows.Forms.TextBox();
            this.lblEsperado = new System.Windows.Forms.Label();
            this.lblCalculado = new System.Windows.Forms.Label();
            this.lblDiferencia = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Text = "Arqueo de Billetes:";
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.AutoSize = true;

            this.lblBilletes1000.Location = new System.Drawing.Point(20, 60);
            this.lblBilletes1000.Text = "Cant. Billetes $1000:";
            this.lblBilletes1000.AutoSize = true;
            this.txtBilletes1000.Location = new System.Drawing.Point(150, 57);
            this.txtBilletes1000.Size = new System.Drawing.Size(100, 23);

            this.lblBilletes500.Location = new System.Drawing.Point(20, 100);
            this.lblBilletes500.Text = "Cant. Billetes $500:";
            this.lblBilletes500.AutoSize = true;
            this.txtBilletes500.Location = new System.Drawing.Point(150, 97);
            this.txtBilletes500.Size = new System.Drawing.Size(100, 23);

            this.lblBilletes100.Location = new System.Drawing.Point(20, 140);
            this.lblBilletes100.Text = "Cant. Billetes $100:";
            this.lblBilletes100.AutoSize = true;
            this.txtBilletes100.Location = new System.Drawing.Point(150, 137);
            this.txtBilletes100.Size = new System.Drawing.Size(100, 23);

            this.lblEsperado.Location = new System.Drawing.Point(350, 60);
            this.lblEsperado.Text = "Efectivo Esperado: $0.00";
            this.lblEsperado.AutoSize = true;
            this.lblEsperado.Font = new System.Drawing.Font("Segoe UI", 11F);

            this.lblCalculado.Location = new System.Drawing.Point(350, 100);
            this.lblCalculado.Text = "Efectivo Contado: $0.00";
            this.lblCalculado.AutoSize = true;
            this.lblCalculado.Font = new System.Drawing.Font("Segoe UI", 11F);

            this.lblDiferencia.Location = new System.Drawing.Point(350, 140);
            this.lblDiferencia.Text = "Diferencia: $0.00";
            this.lblDiferencia.AutoSize = true;
            this.lblDiferencia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);

            this.btnCerrar.Location = new System.Drawing.Point(200, 220);
            this.btnCerrar.Size = new System.Drawing.Size(200, 40);
            this.btnCerrar.Text = "Confirmar Cierre de Caja";

            
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblDiferencia);
            this.Controls.Add(this.lblCalculado);
            this.Controls.Add(this.lblEsperado);
            this.Controls.Add(this.txtBilletes100);
            this.Controls.Add(this.lblBilletes100);
            this.Controls.Add(this.txtBilletes500);
            this.Controls.Add(this.lblBilletes500);
            this.Controls.Add(this.txtBilletes1000);
            this.Controls.Add(this.lblBilletes1000);
            this.Controls.Add(this.lblTitulo);
            
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBilletes1000;
        private System.Windows.Forms.TextBox txtBilletes1000;
        private System.Windows.Forms.Label lblBilletes500;
        private System.Windows.Forms.TextBox txtBilletes500;
        private System.Windows.Forms.Label lblBilletes100;
        private System.Windows.Forms.TextBox txtBilletes100;
        private System.Windows.Forms.Label lblEsperado;
        private System.Windows.Forms.Label lblCalculado;
        private System.Windows.Forms.Label lblDiferencia;
        private System.Windows.Forms.Button btnCerrar;
    }
}

