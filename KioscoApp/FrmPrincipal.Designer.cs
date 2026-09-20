namespace KioscoApp
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            pnlMenu = new Panel();
            BCierreCaja = new Button();
            btnCerrarSesion = new Button();
            BEstadisticas = new Button();
            BUsuarios = new Button();
            BProveedores = new Button();
            BClientes = new Button();
            BStock = new Button();
            BProductos = new Button();
            BCategorias = new Button();
            BVenta = new Button();
            pnlContent = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.FromArgb(45, 45, 48);
            pnlMenu.Controls.Add(BCierreCaja);
            pnlMenu.Controls.Add(btnCerrarSesion);
            pnlMenu.Controls.Add(BEstadisticas);
            pnlMenu.Controls.Add(BUsuarios);
            pnlMenu.Controls.Add(BProveedores);
            pnlMenu.Controls.Add(BClientes);
            pnlMenu.Controls.Add(BStock);
            pnlMenu.Controls.Add(BProductos);
            pnlMenu.Controls.Add(BCategorias);
            pnlMenu.Controls.Add(BVenta);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(1100, 700);
            pnlMenu.TabIndex = 1;
            // 
            // BCierreCaja
            // 
            BCierreCaja.Location = new Point(12, 520);
            BCierreCaja.Name = "BCierreCaja";
            BCierreCaja.Size = new Size(230, 45);
            BCierreCaja.TabIndex = 9;
            BCierreCaja.Text = "Cierre de Caja";
            BCierreCaja.UseVisualStyleBackColor = true;
            BCierreCaja.Click += BCierreCaja_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.BackColor = Color.IndianRed;
            btnCerrarSesion.Dock = DockStyle.Bottom;
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Location = new Point(0, 644);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(1100, 56);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // BEstadisticas
            // 
            BEstadisticas.Location = new Point(12, 340);
            BEstadisticas.Name = "BEstadisticas";
            BEstadisticas.Size = new Size(230, 45);
            BEstadisticas.TabIndex = 3;
            BEstadisticas.Text = "Estadisticas";
            BEstadisticas.UseVisualStyleBackColor = true;
            BEstadisticas.Click += BEstadisticas_Click;
            // 
            // BUsuarios
            // 
            BUsuarios.Location = new Point(12, 40);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new Size(230, 45);
            BUsuarios.TabIndex = 8;
            BUsuarios.Text = "Usuarios";
            BUsuarios.UseVisualStyleBackColor = true;
            BUsuarios.Click += BUsuarios_Click_1;
            // 
            // BProveedores
            // 
            BProveedores.Location = new Point(12, 220);
            BProveedores.Name = "BProveedores";
            BProveedores.Size = new Size(230, 45);
            BProveedores.TabIndex = 5;
            BProveedores.Text = "Proveedores";
            BProveedores.UseVisualStyleBackColor = true;
            BProveedores.Click += BProveedores_Click;
            // 
            // BClientes
            // 
            BClientes.Location = new Point(12, 460);
            BClientes.Name = "BClientes";
            BClientes.Size = new Size(230, 45);
            BClientes.TabIndex = 1;
            BClientes.Text = "Clientes";
            BClientes.UseVisualStyleBackColor = true;
            BClientes.Click += BClientes_Click;
            // 
            // BStock
            // 
            BStock.Location = new Point(12, 280);
            BStock.Name = "BStock";
            BStock.Size = new Size(230, 45);
            BStock.TabIndex = 4;
            BStock.Text = "Stock";
            BStock.UseVisualStyleBackColor = true;
            BStock.Click += BStock_Click;
            // 
            // BProductos
            // 
            BProductos.Location = new Point(12, 100);
            BProductos.Name = "BProductos";
            BProductos.Size = new Size(230, 45);
            BProductos.TabIndex = 7;
            BProductos.Text = "Productos";
            BProductos.UseVisualStyleBackColor = true;
            BProductos.Click += BProductos_Click;
            // 
            // BCategorias
            // 
            BCategorias.Location = new Point(12, 160);
            BCategorias.Name = "BCategorias";
            BCategorias.Size = new Size(230, 45);
            BCategorias.TabIndex = 6;
            BCategorias.Text = "Categorias";
            BCategorias.UseVisualStyleBackColor = true;
            BCategorias.Click += BCategorias_Click;
            // 
            // BVenta
            // 
            BVenta.Location = new Point(12, 400);
            BVenta.Name = "BVenta";
            BVenta.Size = new Size(230, 45);
            BVenta.TabIndex = 2;
            BVenta.Text = "Venta";
            BVenta.UseVisualStyleBackColor = true;
            BVenta.Click += BVenta_Click;
            // 
            // pnlContent
            // 
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1100, 700);
            pnlContent.TabIndex = 0;
            pnlContent.Paint += pnlContent_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1100, 700);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // FrmPrincipal
            // 
            ClientSize = new Size(1100, 700);
            Controls.Add(pnlMenu);
            Controls.Add(pnlContent);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Kiosco";
            WindowState = FormWindowState.Maximized;
            FormClosed += FrmPrincipal_FormClosed;
            pnlMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel pnlMenu;
        private Panel pnlContent;
        
        private Button btnCerrarSesion;
        private Button BUsuarios;
        private Button BProductos;
        private Button BCategorias;
        private Button BProveedores;
        private Button BStock;
        private Button BEstadisticas;
        private Button BVenta;
        private Button BClientes;
        private Button BCierreCaja;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
