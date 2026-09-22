using System;
using System.Windows.Forms;
using KioscoApp.Business;

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
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            var user = _usuarioService.Login(txtUsuario.Text.Trim(), txtContrasena.Text, out string errorMsg);

            if (user == null)
            {
                MessageBox.Show(errorMsg, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserSession.IniciarSesion(user.Id, user.Nombre, user.NombreUsuario, user.Rol);
            
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

    public static class UserSession
    {
        public static int IdUsuario { get; private set; }
        public static string Nombre { get; private set; }
        public static string Usuario { get; private set; }
        public static string Rol { get; private set; }

        public static void IniciarSesion(int id, string nombre, string usuario, string rol)
        {
            IdUsuario = id;
            Nombre = nombre;
            Usuario = usuario;
            Rol = rol;
        }

        public static void CerrarSesion()
        {
            IdUsuario = 0;
            Nombre = string.Empty;
            Usuario = string.Empty;
            Rol = string.Empty;
        }
    }
}
