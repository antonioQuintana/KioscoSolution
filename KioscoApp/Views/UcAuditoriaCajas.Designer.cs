namespace KioscoApp
{
    partial class UcAuditoriaCajas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            lblTitulo = new System.Windows.Forms.Label();
            gbControles = new System.Windows.Forms.GroupBox();
            btnArqueo = new System.Windows.Forms.Button();
            btnRetiro = new System.Windows.Forms.Button();
            btnMovimientos = new System.Windows.Forms.Button();
            gbReportes = new System.Windows.Forms.GroupBox();
            btnHistorial = new System.Windows.Forms.Button();
            btnFaltantes = new System.Windows.Forms.Button();
            btnAnular = new System.Windows.Forms.Button();
            gbHerramientas = new System.Windows.Forms.GroupBox();
            btnExportarCSV = new System.Windows.Forms.Button();
            btnActualizarVista = new System.Windows.Forms.Button();
            pnlFiltros = new System.Windows.Forms.Panel();
            lblVistaActual = new System.Windows.Forms.Label();
            lblFiltroDesde = new System.Windows.Forms.Label();
            dtpDesde = new System.Windows.Forms.DateTimePicker();
            lblFiltroHasta = new System.Windows.Forms.Label();
            dtpHasta = new System.Windows.Forms.DateTimePicker();
            lblCajero = new System.Windows.Forms.Label();
            cmbCajero = new System.Windows.Forms.ComboBox();
            btnFiltrar = new System.Windows.Forms.Button();
            btnLimpiarFiltros = new System.Windows.Forms.Button();
            dgvAuditoria = new System.Windows.Forms.DataGridView();
            lblResumenAuditoria = new System.Windows.Forms.Label();
            gbControles.SuspendLayout();
            gbReportes.SuspendLayout();
            gbHerramientas.SuspendLayout();
            pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = System.Drawing.Color.Transparent;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitulo.ForeColor = System.Drawing.Color.White;
            lblTitulo.Location = new System.Drawing.Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(434, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Auditoría y Control de Cajas - Supervisor";
            // 
            // gbControles
            // 
            gbControles.Controls.Add(btnArqueo);
            gbControles.Controls.Add(btnRetiro);
            gbControles.Controls.Add(btnMovimientos);
            gbControles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            gbControles.ForeColor = System.Drawing.Color.White;
            gbControles.Location = new System.Drawing.Point(20, 55);
            gbControles.Name = "gbControles";
            gbControles.Size = new System.Drawing.Size(305, 195);
            gbControles.TabIndex = 1;
            gbControles.TabStop = false;
            gbControles.Text = "Operaciones de Turno (Caja)";
            // 
            // btnArqueo
            // 
            btnArqueo.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            btnArqueo.Cursor = System.Windows.Forms.Cursors.Hand;
            btnArqueo.FlatAppearance.BorderSize = 0;
            btnArqueo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnArqueo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnArqueo.ForeColor = System.Drawing.Color.White;
            btnArqueo.Location = new System.Drawing.Point(15, 25);
            btnArqueo.Name = "btnArqueo";
            btnArqueo.Size = new System.Drawing.Size(275, 45);
            btnArqueo.TabIndex = 0;
            btnArqueo.Text = "🔍 Arqueo Sorpresa (Corte X)";
            btnArqueo.UseVisualStyleBackColor = false;
            btnArqueo.Click += btnArqueo_Click;
            // 
            // btnRetiro
            // 
            btnRetiro.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            btnRetiro.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRetiro.FlatAppearance.BorderSize = 0;
            btnRetiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRetiro.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnRetiro.ForeColor = System.Drawing.Color.White;
            btnRetiro.Location = new System.Drawing.Point(15, 80);
            btnRetiro.Name = "btnRetiro";
            btnRetiro.Size = new System.Drawing.Size(275, 45);
            btnRetiro.TabIndex = 1;
            btnRetiro.Text = "💸 Registrar Gasto / Retiro";
            btnRetiro.UseVisualStyleBackColor = false;
            btnRetiro.Click += btnRetiro_Click;
            // 
            // btnMovimientos
            // 
            btnMovimientos.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            btnMovimientos.Cursor = System.Windows.Forms.Cursors.Hand;
            btnMovimientos.FlatAppearance.BorderSize = 0;
            btnMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnMovimientos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnMovimientos.ForeColor = System.Drawing.Color.White;
            btnMovimientos.Location = new System.Drawing.Point(15, 135);
            btnMovimientos.Name = "btnMovimientos";
            btnMovimientos.Size = new System.Drawing.Size(275, 45);
            btnMovimientos.TabIndex = 2;
            btnMovimientos.Text = "📋 Auditoría Retiros / Gastos";
            btnMovimientos.UseVisualStyleBackColor = false;
            btnMovimientos.Click += btnMovimientos_Click;
            // 
            // gbReportes
            // 
            gbReportes.Controls.Add(btnHistorial);
            gbReportes.Controls.Add(btnFaltantes);
            gbReportes.Controls.Add(btnAnular);
            gbReportes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            gbReportes.ForeColor = System.Drawing.Color.White;
            gbReportes.Location = new System.Drawing.Point(340, 55);
            gbReportes.Name = "gbReportes";
            gbReportes.Size = new System.Drawing.Size(305, 195);
            gbReportes.TabIndex = 2;
            gbReportes.TabStop = false;
            gbReportes.Text = "Control de Cierres y Empleados";
            // 
            // btnHistorial
            // 
            btnHistorial.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            btnHistorial.Cursor = System.Windows.Forms.Cursors.Hand;
            btnHistorial.FlatAppearance.BorderSize = 0;
            btnHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHistorial.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnHistorial.ForeColor = System.Drawing.Color.White;
            btnHistorial.Location = new System.Drawing.Point(15, 25);
            btnHistorial.Name = "btnHistorial";
            btnHistorial.Size = new System.Drawing.Size(275, 45);
            btnHistorial.TabIndex = 0;
            btnHistorial.Text = "📜 Historial de Cierres (Z)";
            btnHistorial.UseVisualStyleBackColor = false;
            btnHistorial.Click += btnHistorial_Click;
            // 
            // btnFaltantes
            // 
            btnFaltantes.BackColor = System.Drawing.Color.FromArgb(211, 84, 0);
            btnFaltantes.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFaltantes.FlatAppearance.BorderSize = 0;
            btnFaltantes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFaltantes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnFaltantes.ForeColor = System.Drawing.Color.White;
            btnFaltantes.Location = new System.Drawing.Point(15, 80);
            btnFaltantes.Name = "btnFaltantes";
            btnFaltantes.Size = new System.Drawing.Size(275, 45);
            btnFaltantes.TabIndex = 1;
            btnFaltantes.Text = "⚖️ Descuadres por Empleado";
            btnFaltantes.UseVisualStyleBackColor = false;
            btnFaltantes.Click += btnFaltantes_Click;
            // 
            // btnAnular
            // 
            btnAnular.BackColor = System.Drawing.Color.FromArgb(175, 45, 65);
            btnAnular.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAnular.FlatAppearance.BorderSize = 0;
            btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAnular.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnAnular.ForeColor = System.Drawing.Color.White;
            btnAnular.Location = new System.Drawing.Point(15, 135);
            btnAnular.Name = "btnAnular";
            btnAnular.Size = new System.Drawing.Size(275, 45);
            btnAnular.TabIndex = 2;
            btnAnular.Text = "🚫 Auditoría de Ventas Anuladas";
            btnAnular.UseVisualStyleBackColor = false;
            btnAnular.Click += btnAnular_Click;
            // 
            // gbHerramientas
            // 
            gbHerramientas.Controls.Add(btnExportarCSV);
            gbHerramientas.Controls.Add(btnActualizarVista);
            gbHerramientas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            gbHerramientas.ForeColor = System.Drawing.Color.White;
            gbHerramientas.Location = new System.Drawing.Point(660, 55);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new System.Drawing.Size(320, 195);
            gbHerramientas.TabIndex = 3;
            gbHerramientas.TabStop = false;
            gbHerramientas.Text = "Acciones y Exportación";
            // 
            // btnExportarCSV
            // 
            btnExportarCSV.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            btnExportarCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExportarCSV.FlatAppearance.BorderSize = 0;
            btnExportarCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExportarCSV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnExportarCSV.ForeColor = System.Drawing.Color.White;
            btnExportarCSV.Location = new System.Drawing.Point(15, 35);
            btnExportarCSV.Name = "btnExportarCSV";
            btnExportarCSV.Size = new System.Drawing.Size(290, 60);
            btnExportarCSV.TabIndex = 0;
            btnExportarCSV.Text = "📥 Exportar Reporte a CSV / Excel";
            btnExportarCSV.UseVisualStyleBackColor = false;
            btnExportarCSV.Click += btnExportarCSV_Click;
            // 
            // btnActualizarVista
            // 
            btnActualizarVista.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            btnActualizarVista.Cursor = System.Windows.Forms.Cursors.Hand;
            btnActualizarVista.FlatAppearance.BorderSize = 0;
            btnActualizarVista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnActualizarVista.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnActualizarVista.ForeColor = System.Drawing.Color.White;
            btnActualizarVista.Location = new System.Drawing.Point(15, 110);
            btnActualizarVista.Name = "btnActualizarVista";
            btnActualizarVista.Size = new System.Drawing.Size(290, 60);
            btnActualizarVista.TabIndex = 1;
            btnActualizarVista.Text = "🔄 Actualizar Datos";
            btnActualizarVista.UseVisualStyleBackColor = false;
            btnActualizarVista.Click += btnActualizarVista_Click;
            // 
            // pnlFiltros
            // 
            pnlFiltros.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlFiltros.BackColor = System.Drawing.Color.FromArgb(38, 38, 42);
            pnlFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnlFiltros.Controls.Add(lblVistaActual);
            pnlFiltros.Controls.Add(lblFiltroDesde);
            pnlFiltros.Controls.Add(dtpDesde);
            pnlFiltros.Controls.Add(lblFiltroHasta);
            pnlFiltros.Controls.Add(dtpHasta);
            pnlFiltros.Controls.Add(lblCajero);
            pnlFiltros.Controls.Add(cmbCajero);
            pnlFiltros.Controls.Add(btnFiltrar);
            pnlFiltros.Controls.Add(btnLimpiarFiltros);
            pnlFiltros.Location = new System.Drawing.Point(20, 260);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Size = new System.Drawing.Size(960, 50);
            pnlFiltros.TabIndex = 4;
            // 
            // lblVistaActual
            // 
            lblVistaActual.AutoSize = true;
            lblVistaActual.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblVistaActual.ForeColor = System.Drawing.Color.FromArgb(0, 168, 255);
            lblVistaActual.Location = new System.Drawing.Point(10, 14);
            lblVistaActual.Name = "lblVistaActual";
            lblVistaActual.Size = new System.Drawing.Size(220, 19);
            lblVistaActual.TabIndex = 0;
            lblVistaActual.Text = "Vista: Historial de Cierres (Z)";
            // 
            // lblFiltroDesde
            // 
            lblFiltroDesde.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblFiltroDesde.AutoSize = true;
            lblFiltroDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblFiltroDesde.ForeColor = System.Drawing.Color.White;
            lblFiltroDesde.Location = new System.Drawing.Point(315, 16);
            lblFiltroDesde.Name = "lblFiltroDesde";
            lblFiltroDesde.Size = new System.Drawing.Size(44, 15);
            lblFiltroDesde.TabIndex = 1;
            lblFiltroDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpDesde.Location = new System.Drawing.Point(362, 12);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new System.Drawing.Size(95, 23);
            dtpDesde.TabIndex = 2;
            // 
            // lblFiltroHasta
            // 
            lblFiltroHasta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblFiltroHasta.AutoSize = true;
            lblFiltroHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblFiltroHasta.ForeColor = System.Drawing.Color.White;
            lblFiltroHasta.Location = new System.Drawing.Point(465, 16);
            lblFiltroHasta.Name = "lblFiltroHasta";
            lblFiltroHasta.Size = new System.Drawing.Size(41, 15);
            lblFiltroHasta.TabIndex = 3;
            lblFiltroHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpHasta.Location = new System.Drawing.Point(509, 12);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new System.Drawing.Size(95, 23);
            dtpHasta.TabIndex = 4;
            // 
            // lblCajero
            // 
            lblCajero.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblCajero.AutoSize = true;
            lblCajero.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblCajero.ForeColor = System.Drawing.Color.White;
            lblCajero.Location = new System.Drawing.Point(615, 16);
            lblCajero.Name = "lblCajero";
            lblCajero.Size = new System.Drawing.Size(51, 15);
            lblCajero.TabIndex = 5;
            lblCajero.Text = "Usuario:";
            // 
            // cmbCajero
            // 
            cmbCajero.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cmbCajero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCajero.FormattingEnabled = true;
            cmbCajero.Location = new System.Drawing.Point(670, 12);
            cmbCajero.Name = "cmbCajero";
            cmbCajero.Size = new System.Drawing.Size(130, 23);
            cmbCajero.TabIndex = 6;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnFiltrar.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnFiltrar.ForeColor = System.Drawing.Color.White;
            btnFiltrar.Location = new System.Drawing.Point(810, 10);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new System.Drawing.Size(65, 28);
            btnFiltrar.TabIndex = 7;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnLimpiarFiltros.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            btnLimpiarFiltros.Location = new System.Drawing.Point(882, 10);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new System.Drawing.Size(68, 28);
            btnLimpiarFiltros.TabIndex = 8;
            btnLimpiarFiltros.Text = "Limpiar";
            btnLimpiarFiltros.UseVisualStyleBackColor = false;
            btnLimpiarFiltros.Click += btnLimpiarFiltros_Click;
            // 
            // dgvAuditoria
            // 
            dgvAuditoria.AllowUserToAddRows = false;
            dgvAuditoria.AllowUserToDeleteRows = false;
            dgvAuditoria.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvAuditoria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditoria.BackgroundColor = System.Drawing.Color.FromArgb(32, 32, 36);
            dgvAuditoria.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 30, 32);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvAuditoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAuditoria.ColumnHeadersHeight = 32;
            dgvAuditoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvAuditoria.DefaultCellStyle = dataGridViewCellStyle2;
            dgvAuditoria.EnableHeadersVisualStyles = false;
            dgvAuditoria.Location = new System.Drawing.Point(20, 320);
            dgvAuditoria.MultiSelect = false;
            dgvAuditoria.Name = "dgvAuditoria";
            dgvAuditoria.ReadOnly = true;
            dgvAuditoria.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(47, 54, 64);
            dgvAuditoria.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvAuditoria.RowTemplate.Height = 28;
            dgvAuditoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvAuditoria.Size = new System.Drawing.Size(960, 310);
            dgvAuditoria.TabIndex = 5;
            dgvAuditoria.CellFormatting += dgvAuditoria_CellFormatting;
            dgvAuditoria.DoubleClick += dgvAuditoria_DoubleClick;
            // 
            // lblResumenAuditoria
            // 
            lblResumenAuditoria.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblResumenAuditoria.BackColor = System.Drawing.Color.FromArgb(30, 39, 46);
            lblResumenAuditoria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblResumenAuditoria.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblResumenAuditoria.ForeColor = System.Drawing.Color.White;
            lblResumenAuditoria.Location = new System.Drawing.Point(20, 635);
            lblResumenAuditoria.Name = "lblResumenAuditoria";
            lblResumenAuditoria.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            lblResumenAuditoria.Size = new System.Drawing.Size(960, 32);
            lblResumenAuditoria.TabIndex = 6;
            lblResumenAuditoria.Text = "Cargando datos...";
            lblResumenAuditoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcAuditoriaCajas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = System.Drawing.SystemColors.ControlDarkDark;
            Controls.Add(lblResumenAuditoria);
            Controls.Add(dgvAuditoria);
            Controls.Add(pnlFiltros);
            Controls.Add(gbHerramientas);
            Controls.Add(gbReportes);
            Controls.Add(gbControles);
            Controls.Add(lblTitulo);
            ForeColor = System.Drawing.SystemColors.ControlLightLight;
            Name = "UcAuditoriaCajas";
            Size = new System.Drawing.Size(1000, 680);
            gbControles.ResumeLayout(false);
            gbReportes.ResumeLayout(false);
            gbHerramientas.ResumeLayout(false);
            pnlFiltros.ResumeLayout(false);
            pnlFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAuditoria).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbControles;
        private System.Windows.Forms.Button btnArqueo;
        private System.Windows.Forms.Button btnRetiro;
        private System.Windows.Forms.Button btnMovimientos;
        private System.Windows.Forms.GroupBox gbReportes;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnFaltantes;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.GroupBox gbHerramientas;
        private System.Windows.Forms.Button btnExportarCSV;
        private System.Windows.Forms.Button btnActualizarVista;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblVistaActual;
        private System.Windows.Forms.Label lblFiltroDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblCajero;
        private System.Windows.Forms.ComboBox cmbCajero;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.DataGridView dgvAuditoria;
        private System.Windows.Forms.Label lblResumenAuditoria;
    }
}
