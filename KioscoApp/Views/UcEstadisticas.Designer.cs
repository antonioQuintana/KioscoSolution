namespace KioscoApp
{
    partial class UcEstadisticas
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            dtpDesdeProd1 = new DateTimePicker();
            label3 = new Label();
            dtpHastaProd1 = new DateTimePicker();
            btnFiltrar1 = new Button();
            flowLayoutPanel6 = new FlowLayoutPanel();
            label16 = new Label();
            label17 = new Label();
            dtpDesdeProd6 = new DateTimePicker();
            label18 = new Label();
            dtpHastaProd6 = new DateTimePicker();
            btnFiltrar6 = new Button();
            flowLayoutPanel5 = new FlowLayoutPanel();
            label13 = new Label();
            label14 = new Label();
            dtpDesdeProd5 = new DateTimePicker();
            label15 = new Label();
            dtpHastaProd5 = new DateTimePicker();
            btnFiltrar5 = new Button();
            flowLayoutPanel4 = new FlowLayoutPanel();
            label10 = new Label();
            label11 = new Label();
            dtpDesdeProd4 = new DateTimePicker();
            label12 = new Label();
            dtpHastaProd4 = new DateTimePicker();
            btnFiltrar4 = new Button();
            flowLayoutPanel3 = new FlowLayoutPanel();
            label7 = new Label();
            label8 = new Label();
            dtpDesdeProd3 = new DateTimePicker();
            label9 = new Label();
            dtpHastaProd3 = new DateTimePicker();
            btnFiltrar3 = new Button();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            dtpDesdeProd2 = new DateTimePicker();
            label6 = new Label();
            dtpHastaProd2 = new DateTimePicker();
            btnFiltrar2 = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 0, 4);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel4, 0, 6);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel5, 0, 8);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel6, 0, 10);
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.None;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(dtpDesdeProd1);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(dtpHastaProd1);
            flowLayoutPanel1.Controls.Add(btnFiltrar1);
            flowLayoutPanel1.Location = new Point(43, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(764, 25);
            flowLayoutPanel1.TabIndex = 17;
            flowLayoutPanel1.WrapContents = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(361, 25);
            label1.TabIndex = 4;
            label1.Text = "Productos Más Vendidos por Categoría";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(370, 6);
            label2.Margin = new Padding(3, 6, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 19);
            label2.TabIndex = 5;
            label2.Text = "Desde:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpDesdeProd1
            // 
            dtpDesdeProd1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpDesdeProd1.Format = DateTimePickerFormat.Short;
            dtpDesdeProd1.Location = new Point(430, 3);
            dtpDesdeProd1.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpDesdeProd1.Name = "dtpDesdeProd1";
            dtpDesdeProd1.Size = new Size(99, 23);
            dtpDesdeProd1.TabIndex = 6;
            dtpDesdeProd1.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(535, 6);
            label3.Margin = new Padding(3, 6, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(51, 19);
            label3.TabIndex = 7;
            label3.Text = "Hasta:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpHastaProd1
            // 
            dtpHastaProd1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpHastaProd1.Format = DateTimePickerFormat.Short;
            dtpHastaProd1.Location = new Point(592, 3);
            dtpHastaProd1.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpHastaProd1.Name = "dtpHastaProd1";
            dtpHastaProd1.Size = new Size(88, 23);
            dtpHastaProd1.TabIndex = 8;
            dtpHastaProd1.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // btnFiltrar1
            // 
            btnFiltrar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnFiltrar1.Location = new Point(686, 3);
            btnFiltrar1.Name = "btnFiltrar1";
            btnFiltrar1.Size = new Size(75, 19);
            btnFiltrar1.TabIndex = 9;
            btnFiltrar1.Text = "Filtrar";
            btnFiltrar1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Anchor = AnchorStyles.None;
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel6.Controls.Add(label16);
            flowLayoutPanel6.Controls.Add(label17);
            flowLayoutPanel6.Controls.Add(dtpDesdeProd6);
            flowLayoutPanel6.Controls.Add(label18);
            flowLayoutPanel6.Controls.Add(dtpHastaProd6);
            flowLayoutPanel6.Controls.Add(btnFiltrar6);
            flowLayoutPanel6.Location = new Point(36, 215);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(777, 25);
            flowLayoutPanel6.TabIndex = 16;
            flowLayoutPanel6.WrapContents = false;
            // 
            // label16
            // 
            label16.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label16.Location = new Point(3, 0);
            label16.Name = "label16";
            label16.Size = new Size(374, 25);
            label16.TabIndex = 4;
            label16.Text = "Cantidad de Ventas por Método de Pago";
            label16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label17.Location = new Point(383, 6);
            label17.Margin = new Padding(3, 6, 3, 0);
            label17.Name = "label17";
            label17.Size = new Size(54, 19);
            label17.TabIndex = 5;
            label17.Text = "Desde:";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpDesdeProd6
            // 
            dtpDesdeProd6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpDesdeProd6.Format = DateTimePickerFormat.Short;
            dtpDesdeProd6.Location = new Point(443, 3);
            dtpDesdeProd6.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpDesdeProd6.Name = "dtpDesdeProd6";
            dtpDesdeProd6.Size = new Size(99, 23);
            dtpDesdeProd6.TabIndex = 6;
            dtpDesdeProd6.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // label18
            // 
            label18.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label18.Location = new Point(548, 6);
            label18.Margin = new Padding(3, 6, 3, 0);
            label18.Name = "label18";
            label18.Size = new Size(51, 19);
            label18.TabIndex = 7;
            label18.Text = "Hasta:";
            label18.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpHastaProd6
            // 
            dtpHastaProd6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpHastaProd6.Format = DateTimePickerFormat.Short;
            dtpHastaProd6.Location = new Point(605, 3);
            dtpHastaProd6.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpHastaProd6.Name = "dtpHastaProd6";
            dtpHastaProd6.Size = new Size(88, 23);
            dtpHastaProd6.TabIndex = 8;
            dtpHastaProd6.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // btnFiltrar6
            // 
            btnFiltrar6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnFiltrar6.Location = new Point(699, 3);
            btnFiltrar6.Name = "btnFiltrar6";
            btnFiltrar6.Size = new Size(75, 19);
            btnFiltrar6.TabIndex = 9;
            btnFiltrar6.Text = "Filtrar";
            btnFiltrar6.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.None;
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel5.Controls.Add(label13);
            flowLayoutPanel5.Controls.Add(label14);
            flowLayoutPanel5.Controls.Add(dtpDesdeProd5);
            flowLayoutPanel5.Controls.Add(label15);
            flowLayoutPanel5.Controls.Add(dtpHastaProd5);
            flowLayoutPanel5.Controls.Add(btnFiltrar5);
            flowLayoutPanel5.Location = new Point(84, 174);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(682, 25);
            flowLayoutPanel5.TabIndex = 15;
            flowLayoutPanel5.WrapContents = false;
            // 
            // label13
            // 
            label13.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label13.Location = new Point(3, 0);
            label13.Name = "label13";
            label13.Size = new Size(279, 25);
            label13.TabIndex = 4;
            label13.Text = "Ingresos por Método de Pago";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            label14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label14.Location = new Point(288, 6);
            label14.Margin = new Padding(3, 6, 3, 0);
            label14.Name = "label14";
            label14.Size = new Size(54, 19);
            label14.TabIndex = 5;
            label14.Text = "Desde:";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpDesdeProd5
            // 
            dtpDesdeProd5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpDesdeProd5.Format = DateTimePickerFormat.Short;
            dtpDesdeProd5.Location = new Point(348, 3);
            dtpDesdeProd5.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpDesdeProd5.Name = "dtpDesdeProd5";
            dtpDesdeProd5.Size = new Size(99, 23);
            dtpDesdeProd5.TabIndex = 6;
            dtpDesdeProd5.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label15.Location = new Point(453, 6);
            label15.Margin = new Padding(3, 6, 3, 0);
            label15.Name = "label15";
            label15.Size = new Size(51, 19);
            label15.TabIndex = 7;
            label15.Text = "Hasta:";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpHastaProd5
            // 
            dtpHastaProd5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpHastaProd5.Format = DateTimePickerFormat.Short;
            dtpHastaProd5.Location = new Point(510, 3);
            dtpHastaProd5.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpHastaProd5.Name = "dtpHastaProd5";
            dtpHastaProd5.Size = new Size(88, 23);
            dtpHastaProd5.TabIndex = 8;
            dtpHastaProd5.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // btnFiltrar5
            // 
            btnFiltrar5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnFiltrar5.Location = new Point(604, 3);
            btnFiltrar5.Name = "btnFiltrar5";
            btnFiltrar5.Size = new Size(75, 19);
            btnFiltrar5.TabIndex = 9;
            btnFiltrar5.Text = "Filtrar";
            btnFiltrar5.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.None;
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.Controls.Add(label10);
            flowLayoutPanel4.Controls.Add(label11);
            flowLayoutPanel4.Controls.Add(dtpDesdeProd4);
            flowLayoutPanel4.Controls.Add(label12);
            flowLayoutPanel4.Controls.Add(dtpHastaProd4);
            flowLayoutPanel4.Controls.Add(btnFiltrar4);
            flowLayoutPanel4.Location = new Point(115, 134);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(619, 25);
            flowLayoutPanel4.TabIndex = 14;
            flowLayoutPanel4.WrapContents = false;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label10.Location = new Point(3, 0);
            label10.Name = "label10";
            label10.Size = new Size(216, 25);
            label10.TabIndex = 4;
            label10.Text = "Ingresos por Categoría";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label11.Location = new Point(225, 6);
            label11.Margin = new Padding(3, 6, 3, 0);
            label11.Name = "label11";
            label11.Size = new Size(54, 19);
            label11.TabIndex = 5;
            label11.Text = "Desde:";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpDesdeProd4
            // 
            dtpDesdeProd4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpDesdeProd4.Format = DateTimePickerFormat.Short;
            dtpDesdeProd4.Location = new Point(285, 3);
            dtpDesdeProd4.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpDesdeProd4.Name = "dtpDesdeProd4";
            dtpDesdeProd4.Size = new Size(99, 23);
            dtpDesdeProd4.TabIndex = 6;
            dtpDesdeProd4.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label12.Location = new Point(390, 6);
            label12.Margin = new Padding(3, 6, 3, 0);
            label12.Name = "label12";
            label12.Size = new Size(51, 19);
            label12.TabIndex = 7;
            label12.Text = "Hasta:";
            label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpHastaProd4
            // 
            dtpHastaProd4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpHastaProd4.Format = DateTimePickerFormat.Short;
            dtpHastaProd4.Location = new Point(447, 3);
            dtpHastaProd4.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpHastaProd4.Name = "dtpHastaProd4";
            dtpHastaProd4.Size = new Size(88, 23);
            dtpHastaProd4.TabIndex = 8;
            dtpHastaProd4.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // btnFiltrar4
            // 
            btnFiltrar4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnFiltrar4.Location = new Point(541, 3);
            btnFiltrar4.Name = "btnFiltrar4";
            btnFiltrar4.Size = new Size(75, 19);
            btnFiltrar4.TabIndex = 9;
            btnFiltrar4.Text = "Filtrar";
            btnFiltrar4.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.None;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(label7);
            flowLayoutPanel3.Controls.Add(label8);
            flowLayoutPanel3.Controls.Add(dtpDesdeProd3);
            flowLayoutPanel3.Controls.Add(label9);
            flowLayoutPanel3.Controls.Add(dtpHastaProd3);
            flowLayoutPanel3.Controls.Add(btnFiltrar3);
            flowLayoutPanel3.Location = new Point(15, 93);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(819, 25);
            flowLayoutPanel3.TabIndex = 13;
            flowLayoutPanel3.WrapContents = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label7.Location = new Point(3, 0);
            label7.Name = "label7";
            label7.Size = new Size(416, 25);
            label7.TabIndex = 4;
            label7.Text = "Cantidad de Ventas por Intervalo de Horarios";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label8.Location = new Point(425, 6);
            label8.Margin = new Padding(3, 6, 3, 0);
            label8.Name = "label8";
            label8.Size = new Size(54, 19);
            label8.TabIndex = 5;
            label8.Text = "Desde:";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpDesdeProd3
            // 
            dtpDesdeProd3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpDesdeProd3.Format = DateTimePickerFormat.Short;
            dtpDesdeProd3.Location = new Point(485, 3);
            dtpDesdeProd3.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpDesdeProd3.Name = "dtpDesdeProd3";
            dtpDesdeProd3.Size = new Size(99, 23);
            dtpDesdeProd3.TabIndex = 6;
            dtpDesdeProd3.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label9.Location = new Point(590, 6);
            label9.Margin = new Padding(3, 6, 3, 0);
            label9.Name = "label9";
            label9.Size = new Size(51, 19);
            label9.TabIndex = 7;
            label9.Text = "Hasta:";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpHastaProd3
            // 
            dtpHastaProd3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpHastaProd3.Format = DateTimePickerFormat.Short;
            dtpHastaProd3.Location = new Point(647, 3);
            dtpHastaProd3.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpHastaProd3.Name = "dtpHastaProd3";
            dtpHastaProd3.Size = new Size(88, 23);
            dtpHastaProd3.TabIndex = 8;
            dtpHastaProd3.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // btnFiltrar3
            // 
            btnFiltrar3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnFiltrar3.Location = new Point(741, 3);
            btnFiltrar3.Name = "btnFiltrar3";
            btnFiltrar3.Size = new Size(75, 19);
            btnFiltrar3.TabIndex = 9;
            btnFiltrar3.Text = "Filtrar";
            btnFiltrar3.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.None;
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(label4);
            flowLayoutPanel2.Controls.Add(label5);
            flowLayoutPanel2.Controls.Add(dtpDesdeProd2);
            flowLayoutPanel2.Controls.Add(label6);
            flowLayoutPanel2.Controls.Add(dtpHastaProd2);
            flowLayoutPanel2.Controls.Add(btnFiltrar2);
            flowLayoutPanel2.Location = new Point(83, 53);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(684, 25);
            flowLayoutPanel2.TabIndex = 12;
            flowLayoutPanel2.WrapContents = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(281, 25);
            label4.TabIndex = 4;
            label4.Text = "Ingresos por Día de la Semana";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label5.Location = new Point(290, 6);
            label5.Margin = new Padding(3, 6, 3, 0);
            label5.Name = "label5";
            label5.Size = new Size(54, 19);
            label5.TabIndex = 5;
            label5.Text = "Desde:";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpDesdeProd2
            // 
            dtpDesdeProd2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpDesdeProd2.Format = DateTimePickerFormat.Short;
            dtpDesdeProd2.Location = new Point(350, 3);
            dtpDesdeProd2.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpDesdeProd2.Name = "dtpDesdeProd2";
            dtpDesdeProd2.Size = new Size(99, 23);
            dtpDesdeProd2.TabIndex = 6;
            dtpDesdeProd2.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label6.Location = new Point(455, 6);
            label6.Margin = new Padding(3, 6, 3, 0);
            label6.Name = "label6";
            label6.Size = new Size(51, 19);
            label6.TabIndex = 7;
            label6.Text = "Hasta:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtpHastaProd2
            // 
            dtpHastaProd2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            dtpHastaProd2.Format = DateTimePickerFormat.Short;
            dtpHastaProd2.Location = new Point(512, 3);
            dtpHastaProd2.MaxDate = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            dtpHastaProd2.Name = "dtpHastaProd2";
            dtpHastaProd2.Size = new Size(88, 23);
            dtpHastaProd2.TabIndex = 8;
            dtpHastaProd2.Value = new DateTime(2026, 11, 9, 0, 0, 0, 0);
            // 
            // btnFiltrar2
            // 
            btnFiltrar2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            btnFiltrar2.Location = new Point(606, 3);
            btnFiltrar2.Name = "btnFiltrar2";
            btnFiltrar2.Size = new Size(75, 19);
            btnFiltrar2.TabIndex = 9;
            btnFiltrar2.Text = "Filtrar";
            btnFiltrar2.UseVisualStyleBackColor = true;
            // 
            // UcEstadisticas
            // 
            Controls.Add(tableLayoutPanel1);
            Name = "UcEstadisticas";
            Size = new Size(850, 700);
            Load += UcEstadisticas_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel6;
        private Label label16;
        private Label label17;
        private DateTimePicker dtpDesdeProd6;
        private Label label18;
        private DateTimePicker dtpHastaProd6;
        private Button btnFiltrar6;
        private FlowLayoutPanel flowLayoutPanel5;
        private Label label13;
        private Label label14;
        private DateTimePicker dtpDesdeProd5;
        private Label label15;
        private DateTimePicker dtpHastaProd5;
        private Button btnFiltrar5;
        private FlowLayoutPanel flowLayoutPanel4;
        private Label label10;
        private Label label11;
        private DateTimePicker dtpDesdeProd4;
        private Label label12;
        private DateTimePicker dtpHastaProd4;
        private Button btnFiltrar4;
        private FlowLayoutPanel flowLayoutPanel3;
        private Label label7;
        private Label label8;
        private DateTimePicker dtpDesdeProd3;
        private Label label9;
        private DateTimePicker dtpHastaProd3;
        private Button btnFiltrar3;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label4;
        private Label label5;
        private DateTimePicker dtpDesdeProd2;
        private Label label6;
        private DateTimePicker dtpHastaProd2;
        private Button btnFiltrar2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private DateTimePicker dtpDesdeProd1;
        private Label label3;
        private DateTimePicker dtpHastaProd1;
        private Button btnFiltrar1;
    }
}

