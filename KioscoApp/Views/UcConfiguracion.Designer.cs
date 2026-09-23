namespace KioscoApp
{
    partial class UcConfiguracion
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
            gbBaseDatos = new GroupBox();
            btnLimpiar = new Button();
            btnRestore = new Button();
            btnBackup = new Button();
            gbEmpresa = new GroupBox();
            btnLicencia = new Button();
            btnImpresora = new Button();
            btnDatosEmpresa = new Button();
            gbHerramientas = new GroupBox();
            btnLogs = new Button();
            btnTrazabilidad = new Button();
            gbBaseDatos.SuspendLayout();
            gbEmpresa.SuspendLayout();
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
            lblTitulo.Size = new Size(283, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Configuración del Sistema";
            // 
            // gbBaseDatos
            // 
            gbBaseDatos.Controls.Add(btnLimpiar);
            gbBaseDatos.Controls.Add(btnRestore);
            gbBaseDatos.Controls.Add(btnBackup);
            gbBaseDatos.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbBaseDatos.ForeColor = SystemColors.ControlLightLight;
            gbBaseDatos.Location = new Point(30, 80);
            gbBaseDatos.Name = "gbBaseDatos";
            gbBaseDatos.Size = new Size(280, 250);
            gbBaseDatos.TabIndex = 1;
            gbBaseDatos.TabStop = false;
            gbBaseDatos.Text = "Base de Datos y Seguridad";
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = SystemColors.AppWorkspace;
            btnLimpiar.Font = new Font("Segoe UI", 10F);
            btnLimpiar.ForeColor = SystemColors.ButtonHighlight;
            btnLimpiar.Location = new Point(20, 180);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(240, 50);
            btnLimpiar.TabIndex = 2;
            btnLimpiar.Text = "Limpiar Datos Antiguos";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += MostrarProximamente;
            // 
            // btnRestore
            // 
            btnRestore.BackColor = SystemColors.AppWorkspace;
            btnRestore.Font = new Font("Segoe UI", 10F);
            btnRestore.ForeColor = SystemColors.ButtonHighlight;
            btnRestore.Location = new Point(20, 110);
            btnRestore.Name = "btnRestore";
            btnRestore.Size = new Size(240, 50);
            btnRestore.TabIndex = 1;
            btnRestore.Text = "Restaurar Base de Datos";
            btnRestore.UseVisualStyleBackColor = false;
            btnRestore.Click += MostrarProximamente;
            // 
            // btnBackup
            // 
            btnBackup.BackColor = SystemColors.AppWorkspace;
            btnBackup.Font = new Font("Segoe UI", 10F);
            btnBackup.ForeColor = SystemColors.ButtonHighlight;
            btnBackup.Location = new Point(20, 40);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(240, 50);
            btnBackup.TabIndex = 0;
            btnBackup.Text = "Crear Copia de Seguridad";
            btnBackup.UseVisualStyleBackColor = false;
            btnBackup.Click += MostrarProximamente;
            // 
            // gbEmpresa
            // 
            gbEmpresa.Controls.Add(btnLicencia);
            gbEmpresa.Controls.Add(btnImpresora);
            gbEmpresa.Controls.Add(btnDatosEmpresa);
            gbEmpresa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbEmpresa.ForeColor = SystemColors.ControlLightLight;
            gbEmpresa.Location = new Point(340, 80);
            gbEmpresa.Name = "gbEmpresa";
            gbEmpresa.Size = new Size(280, 250);
            gbEmpresa.TabIndex = 2;
            gbEmpresa.TabStop = false;
            gbEmpresa.Text = "Datos de Empresa";
            // 
            // btnLicencia
            // 
            btnLicencia.BackColor = SystemColors.AppWorkspace;
            btnLicencia.Font = new Font("Segoe UI", 10F);
            btnLicencia.ForeColor = SystemColors.ButtonHighlight;
            btnLicencia.Location = new Point(20, 180);
            btnLicencia.Name = "btnLicencia";
            btnLicencia.Size = new Size(240, 50);
            btnLicencia.TabIndex = 2;
            btnLicencia.Text = "Licencia y Acerca De...";
            btnLicencia.UseVisualStyleBackColor = false;
            btnLicencia.Click += MostrarProximamente;
            // 
            // btnImpresora
            // 
            btnImpresora.BackColor = SystemColors.AppWorkspace;
            btnImpresora.Font = new Font("Segoe UI", 10F);
            btnImpresora.ForeColor = SystemColors.ButtonHighlight;
            btnImpresora.Location = new Point(20, 110);
            btnImpresora.Name = "btnImpresora";
            btnImpresora.Size = new Size(240, 50);
            btnImpresora.TabIndex = 1;
            btnImpresora.Text = "Configurar Impresora";
            btnImpresora.UseVisualStyleBackColor = false;
            btnImpresora.Click += MostrarProximamente;
            // 
            // btnDatosEmpresa
            // 
            btnDatosEmpresa.BackColor = SystemColors.AppWorkspace;
            btnDatosEmpresa.Font = new Font("Segoe UI", 10F);
            btnDatosEmpresa.ForeColor = SystemColors.ButtonHighlight;
            btnDatosEmpresa.Location = new Point(20, 40);
            btnDatosEmpresa.Name = "btnDatosEmpresa";
            btnDatosEmpresa.Size = new Size(240, 50);
            btnDatosEmpresa.TabIndex = 0;
            btnDatosEmpresa.Text = "Info del Negocio (Ticket)";
            btnDatosEmpresa.UseVisualStyleBackColor = false;
            btnDatosEmpresa.Click += MostrarProximamente;
            // 
            // gbHerramientas
            // 
            gbHerramientas.Controls.Add(btnLogs);
            gbHerramientas.Controls.Add(btnTrazabilidad);
            gbHerramientas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbHerramientas.ForeColor = SystemColors.ControlLightLight;
            gbHerramientas.Location = new Point(650, 80);
            gbHerramientas.Name = "gbHerramientas";
            gbHerramientas.Size = new Size(280, 250);
            gbHerramientas.TabIndex = 3;
            gbHerramientas.TabStop = false;
            gbHerramientas.Text = "Herramientas del Sistema";
            // 
            // btnLogs
            // 
            btnLogs.BackColor = SystemColors.AppWorkspace;
            btnLogs.Font = new Font("Segoe UI", 10F);
            btnLogs.ForeColor = SystemColors.ButtonHighlight;
            btnLogs.Location = new Point(20, 110);
            btnLogs.Name = "btnLogs";
            btnLogs.Size = new Size(240, 50);
            btnLogs.TabIndex = 2;
            btnLogs.Text = "Ver Registro de Errores (Logs)";
            btnLogs.UseVisualStyleBackColor = false;
            btnLogs.Click += MostrarProximamente;
            // 
            // btnTrazabilidad
            // 
            btnTrazabilidad.BackColor = SystemColors.AppWorkspace;
            btnTrazabilidad.Font = new Font("Segoe UI", 10F);
            btnTrazabilidad.ForeColor = SystemColors.ButtonHighlight;
            btnTrazabilidad.Location = new Point(20, 40);
            btnTrazabilidad.Name = "btnTrazabilidad";
            btnTrazabilidad.Size = new Size(240, 50);
            btnTrazabilidad.TabIndex = 0;
            btnTrazabilidad.Text = "Trazabilidad de Seguridad";
            btnTrazabilidad.UseVisualStyleBackColor = false;
            btnTrazabilidad.Click += MostrarProximamente;
            // 
            // UcConfiguracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(gbHerramientas);
            Controls.Add(gbEmpresa);
            Controls.Add(gbBaseDatos);
            Controls.Add(lblTitulo);
            ForeColor = SystemColors.ControlLightLight;
            Name = "UcConfiguracion";
            Size = new Size(1000, 643);
            gbBaseDatos.ResumeLayout(false);
            gbEmpresa.ResumeLayout(false);
            gbHerramientas.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbBaseDatos;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.GroupBox gbEmpresa;
        private System.Windows.Forms.Button btnLicencia;
        private System.Windows.Forms.Button btnImpresora;
        private System.Windows.Forms.Button btnDatosEmpresa;
        private System.Windows.Forms.GroupBox gbHerramientas;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.Button btnTrazabilidad;
    }
}
