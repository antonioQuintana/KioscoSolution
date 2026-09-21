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
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitulo.Location = new System.Drawing.Point(30, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new System.Drawing.Size(315, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.BackColor = SystemColors.ControlDarkDark;
            lblTitulo.Text = "Auditor�a y Control de Cajas";
            // 
            // gbControles
            // 
            gbControles.Controls.Add(btnRetiro);
            gbControles.Controls.Add(btnArqueo);
            gbControles.Controls.Add(btnHistorial);
            gbControles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            gbControles.Location = new System.Drawing.Point(30, 80);
            gbControles.Name = "gbControles";
            gbControles.Size = new System.Drawing.Size(280, 250);
            gbControles.TabIndex = 1;
            gbControles.TabStop = false;
            gbControles.ForeColor = SystemColors.ControlLightLight;
            gbControles.Text = "Controles Diarios";
            // 
            // btnHistorial
            // 
            btnHistorial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnHistorial.Location = new System.Drawing.Point(20, 40);
            btnHistorial.Name = "btnHistorial";
            btnHistorial.Size = new System.Drawing.Size(240, 50);
            btnHistorial.TabIndex = 0;
            btnHistorial.Text = "Historial de Cierres (Z)";
            btnHistorial.BackColor = SystemColors.AppWorkspace;
            btnHistorial.ForeColor = SystemColors.ButtonHighlight;
            btnHistorial.UseVisualStyleBackColor = false;
            btnHistorial.Click += MostrarProximamente;
            // 
            // btnArqueo
            // 
            btnArqueo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnArqueo.Location = new System.Drawing.Point(20, 110);
            btnArqueo.Name = "btnArqueo";
            btnArqueo.Size = new System.Drawing.Size(240, 50);
            btnArqueo.TabIndex = 1;
            btnArqueo.Text = "Arqueo Sorpresa (Corte X)";
            btnArqueo.BackColor = SystemColors.AppWorkspace;
            btnArqueo.ForeColor = SystemColors.ButtonHighlight;
            btnArqueo.UseVisualStyleBackColor = false;
            btnArqueo.Click += MostrarProximamente;
            // 
            // btnRetiro
            // 
            btnRetiro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnRetiro.Location = new System.Drawing.Point(20, 180);
            btnRetiro.Name = "btnRetiro";
            btnRetiro.Size = new System.Drawing.Size(240, 50);
            btnRetiro.TabIndex = 2;
            btnRetiro.Text = "Registrar Gasto / Retiro";
            btnRetiro.BackColor = SystemColors.AppWorkspace;
            btnRetiro.ForeColor = SystemColors.ButtonHighlight;
            btnRetiro.UseVisualStyleBackColor = false;
            btnRetiro.Click += MostrarProximamente;
            // 
            // gbReportes
            // 
            gbReportes.Controls.Add(btnAnular);
            gbReportes.Controls.Add(btnHorarios);
            gbReportes.Controls.Add(btnFaltantes);
            gbReportes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            gbReportes.Location = new System.Drawing.Point(340, 80);
            gbReportes.Name = "gbReportes";
            gbReportes.Size = new System.Drawing.Size(280, 250);
            gbReportes.TabIndex = 2;
            gbReportes.TabStop = false;
            gbReportes.ForeColor = SystemColors.ControlLightLight;
            gbReportes.Text = "Análisis y Rendimiento";
            // 
            // btnFaltantes
            // 
            btnFaltantes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnFaltantes.Location = new System.Drawing.Point(20, 40);
            btnFaltantes.Name = "btnFaltantes";
            btnFaltantes.Size = new System.Drawing.Size(240, 50);
            btnFaltantes.TabIndex = 0;
            btnFaltantes.Text = "Descuadres por Empleado";
            btnFaltantes.BackColor = SystemColors.AppWorkspace;
            btnFaltantes.ForeColor = SystemColors.ButtonHighlight;
            btnFaltantes.UseVisualStyleBackColor = false;
            btnFaltantes.Click += MostrarProximamente;
            // 
            // btnHorarios
            // 
            btnHorarios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnHorarios.Location = new System.Drawing.Point(20, 110);
            btnHorarios.Name = "btnHorarios";
            btnHorarios.Size = new System.Drawing.Size(240, 50);
            btnHorarios.TabIndex = 1;
            btnHorarios.Text = "Ranking de Ventas por Hora";
            btnHorarios.BackColor = SystemColors.AppWorkspace;
            btnHorarios.ForeColor = SystemColors.ButtonHighlight;
            btnHorarios.UseVisualStyleBackColor = false;
            btnHorarios.Click += MostrarProximamente;
            // 
            // btnAnular
            // 
            btnAnular.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAnular.Location = new System.Drawing.Point(20, 180);
            btnAnular.Name = "btnAnular";
            btnAnular.Size = new System.Drawing.Size(240, 50);
            btnAnular.TabIndex = 2;
            btnAnular.Text = "Auditor�a de Ventas Anuladas";
            btnAnular.BackColor = SystemColors.AppWorkspace;
            btnAnular.ForeColor = SystemColors.ButtonHighlight;
            btnAnular.UseVisualStyleBackColor = false;
            btnAnular.Click += MostrarProximamente;
            // 
            // gbHerramientas
            // 
            gbHerramientas.Controls.Add(btnInventario);
            gbHerramientas.Controls.Add(btnExportarCSV);
            gbHerramientas.Controls.Add(btnImportarExcel);
            gbHerramientas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            gbHerramientas.Location = new System.Drawing.Point(650, 80);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new System.Drawing.Size(280, 250);
            gbHerramientas.TabIndex = 3;
            gbHerramientas.TabStop = false;
            gbHerramientas.ForeColor = SystemColors.ControlLightLight;
            gbHerramientas.Text = "Herramientas de Supervisor";
            // 
            // btnImportarExcel
            // 
            btnImportarExcel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnImportarExcel.Location = new System.Drawing.Point(20, 40);
            btnImportarExcel.Name = "btnImportarExcel";
            btnImportarExcel.Size = new System.Drawing.Size(240, 50);
            btnImportarExcel.TabIndex = 0;
            btnImportarExcel.Text = "Actualizar Precios (Excel)";
            btnImportarExcel.BackColor = SystemColors.AppWorkspace;
            btnImportarExcel.ForeColor = SystemColors.ButtonHighlight;
            btnImportarExcel.UseVisualStyleBackColor = false;
            btnImportarExcel.Click += MostrarProximamente;
            // 
            // btnExportarCSV
            // 
            btnExportarCSV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnExportarCSV.Location = new System.Drawing.Point(20, 110);
            btnExportarCSV.Name = "btnExportarCSV";
            btnExportarCSV.Size = new System.Drawing.Size(240, 50);
            btnExportarCSV.TabIndex = 1;
            btnExportarCSV.Text = "Exportar Datos Contador (CSV)";
            btnExportarCSV.BackColor = SystemColors.AppWorkspace;
            btnExportarCSV.ForeColor = SystemColors.ButtonHighlight;
            btnExportarCSV.UseVisualStyleBackColor = false;
            btnExportarCSV.Click += MostrarProximamente;
            // 
            // btnInventario
            // 
            btnInventario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnInventario.Location = new System.Drawing.Point(20, 180);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new System.Drawing.Size(240, 50);
            btnInventario.TabIndex = 2;
            btnInventario.Text = "Reporte Inventario Valorizado";
            btnInventario.BackColor = SystemColors.AppWorkspace;
            btnInventario.ForeColor = SystemColors.ButtonHighlight;
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += MostrarProximamente;
            // 
            // UcAuditoriaCajas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ForeColor = SystemColors.ControlLightLight;
            Controls.Add(gbHerramientas);
            Controls.Add(gbReportes);
            Controls.Add(gbControles);
            Controls.Add(lblTitulo);
            Name = "UcAuditoriaCajas";
            Size = new System.Drawing.Size(1000, 643);
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
