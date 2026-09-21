namespace KioscoApp
{
    partial class UcABMProductos
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            dgvProductos = new DataGridView();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            lblDesc = new Label();
            txtDesc = new TextBox();
            lblPrecio = new Label();
            txtPrecio = new TextBox();
            lblStock = new Label();
            txtStock = new TextBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // dgvProductos
            // 
            dgvProductos.Location = new Point(20, 20);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(500, 400);
            dgvProductos.TabIndex = 10;
            // 
            // lblCodigo
            // 
            lblCodigo.Location = new Point(540, 20);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(100, 23);
            lblCodigo.TabIndex = 9;
            lblCodigo.Text = "Código de Barras:";
            // 
            // txtCodigo
            // 
            txtCodigo.Location = new Point(540, 40);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(200, 23);
            txtCodigo.TabIndex = 8;
            // 
            // lblDesc
            // 
            lblDesc.Location = new Point(540, 70);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(100, 23);
            lblDesc.TabIndex = 7;
            lblDesc.Text = "Descripción:";
            // 
            // txtDesc
            // 
            txtDesc.Location = new Point(540, 90);
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new Size(200, 23);
            txtDesc.TabIndex = 6;
            // 
            // lblPrecio
            // 
            lblPrecio.Location = new Point(540, 120);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(100, 23);
            lblPrecio.TabIndex = 5;
            lblPrecio.Text = "Precio Venta:";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(540, 140);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(200, 23);
            txtPrecio.TabIndex = 4;
            // 
            // lblStock
            // 
            lblStock.Location = new Point(540, 170);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(100, 23);
            lblStock.TabIndex = 3;
            lblStock.Text = "Stock Actual:";
            // 
            // txtStock
            // 
            txtStock.Location = new Point(540, 190);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(200, 23);
            txtStock.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(540, 240);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(645, 240);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 30);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            // 
            // UcABMProductos
            // 
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtStock);
            Controls.Add(lblStock);
            Controls.Add(txtPrecio);
            Controls.Add(lblPrecio);
            Controls.Add(txtDesc);
            Controls.Add(lblDesc);
            Controls.Add(txtCodigo);
            Controls.Add(lblCodigo);
            Controls.Add(dgvProductos);
            Name = "UcABMProductos";
            Size = new Size(762, 354);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
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

