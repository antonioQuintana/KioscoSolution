namespace KioscoApp
{
    partial class UcABMCategorias
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
            dgvCategorias = new DataGridView();
            gbFormulario = new GroupBox();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            txtDescripcion = new TextBox();
            lblDesc = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            gbFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = SystemColors.ControlLightLight;
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(241, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Gestión de Categorías";
            // 
            // dgvCategorias
            // 
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategorias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategorias.Location = new Point(20, 70);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.ReadOnly = true;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.Size = new Size(600, 500);
            dgvCategorias.TabIndex = 1;
            dgvCategorias.CellClick += dgvCategorias_CellClick;
            // 
            // gbFormulario
            // 
            gbFormulario.Controls.Add(btnLimpiar);
            gbFormulario.Controls.Add(btnEliminar);
            gbFormulario.Controls.Add(btnGuardar);
            gbFormulario.Controls.Add(txtDescripcion);
            gbFormulario.Controls.Add(lblDesc);
            gbFormulario.Controls.Add(txtNombre);
            gbFormulario.Controls.Add(lblNombre);
            gbFormulario.Font = new Font("Segoe UI", 10F);
            gbFormulario.ForeColor = SystemColors.ControlLightLight;
            gbFormulario.Location = new Point(640, 70);
            gbFormulario.Name = "gbFormulario";
            gbFormulario.Size = new Size(320, 500);
            gbFormulario.TabIndex = 2;
            gbFormulario.TabStop = false;
            gbFormulario.Text = "Datos de la Categoría";
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = Color.FromArgb(149, 165, 166);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.ForeColor = Color.White;
            btnLimpiar.Location = new Point(20, 360);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(280, 45);
            btnLimpiar.TabIndex = 6;
            btnLimpiar.Text = "Limpiar Formulario";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(20, 300);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(280, 45);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "Eliminar Categoría";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 240);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(280, 45);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar Categoría";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(20, 135);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(280, 80);
            txtDescripcion.TabIndex = 3;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(20, 110);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(82, 19);
            lblDesc.TabIndex = 2;
            lblDesc.Text = "Descripción:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(20, 65);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(280, 25);
            txtNombre.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(20, 40);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(62, 19);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // UcABMCategorias
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(gbFormulario);
            Controls.Add(dgvCategorias);
            Controls.Add(lblTitulo);
            Name = "UcABMCategorias";
            Size = new Size(1247, 643);
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            gbFormulario.ResumeLayout(false);
            gbFormulario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.GroupBox gbFormulario;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}
