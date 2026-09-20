using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    partial class UcABMUsuarios
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
            dgvUsuarios = new DataGridView();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            lblRol = new Label();
            cmbRol = new ComboBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            
            lblCalle = new Label();
            txtCalle = new TextBox();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblCiudad = new Label();
            cmbCiudad = new ComboBox();
            lblProvincia = new Label();
            cmbProvincia = new ComboBox();
  
            lblSexo = new Label();
            cmbSexo = new ComboBox();
            lblNacimiento = new Label();
            dtpNacimiento = new DateTimePicker();
            
            btnAgregar = new Button();
            btnEditar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(20, 20);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(500, 420);
            dgvUsuarios.TabIndex = 0;
            dgvUsuarios.CellClick += DgvUsuarios_CellClick;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(540, 20);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(54, 15);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(540, 38);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 23);
            txtNombre.TabIndex = 2;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(540, 70);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(54, 15);
            lblApellido.TabIndex = 3;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(540, 88);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(200, 23);
            txtApellido.TabIndex = 4;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(540, 120);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(50, 15);
            lblUsuario.TabIndex = 5;
            lblUsuario.Text = "Usuario:";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(540, 138);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(200, 23);
            txtUsuario.TabIndex = 6;
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Location = new Point(540, 170);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(70, 15);
            lblContrasena.TabIndex = 7;
            lblContrasena.Text = "Contraseña:";
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(540, 188);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(200, 23);
            txtContrasena.TabIndex = 8;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new Point(540, 220);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(27, 15);
            lblRol.TabIndex = 9;
            lblRol.Text = "Rol:";
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.FormattingEnabled = true;
            cmbRol.Items.AddRange(new object[] { "Admin", "Vendedor" });
            cmbRol.Location = new Point(540, 238);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(200, 23);
            cmbRol.TabIndex = 10;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(540, 270);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 11;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(540, 288);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 23);
            txtEmail.TabIndex = 12;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(540, 320);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 13;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(540, 338);
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
            lblCalle.TabIndex = 15;
            lblCalle.Text = "Calle:";
            // 
            // txtCalle
            // 
            txtCalle.Location = new Point(760, 38);
            txtCalle.Name = "txtCalle";
            txtCalle.Size = new Size(200, 23);
            txtCalle.TabIndex = 16;
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(760, 70);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(54, 15);
            lblNumero.TabIndex = 17;
            lblNumero.Text = "Número:";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(760, 88);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(200, 23);
            txtNumero.TabIndex = 18;
            // 
            // lblProvincia
            // 
            lblProvincia.AutoSize = true;
            lblProvincia.Location = new Point(760, 120);
            lblProvincia.Name = "lblProvincia";
            lblProvincia.Size = new Size(59, 15);
            lblProvincia.TabIndex = 19;
            lblProvincia.Text = "Provincia:";
            // 
            // cmbProvincia
            // 
            cmbProvincia.FormattingEnabled = true;
            cmbProvincia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProvincia.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProvincia.Location = new Point(760, 138);
            cmbProvincia.Name = "cmbProvincia";
            cmbProvincia.Size = new Size(200, 23);
            cmbProvincia.TabIndex = 20;
            // 
            // lblCiudad
            // 
            lblCiudad.AutoSize = true;
            lblCiudad.Location = new Point(760, 170);
            lblCiudad.Name = "lblCiudad";
            lblCiudad.Size = new Size(48, 15);
            lblCiudad.TabIndex = 21;
            lblCiudad.Text = "Ciudad:";
            // 
            // cmbCiudad
            // 
            cmbCiudad.FormattingEnabled = true;
            cmbCiudad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCiudad.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCiudad.Location = new Point(760, 188);
            cmbCiudad.Name = "cmbCiudad";
            cmbCiudad.Size = new Size(200, 23);
            cmbCiudad.TabIndex = 22;
            //
            // lblSexo
            // 
            lblSexo.AutoSize = true;
            lblSexo.Location = new Point(760, 220);
            lblSexo.Name = "lblSexo";
            lblSexo.Size = new Size(35, 15);
            lblSexo.TabIndex = 23;
            lblSexo.Text = "Sexo:";
            // 
            // cmbSexo
            // 
            cmbSexo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSexo.FormattingEnabled = true;
            cmbSexo.Items.AddRange(new object[] { "Femenino", "Masculino", "Otro" });
            cmbSexo.Location = new Point(760, 238);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new Size(200, 23);
            cmbSexo.TabIndex = 24;
            // 
            // lblNacimiento
            // 
            lblNacimiento.AutoSize = true;
            lblNacimiento.Location = new Point(760, 270);
            lblNacimiento.Name = "lblNacimiento";
            lblNacimiento.Size = new Size(72, 15);
            lblNacimiento.TabIndex = 25;
            lblNacimiento.Text = "Nacimiento:";
            // 
            // dtpNacimiento
            // 
            dtpNacimiento.Location = new Point(760, 288);
            dtpNacimiento.Name = "dtpNacimiento";
            dtpNacimiento.Format = DateTimePickerFormat.Short;
            dtpNacimiento.Size = new Size(200, 23);
            dtpNacimiento.TabIndex = 26;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(540, 380);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 30);
            btnAgregar.TabIndex = 27;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += BtnAgregar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(645, 380);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(95, 30);
            btnEditar.TabIndex = 28;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += BtnEditar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(760, 380);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(95, 30);
            btnEliminar.TabIndex = 29;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += BtnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(865, 380);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(95, 30);
            btnLimpiar.TabIndex = 30;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += BtnLimpiar_Click;
            // 
            // UcABMUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnAgregar);
            
            Controls.Add(dtpNacimiento);
            Controls.Add(lblNacimiento);
            Controls.Add(cmbSexo);
            Controls.Add(lblSexo);
            Controls.Add(cmbProvincia);
            Controls.Add(lblProvincia);
            Controls.Add(cmbCiudad);
            Controls.Add(lblCiudad);
            Controls.Add(txtNumero);
            Controls.Add(lblNumero);
            Controls.Add(txtCalle);
            Controls.Add(lblCalle);
            
            Controls.Add(txtTelefono);
            Controls.Add(lblTelefono);
            Controls.Add(txtEmail);
            Controls.Add(lblEmail);
            Controls.Add(cmbRol);
            Controls.Add(lblRol);
            Controls.Add(txtContrasena);
            Controls.Add(lblContrasena);
            Controls.Add(txtUsuario);
            Controls.Add(lblUsuario);
            Controls.Add(txtApellido);
            Controls.Add(lblApellido);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(dgvUsuarios);
            Name = "UcABMUsuarios";
            Size = new Size(1000, 470);
            Load += FrmGestionUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox txtContrasena;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.TextBox txtCalle;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label lblCiudad;
        private System.Windows.Forms.ComboBox cmbCiudad;
        private System.Windows.Forms.Label lblProvincia;
        private System.Windows.Forms.ComboBox cmbProvincia;
        
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.ComboBox cmbSexo;
        private System.Windows.Forms.Label lblNacimiento;
        private System.Windows.Forms.DateTimePicker dtpNacimiento;
        
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}
