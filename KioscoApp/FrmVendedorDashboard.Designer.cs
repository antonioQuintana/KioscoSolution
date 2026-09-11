namespace KioscoApp
{
    partial class FrmVendedorDashboard
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnVenta = new System.Windows.Forms.Button();
            this.btnABMClientes = new System.Windows.Forms.Button();
            this.btnCierreCaja = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVenta
            // 
            this.btnVenta.Location = new System.Drawing.Point(50, 50);
            this.btnVenta.Name = "btnVenta";
            this.btnVenta.Size = new System.Drawing.Size(150, 60);
            this.btnVenta.TabIndex = 0;
            this.btnVenta.Text = "Punto de Venta (Nueva Venta)";
            this.btnVenta.UseVisualStyleBackColor = true;
            this.btnVenta.Click += new System.EventHandler(this.BtnVenta_Click);
            // 
            // btnABMClientes
            // 
            this.btnABMClientes.Location = new System.Drawing.Point(220, 50);
            this.btnABMClientes.Name = "btnABMClientes";
            this.btnABMClientes.Size = new System.Drawing.Size(150, 60);
            this.btnABMClientes.TabIndex = 1;
            this.btnABMClientes.Text = "Gestión de Clientes";
            this.btnABMClientes.UseVisualStyleBackColor = true;
            this.btnABMClientes.Click += new System.EventHandler(this.BtnABMClientes_Click);
            // 
            // btnCierreCaja
            // 
            this.btnCierreCaja.Location = new System.Drawing.Point(390, 50);
            this.btnCierreCaja.Name = "btnCierreCaja";
            this.btnCierreCaja.Size = new System.Drawing.Size(150, 60);
            this.btnCierreCaja.TabIndex = 2;
            this.btnCierreCaja.Text = "Cierre de Caja";
            this.btnCierreCaja.UseVisualStyleBackColor = true;
            this.btnCierreCaja.Click += new System.EventHandler(this.BtnCierreCaja_Click);
            // 
            // FrmVendedorDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.btnCierreCaja);
            this.Controls.Add(this.btnABMClientes);
            this.Controls.Add(this.btnVenta);
            this.Name = "FrmVendedorDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Vendedor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmVendedorDashboard_FormClosed);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnVenta;
        private System.Windows.Forms.Button btnABMClientes;
        private System.Windows.Forms.Button btnCierreCaja;
    }
}
