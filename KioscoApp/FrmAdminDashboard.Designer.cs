namespace KioscoApp
{
    partial class FrmAdminDashboard
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.btnGestionUsuarios = new System.Windows.Forms.Button();
            this.btnABMProductos = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.btnCategorias = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            this.btnGestionUsuarios.Location = new System.Drawing.Point(50, 50);
            this.btnGestionUsuarios.Size = new System.Drawing.Size(150, 60);
            this.btnGestionUsuarios.Text = "Gestión de Usuarios";
            this.btnGestionUsuarios.Click += new System.EventHandler(this.BtnGestionUsuarios_Click);
            
            this.btnABMProductos.Location = new System.Drawing.Point(220, 50);
            this.btnABMProductos.Size = new System.Drawing.Size(150, 60);
            this.btnABMProductos.Text = "Gestión de Productos";
            this.btnABMProductos.Click += new System.EventHandler(this.BtnABMProductos_Click);
            
            this.btnEstadisticas.Location = new System.Drawing.Point(390, 50);
            this.btnEstadisticas.Size = new System.Drawing.Size(150, 60);
            this.btnEstadisticas.Text = "Estadísticas Ventas";
            this.btnEstadisticas.Click += new System.EventHandler(this.BtnEstadisticas_Click);

            this.btnProveedores.Location = new System.Drawing.Point(50, 130);
            this.btnProveedores.Size = new System.Drawing.Size(150, 60);
            this.btnProveedores.Text = "Proveedores";
            this.btnProveedores.Click += new System.EventHandler(this.BtnProveedores_Click);

            this.btnCategorias.Location = new System.Drawing.Point(220, 130);
            this.btnCategorias.Size = new System.Drawing.Size(150, 60);
            this.btnCategorias.Text = "Categorías";
            this.btnCategorias.Click += new System.EventHandler(this.BtnCategorias_Click);

            this.btnStock.Location = new System.Drawing.Point(390, 130);
            this.btnStock.Size = new System.Drawing.Size(150, 60);
            this.btnStock.Text = "Movimientos Stock";
            this.btnStock.Click += new System.EventHandler(this.BtnStock_Click);
            
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.btnStock);
            this.Controls.Add(this.btnCategorias);
            this.Controls.Add(this.btnProveedores);
            this.Controls.Add(this.btnEstadisticas);
            this.Controls.Add(this.btnABMProductos);
            this.Controls.Add(this.btnGestionUsuarios);
            this.Name = "FrmAdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Administrador";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAdminDashboard_FormClosed);
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Button btnGestionUsuarios;
        private System.Windows.Forms.Button btnABMProductos;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnProveedores;
        private System.Windows.Forms.Button btnCategorias;
        private System.Windows.Forms.Button btnStock;
    }
}
