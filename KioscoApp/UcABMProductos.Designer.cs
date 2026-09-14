namespace KioscoApp
{
    partial class UcABMProductos
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            
            this.dgvProductos.Location = new System.Drawing.Point(20, 20);
            this.dgvProductos.Size = new System.Drawing.Size(500, 400);
            
            this.lblCodigo.Location = new System.Drawing.Point(540, 20);
            this.lblCodigo.Text = "Código de Barras:";
            this.txtCodigo.Location = new System.Drawing.Point(540, 40);
            this.txtCodigo.Size = new System.Drawing.Size(200, 23);
            
            this.lblDesc.Location = new System.Drawing.Point(540, 70);
            this.lblDesc.Text = "Descripción:";
            this.txtDesc.Location = new System.Drawing.Point(540, 90);
            this.txtDesc.Size = new System.Drawing.Size(200, 23);
            
            this.lblPrecio.Location = new System.Drawing.Point(540, 120);
            this.lblPrecio.Text = "Precio Venta:";
            this.txtPrecio.Location = new System.Drawing.Point(540, 140);
            this.txtPrecio.Size = new System.Drawing.Size(200, 23);

            this.lblStock.Location = new System.Drawing.Point(540, 170);
            this.lblStock.Text = "Stock Actual:";
            this.txtStock.Location = new System.Drawing.Point(540, 190);
            this.txtStock.Size = new System.Drawing.Size(200, 23);
            
            this.btnGuardar.Location = new System.Drawing.Point(540, 240);
            this.btnGuardar.Size = new System.Drawing.Size(95, 30);
            this.btnGuardar.Text = "Guardar";

            this.btnEliminar.Location = new System.Drawing.Point(645, 240);
            this.btnEliminar.Size = new System.Drawing.Size(95, 30);
            this.btnEliminar.Text = "Eliminar";

            
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.dgvProductos);
            
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
    }
}

