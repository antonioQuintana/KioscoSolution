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
            lblTitulo = new Label();
            lblInfo = new Label();
            pnlFiltros = new Panel();
            chkSoloTurnoActual = new CheckBox();
            lblDesde = new Label();
            dtpDesde = new DateTimePicker();
            lblHasta = new Label();
            dtpHasta = new DateTimePicker();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnFiltrar = new Button();
            btnReset = new Button();
            btnAnular = new Button();
            dgvHistorial = new DataGridView();
            lblResumenTotales = new Label();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(25, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(209, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Historial de Ventas";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Font = new Font("Segoe UI", 9.5F);
            lblInfo.ForeColor = Color.DarkGray;
            lblInfo.Location = new Point(26, 47);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(593, 17);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "Haga doble clic sobre cualquier ticket para ver su detalle itemizado y copiar/imprimir comprobante.";
            // 
            // pnlFiltros
            // 
            pnlFiltros.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlFiltros.BackColor = Color.FromArgb(40, 40, 44);
            pnlFiltros.Controls.Add(chkSoloTurnoActual);
            pnlFiltros.Controls.Add(lblDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(lblBuscar);
            pnlFiltros.Controls.Add(txtBuscar);
            pnlFiltros.Controls.Add(btnFiltrar);
            pnlFiltros.Controls.Add(btnReset);
            pnlFiltros.Controls.Add(btnAnular);
            pnlFiltros.Location = new Point(25, 75);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new Size(950, 50);
            pnlFiltros.TabIndex = 2;
            // 
            // chkSoloTurnoActual
            // 
            chkSoloTurnoActual.AutoSize = true;
            chkSoloTurnoActual.Checked = false;
            chkSoloTurnoActual.CheckState = CheckState.Unchecked;
            chkSoloTurnoActual.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkSoloTurnoActual.ForeColor = Color.FromArgb(46, 204, 113);
            chkSoloTurnoActual.Location = new Point(10, 15);
            chkSoloTurnoActual.Name = "chkSoloTurnoActual";
            chkSoloTurnoActual.Size = new Size(96, 19);
            chkSoloTurnoActual.TabIndex = 0;
            chkSoloTurnoActual.Text = "Turno Actual";
            chkSoloTurnoActual.UseVisualStyleBackColor = true;
            chkSoloTurnoActual.CheckedChanged += chkSoloTurnoActual_CheckedChanged;
            // 
            // lblDesde
            // 
            lblDesde.AutoSize = true;
            lblDesde.ForeColor = Color.White;
            lblDesde.Location = new Point(115, 17);
            lblDesde.Name = "lblDesde";
            lblDesde.Size = new Size(42, 15);
            lblDesde.TabIndex = 1;
            lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.Location = new Point(160, 13);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(95, 23);
            dtpDesde.TabIndex = 2;
            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            // 
            // lblHasta
            // 
            lblHasta.AutoSize = true;
            lblHasta.ForeColor = Color.White;
            lblHasta.Location = new Point(265, 17);
            lblHasta.Name = "lblHasta";
            lblHasta.Size = new Size(40, 15);
            lblHasta.TabIndex = 3;
            lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.Location = new Point(310, 13);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(95, 23);
            dtpHasta.TabIndex = 4;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.ForeColor = Color.White;
            lblBuscar.Location = new Point(415, 17);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(45, 15);
            lblBuscar.TabIndex = 5;
            lblBuscar.Text = "Buscar:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(465, 13);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Ticket N°, Cliente, Método...";
            txtBuscar.Size = new Size(160, 23);
            txtBuscar.TabIndex = 6;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // btnFiltrar
            // 
            btnFiltrar.BackColor = Color.FromArgb(52, 152, 219);
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFiltrar.ForeColor = Color.White;
            btnFiltrar.Location = new Point(635, 10);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(70, 29);
            btnFiltrar.TabIndex = 7;
            btnFiltrar.Text = "Buscar";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(127, 140, 141);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(710, 10);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(65, 29);
            btnReset.TabIndex = 8;
            btnReset.Text = "Limpiar";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // btnAnular
            // 
            btnAnular.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAnular.BackColor = Color.FromArgb(192, 57, 43);
            btnAnular.FlatStyle = FlatStyle.Flat;
            btnAnular.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAnular.ForeColor = Color.White;
            btnAnular.Location = new Point(785, 10);
            btnAnular.Name = "btnAnular";
            btnAnular.Size = new Size(150, 29);
            btnAnular.TabIndex = 9;
            btnAnular.Text = "🚫 Anular Venta";
            btnAnular.UseVisualStyleBackColor = false;
            btnAnular.Click += btnAnular_Click;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorial.Location = new Point(25, 135);
            dgvHistorial.MultiSelect = false;
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new Size(950, 460);
            dgvHistorial.TabIndex = 3;
            // 
            // lblResumenTotales
            // 
            lblResumenTotales.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumenTotales.AutoSize = true;
            lblResumenTotales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResumenTotales.ForeColor = Color.FromArgb(220, 220, 220);
            lblResumenTotales.Location = new Point(25, 608);
            lblResumenTotales.Name = "lblResumenTotales";
            lblResumenTotales.Size = new Size(212, 19);
            lblResumenTotales.TabIndex = 4;
            lblResumenTotales.Text = "Calculando totales de ventas...";
            // 
            // UcHistorialVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblResumenTotales);
            Controls.Add(dgvHistorial);
            Controls.Add(pnlFiltros);
            Controls.Add(lblInfo);
            Controls.Add(lblTitulo);
            Name = "UcHistorialVentas";
            Size = new Size(1000, 643);
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private Label lblInfo;
        private Panel pnlFiltros;
        private CheckBox chkSoloTurnoActual;
        private Label lblDesde;
        private DateTimePicker dtpDesde;
        private Label lblHasta;
        private DateTimePicker dtpHasta;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnFiltrar;
        private Button btnReset;
        private Button btnAnular;
        private DataGridView dgvHistorial;
        private Label lblResumenTotales;
    }
}
