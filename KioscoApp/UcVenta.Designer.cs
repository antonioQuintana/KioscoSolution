namespace KioscoApp
{
    partial class UcVenta
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.lblScanner = new System.Windows.Forms.Label();
            this.txtScanner = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCobrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.SuspendLayout();

            this.lblScanner.Location = new System.Drawing.Point(20, 20);
            this.lblScanner.Text = "Escanear Código:";
            this.lblScanner.AutoSize = true;

            this.txtScanner.Location = new System.Drawing.Point(130, 17);
            this.txtScanner.Size = new System.Drawing.Size(300, 23);

            this.dgvCarrito.Location = new System.Drawing.Point(20, 50);
            this.dgvCarrito.Size = new System.Drawing.Size(550, 400);

            this.lblTotal.Location = new System.Drawing.Point(600, 250);
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Text = "Total: .00";
            this.lblTotal.AutoSize = true;

            this.btnCobrar.Location = new System.Drawing.Point(600, 320);
            this.btnCobrar.Size = new System.Drawing.Size(200, 60);
            this.btnCobrar.Text = "COBRAR (F12)";
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCobrar.BackColor = System.Drawing.Color.LightGreen;

            
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.txtScanner);
            this.Controls.Add(this.lblScanner);
            
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Label lblScanner;
        private System.Windows.Forms.TextBox txtScanner;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCobrar;
    }
}

