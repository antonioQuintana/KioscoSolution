namespace KioscoApp
{
    partial class UcMovimientosStock
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
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            dgvStock = new DataGridView();
            groupBoxModificar = new GroupBox();
            btnGuardar = new Button();
            numStockMinimo = new NumericUpDown();
            lblStockMinimo = new Label();
            numStockActual = new NumericUpDown();
            lblStockActual = new Label();
            lblProductoSeleccionado = new Label();
            lblProductoLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            groupBoxModificar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numStockMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStockActual).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(312, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Control de Inventario (Stock)";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(20, 70);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(147, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar por SKU o Nombre:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(190, 67);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(300, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvStock
            // 
            dgvStock.AllowUserToAddRows = false;
            dgvStock.AllowUserToDeleteRows = false;
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Location = new Point(20, 110);
            dgvStock.MultiSelect = false;
            dgvStock.Name = "dgvStock";
            dgvStock.ReadOnly = true;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.Size = new Size(887, 480);
            dgvStock.TabIndex = 3;
            dgvStock.SelectionChanged += dgvStock_SelectionChanged;
            // 
            // groupBoxModificar
            // 
            groupBoxModificar.Controls.Add(btnGuardar);
            groupBoxModificar.Controls.Add(numStockMinimo);
            groupBoxModificar.Controls.Add(lblStockMinimo);
            groupBoxModificar.Controls.Add(numStockActual);
            groupBoxModificar.Controls.Add(lblStockActual);
            groupBoxModificar.Controls.Add(lblProductoSeleccionado);
            groupBoxModificar.Controls.Add(lblProductoLabel);
            groupBoxModificar.ForeColor = SystemColors.ControlLightLight;
            groupBoxModificar.Location = new Point(932, 110);
            groupBoxModificar.Name = "groupBoxModificar";
            groupBoxModificar.Size = new Size(280, 280);
            groupBoxModificar.TabIndex = 4;
            groupBoxModificar.TabStop = false;
            groupBoxModificar.Text = "Ajuste de Stock";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.MediumSeaGreen;
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 225);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(240, 40);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "💾 Guardar Stock";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // numStockMinimo
            // 
            numStockMinimo.Font = new Font("Segoe UI", 11F);
            numStockMinimo.Location = new Point(20, 180);
            numStockMinimo.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numStockMinimo.Name = "numStockMinimo";
            numStockMinimo.Size = new Size(120, 27);
            numStockMinimo.TabIndex = 5;
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(20, 160);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(84, 15);
            lblStockMinimo.TabIndex = 4;
            lblStockMinimo.Text = "Stock Mínimo:";
            // 
            // numStockActual
            // 
            numStockActual.Font = new Font("Segoe UI", 11F);
            numStockActual.Location = new Point(20, 120);
            numStockActual.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numStockActual.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            numStockActual.Name = "numStockActual";
            numStockActual.Size = new Size(120, 27);
            numStockActual.TabIndex = 3;
            // 
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Location = new Point(20, 100);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(76, 15);
            lblStockActual.TabIndex = 2;
            lblStockActual.Text = "Stock Actual:";
            // 
            // lblProductoSeleccionado
            // 
            lblProductoSeleccionado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblProductoSeleccionado.ForeColor = Color.LightSkyBlue;
            lblProductoSeleccionado.Location = new Point(20, 50);
            lblProductoSeleccionado.Name = "lblProductoSeleccionado";
            lblProductoSeleccionado.Size = new Size(240, 45);
            lblProductoSeleccionado.TabIndex = 1;
            lblProductoSeleccionado.Text = "Seleccione un producto";
            // 
            // lblProductoLabel
            // 
            lblProductoLabel.AutoSize = true;
            lblProductoLabel.Location = new Point(20, 30);
            lblProductoLabel.Name = "lblProductoLabel";
            lblProductoLabel.Size = new Size(59, 15);
            lblProductoLabel.TabIndex = 0;
            lblProductoLabel.Text = "Producto:";
            // 
            // UcMovimientosStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBoxModificar);
            Controls.Add(dgvStock);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(lblTitulo);
            Name = "UcMovimientosStock";
            Size = new Size(1241, 643);
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            groupBoxModificar.ResumeLayout(false);
            groupBoxModificar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numStockMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStockActual).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.GroupBox groupBoxModificar;
        private System.Windows.Forms.Label lblProductoLabel;
        private System.Windows.Forms.Label lblProductoSeleccionado;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.NumericUpDown numStockActual;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.NumericUpDown numStockMinimo;
        private System.Windows.Forms.Button btnGuardar;
    }
}
