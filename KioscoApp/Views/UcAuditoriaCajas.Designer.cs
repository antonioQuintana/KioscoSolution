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
            btnRetiro = new System.Windows.Forms.Button();
            btnArqueo = new System.Windows.Forms.Button();
            btnHistorial = new System.Windows.Forms.Button();
            gbReportes = new System.Windows.Forms.GroupBox();
            btnAnular = new System.Windows.Forms.Button();
            btnHorarios = new System.Windows.Forms.Button();
            btnFaltantes = new System.Windows.Forms.Button();
            gbHerramientas = new System.Windows.Forms.GroupBox();
            btnInventario = new System.Windows.Forms.Button();
            btnExportarCSV = new System.Windows.Forms.Button();
            btnImportarExcel = new System.Windows.Forms.Button();
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
            gbControles.Controls.Add(btnRetiro);
            gbControles.Controls.Add(btnArqueo);
            gbControles.Controls.Add(btnHistorial);
            gbControles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            gbControles.ForeColor = System.Drawing.Color.White;
            gbControles.Location = new System.Drawing.Point(20, 55);
            gbControles.Name = "gbControles";
            gbControles.Size = new System.Drawing.Size(305, 195);
            gbControles.TabIndex = 1;
            gbControles.TabStop = false;
            gbControles.Text = "Controles de Caja Diarios";
            // 
            // btnRetiro
            // 
            btnRetiro.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            btnRetiro.Cursor = System.Windows.Forms.Cursors.Hand;
            btnRetiro.FlatAppearance.BorderSize = 0;
            btnRetiro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRetiro.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnRetiro.ForeColor = System.Drawing.Color.White;
            btnRetiro.Location = new System.Drawing.Point(15, 135);
            btnRetiro.Name = "btnRetiro";
            btnRetiro.Size = new System.Drawing.Size(275, 45);
            btnRetiro.TabIndex = 2;
            btnRetiro.Text = "Registrar Gasto / Retiro";
            btnRetiro.UseVisualStyleBackColor = false;
            btnRetiro.Click += btnRetiro_Click;
            // 
            // btnArqueo
            // 
            btnArqueo.BackColor = System.Drawing.Color.FromArgb(230, 126, 34);
            btnArqueo.Cursor = System.Windows.Forms.Cursors.Hand;
            btnArqueo.FlatAppearance.BorderSize = 0;
            btnArqueo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnArqueo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnArqueo.ForeColor = System.Drawing.Color.White;
            btnArqueo.Location = new System.Drawing.Point(15, 80);
            btnArqueo.Name = "btnArqueo";
            btnArqueo.Size = new System.Drawing.Size(275, 45);
            btnArqueo.TabIndex = 1;
            btnArqueo.Text = "Arqueo Sorpresa (Corte X)";
            btnArqueo.UseVisualStyleBackColor = false;
            btnArqueo.Click += btnArqueo_Click;
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
            btnHistorial.Text = "Historial de Cierres (Z)";
            btnHistorial.UseVisualStyleBackColor = false;
            btnHistorial.Click += btnHistorial_Click;
            // 
            // gbReportes
            // 
            gbReportes.Controls.Add(btnAnular);
            gbReportes.Controls.Add(btnHorarios);
            gbReportes.Controls.Add(btnFaltantes);
            gbReportes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            gbReportes.ForeColor = System.Drawing.Color.White;
            gbReportes.Location = new System.Drawing.Point(340, 55);
            gbReportes.Name = "gbReportes";
            gbReportes.Size = new System.Drawing.Size(305, 195);
            gbReportes.TabIndex = 2;
            gbReportes.TabStop = false;
            gbReportes.Text = "Análisis y Desempeño";
            // 
            // btnAnular
            // 
            btnAnular.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            btnAnular.Cursor = System.Windows.Forms.Cursors.Hand;
            btnAnular.FlatAppearance.BorderSize = 0;
            btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAnular.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnAnular.ForeColor = System.Drawing.Color.White;
            btnAnular.Location = new System.Drawing.Point(15, 135);
            btnAnular.Name = "btnAnular";
            btnAnular.Size = new System.Drawing.Size(275, 45);
            btnAnular.TabIndex = 2;
            btnAnular.Text = "Auditoría de Ventas Anuladas";
            btnAnular.UseVisualStyleBackColor = false;
            btnAnular.Click += btnAnular_Click;
            // 
            // btnHorarios
            // 
            btnHorarios.BackColor = System.Drawing.Color.FromArgb(22, 160, 133);
            btnHorarios.Cursor = System.Windows.Forms.Cursors.Hand;
            btnHorarios.FlatAppearance.BorderSize = 0;
            btnHorarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnHorarios.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnHorarios.ForeColor = System.Drawing.Color.White;
            btnHorarios.Location = new System.Drawing.Point(15, 80);
            btnHorarios.Name = "btnHorarios";
            btnHorarios.Size = new System.Drawing.Size(275, 45);
            btnHorarios.TabIndex = 1;
            btnHorarios.Text = "Ranking Ventas por Hora";
            btnHorarios.UseVisualStyleBackColor = false;
            btnHorarios.Click += btnHorarios_Click;
            // 
            // btnFaltantes
            // 
            btnFaltantes.BackColor = System.Drawing.Color.FromArgb(211, 84, 0);
            btnFaltantes.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFaltantes.FlatAppearance.BorderSize = 0;
            btnFaltantes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFaltantes.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnFaltantes.ForeColor = System.Drawing.Color.White;
            btnFaltantes.Location = new System.Drawing.Point(15, 25);
            btnFaltantes.Name = "btnFaltantes";
            btnFaltantes.Size = new System.Drawing.Size(275, 45);
            btnFaltantes.TabIndex = 0;
            btnFaltantes.Text = "Descuadres por Empleado";
            btnFaltantes.UseVisualStyleBackColor = false;
            btnFaltantes.Click += btnFaltantes_Click;
            // 
            // gbHerramientas
            // 
            gbHerramientas.Controls.Add(btnInventario);
            gbHerramientas.Controls.Add(btnExportarCSV);
            gbHerramientas.Controls.Add(btnImportarExcel);
            gbHerramientas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            gbHerramientas.ForeColor = System.Drawing.Color.White;
            gbHerramientas.Location = new System.Drawing.Point(660, 55);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new System.Drawing.Size(320, 195);
            gbHerramientas.TabIndex = 3;
            gbHerramientas.TabStop = false;
            gbHerramientas.Text = "Herramientas de Gestión";
            // 
            // btnInventario
            // 
            btnInventario.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            btnInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            btnInventario.FlatAppearance.BorderSize = 0;
            btnInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnInventario.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnInventario.ForeColor = System.Drawing.Color.White;
            btnInventario.Location = new System.Drawing.Point(15, 25);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new System.Drawing.Size(290, 45);
            btnInventario.TabIndex = 2;
            btnInventario.Text = "Reporte Inventario Valorizado";
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += btnInventario_Click;
            // 
            // btnExportarCSV
            // 
            btnExportarCSV.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
            btnExportarCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExportarCSV.FlatAppearance.BorderSize = 0;
            btnExportarCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExportarCSV.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnExportarCSV.ForeColor = System.Drawing.Color.White;
            btnExportarCSV.Location = new System.Drawing.Point(15, 80);
            btnExportarCSV.Name = "btnExportarCSV";
            btnExportarCSV.Size = new System.Drawing.Size(290, 45);
            btnExportarCSV.TabIndex = 1;
            btnExportarCSV.Text = "Exportar Reporte a CSV / Excel";
            btnExportarCSV.UseVisualStyleBackColor = false;
            btnExportarCSV.Click += btnExportarCSV_Click;
            // 
            // btnImportarExcel
            // 
            btnImportarExcel.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            btnImportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnImportarExcel.FlatAppearance.BorderSize = 0;
            btnImportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnImportarExcel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnImportarExcel.ForeColor = System.Drawing.Color.White;
            btnImportarExcel.Location = new System.Drawing.Point(15, 135);
            btnImportarExcel.Name = "btnImportarExcel";
            btnImportarExcel.Size = new System.Drawing.Size(290, 45);
            btnImportarExcel.TabIndex = 0;
            btnImportarExcel.Text = "Actualización Masiva Precios";
            btnImportarExcel.UseVisualStyleBackColor = false;
            btnImportarExcel.Click += btnImportarExcel_Click;
            // 
            // pnlFiltros
            // 
            pnlFiltros.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlFiltros.BackColor = System.Drawing.Color.FromArgb(45, 52, 54);
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
            pnlFiltros.Size = new System.Drawing.Size(960, 46);
            pnlFiltros.TabIndex = 4;
            // 
            // lblVistaActual
            // 
            lblVistaActual.AutoSize = true;
            lblVistaActual.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblVistaActual.ForeColor = System.Drawing.Color.Gold;
            lblVistaActual.Location = new System.Drawing.Point(10, 14);
            lblVistaActual.Name = "lblVistaActual";
            lblVistaActual.Size = new System.Drawing.Size(186, 17);
            lblVistaActual.TabIndex = 0;
            lblVistaActual.Text = "Vista: Historial de Cierres (Z)";
            // 
            // lblFiltroDesde
            // 
            lblFiltroDesde.AutoSize = true;
            lblFiltroDesde.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            lblFiltroDesde.ForeColor = System.Drawing.Color.White;
            lblFiltroDesde.Location = new System.Drawing.Point(255, 14);
            lblFiltroDesde.Name = "lblFiltroDesde";
            lblFiltroDesde.Size = new System.Drawing.Size(42, 15);
            lblFiltroDesde.TabIndex = 1;
            lblFiltroDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpDesde.Location = new System.Drawing.Point(303, 11);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new System.Drawing.Size(100, 23);
            dtpDesde.TabIndex = 2;
            // 
            // lblFiltroHasta
            // 
            lblFiltroHasta.AutoSize = true;
            lblFiltroHasta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            lblFiltroHasta.ForeColor = System.Drawing.Color.White;
            lblFiltroHasta.Location = new System.Drawing.Point(415, 14);
            lblFiltroHasta.Name = "lblFiltroHasta";
            lblFiltroHasta.Size = new System.Drawing.Size(40, 15);
            lblFiltroHasta.TabIndex = 3;
            lblFiltroHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpHasta.Location = new System.Drawing.Point(461, 11);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new System.Drawing.Size(100, 23);
            dtpHasta.TabIndex = 4;
            // 
            // lblCajero
            // 
            lblCajero.AutoSize = true;
            lblCajero.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            lblCajero.ForeColor = System.Drawing.Color.White;
            lblCajero.Location = new System.Drawing.Point(575, 14);
            lblCajero.Name = "lblCajero";
            lblCajero.Size = new System.Drawing.Size(44, 15);
            lblCajero.TabIndex = 5;
            lblCajero.Text = "Cajero:";
            // 
            // cmbCajero
            // 
            cmbCajero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbCajero.FormattingEnabled = true;
            cmbCajero.Location = new System.Drawing.Point(625, 11);
            cmbCajero.Name = "cmbCajero";
            cmbCajero.Size = new System.Drawing.Size(130, 23);
            cmbCajero.TabIndex = 6;
            // 
            // btnFiltrar
            // 
            btnFiltrar.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnFiltrar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnFiltrar.ForeColor = System.Drawing.Color.White;
            btnFiltrar.Location = new System.Drawing.Point(765, 9);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new System.Drawing.Size(85, 27);
            btnFiltrar.TabIndex = 7;
            btnFiltrar.Text = "Filtrar";
            btnFiltrar.UseVisualStyleBackColor = false;
            btnFiltrar.Click += btnFiltrar_Click;
            // 
            // btnLimpiarFiltros
            // 
            btnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            btnLimpiarFiltros.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            btnLimpiarFiltros.Location = new System.Drawing.Point(858, 9);
            btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            btnLimpiarFiltros.Size = new System.Drawing.Size(90, 27);
            btnLimpiarFiltros.TabIndex = 8;
            btnLimpiarFiltros.Text = "Ver Todo";
            btnLimpiarFiltros.UseVisualStyleBackColor = false;
            btnLimpiarFiltros.Click += btnLimpiarFiltros_Click;
            // 
            // dgvAuditoria
            // 
            dgvAuditoria.AllowUserToAddRows = false;
            dgvAuditoria.AllowUserToDeleteRows = false;
            dgvAuditoria.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvAuditoria.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvAuditoria.BackgroundColor = System.Drawing.Color.FromArgb(45, 52, 54);
            dgvAuditoria.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(30, 39, 46);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvAuditoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvAuditoria.ColumnHeadersHeight = 35;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(53, 59, 72);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvAuditoria.DefaultCellStyle = dataGridViewCellStyle2;
            dgvAuditoria.EnableHeadersVisualStyles = false;
            dgvAuditoria.GridColor = System.Drawing.Color.FromArgb(71, 71, 71);
            dgvAuditoria.Location = new System.Drawing.Point(20, 316);
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
        private System.Windows.Forms.Button btnRetiro;
        private System.Windows.Forms.Button btnArqueo;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.GroupBox gbReportes;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnHorarios;
        private System.Windows.Forms.Button btnFaltantes;
        private System.Windows.Forms.GroupBox gbHerramientas;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnExportarCSV;
        private System.Windows.Forms.Button btnImportarExcel;
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
