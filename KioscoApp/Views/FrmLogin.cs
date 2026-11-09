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