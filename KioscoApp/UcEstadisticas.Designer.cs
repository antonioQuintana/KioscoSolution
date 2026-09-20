namespace KioscoApp
{
    partial class UcEstadisticas
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            lblProdMasVendido = new Label();
            lblDiaMasVentas = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblProdMasVendido
            // 
            lblProdMasVendido.Anchor = AnchorStyles.None;
            lblProdMasVendido.AutoSize = true;
            lblProdMasVendido.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblProdMasVendido.Location = new Point(152, 25);
            lblProdMasVendido.Name = "lblProdMasVendido";
            lblProdMasVendido.Size = new Size(545, 25);
            lblProdMasVendido.TabIndex = 1;
            lblProdMasVendido.Text = "Producto Más Vendido (Semanal): Cigarrillo Marlboro (210)";
            lblProdMasVendido.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDiaMasVentas
            // 
            lblDiaMasVentas.Anchor = AnchorStyles.None;
            lblDiaMasVentas.AutoSize = true;
            lblDiaMasVentas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDiaMasVentas.Location = new Point(217, 374);
            lblDiaMasVentas.Name = "lblDiaMasVentas";
            lblDiaMasVentas.Size = new Size(416, 25);
            lblDiaMasVentas.TabIndex = 3;
            lblDiaMasVentas.Text = "Dia Con Más Ventas (Última Semana): Sábado";
            lblDiaMasVentas.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblProdMasVendido, 0, 0);
            tableLayoutPanel1.Controls.Add(lblDiaMasVentas, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(850, 700);
            tableLayoutPanel1.TabIndex = 4;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // UcEstadisticas
            // 
            Controls.Add(tableLayoutPanel1);
            Name = "UcEstadisticas";
            Size = new Size(850, 700);
            Load += UcEstadisticas_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }
        private System.Windows.Forms.Label lblProdMasVendido;
        private Label lblDiaMasVentas;
        private TableLayoutPanel tableLayoutPanel1;
    }
}

