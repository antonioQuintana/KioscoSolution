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
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.lblScanner = new System.Windows.Forms.Label();
            this.txtScanner = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            
            this.btnCobrar = new System.Windows.Forms.Button(); 
            this.btnCobroQR = new System.Windows.Forms.Button();
            this.btnCobroTarjeta = new System.Windows.Forms.Button();
            this.btnCuentaCorriente = new System.Windows.Forms.Button();
            
            this.btnCancelarVenta = new System.Windows.Forms.Button();
            this.btnArticulosFrecuentes = new System.Windows.Forms.Button();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.btnAplicarDescuento = new System.Windows.Forms.Button();
            this.btnSuspenderVenta = new System.Windows.Forms.Button();

            // Panel Cliente
            this.groupBoxCliente = new System.Windows.Forms.GroupBox();
            this.lblDniBusqueda = new System.Windows.Forms.Label();
            this.txtBusquedaCliente = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.lblClienteActual = new System.Windows.Forms.Label();
            this.btnNuevoCliente = new System.Windows.Forms.Button();

            this.lblSubtotal = new System.Windows.Forms.Label();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).BeginInit();
            this.groupBoxCliente.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // SECCIÓN CLIENTE (Arriba)
            // ==========================================
            this.groupBoxCliente.Location = new System.Drawing.Point(20, 10);
            this.groupBoxCliente.Size = new System.Drawing.Size(1020, 70);
            this.groupBoxCliente.Text = "Datos del Cliente";
            this.groupBoxCliente.Controls.Add(this.lblDniBusqueda);
            this.groupBoxCliente.Controls.Add(this.txtBusquedaCliente);
            this.groupBoxCliente.Controls.Add(this.btnBuscarCliente);
            this.groupBoxCliente.Controls.Add(this.lblClienteActual);
            this.groupBoxCliente.Controls.Add(this.btnNuevoCliente);

            this.lblDniBusqueda.Location = new System.Drawing.Point(20, 30);
            this.lblDniBusqueda.Text = "DNI / Teléfono:";
            this.lblDniBusqueda.AutoSize = true;

            this.txtBusquedaCliente.Location = new System.Drawing.Point(120, 27);
            this.txtBusquedaCliente.Size = new System.Drawing.Size(150, 25);
            this.txtBusquedaCliente.Font = new System.Drawing.Font("Segoe UI", 10F);

            this.btnBuscarCliente.Location = new System.Drawing.Point(280, 25);
            this.btnBuscarCliente.Size = new System.Drawing.Size(100, 30);
            this.btnBuscarCliente.Text = "Buscar (F4)";
            this.btnBuscarCliente.BackColor = System.Drawing.Color.LightGray;

            this.btnNuevoCliente.Location = new System.Drawing.Point(390, 25);
            this.btnNuevoCliente.Size = new System.Drawing.Size(140, 30);
            this.btnNuevoCliente.Text = "+ Nuevo Cliente";
            this.btnNuevoCliente.BackColor = System.Drawing.Color.PaleTurquoise;

            this.lblClienteActual.Location = new System.Drawing.Point(550, 25);
            this.lblClienteActual.AutoSize = true;
            this.lblClienteActual.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblClienteActual.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblClienteActual.Text = "Cliente: CONSUMIDOR FINAL";

            // ==========================================
            // SECCIÓN CARRITO (Izquierda)
            // ==========================================
            this.lblScanner.Location = new System.Drawing.Point(20, 100);
            this.lblScanner.Text = "Escanear Código de Barras:";
            this.lblScanner.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScanner.AutoSize = true;

            this.txtScanner.Location = new System.Drawing.Point(260, 95);
            this.txtScanner.Size = new System.Drawing.Size(460, 35);
            this.txtScanner.Font = new System.Drawing.Font("Segoe UI", 16F);

            this.dgvCarrito.Location = new System.Drawing.Point(20, 140);
            this.dgvCarrito.Size = new System.Drawing.Size(700, 520);
            this.dgvCarrito.RowTemplate.Height = 35; // Filas más grandes para POS
            this.dgvCarrito.ReadOnly = true;

            // ==========================================
            // SECCIÓN BOTONERA Y TOTALES (Derecha)
            // ==========================================
            int rightX = 740;
            
            // Botones de Acción
            this.btnBuscarProducto.Location = new System.Drawing.Point(rightX, 140);
            this.btnBuscarProducto.Size = new System.Drawing.Size(300, 45);
            this.btnBuscarProducto.Text = "🔍 Buscar Producto (F2)";
            this.btnBuscarProducto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBuscarProducto.BackColor = System.Drawing.Color.LightSkyBlue;

            this.btnArticulosFrecuentes.Location = new System.Drawing.Point(rightX, 195);
            this.btnArticulosFrecuentes.Size = new System.Drawing.Size(300, 45);
            this.btnArticulosFrecuentes.Text = "⭐ Artículos Frecuentes (F3)";
            this.btnArticulosFrecuentes.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnArticulosFrecuentes.BackColor = System.Drawing.Color.LightSkyBlue;

            this.btnAplicarDescuento.Location = new System.Drawing.Point(rightX, 250);
            this.btnAplicarDescuento.Size = new System.Drawing.Size(145, 45);
            this.btnAplicarDescuento.Text = "% Descuento";
            this.btnAplicarDescuento.BackColor = System.Drawing.Color.Gold;

            this.btnSuspenderVenta.Location = new System.Drawing.Point(rightX + 155, 250);
            this.btnSuspenderVenta.Size = new System.Drawing.Size(145, 45);
            this.btnSuspenderVenta.Text = "⏸ Suspender";
            this.btnSuspenderVenta.BackColor = System.Drawing.Color.Orange;

            // Totales
            this.lblSubtotal.Location = new System.Drawing.Point(rightX, 320);
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblSubtotal.Text = "Subtotal: $ 0.00\nDescuentos: $ 0.00";

            this.lblTotal.Location = new System.Drawing.Point(rightX, 370);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotal.Text = "TOTAL: $0.00";

            // Botones de Pago
            this.btnCobrar.Location = new System.Drawing.Point(rightX, 450);
            this.btnCobrar.Size = new System.Drawing.Size(300, 55);
            this.btnCobrar.Text = "💵 COBRO EFECTIVO (F10)";
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCobrar.BackColor = System.Drawing.Color.LightGreen;

            this.btnCobroQR.Location = new System.Drawing.Point(rightX, 515);
            this.btnCobroQR.Size = new System.Drawing.Size(145, 50);
            this.btnCobroQR.Text = "📱 COBRO QR";
            this.btnCobroQR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCobroQR.BackColor = System.Drawing.Color.PaleTurquoise;

            this.btnCobroTarjeta.Location = new System.Drawing.Point(rightX + 155, 515);
            this.btnCobroTarjeta.Size = new System.Drawing.Size(145, 50);
            this.btnCobroTarjeta.Text = "💳 TARJETA";
            this.btnCobroTarjeta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCobroTarjeta.BackColor = System.Drawing.Color.Khaki;

            this.btnCuentaCorriente.Location = new System.Drawing.Point(rightX, 575);
            this.btnCuentaCorriente.Size = new System.Drawing.Size(145, 40);
            this.btnCuentaCorriente.Text = "📝 Cuenta Corriente";
            this.btnCuentaCorriente.BackColor = System.Drawing.Color.Plum;

            this.btnCancelarVenta.Location = new System.Drawing.Point(rightX + 155, 575);
            this.btnCancelarVenta.Size = new System.Drawing.Size(145, 40);
            this.btnCancelarVenta.Text = "❌ Cancelar Venta";
            this.btnCancelarVenta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelarVenta.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancelarVenta.ForeColor = System.Drawing.Color.White;

            
            this.Controls.Add(this.groupBoxCliente);
            this.Controls.Add(this.btnCobroTarjeta);
            this.Controls.Add(this.btnCobroQR);
            this.Controls.Add(this.btnCobrar);
            this.Controls.Add(this.btnCuentaCorriente);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnCancelarVenta);
            this.Controls.Add(this.btnSuspenderVenta);
            this.Controls.Add(this.btnAplicarDescuento);
            this.Controls.Add(this.btnArticulosFrecuentes);
            this.Controls.Add(this.btnBuscarProducto);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.txtScanner);
            this.Controls.Add(this.lblScanner);
            
            // Configurar el tamaño del control principal para pantalla completa
            this.Size = new System.Drawing.Size(1060, 680);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrito)).EndInit();
            this.groupBoxCliente.ResumeLayout(false);
            this.groupBoxCliente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
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

        private System.Windows.Forms.GroupBox groupBoxCliente;
        private System.Windows.Forms.Label lblDniBusqueda;
        private System.Windows.Forms.TextBox txtBusquedaCliente;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label lblClienteActual;
        private System.Windows.Forms.Button btnNuevoCliente;
    }
}
