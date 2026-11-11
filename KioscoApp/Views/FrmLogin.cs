using System;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Utils;

namespace KioscoApp
{
    public partial class FrmLogin : Form
    {
        private UsuarioService _usuarioService;

        public FrmLogin()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            _usuarioService = new UsuarioService();
            ConfigurarAyuda();
        }

        private void ConfigurarAyuda()
        {
            HelpTooltipHelper.AttachFormHelp(
                this,
                "Pantalla de Inicio de Sesión",
                "Bienvenido a KioscoApp. En este formulario debe autenticarse con sus credenciales asignadas.\n\n" +
                "• El sistema aplica el principio de Mínimo Privilegio: según su rol (Vendedor, Supervisor o Administrador) se habilitarán únicamente las pantallas correspondientes a su puesto.\n" +
                "• Todos los accesos e intentos fallidos quedan registrados en la auditoría de seguridad.",
                new Point(344, 12));

            HelpTooltipHelper.AttachHelp(
                txtUsuario,
                "Campo Usuario",
                "Ingrese aquí su nombre de usuario asignado por el Administrador (por ejemplo: admin, supervisor o vendedor).",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                txtContrasena,
                "Contraseña y Visor de Caracteres",
                "Ingrese su clave secreta en el campo de contraseña.\n" +
                "Haga clic en el botón del ojo para mostrar u ocultar los caracteres escritos y verificar que no haya errores de tipeo.");

            HelpTooltipHelper.AttachHelp(
                BMostrar,
                "Contraseña y Visor de Caracteres",
                "Ingrese su clave secreta en el campo de contraseña.\n" +
                "Haga clic en el botón del ojo para mostrar u ocultar los caracteres escritos y verificar que no haya errores de tipeo.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                btnIngresar,
                "Botón Ingresar",
                "Haga clic aquí o presione la tecla [Enter] para validar sus credenciales e ingresar al menú principal del sistema.",
                shrinkControl: false,
                offsetX: 8);
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            var user = _usuarioService.Login(username, txtContrasena.Text, out string errorMsg);

            if (user == null)
            {
                AuditoriaService.Registrar("Seguridad", "Intento Fallido de Inicio de Sesión", $"Usuario: {username}");
                MessageBox.Show(errorMsg, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserSession.IniciarSesion(user.Id, user.Nombre, user.NombreUsuario, user.Rol);
            AuditoriaService.Registrar("Seguridad", "Inicio de Sesión Exitoso", $"Usuario: {user.NombreUsuario}, Rol: {user.Rol}");
            
            txtUsuario.Text = null;
            txtContrasena.Text = null;
            
            FrmPrincipal frm = new FrmPrincipal(user.Rol);
            frm.Show();
            this.Hide();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void BMostrar_Click(object sender, EventArgs e)
        {
            // Si la contraseña está oculta, la mostramos
            if (txtContrasena.UseSystemPasswordChar == true)
            {
                txtContrasena.UseSystemPasswordChar = false;
                BMostrar.BackgroundImage = Properties.Resources.eyeIcon;
            }
            // Si la contraseña está visible, la ocultamos
            else
            {
                txtContrasena.UseSystemPasswordChar = true;
                BMostrar.BackgroundImage = Properties.Resources.eyeBlindIcon;
            }
        }
    }

}