namespace KioscoApp
{
    partial class UcHistorialVentas
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
            lblTitulo = new System.Windows.Forms.Label();
            dgvHistorial = new System.Windows.Forms.DataGridView();
            lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitulo.Location = new System.Drawing.Point(30, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(281, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Historial de Ventas (Turno)";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblInfo.ForeColor = System.Drawing.Color.DimGray;
            lblInfo.Location = new System.Drawing.Point(30, 55);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new System.Drawing.Size(434, 19);
            lblInfo.TabIndex = 2;
            lblInfo.Text = "Aquí se muestran únicamente las ventas realizadas en tu turno actual.";
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorial.Location = new System.Drawing.Point(30, 90);
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new System.Drawing.Size(940, 520);
            dgvHistorial.TabIndex = 1;
            // 
            // UcHistorialVentas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lblInfo);
            Controls.Add(dgvHistorial);
            Controls.Add(lblTitulo);
            Name = "UcHistorialVentas";
            Size = new System.Drawing.Size(1000, 643);
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Label lblInfo;
    }
}
