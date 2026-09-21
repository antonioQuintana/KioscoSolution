namespace KioscoApp
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            pnlMenu = new System.Windows.Forms.Panel();
            BCierreCaja = new System.Windows.Forms.Button();
            BHistorialVentas = new System.Windows.Forms.Button();
            btnCerrarSesion = new System.Windows.Forms.Button();
            BEstadisticas = new System.Windows.Forms.Button();
            BUsuarios = new System.Windows.Forms.Button();
            BConfiguracion = new System.Windows.Forms.Button();
            BClientes = new System.Windows.Forms.Button();
            BStock = new System.Windows.Forms.Button();
            BProductos = new System.Windows.Forms.Button();
            BCategorias = new System.Windows.Forms.Button();
            BAuditoriaCajas = new System.Windows.Forms.Button();
            BVenta = new System.Windows.Forms.Button();
            pnlContent = new System.Windows.Forms.Panel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            pnlMenu.Controls.Add(btnCerrarSesion);
            pnlMenu.Controls.Add(BUsuarios);
            pnlMenu.Controls.Add(BConfiguracion);
            pnlMenu.Controls.Add(BProductos);
            pnlMenu.Controls.Add(BCategorias);
            pnlMenu.Controls.Add(BStock);
            pnlMenu.Controls.Add(BEstadisticas);
            pnlMenu.Controls.Add(BAuditoriaCajas);
            pnlMenu.Controls.Add(BVenta);
            pnlMenu.Controls.Add(BClientes);
            pnlMenu.Controls.Add(BCierreCaja);
            pnlMenu.Controls.Add(BHistorialVentas);
            pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            pnlMenu.Location = new System.Drawing.Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new System.Drawing.Size(250, 700);
            pnlMenu.TabIndex = 1;
            // 
            // BUsuarios
            // 
            BUsuarios.Location = new System.Drawing.Point(10, 20);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new System.Drawing.Size(230, 45);
            BUsuarios.TabIndex = 8;
            BUsuarios.Text = "Usuarios (Admin)";
            BUsuarios.UseVisualStyleBackColor = true;
            BUsuarios.Click += BUsuarios_Click_1;
            // 
            // BConfiguracion
            // 
            BConfiguracion.Location = new System.Drawing.Point(10, 75);
            BConfiguracion.Name = "BConfiguracion";
            BConfiguracion.Size = new System.Drawing.Size(230, 45);
            BConfiguracion.TabIndex = 10;
            BConfiguracion.Text = "Configuración (Admin)";
            BConfiguracion.UseVisualStyleBackColor = true;
            BConfiguracion.Click += BConfiguracion_Click;
            // 
            // BProductos
            // 
            BProductos.Location = new System.Drawing.Point(10, 130);
            BProductos.Name = "BProductos";
            BProductos.Size = new System.Drawing.Size(230, 45);
            BProductos.TabIndex = 7;
            BProductos.Text = "Productos";
            BProductos.UseVisualStyleBackColor = true;
            BProductos.Click += BProductos_Click;
            // 
            // BCategorias
            // 
            BCategorias.Location = new System.Drawing.Point(10, 185);
            BCategorias.Name = "BCategorias";
            BCategorias.Size = new System.Drawing.Size(230, 45);
            BCategorias.TabIndex = 6;
            BCategorias.Text = "Categorias";
            BCategorias.UseVisualStyleBackColor = true;
            BCategorias.Click += BCategorias_Click;
            // 
            // 
            // 
            // BStock
            // 
            BStock.Location = new System.Drawing.Point(10, 295);
            BStock.Name = "BStock";
            BStock.Size = new System.Drawing.Size(230, 45);
            BStock.TabIndex = 4;
            BStock.Text = "Stock";
            BStock.UseVisualStyleBackColor = true;
            BStock.Click += BStock_Click;
            // 
            // BEstadisticas
            // 
            BEstadisticas.Location = new System.Drawing.Point(10, 350);
            BEstadisticas.Name = "BEstadisticas";
            BEstadisticas.Size = new System.Drawing.Size(230, 45);
            BEstadisticas.TabIndex = 3;
            BEstadisticas.Text = "Estadisticas";
            BEstadisticas.UseVisualStyleBackColor = true;
            BEstadisticas.Click += BEstadisticas_Click;
            // 
            // BAuditoriaCajas
            // 
            BAuditoriaCajas.Location = new System.Drawing.Point(10, 405);
            BAuditoriaCajas.Name = "BAuditoriaCajas";
            BAuditoriaCajas.Size = new System.Drawing.Size(230, 45);
            BAuditoriaCajas.TabIndex = 11;
            BAuditoriaCajas.Text = "Auditoría Cajas (Sup)";
            BAuditoriaCajas.UseVisualStyleBackColor = true;
            BAuditoriaCajas.Click += BAuditoriaCajas_Click;
            // 
            // BVenta
            // 
            BVenta.Location = new System.Drawing.Point(10, 460);
            BVenta.Name = "BVenta";
            BVenta.Size = new System.Drawing.Size(230, 45);
            BVenta.TabIndex = 2;
            BVenta.Text = "Venta";
            BVenta.UseVisualStyleBackColor = true;
            BVenta.Click += BVenta_Click;
            // 
            // BClientes
            // 
            BClientes.Location = new System.Drawing.Point(10, 515);
            BClientes.Name = "BClientes";
            BClientes.Size = new System.Drawing.Size(230, 45);
            BClientes.TabIndex = 1;
            BClientes.Text = "Clientes";
            BClientes.UseVisualStyleBackColor = true;
            BClientes.Click += BClientes_Click;
            // 
            // BCierreCaja
            // 
            BCierreCaja.Location = new System.Drawing.Point(10, 570);
            BCierreCaja.Name = "BCierreCaja";
            BCierreCaja.Size = new System.Drawing.Size(230, 45);
            BCierreCaja.TabIndex = 9;
            BCierreCaja.Text = "Cierre de Caja";
            BCierreCaja.UseVisualStyleBackColor = true;
            BCierreCaja.Click += BCierreCaja_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.BackColor = System.Drawing.Color.IndianRed;
            btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            btnCerrarSesion.Location = new System.Drawing.Point(0, 644);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new System.Drawing.Size(250, 56);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // 
            // BHistorialVentas
            // 
            BHistorialVentas.Location = new System.Drawing.Point(10, 625);
            
            
            
            
            BHistorialVentas.Name = "BHistorialVentas";
            BHistorialVentas.Size = new System.Drawing.Size(230, 45);
            BHistorialVentas.TabIndex = 8;
            BHistorialVentas.Text = "Historial de Ventas";
            BHistorialVentas.UseVisualStyleBackColor = true;
            BHistorialVentas.Click += BHistorialVentas_Click;
            // pnlContent
            // 
            pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlContent.Location = new System.Drawing.Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new System.Drawing.Size(1100, 700);
            pnlContent.TabIndex = 0;
            pnlContent.Paint += pnlContent_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1100, 700);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // FrmPrincipal
            // 
            ClientSize = new System.Drawing.Size(1100, 700);
            Controls.Add(pnlMenu);
            Controls.Add(pnlContent);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "FrmPrincipal";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sistema Kiosco";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormClosed += FrmPrincipal_FormClosed;
            pnlMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlContent;
        
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button BUsuarios;
        private System.Windows.Forms.Button BConfiguracion;
        private System.Windows.Forms.Button BProductos;
        private System.Windows.Forms.Button BCategorias;
        private System.Windows.Forms.Button BStock;
        private System.Windows.Forms.Button BEstadisticas;
        private System.Windows.Forms.Button BAuditoriaCajas;
        private System.Windows.Forms.Button BVenta;
        private System.Windows.Forms.Button BClientes;
        private System.Windows.Forms.Button BCierreCaja;
        private System.Windows.Forms.Button BHistorialVentas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
