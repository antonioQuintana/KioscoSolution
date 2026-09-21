using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    partial class UcVenta
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            dgvCarrito = new DataGridView();
            lblScanner = new Label();
            txtScanner = new TextBox();
            lblTotal = new Label();
            btnCobrar = new Button();
            btnCobroQR = new Button();
            btnCobroTarjeta = new Button();
            btnCuentaCorriente = new Button();
            btnCancelarVenta = new Button();
            btnArticulosFrecuentes = new Button();
            btnBuscarProducto = new Button();
            btnAplicarDescuento = new Button();
            btnSuspenderVenta = new Button();
            btnConsultarPrecio = new Button();
            btnRetiroCaja = new Button();
            btnAutorizacion = new Button();
            
            groupBoxCliente = new GroupBox();
            lblDniBusqueda = new Label();
            txtBusquedaCliente = new TextBox();
            btnBuscarCliente = new Button();
            lblClienteActual = new Label();
            btnNuevoCliente = new Button();
            lblSubtotal = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).BeginInit();
            groupBoxCliente.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCarrito
            // 
            dgvCarrito.Location = new Point(20, 140);
            dgvCarrito.Name = "dgvCarrito";
            dgvCarrito.ReadOnly = true;
            dgvCarrito.RowTemplate.Height = 35;
            dgvCarrito.Size = new Size(700, 520);
            dgvCarrito.TabIndex = 12;
            // 
            // lblScanner
            // 
            lblScanner.AutoSize = true;
            lblScanner.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblScanner.Location = new Point(20, 100);
            lblScanner.Name = "lblScanner";
            lblScanner.Size = new Size(214, 21);
            lblScanner.TabIndex = 14;
            lblScanner.Text = "Escanear Código de Barras:";
            // 
            // txtScanner
            // 
            txtScanner.Font = new Font("Segoe UI", 16F);
            txtScanner.Location = new Point(260, 95);
            txtScanner.Name = "txtScanner";
            txtScanner.Size = new Size(460, 36);
            txtScanner.TabIndex = 13;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblTotal.ForeColor = Color.DarkGreen;
            lblTotal.Location = new Point(740, 370);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(289, 59);
            lblTotal.TabIndex = 6;
            lblTotal.Text = "TOTAL: $0.00";
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.LightGreen;
            btnCobrar.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCobrar.ForeColor = SystemColors.ActiveCaptionText;
            btnCobrar.Location = new Point(740, 450);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(300, 55);
            btnCobrar.TabIndex = 3;
            btnCobrar.Text = "💵 COBRO EFECTIVO (F10)";
            btnCobrar.UseVisualStyleBackColor = false;
            // 
            // btnCobroQR
            // 
            btnCobroQR.BackColor = Color.PaleTurquoise;
            btnCobroQR.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCobroQR.ForeColor = SystemColors.ActiveCaptionText;
            btnCobroQR.Location = new Point(740, 515);
            btnCobroQR.Name = "btnCobroQR";
            btnCobroQR.Size = new Size(145, 50);
            btnCobroQR.TabIndex = 2;
            btnCobroQR.Text = "📱 COBRO QR";
            btnCobroQR.UseVisualStyleBackColor = false;
            // 
            // btnCobroTarjeta
            // 
            btnCobroTarjeta.BackColor = Color.Khaki;
            btnCobroTarjeta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCobroTarjeta.ForeColor = SystemColors.ActiveCaptionText;
            btnCobroTarjeta.Location = new Point(895, 515);
            btnCobroTarjeta.Name = "btnCobroTarjeta";
            btnCobroTarjeta.Size = new Size(145, 50);
            btnCobroTarjeta.TabIndex = 1;
            btnCobroTarjeta.Text = "💳 TARJETA";
            btnCobroTarjeta.UseVisualStyleBackColor = false;
            // 
            // btnCuentaCorriente
            // 
            btnCuentaCorriente.BackColor = Color.Plum;
            btnCuentaCorriente.ForeColor = SystemColors.ActiveCaptionText;
            btnCuentaCorriente.Location = new Point(740, 575);
            btnCuentaCorriente.Name = "btnCuentaCorriente";
            btnCuentaCorriente.Size = new Size(145, 40);
            btnCuentaCorriente.TabIndex = 4;
            btnCuentaCorriente.Text = "📝 Cuenta Corriente";
            btnCuentaCorriente.UseVisualStyleBackColor = false;
            // 
            // btnCancelarVenta
            // 
            btnCancelarVenta.BackColor = Color.LightCoral;
            btnCancelarVenta.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancelarVenta.ForeColor = SystemColors.ActiveCaptionText;
            btnCancelarVenta.Location = new Point(895, 575);
            btnCancelarVenta.Name = "btnCancelarVenta";
            btnCancelarVenta.Size = new Size(145, 40);
            btnCancelarVenta.TabIndex = 7;
            btnCancelarVenta.Text = "❌ Cancelar Venta";
            btnCancelarVenta.UseVisualStyleBackColor = false;
            // 
            // btnArticulosFrecuentes
            // 
            btnArticulosFrecuentes.BackColor = Color.LightSkyBlue;
            btnArticulosFrecuentes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnArticulosFrecuentes.ForeColor = SystemColors.ActiveCaptionText;
            btnArticulosFrecuentes.Location = new Point(740, 195);
            btnArticulosFrecuentes.Name = "btnArticulosFrecuentes";
            btnArticulosFrecuentes.Size = new Size(300, 45);
            btnArticulosFrecuentes.TabIndex = 10;
            btnArticulosFrecuentes.Text = "⭐ Artículos Frecuentes (F3)";
            btnArticulosFrecuentes.UseVisualStyleBackColor = false;
            // 
            // btnBuscarProducto
            // 
            btnBuscarProducto.BackColor = Color.LightSkyBlue;
            btnBuscarProducto.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnBuscarProducto.ForeColor = SystemColors.ActiveCaptionText;
            btnBuscarProducto.Location = new Point(740, 140);
            btnBuscarProducto.Name = "btnBuscarProducto";
            btnBuscarProducto.Size = new Size(300, 45);
            btnBuscarProducto.TabIndex = 11;
            btnBuscarProducto.Text = "🔍 Buscar Producto (F2)";
            btnBuscarProducto.UseVisualStyleBackColor = false;
            // 
            // btnAplicarDescuento
            // 
            btnAplicarDescuento.BackColor = Color.Gold;
            btnAplicarDescuento.ForeColor = SystemColors.ActiveCaptionText;
            btnAplicarDescuento.Location = new Point(740, 250);
            btnAplicarDescuento.Name = "btnAplicarDescuento";
            btnAplicarDescuento.Size = new Size(145, 45);
            btnAplicarDescuento.TabIndex = 9;
            btnAplicarDescuento.Text = "% Descuento";
            btnAplicarDescuento.UseVisualStyleBackColor = false;
            // 
            // btnSuspenderVenta
            // 
            btnSuspenderVenta.BackColor = Color.Orange;
            btnSuspenderVenta.ForeColor = SystemColors.ActiveCaptionText;
            btnSuspenderVenta.Location = new Point(895, 250);
            btnSuspenderVenta.Name = "btnSuspenderVenta";
            btnSuspenderVenta.Size = new Size(145, 45);
            btnSuspenderVenta.TabIndex = 8;
            btnSuspenderVenta.Text = "⏸ Suspender";
            btnSuspenderVenta.UseVisualStyleBackColor = false;
            // 
            // btnConsultarPrecio
            // 
            btnConsultarPrecio.BackColor = Color.LightYellow;
            btnConsultarPrecio.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConsultarPrecio.ForeColor = SystemColors.ActiveCaptionText;
            btnConsultarPrecio.Location = new Point(740, 85);
            btnConsultarPrecio.Name = "btnConsultarPrecio";
            btnConsultarPrecio.Size = new Size(145, 45);
            btnConsultarPrecio.TabIndex = 9;
            btnConsultarPrecio.Text = "Consulta Precio";
            btnConsultarPrecio.UseVisualStyleBackColor = false;
            // 
            // btnRetiroCaja
            // 
            btnRetiroCaja.BackColor = Color.DarkSeaGreen;
            btnRetiroCaja.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRetiroCaja.ForeColor = SystemColors.ActiveCaptionText;
            btnRetiroCaja.Location = new Point(895, 85);
            btnRetiroCaja.Name = "btnRetiroCaja";
            btnRetiroCaja.Size = new Size(145, 45);
            btnRetiroCaja.TabIndex = 10;
            btnRetiroCaja.Text = "Retiro / Gasto";
            btnRetiroCaja.UseVisualStyleBackColor = false;
            // 
            // btnAutorizacion
            // 
            btnAutorizacion.BackColor = Color.IndianRed;
            btnAutorizacion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAutorizacion.ForeColor = Color.White;
            btnAutorizacion.Location = new Point(740, 30);
            btnAutorizacion.Name = "btnAutorizacion";
            btnAutorizacion.Size = new Size(300, 45);
            btnAutorizacion.TabIndex = 11;
            btnAutorizacion.Text = "Autorizacion Supervisor";
            btnAutorizacion.UseVisualStyleBackColor = false;
            //
            // groupBoxCliente
            // 
            groupBoxCliente.Controls.Add(lblDniBusqueda);
            groupBoxCliente.Controls.Add(txtBusquedaCliente);
            groupBoxCliente.Controls.Add(btnBuscarCliente);
            groupBoxCliente.Controls.Add(lblClienteActual);
            groupBoxCliente.Controls.Add(btnNuevoCliente);
            groupBoxCliente.ForeColor = SystemColors.ControlLightLight;
            groupBoxCliente.Location = new Point(20, 10);
            groupBoxCliente.Name = "groupBoxCliente";
            groupBoxCliente.Size = new Size(1020, 70);
            groupBoxCliente.TabIndex = 0;
            groupBoxCliente.TabStop = false;
            groupBoxCliente.Text = "Datos del Cliente";
            // 
            // lblDniBusqueda
            // 
            lblDniBusqueda.AutoSize = true;
            lblDniBusqueda.Location = new Point(20, 30);
            lblDniBusqueda.Name = "lblDniBusqueda";
            lblDniBusqueda.Size = new Size(86, 15);
            lblDniBusqueda.TabIndex = 0;
            lblDniBusqueda.Text = "DNI / Teléfono:";
            // 
            // txtBusquedaCliente
            // 
            txtBusquedaCliente.Font = new Font("Segoe UI", 10F);
            txtBusquedaCliente.Location = new Point(120, 27);
            txtBusquedaCliente.Name = "txtBusquedaCliente";
            txtBusquedaCliente.Size = new Size(150, 25);
            txtBusquedaCliente.TabIndex = 1;
            // 
            // btnBuscarCliente
            // 
            btnBuscarCliente.BackColor = Color.LightGray;
            btnBuscarCliente.ForeColor = SystemColors.ActiveCaptionText;
            btnBuscarCliente.Location = new Point(280, 25);
            btnBuscarCliente.Name = "btnBuscarCliente";
            btnBuscarCliente.Size = new Size(100, 30);
            btnBuscarCliente.TabIndex = 2;
            btnBuscarCliente.Text = "Buscar (F4)";
            btnBuscarCliente.UseVisualStyleBackColor = false;
            // 
            // lblClienteActual
            // 
            lblClienteActual.AutoSize = true;
            lblClienteActual.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblClienteActual.ForeColor = Color.MediumBlue;
            lblClienteActual.Location = new Point(550, 25);
            lblClienteActual.Name = "lblClienteActual";
            lblClienteActual.Size = new Size(272, 25);
            lblClienteActual.TabIndex = 3;
            lblClienteActual.Text = "Cliente: CONSUMIDOR FINAL";
            // 
            // btnNuevoCliente
            // 
            btnNuevoCliente.BackColor = Color.PaleTurquoise;
            btnNuevoCliente.ForeColor = SystemColors.ActiveCaptionText;
            btnNuevoCliente.Location = new Point(390, 25);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.Size = new Size(140, 30);
            btnNuevoCliente.TabIndex = 4;
            btnNuevoCliente.Text = "+ Nuevo Cliente";
            btnNuevoCliente.UseVisualStyleBackColor = false;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI", 14F);
            lblSubtotal.Location = new Point(740, 320);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(167, 50);
            lblSubtotal.TabIndex = 5;
            lblSubtotal.Text = "Subtotal: $ 0.00\nDescuentos: $ 0.00";
            // 
            // UcVenta
            // 
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(groupBoxCliente);
            Controls.Add(btnCobroTarjeta);
            Controls.Add(btnCobroQR);
            Controls.Add(btnCobrar);
            Controls.Add(btnCuentaCorriente);
            Controls.Add(lblSubtotal);
            Controls.Add(lblTotal);
            Controls.Add(btnCancelarVenta);
            Controls.Add(btnSuspenderVenta);
            Controls.Add(btnConsultarPrecio);
            Controls.Add(btnRetiroCaja);
            Controls.Add(btnAutorizacion);
            
            Controls.Add(btnAplicarDescuento);
            Controls.Add(btnArticulosFrecuentes);
            Controls.Add(btnBuscarProducto);
            Controls.Add(dgvCarrito);
            Controls.Add(txtScanner);
            Controls.Add(lblScanner);
            Name = "UcVenta";
            Size = new Size(1060, 680);
            ((System.ComponentModel.ISupportInitialize)dgvCarrito).EndInit();
            groupBoxCliente.ResumeLayout(false);
            groupBoxCliente.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Label lblScanner;
        private System.Windows.Forms.TextBox txtScanner;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSubtotal;
        
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnCobroQR;
        private System.Windows.Forms.Button btnCobroTarjeta;
        private System.Windows.Forms.Button btnCuentaCorriente;
        private System.Windows.Forms.Button btnCancelarVenta;
        private System.Windows.Forms.Button btnArticulosFrecuentes;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.Button btnAplicarDescuento;
        private System.Windows.Forms.Button btnSuspenderVenta;
        private System.Windows.Forms.Button btnConsultarPrecio;
        private System.Windows.Forms.Button btnRetiroCaja;
        private System.Windows.Forms.Button btnAutorizacion;
        

        private System.Windows.Forms.GroupBox groupBoxCliente;
        private System.Windows.Forms.Label lblDniBusqueda;
        private System.Windows.Forms.TextBox txtBusquedaCliente;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label lblClienteActual;
        private System.Windows.Forms.Button btnNuevoCliente;
    }
}
