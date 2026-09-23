using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcABMUsuarios : UserControl
    {
        private int usuarioIdSeleccionado = 0;
        private ErrorProvider errorProvider = new ErrorProvider();
        private UsuarioService _usuarioService;
        private UbicacionService _ubicacionService;
        private List<Usuario> _listaUsuarios;

        public UcABMUsuarios()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            _ubicacionService = new UbicacionService();
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

            txtTelefono.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != ' ')
                    e.Handled = true;
            };

            txtNumero.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
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
                var provincias = _ubicacionService.ObtenerProvincias();
                cmbProvincia.DisplayMember = "Nombre";
                cmbProvincia.ValueMember = "Id";
                cmbProvincia.DataSource = provincias;
                cmbProvincia.SelectedIndex = -1;
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
                var ciudades = _ubicacionService.ObtenerCiudadesPorProvincia(provinciaId);
                cmbCiudad.DisplayMember = "Nombre";
                cmbCiudad.ValueMember = "Id";
                cmbCiudad.DataSource = ciudades;
                cmbCiudad.SelectedIndex = -1;
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
                _listaUsuarios = _usuarioService.ObtenerTodos();
                dgvUsuarios.DataSource = _listaUsuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvUsuarios.Rows[e.RowIndex];
                usuarioIdSeleccionado = Convert.ToInt32(row.Cells["Id"].Value);

                txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                txtApellido.Text = row.Cells["Apellido"].Value?.ToString();
                txtUsuario.Text = row.Cells["NombreUsuario"].Value?.ToString(); // Mapeado a NombreUsuario
                txtContrasena.Text = "";
                cmbRol.SelectedItem = row.Cells["Rol"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtTelefono.Text = row.Cells["Telefono"].Value?.ToString();
                txtCalle.Text = row.Cells["Calle"].Value?.ToString();
                txtNumero.Text = row.Cells["Numero"].Value?.ToString();

                cmbProvincia.SelectedIndexChanged -= CmbProvincia_SelectedIndexChanged;
                cmbProvincia.Text = row.Cells["Provincia"].Value?.ToString();
                cmbProvincia.SelectedIndexChanged += CmbProvincia_SelectedIndexChanged;

                if (cmbProvincia.SelectedValue != null && int.TryParse(cmbProvincia.SelectedValue.ToString(), out int provId))
                {
                    CargarCiudades(provId);
                }

                cmbCiudad.Text = row.Cells["Ciudad"].Value?.ToString();
                cmbSexo.SelectedItem = row.Cells["Sexo"].Value?.ToString();

                if (row.Cells["Nacimiento"].Value != null)
                    dtpNacimiento.Value = Convert.ToDateTime(row.Cells["Nacimiento"].Value);
                else
                    dtpNacimiento.Value = DateTime.Now;

                cmbRol.Enabled = (usuarioIdSeleccionado != UserSession.IdUsuario);
            }
        }

        private Usuario ConstruirUsuarioDesdeForm()
        {
            return new Usuario
            {
                Id = usuarioIdSeleccionado,
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                NombreUsuario = txtUsuario.Text.Trim(),
                Contrasena = txtContrasena.Text,
                Rol = cmbRol.SelectedItem?.ToString(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Calle = txtCalle.Text.Trim(),
                Numero = txtNumero.Text.Trim(),
                Ciudad = cmbCiudad.Text.Trim(),
                Provincia = cmbProvincia.Text.Trim(),
                Sexo = cmbSexo.SelectedItem?.ToString(),
                Nacimiento = dtpNacimiento.Value.Date
            };
        }

        private bool MostrarErroresDeValidacion(ValidationResult resultado)
        {
            errorProvider.Clear();
            if (resultado.IsValid) return true;

            var controlMap = new Dictionary<string, Control>
            {
                { "Nombre", txtNombre },
                { "Apellido", txtApellido },
                { "Usuario", txtUsuario },
                { "Contrasena", txtContrasena },
                { "Rol", cmbRol },
                { "Email", txtEmail },
                { "Telefono", txtTelefono },
                { "Calle", txtCalle },
                { "Numero", txtNumero },
                { "Ciudad", cmbCiudad },
                { "Provincia", cmbProvincia },
                { "Sexo", cmbSexo },
                { "Nacimiento", dtpNacimiento },
                { "General", this }
            };

            List<string> mensajesError = new List<string>();

            foreach (var error in resultado.Errors)
            {
                if (controlMap.TryGetValue(error.Key, out Control ctrl) && ctrl != this)
                {
                    errorProvider.SetError(ctrl, error.Value);
                }
                mensajesError.Add($"- {error.Key}: {error.Value}");
            }

            MessageBox.Show("Por favor, complete o corrija los campos marcados en rojo.\n\nDetalles:\n" + string.Join("\n", mensajesError), "Validación de Formulario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            var u = ConstruirUsuarioDesdeForm();
            var result = _usuarioService.GuardarUsuario(u, isEdit: false, idLoggedUser: UserSession.IdUsuario);

            if (!MostrarErroresDeValidacion(result)) return;

            LimpiarFormulario();
            CargarUsuarios();
            MessageBox.Show("Usuario registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado == 0) return;

            var u = ConstruirUsuarioDesdeForm();

            // Check if anything changed by comparing with the current list
            var current = _listaUsuarios?.Find(x => x.Id == usuarioIdSeleccionado);
            if (current != null)
            {
                bool hayCambios = (
                    current.Nombre != u.Nombre ||
                    current.Apellido != u.Apellido ||
                    current.NombreUsuario != u.NombreUsuario ||
                    !string.IsNullOrEmpty(u.Contrasena) ||
                    current.Rol != u.Rol ||
                    current.Email != u.Email ||
                    current.Telefono != u.Telefono ||
                    current.Calle != u.Calle ||
                    current.Numero != u.Numero ||
                    current.Ciudad != u.Ciudad ||
                    current.Provincia != u.Provincia ||
                    current.Sexo != u.Sexo ||
                    (current.Nacimiento?.Date != u.Nacimiento?.Date)
                );

                if (!hayCambios)
                {
                    MessageBox.Show("No se modificó ningún dato.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            var result = _usuarioService.GuardarUsuario(u, isEdit: true, idLoggedUser: UserSession.IdUsuario);
            if (!MostrarErroresDeValidacion(result)) return;

            LimpiarFormulario();
            CargarUsuarios();
            MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado == 0) return;

            var confirmResult = MessageBox.Show("¿Está seguro que desea eliminar este usuario?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult != DialogResult.Yes) return;

            var result = _usuarioService.EliminarUsuario(usuarioIdSeleccionado, UserSession.IdUsuario);
            if (!MostrarErroresDeValidacion(result)) return;

            LimpiarFormulario();
            CargarUsuarios();
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
