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
            dgvClientes = new DataGridView();
            lblDNI = new Label();
            txtDNI = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblCalle = new Label();
            txtCalle = new TextBox();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblProvincia = new Label();
            cmbProvincia = new ComboBox();
            lblCiudad = new Label();
            cmbCiudad = new ComboBox();
            lblNacimiento = new Label();
            dtpNacimiento = new DateTimePicker();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(20, 20);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(500, 420);
            dgvClientes.TabIndex = 0;
            // 
            // lblDNI
            // 
            lblDNI.AutoSize = true;
            lblDNI.Location = new Point(540, 20);
            lblDNI.Name = "lblDNI";
            lblDNI.Size = new Size(104, 15);
            lblDNI.TabIndex = 23;
            lblDNI.Text = "DNI / Documento:";
            // 
            // txtDNI
            // 
            txtDNI.Location = new Point(540, 38);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(200, 23);
            txtDNI.TabIndex = 22;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(540, 70);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 21;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(540, 88);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 23);
            txtNombre.TabIndex = 20;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(540, 120);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 19;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(540, 138);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(200, 23);
            txtApellido.TabIndex = 18;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(540, 170);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 17;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(540, 188);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 23);
            txtEmail.TabIndex = 16;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(540, 220);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 15;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(540, 238);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(200, 23);
            txtTelefono.TabIndex = 14;
            // 
            // lblCalle
            // 
            lblCalle.AutoSize = true;
            lblCalle.Location = new Point(760, 20);
            lblCalle.Name = "lblCalle";
            lblCalle.Size = new Size(36, 15);
            lblCalle.TabIndex = 13;
            lblCalle.Text = "Calle:";
            // 
            // txtCalle
            // 
            txtCalle.Location = new Point(760, 38);
            txtCalle.Name = "txtCalle";
            txtCalle.Size = new Size(200, 23);
            txtCalle.TabIndex = 12;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(760, 70);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(54, 15);
            lblNumero.TabIndex = 11;
            lblNumero.Text = "Número:";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(760, 88);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(200, 23);
            txtNumero.TabIndex = 10;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new Point(760, 120);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new Size(59, 15);
            lblProvincia.TabIndex = 9;
            lblProvincia.Text = "Provincia:";
            // 
            // cmbProvincia
            // 
            cmbProvincia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProvincia.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.Location = new Point(760, 138);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new Size(200, 23);
            cmbProvincia.TabIndex = 8;
            // 
            // lblCiudad
            // 
            lblCiudad.AutoSize = true;
            lblCiudad.Location = new Point(760, 170);
            lblCiudad.Name = "lblCiudad";
            lblCiudad.Size = new Size(48, 15);
            lblCiudad.TabIndex = 7;
            lblCiudad.Text = "Ciudad:";
            // 
            // cmbCiudad
            // 
            cmbCiudad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCiudad.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCiudad.FormattingEnabled = true;
            cmbCiudad.Location = new Point(760, 188);
            cmbCiudad.Name = "cmbCiudad";
            cmbCiudad.Size = new Size(200, 23);
            cmbCiudad.TabIndex = 6;
            // 
            // lblNacimiento
            // 
            lblNacimiento.AutoSize = true;
            lblNacimiento.Location = new Point(760, 220);
            lblNacimiento.Name = "lblNacimiento";
            lblNacimiento.Size = new Size(106, 15);
            lblNacimiento.TabIndex = 5;
            lblNacimiento.Text = "Fecha Nacimiento:";
            // 
            // dtpNacimiento
            // 
            dtpNacimiento.Format = DateTimePickerFormat.Short;
            dtpNacimiento.Location = new Point(760, 238);
            dtpNacimiento.Name = "dtpNacimiento";
            dtpNacimiento.Size = new Size(200, 23);
            dtpNacimiento.TabIndex = 4;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(540, 290);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 30);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(645, 290);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(95, 30);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(760, 290);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 30);
            btnEliminar.TabIndex = 1;
            btnEliminar.Text = "Eliminar";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(865, 290);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(95, 30);
            btnLimpiar.TabIndex = 0;
            btnLimpiar.Text = "Limpiar";
            // 
            // UcABMClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnGuardar);
            Controls.Add(dtpNacimiento);
            Controls.Add(lblNacimiento);
            Controls.Add(cmbCiudad);
            Controls.Add(lblCiudad);
            Controls.Add(cmbProvincia);
            Controls.Add(lblProvincia);
            Controls.Add(txtNumero);
            Controls.Add(lblNumero);
            Controls.Add(txtCalle);
            Controls.Add(lblCalle);
            Controls.Add(txtTelefono);
            Controls.Add(lblTelefono);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(txtApellido);
            Controls.Add(lblApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(txtDNI);
            Controls.Add(lblDNI);
            Controls.Add(dgvClientes);
            Name = "UcABMClientes";
            Size = new Size(1000, 470);
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
