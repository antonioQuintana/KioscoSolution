namespace KioscoApp
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnCategorias = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnVenta = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnCierreCaja = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            
            // pnlMenu
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Size = new System.Drawing.Size(250, 700);
            
            // Botones
            int yPos = 20;
            System.Windows.Forms.Button[] btns = { btnVenta, btnCierreCaja, btnClientes, btnUsuarios, btnProductos, btnCategorias, btnProveedores, btnStock, btnEstadisticas };
            String[] texts = { "Punto de Venta", "Cierre de Caja", "Clientes", "Usuarios (Admin)", "Productos (Admin)", "Categorías (Admin)", "Proveedores (Admin)", "Stock (Admin)", "Estadísticas (Admin)" };
            
            for(int i = 0; i < btns.Length; i++) {
                btns[i].Location = new System.Drawing.Point(10, yPos);
                btns[i].Size = new System.Drawing.Size(230, 45);
                btns[i].Text = texts[i];
                btns[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btns[i].ForeColor = System.Drawing.Color.White;
                this.pnlMenu.Controls.Add(btns[i]);
                yPos += 55;
            }
            
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            this.btnCategorias.Click += new System.EventHandler(this.btnCategorias_Click);
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            this.btnStock.Click += new System.EventHandler(this.btnStock_Click);
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            this.btnVenta.Click += new System.EventHandler(this.btnVenta_Click);
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            this.btnCierreCaja.Click += new System.EventHandler(this.btnCierreCaja_Click);
            
            // btnCerrarSesion
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCerrarSesion.Size = new System.Drawing.Size(250, 50);
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.BackColor = System.Drawing.Color.IndianRed;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            this.pnlMenu.Controls.Add(this.btnCerrarSesion);
            
            // pnlContent
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(250, 0);
            
            // FrmPrincipal
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlMenu);
            this.Name = "FrmPrincipal";
            this.Text = "Sistema Kiosco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPrincipal_FormClosed);
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnCategorias;
        private System.Windows.Forms.Button btnProveedores;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnVenta;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnCierreCaja;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}
