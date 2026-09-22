using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace KioscoApp
{
    public partial class UcABMUsuarios : UserControl
    {
        private int usuarioIdSeleccionado = 0;
        private ErrorProvider errorProvider = new ErrorProvider();

        public UcABMUsuarios()
        {
            InitializeComponent();
            ConfigurarValidacionesEnTiempoReal();
        }

        private void ConfigurarValidacionesEnTiempoReal()
        {
            txtNombre.MaxLength = 100;
            txtApellido.MaxLength = 100;
            txtUsuario.MaxLength = 50;
            txtContrasena.MaxLength = 100;
            txtEmail.MaxLength = 100;
            txtTelefono.MaxLength = 50;
            txtCalle.MaxLength = 100;
            txtNumero.MaxLength = 20;

            // Validaciones en tiempo real al perder el foco en el campo
            txtEmail.Validating += (s, e) => {
                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    errorProvider.SetError(txtEmail, "Formato de email inválido (ej: correo@gmail.com)");
                else
                    errorProvider.SetError(txtEmail, "");
            };

            txtTelefono.Validating += (s, e) => {
                if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && !System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, @"^[0-9\+\-\s]+$"))
                    errorProvider.SetError(txtTelefono, "Solo se admiten números y signos + o -");
                else
                    errorProvider.SetError(txtTelefono, "");
            };

            // Bloquear ingreso de letras directamente en el teclado para el teléfono
            txtTelefono.KeyPress += (s, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != ' ')
                {
                    e.Handled = true; // Cancela la tecla ingresada
                }
            };

            // Bloquear ingreso de letras directamente en el teclado para el número de calle
            txtNumero.KeyPress += (s, e) => {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Cancela la tecla ingresada (solo admite números)
                }
            };
        }

        private void FrmGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarProvincias();
            cmbProvincia.SelectedIndexChanged += CmbProvincia_SelectedIndexChanged;
            CargarUsuarios();
        }

        private void CargarProvincias()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Nombre FROM Provincias ORDER BY Nombre";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    cmbProvincia.DisplayMember = "Nombre";
                    cmbProvincia.ValueMember = "Id";
                    cmbProvincia.DataSource = dt;
                    cmbProvincia.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar provincias: " + ex.Message);
            }
        }

        private void CmbProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvincia.SelectedValue != null && int.TryParse(cmbProvincia.SelectedValue.ToString(), out int provinciaId))
            {
                CargarCiudades(provinciaId);
            }
            else
            {
                cmbCiudad.DataSource = null;
            }
        }

        private void CargarCiudades(int provinciaId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Nombre FROM Ciudades WHERE ProvinciaId = @ProvinciaId ORDER BY Nombre";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProvinciaId", provinciaId);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        cmbCiudad.DisplayMember = "Nombre";
                        cmbCiudad.ValueMember = "Id";
                        cmbCiudad.DataSource = dt;
                        cmbCiudad.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ciudades: " + ex.Message);
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Nombre, Apellido, Usuario, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento FROM Usuarios";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                usuarioIdSeleccionado = Convert.ToInt32(row.Cells["Id"].Value);
                
                txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                txtApellido.Text = row.Cells["Apellido"].Value?.ToString();
                txtUsuario.Text = row.Cells["Usuario"].Value?.ToString();
                txtContrasena.Text = ""; // Por seguridad
                cmbRol.SelectedItem = row.Cells["Rol"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtTelefono.Text = row.Cells["Telefono"].Value?.ToString();
                txtCalle.Text = row.Cells["Calle"].Value?.ToString();
                txtNumero.Text = row.Cells["Numero"].Value?.ToString();
                
                // Cargar Provincia y Ciudad
                cmbProvincia.SelectedIndexChanged -= CmbProvincia_SelectedIndexChanged;
                cmbProvincia.Text = row.Cells["Provincia"].Value?.ToString();
                cmbProvincia.SelectedIndexChanged += CmbProvincia_SelectedIndexChanged;
                
                if (cmbProvincia.SelectedValue != null && int.TryParse(cmbProvincia.SelectedValue.ToString(), out int provId))
                {
                    CargarCiudades(provId);
                }
                
                cmbCiudad.Text = row.Cells["Ciudad"].Value?.ToString();
                cmbSexo.SelectedItem = row.Cells["Sexo"].Value?.ToString();
                
                if (row.Cells["Nacimiento"].Value != DBNull.Value)
                    dtpNacimiento.Value = Convert.ToDateTime(row.Cells["Nacimiento"].Value);
                else
                    dtpNacimiento.Value = DateTime.Now;

                if (usuarioIdSeleccionado == UserSession.IdUsuario)
                {
                    cmbRol.Enabled = false;
                }
                else
                {
                    cmbRol.Enabled = true;
                }
            }
        }

        private bool ExisteDatoUnico(string campo, string valor, int idExcluir)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = $"SELECT COUNT(1) FROM Usuarios WHERE {campo} = @Valor AND Id != @Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Valor", valor);
                        cmd.Parameters.AddWithValue("@Id", idExcluir);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private bool ValidarFormulario(bool isEdit)
        {
            int idExcluir = isEdit ? usuarioIdSeleccionado : 0;
            bool esValido = true;
            errorProvider.Clear(); // Limpiar errores previos
            System.Collections.Generic.List<string> mensajesError = new System.Collections.Generic.List<string>();

            // Función local para simplificar el registro de errores
            void AddError(Control ctrl, string msg, bool esErrorDetallado)
            {
                errorProvider.SetError(ctrl, msg);
                esValido = false;
                if (esErrorDetallado) mensajesError.Add($"- {msg}");
            }

            // Validaciones de obligatoriedad y formato
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { 
                AddError(txtNombre, "Requerido", false); 
            } else if (txtNombre.Text.Trim().Length < 3) {
                AddError(txtNombre, "El nombre debe tener mínimo 3 caracteres", true);
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text)) { AddError(txtApellido, "Requerido", false); }
            
            if (string.IsNullOrWhiteSpace(txtUsuario.Text)) { 
                AddError(txtUsuario, "Requerido", false); 
            } else if (txtUsuario.Text.Trim().Length < 5) { 
                AddError(txtUsuario, "El usuario debe tener mínimo 5 caracteres", true); 
            } else if (ExisteDatoUnico("Usuario", txtUsuario.Text.Trim(), idExcluir)) {
                AddError(txtUsuario, "El usuario ingresado ya existe en el sistema", true);
            }
            
            if (!isEdit && string.IsNullOrWhiteSpace(txtContrasena.Text)) { AddError(txtContrasena, "Requerido para usuarios nuevos", false); }
            if (cmbRol.SelectedIndex == -1) { AddError(cmbRol, "Requerido", false); }

            // Email (Regex + Unicidad)
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) { 
                AddError(txtEmail, "Requerido", false); 
            } else if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) {
                AddError(txtEmail, "El formato del email es inválido (ej: correo@gmail.com)", true);
            } else if (ExisteDatoUnico("Email", txtEmail.Text.Trim(), idExcluir)) {
                AddError(txtEmail, "El email ingresado ya está en uso por otro usuario", true);
            }

            // Teléfono (Regex + Unicidad)
            if (string.IsNullOrWhiteSpace(txtTelefono.Text)) { 
                AddError(txtTelefono, "Requerido", false); 
            } else if (!System.Text.RegularExpressions.Regex.IsMatch(txtTelefono.Text, @"^[0-9\+\-\s]+$")) {
                AddError(txtTelefono, "El teléfono solo puede contener números y signos + o -", true);
            } else if (txtTelefono.Text.Replace(" ", "").Replace("-", "").Replace("+", "").Length < 8) {
                AddError(txtTelefono, "El teléfono debe tener mínimo 8 números", true);
            } else if (ExisteDatoUnico("Telefono", txtTelefono.Text.Trim(), idExcluir)) {
                AddError(txtTelefono, "El teléfono ingresado ya está registrado", true);
            }

            // Dirección
            if (string.IsNullOrWhiteSpace(txtCalle.Text)) { 
                AddError(txtCalle, "Requerido", false); 
            } else if (txtCalle.Text.Trim().Length < 5) {
                AddError(txtCalle, "La calle debe tener mínimo 5 caracteres", true);
            }
            if (string.IsNullOrWhiteSpace(txtNumero.Text)) { AddError(txtNumero, "Requerido", false); }
            
            // Provincia y Ciudad (Debe estar en la base de datos)
            if (cmbProvincia.SelectedIndex == -1) { 
                if (string.IsNullOrWhiteSpace(cmbProvincia.Text)) AddError(cmbProvincia, "Requerido", false); 
                else AddError(cmbProvincia, "Debe seleccionar una Provincia de la lista sugerida", true); 
            }
            if (cmbCiudad.SelectedIndex == -1) { 
                if (string.IsNullOrWhiteSpace(cmbCiudad.Text)) AddError(cmbCiudad, "Requerido", false); 
                else AddError(cmbCiudad, "Debe seleccionar una Ciudad de la lista sugerida", true); 
            }
            
            if (cmbSexo.SelectedIndex == -1) { AddError(cmbSexo, "Requerido", false); }
            
            if (dtpNacimiento.Value.Date >= DateTime.Now.Date) { 
                AddError(dtpNacimiento, "La fecha de nacimiento no es válida", true); 
            }

            if (!esValido)
            {
                string mensajeAlerta = "Por favor, complete o corrija los campos marcados en rojo.\n\n";
                if (mensajesError.Count > 0)
                {
                    mensajeAlerta += "Detalles de los errores:\n" + string.Join("\n", mensajesError);
                }
                MessageBox.Show(mensajeAlerta, "Validación de Formulario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return esValido;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario(false)) return;

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"INSERT INTO Usuarios 
                        (Nombre, Apellido, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
                        VALUES 
                        (@Nombre, @Apellido, @Usuario, @Contrasena, @Rol, @Email, @Telefono, @Calle, @Numero, @Ciudad, @Provincia, @Sexo, @Nacimiento)";
                        
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        string hashPassword = BCrypt.Net.BCrypt.HashPassword(txtContrasena.Text);

                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@Contrasena", hashPassword);
                        cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                        cmd.Parameters.AddWithValue("@Calle", txtCalle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Numero", txtNumero.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ciudad", cmbCiudad.Text.Trim());
                        cmd.Parameters.AddWithValue("@Provincia", cmbProvincia.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sexo", cmbSexo.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@Nacimiento", dtpNacimiento.Value.Date);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                CargarUsuarios();
                MessageBox.Show("Usuario registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado == 0) return;
            if (usuarioIdSeleccionado == UserSession.IdUsuario && cmbRol.SelectedItem?.ToString() != "admin")
            {
                MessageBox.Show("No puede quitarse el rol de administrador a sí mismo mientras está en sesión.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si hay cambios
            DataGridViewRow row = null;
            foreach (DataGridViewRow r in dgvUsuarios.Rows)
            {
                if (r.Cells["Id"].Value != null && Convert.ToInt32(r.Cells["Id"].Value) == usuarioIdSeleccionado)
                {
                    row = r;
                    break;
                }
            }

            if (row != null)
            {
                bool hayCambios = false;
                if (txtNombre.Text.Trim() != row.Cells["Nombre"].Value?.ToString()) hayCambios = true;
                if (txtApellido.Text.Trim() != row.Cells["Apellido"].Value?.ToString()) hayCambios = true;
                if (txtUsuario.Text.Trim() != row.Cells["Usuario"].Value?.ToString()) hayCambios = true;
                if (!string.IsNullOrEmpty(txtContrasena.Text)) hayCambios = true;
                if (cmbRol.SelectedItem?.ToString() != row.Cells["Rol"].Value?.ToString()) hayCambios = true;
                if (txtEmail.Text.Trim() != row.Cells["Email"].Value?.ToString()) hayCambios = true;
                if (txtTelefono.Text.Trim() != row.Cells["Telefono"].Value?.ToString()) hayCambios = true;
                if (txtCalle.Text.Trim() != row.Cells["Calle"].Value?.ToString()) hayCambios = true;
                if (txtNumero.Text.Trim() != row.Cells["Numero"].Value?.ToString()) hayCambios = true;
                if (cmbProvincia.Text.Trim() != row.Cells["Provincia"].Value?.ToString()) hayCambios = true;
                if (cmbCiudad.Text.Trim() != row.Cells["Ciudad"].Value?.ToString()) hayCambios = true;
                if (cmbSexo.SelectedItem?.ToString() != row.Cells["Sexo"].Value?.ToString()) hayCambios = true;
                
                if (row.Cells["Nacimiento"].Value != DBNull.Value)
                {
                    if (dtpNacimiento.Value.Date != Convert.ToDateTime(row.Cells["Nacimiento"].Value).Date) hayCambios = true;
                }
                else
                {
                    hayCambios = true;
                }

                if (!hayCambios)
                {
                    MessageBox.Show("No se modificó ningún dato.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (!ValidarFormulario(true)) return;

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"UPDATE Usuarios SET 
                        Nombre=@Nombre, Apellido=@Apellido, Usuario=@Usuario, Rol=@Rol, 
                        Email=@Email, Telefono=@Telefono, Calle=@Calle, Numero=@Numero, 
                        Ciudad=@Ciudad, Provincia=@Provincia, Sexo=@Sexo, Nacimiento=@Nacimiento " + 
                        (string.IsNullOrEmpty(txtContrasena.Text) ? "" : ", Contrasena=@Contrasena ") + 
                        "WHERE Id=@Id";
                        
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", usuarioIdSeleccionado);
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                        cmd.Parameters.AddWithValue("@Calle", txtCalle.Text.Trim());
                        cmd.Parameters.AddWithValue("@Numero", txtNumero.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ciudad", cmbCiudad.Text.Trim());
                        cmd.Parameters.AddWithValue("@Provincia", cmbProvincia.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sexo", cmbSexo.SelectedItem?.ToString());
                        cmd.Parameters.AddWithValue("@Nacimiento", dtpNacimiento.Value.Date);

                        if (!string.IsNullOrEmpty(txtContrasena.Text))
                        {
                            string hashPassword = BCrypt.Net.BCrypt.HashPassword(txtContrasena.Text);
                            cmd.Parameters.AddWithValue("@Contrasena", hashPassword);
                        }
                            
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                CargarUsuarios();
                MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado == 0) return;
            if (usuarioIdSeleccionado == UserSession.IdUsuario)
            {
                MessageBox.Show("No puede eliminar su propio usuario mientras está en sesión.", "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var confirmResult = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult != DialogResult.Yes) return;

            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Usuarios WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", usuarioIdSeleccionado);
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            usuarioIdSeleccionado = 0;
            errorProvider.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtCalle.Clear();
            txtNumero.Clear();
            cmbCiudad.SelectedIndex = -1;
            cmbCiudad.Text = "";
            cmbProvincia.SelectedIndex = -1;
            cmbProvincia.Text = "";
            cmbSexo.SelectedIndex = -1;
            cmbRol.SelectedIndex = -1;
            cmbRol.Enabled = true;
            dtpNacimiento.Value = DateTime.Now;
        }
    }
}
