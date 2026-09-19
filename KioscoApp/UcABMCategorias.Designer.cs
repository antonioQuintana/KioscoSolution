namespace KioscoApp
{
    partial class UcABMCategorias
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            dgvCategorias = new DataGridView();
            lblNombre = new Label();
            txtNombre = new TextBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).BeginInit();
            SuspendLayout();
            // 
            // dgvCategorias
            // 
            dgvCategorias.Location = new Point(20, 20);
            dgvCategorias.Name = "dgvCategorias";
            dgvCategorias.Size = new Size(300, 300);
            dgvCategorias.TabIndex = 4;
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(340, 20);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 23);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre de Categoría:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(340, 40);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 23);
            txtNombre.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(340, 80);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(445, 80);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 30);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            // 
            // UcABMCategorias
            // 
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(dgvCategorias);
            Name = "UcABMCategorias";
            Size = new Size(851, 502);
            ((System.ComponentModel.ISupportInitialize)dgvCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
    }
}

