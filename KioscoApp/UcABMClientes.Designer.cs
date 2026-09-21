using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    partial class UcABMClientes
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            
            this.lblCalle = new System.Windows.Forms.Label();
            this.txtCalle = new System.Windows.Forms.TextBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblProvincia = new System.Windows.Forms.Label();
            this.cmbProvincia = new System.Windows.Forms.ComboBox();
            this.lblCiudad = new System.Windows.Forms.Label();
            this.cmbCiudad = new System.Windows.Forms.ComboBox();
            this.lblNacimiento = new System.Windows.Forms.Label();
            this.dtpNacimiento = new System.Windows.Forms.DateTimePicker();
            
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvClientes
            // 
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(20, 20);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(500, 420);
            this.dgvClientes.TabIndex = 0;

            // =========================
            // COLUMNA 1 (X = 540)
            // =========================
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(540, 20);
            this.lblDNI.Text = "DNI / Documento:";
            this.txtDNI.Location = new System.Drawing.Point(540, 38);
            this.txtDNI.Size = new System.Drawing.Size(200, 23);

            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(540, 70);
            this.lblNombre.Text = "Nombre:";
            this.txtNombre.Location = new System.Drawing.Point(540, 88);
            this.txtNombre.Size = new System.Drawing.Size(200, 23);

            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(540, 120);
            this.lblApellido.Text = "Apellido:";
            this.txtApellido.Location = new System.Drawing.Point(540, 138);
            this.txtApellido.Size = new System.Drawing.Size(200, 23);

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(540, 170);
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(540, 188);
            this.txtEmail.Size = new System.Drawing.Size(200, 23);

            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(540, 220);
            this.lblTelefono.Text = "Teléfono:";
            this.txtTelefono.Location = new System.Drawing.Point(540, 238);
            this.txtTelefono.Size = new System.Drawing.Size(200, 23);

            // =========================
            // COLUMNA 2 (X = 760)
            // =========================
            this.lblCalle.AutoSize = true;
            this.lblCalle.Location = new System.Drawing.Point(760, 20);
            this.lblCalle.Text = "Calle:";
            this.txtCalle.Location = new System.Drawing.Point(760, 38);
            this.txtCalle.Size = new System.Drawing.Size(200, 23);

            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(760, 70);
            this.lblNumero.Text = "Número:";
            this.txtNumero.Location = new System.Drawing.Point(760, 88);
            this.txtNumero.Size = new System.Drawing.Size(200, 23);

            this.lblProvincia.AutoSize = true;
            this.lblProvincia.Location = new System.Drawing.Point(760, 120);
            this.lblProvincia.Text = "Provincia:";
            this.cmbProvincia.FormattingEnabled = true;
            this.cmbProvincia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProvincia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProvincia.Location = new System.Drawing.Point(760, 138);
            this.cmbProvincia.Size = new System.Drawing.Size(200, 23);

            this.lblCiudad.AutoSize = true;
            this.lblCiudad.Location = new System.Drawing.Point(760, 170);
            this.lblCiudad.Text = "Ciudad:";
            this.cmbCiudad.FormattingEnabled = true;
            this.cmbCiudad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCiudad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCiudad.Location = new System.Drawing.Point(760, 188);
            this.cmbCiudad.Size = new System.Drawing.Size(200, 23);

            this.lblNacimiento.AutoSize = true;
            this.lblNacimiento.Location = new System.Drawing.Point(760, 220);
            this.lblNacimiento.Text = "Fecha Nacimiento:";
            this.dtpNacimiento.Location = new System.Drawing.Point(760, 238);
            this.dtpNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNacimiento.Size = new System.Drawing.Size(200, 23);

            // =========================
            // BOTONERA (Abajo)
            // =========================
            this.btnGuardar.Location = new System.Drawing.Point(540, 290);
            this.btnGuardar.Size = new System.Drawing.Size(95, 30);
            this.btnGuardar.Text = "Guardar";

            this.btnEditar.Location = new System.Drawing.Point(645, 290);
            this.btnEditar.Size = new System.Drawing.Size(95, 30);
            this.btnEditar.Text = "Editar";

            this.btnEliminar.Location = new System.Drawing.Point(760, 290);
            this.btnEliminar.Size = new System.Drawing.Size(95, 30);
            this.btnEliminar.Text = "Eliminar";

            this.btnLimpiar.Location = new System.Drawing.Point(865, 290);
            this.btnLimpiar.Size = new System.Drawing.Size(95, 30);
            this.btnLimpiar.Text = "Limpiar";

            // 
            // UcABMClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnGuardar);
            
            this.Controls.Add(this.dtpNacimiento);
            this.Controls.Add(this.lblNacimiento);
            this.Controls.Add(this.cmbCiudad);
            this.Controls.Add(this.lblCiudad);
            this.Controls.Add(this.cmbProvincia);
            this.Controls.Add(this.lblProvincia);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtCalle);
            this.Controls.Add(this.lblCalle);
            
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtDNI);
            this.Controls.Add(this.lblDNI);
            
            this.Controls.Add(this.dgvClientes);
            
            this.Name = "UcABMClientes";
            this.Size = new System.Drawing.Size(1000, 470);
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.TextBox txtCalle;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblProvincia;
        private System.Windows.Forms.ComboBox cmbProvincia;
        private System.Windows.Forms.Label lblCiudad;
        private System.Windows.Forms.ComboBox cmbCiudad;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}
