namespace KioscoApp
{
    partial class UcABMProductos
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
            lblTitulo = new Label();
            dgvProductos = new DataGridView();
            gbFormulario = new GroupBox();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            txtStockMin = new TextBox();
            lblStockMin = new Label();
            txtStock = new TextBox();
            lblStock = new Label();
            txtVenta = new TextBox();
            lblVenta = new Label();
            txtCosto = new TextBox();
            lblCosto = new Label();
            cmbCategoria = new ComboBox();
            lblCategoria = new Label();
            txtDesc = new TextBox();
            lblDesc = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            txtSKU = new TextBox();
            lblSKU = new Label();
            gbHerramientas = new GroupBox();
            btnExportarExcel = new Button();
            btnImportarExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            gbFormulario.SuspendLayout();
            gbHerramientas.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(250, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Catálogo de Productos";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(20, 50);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(834, 480);
            dgvProductos.TabIndex = 1;
            dgvProductos.CellClick += dgvProductos_CellClick;
            // 
            // gbFormulario
            // 
            gbFormulario.Controls.Add(btnLimpiar);
            gbFormulario.Controls.Add(btnEliminar);
            gbFormulario.Controls.Add(btnGuardar);
            gbFormulario.Controls.Add(txtStockMin);
            gbFormulario.Controls.Add(lblStockMin);
            gbFormulario.Controls.Add(txtStock);
            gbFormulario.Controls.Add(lblStock);
            gbFormulario.Controls.Add(txtVenta);
            gbFormulario.Controls.Add(lblVenta);
            gbFormulario.Controls.Add(txtCosto);
            gbFormulario.Controls.Add(lblCosto);
            gbFormulario.Controls.Add(cmbCategoria);
            gbFormulario.Controls.Add(lblCategoria);
            gbFormulario.Controls.Add(txtDesc);
            gbFormulario.Controls.Add(lblDesc);
            gbFormulario.Controls.Add(txtNombre);
            gbFormulario.Controls.Add(lblNombre);
            gbFormulario.Controls.Add(txtSKU);
            gbFormulario.Controls.Add(lblSKU);
            gbFormulario.Font = new Font("Segoe UI", 10F);
            gbFormulario.ForeColor = SystemColors.ControlLightLight;
            gbFormulario.Location = new Point(873, 40);
            gbFormulario.Name = "gbFormulario";
            gbFormulario.Size = new Size(360, 580);
            gbFormulario.TabIndex = 2;
            gbFormulario.TabStop = false;
            gbFormulario.Text = "Detalles del Producto";
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(149, 165, 166);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(20, 500);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(320, 45);
            btnLimpiar.TabIndex = 18;
            btnLimpiar.Text = "Limpiar Formulario";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(20, 440);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(320, 45);
            btnEliminar.TabIndex = 17;
            btnEliminar.Text = "Eliminar Producto";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 380);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(320, 45);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar Producto";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtStockMin
            // 
            txtStockMin.Location = new Point(120, 317);
            txtStockMin.Name = "txtStockMin";
            txtStockMin.Size = new Size(100, 25);
            txtStockMin.TabIndex = 15;
            // 
            // lblStockMin
            // 
            lblStockMin.AutoSize = true;
            lblStockMin.Location = new Point(20, 320);
            lblStockMin.Name = "lblStockMin";
            lblStockMin.Size = new Size(96, 19);
            lblStockMin.TabIndex = 14;
            lblStockMin.Text = "Stock Mínimo:";
            // 
            // txtStock
            // 
            txtStock.Location = new Point(120, 277);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(100, 25);
            txtStock.TabIndex = 13;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(20, 280);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(87, 19);
            lblStock.TabIndex = 12;
            lblStock.Text = "Stock Actual:";
            // 
            // txtVenta
            // 
            txtVenta.Location = new Point(120, 237);
            txtVenta.Name = "txtVenta";
            txtVenta.Size = new Size(100, 25);
            txtVenta.TabIndex = 11;
            // 
            // lblVenta
            // 
            lblVenta.AutoSize = true;
            lblVenta.Location = new Point(20, 240);
            lblVenta.Name = "lblVenta";
            lblVenta.Size = new Size(62, 19);
            lblVenta.TabIndex = 10;
            lblVenta.Text = "P. Venta:";
            // 
            // txtCosto
            // 
            txtCosto.Location = new Point(120, 197);
            txtCosto.Name = "txtCosto";
            txtCosto.Size = new Size(100, 25);
            txtCosto.TabIndex = 9;
            // 
            // lblCosto
            // 
            lblCosto.AutoSize = true;
            lblCosto.Location = new Point(20, 200);
            lblCosto.Name = "lblCosto";
            lblCosto.Size = new Size(63, 19);
            lblCosto.TabIndex = 8;
            lblCosto.Text = "P. Costo:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(120, 157);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(220, 25);
            cmbCategoria.TabIndex = 7;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(20, 160);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(71, 19);
            lblCategoria.TabIndex = 6;
            lblCategoria.Text = "Categoría:";
            // 
            // txtDesc
            // 
            txtDesc.Location = new Point(120, 117);
            txtDesc.Name = "txtDesc";
            txtDesc.Size = new Size(220, 25);
            txtDesc.TabIndex = 5;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(20, 120);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(82, 19);
            lblDesc.TabIndex = 4;
            lblDesc.Text = "Descripción:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(120, 77);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(220, 25);
            txtNombre.TabIndex = 3;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 80);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(62, 19);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre:";
            // 
            // txtSKU
            // 
            txtSKU.Location = new Point(120, 37);
            txtSKU.Name = "txtSKU";
            txtSKU.Size = new Size(220, 25);
            txtSKU.TabIndex = 1;
            // 
            // lblSKU
            // 
            lblSKU.AutoSize = true;
            lblSKU.Location = new Point(20, 40);
            lblSKU.Name = "lblSKU";
            lblSKU.Size = new Size(37, 19);
            lblSKU.TabIndex = 0;
            lblSKU.Text = "SKU:";
            // 
            // gbHerramientas
            // 
            gbHerramientas.Controls.Add(btnExportarExcel);
            gbHerramientas.Controls.Add(btnImportarExcel);
            gbHerramientas.ForeColor = SystemColors.ControlLightLight;
            gbHerramientas.Location = new Point(127, 540);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new Size(580, 80);
            gbHerramientas.TabIndex = 3;
            gbHerramientas.TabStop = false;
            gbHerramientas.Text = "Herramientas Masivas (Excel)";
            // 
            // btnExportarExcel
            // 
            btnExportarExcel.BackColor = Color.FromArgb(41, 128, 185);
            btnExportarExcel.FlatStyle = FlatStyle.Flat;
            btnExportarExcel.ForeColor = Color.White;
            btnExportarExcel.Location = new Point(20, 30);
            btnExportarExcel.Name = "btnExportarExcel";
            btnExportarExcel.Size = new Size(250, 35);
            btnExportarExcel.TabIndex = 0;
            btnExportarExcel.Text = "Descargar Plantilla Excel";
            btnExportarExcel.UseVisualStyleBackColor = false;
            btnExportarExcel.Click += btnExportarExcel_Click;
            // 
            // btnImportarExcel
            // 
            btnImportarExcel.BackColor = Color.FromArgb(39, 174, 96);
            btnImportarExcel.FlatStyle = FlatStyle.Flat;
            btnImportarExcel.ForeColor = Color.White;
            btnImportarExcel.Location = new Point(310, 30);
            btnImportarExcel.Name = "btnImportarExcel";
            btnImportarExcel.Size = new Size(250, 35);
            btnImportarExcel.TabIndex = 1;
            btnImportarExcel.Text = "Subir y Actualizar Precios";
            btnImportarExcel.UseVisualStyleBackColor = false;
            btnImportarExcel.Click += btnImportarExcel_Click;
            // 
            // UcABMProductos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(gbFormulario);
            Controls.Add(gbHerramientas);
            Controls.Add(dgvProductos);
            Controls.Add(lblTitulo);
            Name = "UcABMProductos";
            Size = new Size(1254, 643);
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            gbFormulario.ResumeLayout(false);
            gbFormulario.PerformLayout();
            gbHerramientas.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox gbFormulario;
        private System.Windows.Forms.Label lblSKU;
        private System.Windows.Forms.TextBox txtSKU;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.TextBox txtVenta;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblStockMin;
        private System.Windows.Forms.TextBox txtStockMin;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.GroupBox gbHerramientas;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnImportarExcel;
    }
}
