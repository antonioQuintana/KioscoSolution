namespace KioscoApp
{
    partial class UcEstadisticas
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblVentasHoy = new System.Windows.Forms.Label();
            this.lblProdMasVendido = new System.Windows.Forms.Label();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.SuspendLayout();

            this.lblVentasHoy.Location = new System.Drawing.Point(30, 30);
            this.lblVentasHoy.Text = "Ventas Totales Hoy: $0.00";
            this.lblVentasHoy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblVentasHoy.AutoSize = true;

            this.lblProdMasVendido.Location = new System.Drawing.Point(30, 80);
            this.lblProdMasVendido.Text = "Producto Top: Ninguno";
            this.lblProdMasVendido.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblProdMasVendido.AutoSize = true;

            this.dgvHistorico.Location = new System.Drawing.Point(30, 150);
            this.dgvHistorico.Size = new System.Drawing.Size(500, 200);

            
            this.Controls.Add(this.dgvHistorico);
            this.Controls.Add(this.lblProdMasVendido);
            this.Controls.Add(this.lblVentasHoy);
            
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblVentasHoy;
        private System.Windows.Forms.Label lblProdMasVendido;
        private System.Windows.Forms.DataGridView dgvHistorico;
    }
}

