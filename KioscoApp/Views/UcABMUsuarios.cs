using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Utils;

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
            ConfigurarAyuda();
        }

        private void ConfigurarAyuda()
        {
            HelpTooltipHelper.AttachFormHelp(
                this,
                "Gestión de Usuarios y Permisos (Administrador)",
                "En este formulario el Administrador gestiona las cuentas de acceso del personal:\n" +
                "• Para crear un nuevo usuario: complete todos los campos y presione 'Agregar'.\n" +
                "• Para modificar un usuario existente: haga clic en su fila en la tabla inferior, modifique los datos deseados arriba y presione 'Editar'.\n" +
                "• Las contraseñas se almacenan cifradas con algoritmo BCrypt.",
                new Point(615, 232));

            HelpTooltipHelper.AttachHelp(
                lblNombre,
                "Nombre y Apellido del Operador",
                "Ingrese el nombre y apellido reales del empleado (solo letras y espacios).",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblUsuario,
                "Nombre de Usuario (Login)",
                "Identificador único con el que el empleado iniciará sesión en el sistema (ej. vendedor1, encargado). No puede repetirse.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblDni,
                "DNI del Usuario",
                "Número de Documento Nacional de Identidad (solo números, único por empleado).",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblContrasena,
                "Contraseña de Acceso",
                "• Al crear un usuario nuevo: es obligatoria.\n" +
                "• Al editar un usuario existente: déjela en blanco si desea conservar su contraseña actual, o escriba una nueva clave para restablecerla.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblRol,
                "Rol y Nivel de Acceso",
                "Define qué pantallas verá el usuario al iniciar sesión:\n" +
                "• admin: Gestión de Usuarios, Backups, Datos de Empresa, Logs y Trazabilidad.\n" +
                "• supervisor: Productos, Categorías, Stock, Estadísticas y Auditoría de Cajas.\n" +
                "• vendedor: Punto de Venta (POS), Clientes, Cierre de Caja e Historial de Ventas.\n" +
                "(Nota: Un administrador no puede cambiar su propio rol activo por seguridad).",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblEmail,
                "Correo Electrónico",
                "Dirección de email válida y única del operador (formato usuario@dominio.com).",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblTelefono,
                "Teléfono y Domicilio",
                "Complete el teléfono de contacto, calle y número de domicilio del empleado.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblProvincia,
                "Provincia, Ciudad y Datos Demográficos",
                "Seleccione primero la Provincia para cargar automáticamente sus localidades en el desplegable 'Ciudad', e indique Sexo y Fecha de Nacimiento.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                btnAgregar,
                "Agregar Nuevo Usuario",
                "Valida todos los campos del formulario (unicidad de DNI, Usuario y Email) y registra el nuevo operador en el sistema.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnEditar,
                "Guardar Cambios del Usuario Seleccionado",
                "Actualiza la ficha del usuario que fue seleccionado previamente en la tabla inferior.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnEliminar,
                "Eliminar Usuario",
                "Da de baja al usuario seleccionado en la tabla.\n" +
                "Nota: Por seguridad, el sistema impide que el usuario conectado elimine su propia cuenta activa.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnLimpiar,
                "Limpiar Formulario",
                "Vacía todos los campos y deselecciona el usuario actual para preparar una nueva carga.",
                shrinkControl: true);
        }

        private void ConfigurarValidacionesEnTiempoReal()
        {
            txtNombre.MaxLength = 100;
            txtApellido.MaxLength = 100;
            txtDni.MaxLength = 20;
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
                txtDni.Text = row.Cells["Dni"].Value?.ToString();
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
                Dni = txtDni.Text.Trim(),
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
                { "Dni", txtDni },
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
                    current.Dni != u.Dni ||
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
            txtDni.Clear();
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

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
