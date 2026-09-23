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
            lblTitulo = new Label();
            gbControles = new GroupBox();
            btnRetiro = new Button();
            btnArqueo = new Button();
            btnHistorial = new Button();
            gbReportes = new GroupBox();
            btnAnular = new Button();
            btnHorarios = new Button();
            btnFaltantes = new Button();
            gbHerramientas = new GroupBox();
            btnInventario = new Button();
            btnExportarCSV = new Button();
            btnImportarExcel = new Button();
            gbControles.SuspendLayout();
            gbReportes.SuspendLayout();
            gbHerramientas.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = SystemColors.ControlDarkDark;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(30, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(303, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Auditoría y Control de Cajas";
            // 
            // gbControles
            // 
            gbControles.Controls.Add(btnRetiro);
            gbControles.Controls.Add(btnArqueo);
            gbControles.Controls.Add(btnHistorial);
            gbControles.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbControles.ForeColor = SystemColors.ControlLightLight;
            gbControles.Location = new Point(30, 80);
            gbControles.Name = "gbControles";
            gbControles.Size = new Size(280, 250);
            gbControles.TabIndex = 1;
            gbControles.TabStop = false;
            gbControles.Text = "Controles Diarios";
            // 
            // btnRetiro
            // 
            btnRetiro.BackColor = SystemColors.AppWorkspace;
            btnRetiro.Font = new Font("Segoe UI", 10F);
            btnRetiro.ForeColor = SystemColors.ButtonHighlight;
            btnRetiro.Location = new Point(20, 180);
            btnRetiro.Name = "btnRetiro";
            btnRetiro.Size = new Size(240, 50);
            btnRetiro.TabIndex = 2;
            btnRetiro.Text = "Registrar Gasto / Retiro";
            btnRetiro.UseVisualStyleBackColor = false;
            btnRetiro.Click += MostrarProximamente;
            // 
            // btnArqueo
            // 
            btnArqueo.BackColor = SystemColors.AppWorkspace;
            btnArqueo.Font = new Font("Segoe UI", 10F);
            btnArqueo.ForeColor = SystemColors.ButtonHighlight;
            btnArqueo.Location = new Point(20, 110);
            btnArqueo.Name = "btnArqueo";
            btnArqueo.Size = new Size(240, 50);
            btnArqueo.TabIndex = 1;
            btnArqueo.Text = "Arqueo Sorpresa (Corte X)";
            btnArqueo.UseVisualStyleBackColor = false;
            btnArqueo.Click += MostrarProximamente;
            // 
            // btnHistorial
            // 
            btnHistorial.BackColor = SystemColors.AppWorkspace;
            btnHistorial.Font = new Font("Segoe UI", 10F);
            btnHistorial.ForeColor = SystemColors.ButtonHighlight;
            btnHistorial.Location = new Point(20, 40);
            btnHistorial.Name = "btnHistorial";
            btnHistorial.Size = new Size(240, 50);
            btnHistorial.TabIndex = 0;
            btnHistorial.Text = "Historial de Cierres (Z)";
            btnHistorial.UseVisualStyleBackColor = false;
            btnHistorial.Click += MostrarProximamente;
            // 
            // gbReportes
            // 
            gbReportes.Controls.Add(btnAnular);
            gbReportes.Controls.Add(btnHorarios);
            gbReportes.Controls.Add(btnFaltantes);
            gbReportes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbReportes.ForeColor = SystemColors.ControlLightLight;
            gbReportes.Location = new Point(340, 80);
            gbReportes.Name = "gbReportes";
            gbReportes.Size = new Size(280, 250);
            gbReportes.TabIndex = 2;
            gbReportes.TabStop = false;
            gbReportes.Text = "Análisis y Rendimiento";
            // 
            // btnAnular
            // 
            btnAnular.BackColor = SystemColors.AppWorkspace;
            btnAnular.Font = new Font("Segoe UI", 10F);
            btnAnular.ForeColor = SystemColors.ButtonHighlight;
            btnAnular.Location = new Point(20, 180);
            btnAnular.Name = "btnAnular";
            btnAnular.Size = new Size(240, 50);
            btnAnular.TabIndex = 2;
            btnAnular.Text = "Auditoría de Ventas Anuladas";
            btnAnular.UseVisualStyleBackColor = false;
            btnAnular.Click += MostrarProximamente;
            // 
            // btnHorarios
            // 
            btnHorarios.BackColor = SystemColors.AppWorkspace;
            btnHorarios.Font = new Font("Segoe UI", 10F);
            btnHorarios.ForeColor = SystemColors.ButtonHighlight;
            btnHorarios.Location = new Point(20, 110);
            btnHorarios.Name = "btnHorarios";
            btnHorarios.Size = new Size(240, 50);
            btnHorarios.TabIndex = 1;
            btnHorarios.Text = "Ranking de Ventas por Hora";
            btnHorarios.UseVisualStyleBackColor = false;
            btnHorarios.Click += MostrarProximamente;
            // 
            // btnFaltantes
            // 
            btnFaltantes.BackColor = SystemColors.AppWorkspace;
            btnFaltantes.Font = new Font("Segoe UI", 10F);
            btnFaltantes.ForeColor = SystemColors.ButtonHighlight;
            btnFaltantes.Location = new Point(20, 40);
            btnFaltantes.Name = "btnFaltantes";
            btnFaltantes.Size = new Size(240, 50);
            btnFaltantes.TabIndex = 0;
            btnFaltantes.Text = "Descuadres por Empleado";
            btnFaltantes.UseVisualStyleBackColor = false;
            btnFaltantes.Click += MostrarProximamente;
            // 
            // gbHerramientas
            // 
            gbHerramientas.Controls.Add(btnInventario);
            gbHerramientas.Controls.Add(btnExportarCSV);
            gbHerramientas.Controls.Add(btnImportarExcel);
            gbHerramientas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbHerramientas.ForeColor = SystemColors.ControlLightLight;
            gbHerramientas.Location = new Point(650, 80);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new Size(280, 250);
            gbHerramientas.TabIndex = 3;
            gbHerramientas.TabStop = false;
            gbHerramientas.Text = "Herramientas de Supervisor";
            // 
            // btnInventario
            // 
            btnInventario.BackColor = SystemColors.AppWorkspace;
            btnInventario.Font = new Font("Segoe UI", 10F);
            btnInventario.ForeColor = SystemColors.ButtonHighlight;
            btnInventario.Location = new Point(20, 180);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new Size(240, 50);
            btnInventario.TabIndex = 2;
            btnInventario.Text = "Reporte Inventario Valorizado";
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += MostrarProximamente;
            // 
            // btnExportarCSV
            // 
            btnExportarCSV.BackColor = SystemColors.AppWorkspace;
            btnExportarCSV.Font = new Font("Segoe UI", 10F);
            btnExportarCSV.ForeColor = SystemColors.ButtonHighlight;
            btnExportarCSV.Location = new Point(20, 110);
            btnExportarCSV.Name = "btnExportarCSV";
            btnExportarCSV.Size = new Size(240, 50);
            btnExportarCSV.TabIndex = 1;
            btnExportarCSV.Text = "Exportar Datos Contador (CSV)";
            btnExportarCSV.UseVisualStyleBackColor = false;
            btnExportarCSV.Click += MostrarProximamente;
            // 
            // btnImportarExcel
            // 
            btnImportarExcel.BackColor = SystemColors.AppWorkspace;
            btnImportarExcel.Font = new Font("Segoe UI", 10F);
            btnImportarExcel.ForeColor = SystemColors.ButtonHighlight;
            btnImportarExcel.Location = new Point(20, 40);
            btnImportarExcel.Name = "btnImportarExcel";
            btnImportarExcel.Size = new Size(240, 50);
            btnImportarExcel.TabIndex = 0;
            btnImportarExcel.Text = "Actualizar Precios (Excel)";
            btnImportarExcel.UseVisualStyleBackColor = false;
            btnImportarExcel.Click += MostrarProximamente;
            // 
            // UcAuditoriaCajas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(gbHerramientas);
            Controls.Add(gbReportes);
            Controls.Add(gbControles);
            Controls.Add(lblTitulo);
            ForeColor = SystemColors.ControlLightLight;
            Name = "UcAuditoriaCajas";
            Size = new Size(1000, 643);
            gbControles.ResumeLayout(false);
            gbReportes.ResumeLayout(false);
            gbHerramientas.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitulo;
        private GroupBox gbControles;
        private Button btnRetiro;
        private Button btnArqueo;
        private Button btnHistorial;
        private GroupBox gbReportes;
        private Button btnAnular;
        private Button btnHorarios;
        private Button btnFaltantes;
        private GroupBox gbHerramientas;
        private Button btnInventario;
        private Button btnExportarCSV;
        private Button btnImportarExcel;
    }
}
