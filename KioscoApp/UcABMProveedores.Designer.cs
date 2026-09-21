namespace KioscoApp
{
    partial class UcABMProveedores
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            dgvProveedores = new DataGridView();
            lblRazonSocial = new Label();
            txtRazonSocial = new TextBox();
            lblCuit = new Label();
            txtCuit = new TextBox();
            lblTel = new Label();
            txtTel = new TextBox();
            btnGuardar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            SuspendLayout();
            // 
            // dgvProveedores
            // 
            dgvProveedores.Location = new Point(20, 20);
            dgvProveedores.Name = "dgvProveedores";
            dgvProveedores.Size = new Size(400, 300);
            dgvProveedores.TabIndex = 8;
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.Location = new Point(450, 20);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(100, 23);
            lblRazonSocial.TabIndex = 7;
            lblRazonSocial.Text = "Razón Social:";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(450, 40);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(200, 23);
            txtRazonSocial.TabIndex = 6;
            // 
            // lblCuit
            // 
            lblCuit.Location = new Point(450, 70);
            lblCuit.Name = "lblCuit";
            lblCuit.Size = new Size(100, 23);
            lblCuit.TabIndex = 5;
            lblCuit.Text = "CUIT/RUT:";
            // 
            // txtCuit
            // 
            txtCuit.Location = new Point(450, 90);
            txtCuit.Name = "txtCuit";
            txtCuit.Size = new Size(200, 23);
            txtCuit.TabIndex = 4;
            // 
            // lblTel
            // 
            lblTel.Location = new Point(450, 120);
            lblTel.Name = "lblTel";
            lblTel.Size = new Size(100, 23);
            lblTel.TabIndex = 3;
            lblTel.Text = "Teléfono:";
            // 
            // txtTel
            // 
            txtTel.Location = new Point(450, 140);
            txtTel.Name = "txtTel";
            txtTel.Size = new Size(200, 23);
            txtTel.TabIndex = 2;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(450, 200);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(555, 200);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 30);
            btnEliminar.TabIndex = 0;
            btnEliminar.Text = "Eliminar";
            // 
            // UcABMProveedores
            // 
            Controls.Add(btnEliminar);
            Controls.Add(btnGuardar);
            Controls.Add(txtTel);
            Controls.Add(lblTel);
            Controls.Add(txtCuit);
            Controls.Add(lblCuit);
            Controls.Add(txtRazonSocial);
            Controls.Add(lblRazonSocial);
            Controls.Add(dgvProveedores);
            Name = "UcABMProveedores";
            Size = new Size(769, 379);
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgvProveedores;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblCuit;
        private System.Windows.Forms.TextBox txtCuit;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
    }
}

