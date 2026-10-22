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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblProdMasVendido
            // 
            lblProdMasVendido.Anchor = AnchorStyles.None;
            lblProdMasVendido.AutoSize = true;
            lblProdMasVendido.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblProdMasVendido.Location = new Point(252, 12);
            lblProdMasVendido.Name = "lblProdMasVendido";
            lblProdMasVendido.Size = new Size(345, 25);
            lblProdMasVendido.TabIndex = 1;
            lblProdMasVendido.Text = "Producto Más Vendido por Categoría";
            lblProdMasVendido.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDiaMasVentas
            // 
            lblDiaMasVentas.Anchor = AnchorStyles.None;
            lblDiaMasVentas.AutoSize = true;
            lblDiaMasVentas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDiaMasVentas.Location = new Point(302, 62);
            lblDiaMasVentas.Name = "lblDiaMasVentas";
            lblDiaMasVentas.Size = new Size(246, 25);
            lblDiaMasVentas.TabIndex = 3;
            lblDiaMasVentas.Text = "Ingresos la Última Semana";
            lblDiaMasVentas.TextAlign = ContentAlignment.MiddleCenter;
            lblDiaMasVentas.Click += lblDiaMasVentas_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(lblProdMasVendido, 0, 0);
            tableLayoutPanel1.Controls.Add(lblDiaMasVentas, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 4);
            tableLayoutPanel1.Controls.Add(label2, 0, 6);
            tableLayoutPanel1.Controls.Add(label3, 0, 8);
            tableLayoutPanel1.Controls.Add(label4, 0, 10);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 12;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(850, 700);
            tableLayoutPanel1.TabIndex = 4;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(217, 112);
            label1.Name = "label1";
            label1.Size = new Size(416, 25);
            label1.TabIndex = 4;
            label1.Text = "Cantidad de Ventas por Intervalo de Horarios";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.Location = new Point(317, 162);
            label2.Name = "label2";
            label2.Size = new Size(216, 25);
            label2.TabIndex = 5;
            label2.Text = "Ingresos por Categoria";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label3.Location = new Point(285, 212);
            label3.Name = "label3";
            label3.Size = new Size(279, 25);
            label3.TabIndex = 6;
            label3.Text = "Ingresos por Método de Pago";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label4.Location = new Point(238, 262);
            label4.Name = "label4";
            label4.Size = new Size(374, 25);
            label4.TabIndex = 7;
            label4.Text = "Cantidad de Ventas por Método de Pago";
            label4.TextAlign = ContentAlignment.MiddleCenter;
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
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}

