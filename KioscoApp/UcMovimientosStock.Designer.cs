namespace KioscoApp
{
    partial class UcMovimientosStock
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.lblProducto = new System.Windows.Forms.Label();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.SuspendLayout();
            
            this.dgvStock.Location = new System.Drawing.Point(20, 20);
            this.dgvStock.Size = new System.Drawing.Size(500, 400);
            
            this.lblProducto.Location = new System.Drawing.Point(540, 20);
            this.lblProducto.Text = "ID Producto (Barras):";
            this.txtProducto.Location = new System.Drawing.Point(540, 40);
            this.txtProducto.Size = new System.Drawing.Size(200, 23);
            
            this.lblCantidad.Location = new System.Drawing.Point(540, 70);
            this.lblCantidad.Text = "Cantidad a Ingresar:";
            this.txtCantidad.Location = new System.Drawing.Point(540, 90);
            this.txtCantidad.Size = new System.Drawing.Size(200, 23);
            
            this.btnIngresar.Location = new System.Drawing.Point(540, 130);
            this.btnIngresar.Size = new System.Drawing.Size(200, 40);
            this.btnIngresar.Text = "Registrar Ingreso de Stock";

            
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.txtProducto);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.dgvStock);
            
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnIngresar;
    }
}

