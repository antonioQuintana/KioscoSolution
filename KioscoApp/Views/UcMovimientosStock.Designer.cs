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
            chkSoloAlertas = new CheckBox();
            lblResumenStock = new Label();
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
            txtBuscar.Size = new Size(280, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // chkSoloAlertas
            // 
            chkSoloAlertas.AutoSize = true;
            chkSoloAlertas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkSoloAlertas.ForeColor = Color.Gold;
            chkSoloAlertas.Location = new Point(490, 68);
            chkSoloAlertas.Name = "chkSoloAlertas";
            chkSoloAlertas.Size = new Size(240, 21);
            chkSoloAlertas.TabIndex = 5;
            chkSoloAlertas.Text = "⚠️ Solo Alertas (Bajo stock / Agotado)";
            chkSoloAlertas.UseVisualStyleBackColor = true;
            chkSoloAlertas.CheckedChanged += chkSoloAlertas_CheckedChanged;
            // 
            // dgvStock
            // 
            dgvStock.AllowUserToAddRows = false;
            dgvStock.AllowUserToDeleteRows = false;
            dgvStock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Location = new Point(20, 105);
            dgvStock.MultiSelect = false;
            dgvStock.Name = "dgvStock";
            dgvStock.ReadOnly = true;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.Size = new Size(650, 490);
            dgvStock.TabIndex = 3;
            dgvStock.SelectionChanged += dgvStock_SelectionChanged;
            // 
            // lblResumenStock
            // 
            lblResumenStock.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumenStock.AutoSize = true;
            lblResumenStock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResumenStock.ForeColor = Color.FromArgb(220, 220, 220);
            lblResumenStock.Location = new Point(20, 608);
            lblResumenStock.Name = "lblResumenStock";
            lblResumenStock.Size = new Size(280, 17);
            lblResumenStock.TabIndex = 6;
            lblResumenStock.Text = "Cargando estado del inventario...";
            // 
            // groupBoxModificar
            // 
            groupBoxModificar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxModificar.Controls.Add(btnGuardar);
            groupBoxModificar.Controls.Add(numStockMinimo);
            groupBoxModificar.Controls.Add(lblStockMinimo);
            groupBoxModificar.Controls.Add(numStockActual);
            groupBoxModificar.Controls.Add(lblStockActual);
            groupBoxModificar.Controls.Add(lblProductoSeleccionado);
            groupBoxModificar.Controls.Add(lblProductoLabel);
            groupBoxModificar.ForeColor = SystemColors.ControlLightLight;
            groupBoxModificar.Location = new Point(690, 105);
            groupBoxModificar.Name = "groupBoxModificar";
            groupBoxModificar.Size = new Size(290, 290);
            groupBoxModificar.TabIndex = 4;
            groupBoxModificar.TabStop = false;
            groupBoxModificar.Text = "Ajuste de Stock";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.MediumSeaGreen;
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.ForeColor = SystemColors.ButtonHighlight;
            btnGuardar.Location = new Point(20, 220);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(250, 45);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar Cambios";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // numStockMinimo
            // 
            numStockMinimo.Location = new Point(130, 157);
            numStockMinimo.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numStockMinimo.Name = "numStockMinimo";
            numStockMinimo.Size = new Size(140, 23);
            numStockMinimo.TabIndex = 5;
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(20, 160);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(85, 15);
            lblStockMinimo.TabIndex = 4;
            lblStockMinimo.Text = "Stock Mínimo:";
            // 
            // numStockActual
            // 
            numStockActual.Location = new Point(130, 107);
            numStockActual.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numStockActual.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            numStockActual.Name = "numStockActual";
            numStockActual.Size = new Size(140, 23);
            numStockActual.TabIndex = 3;
            // 
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Location = new Point(20, 110);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(76, 15);
            lblStockActual.TabIndex = 2;
            lblStockActual.Text = "Stock Actual:";
            // 
            // lblProductoSeleccionado
            // 
            lblProductoSeleccionado.AutoSize = true;
            lblProductoSeleccionado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductoSeleccionado.Location = new Point(20, 60);
            lblProductoSeleccionado.MaximumSize = new Size(250, 0);
            lblProductoSeleccionado.Name = "lblProductoSeleccionado";
            lblProductoSeleccionado.Size = new Size(175, 15);
            lblProductoSeleccionado.TabIndex = 1;
            lblProductoSeleccionado.Text = "Ningún producto seleccionado";
            // 
            // lblProductoLabel
            // 
            lblProductoLabel.AutoSize = true;
            lblProductoLabel.Location = new Point(20, 35);
            lblProductoLabel.Name = "lblProductoLabel";
            lblProductoLabel.Size = new Size(128, 15);
            lblProductoLabel.TabIndex = 0;
            lblProductoLabel.Text = "Producto seleccionado:";
            // 
            // UcMovimientosStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoScrollMinSize = new Size(950, 580);
            Controls.Add(lblResumenStock);
            Controls.Add(chkSoloAlertas);
            Controls.Add(groupBoxModificar);
            Controls.Add(dgvStock);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(lblTitulo);
            Name = "UcMovimientosStock";
            Size = new Size(1000, 640);
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
        private System.Windows.Forms.CheckBox chkSoloAlertas;
        private System.Windows.Forms.Label lblResumenStock;
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
