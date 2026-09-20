using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT Id, Nombre, Usuario, Contrasena, Rol FROM Usuarios WHERE Usuario = @Usuario";

            using var conexion = DatabaseHelper.GetConnection();
            conexion.Open();

            using var cmd = new SqlCommand(query, conexion);
            // El parámetro sanitiza automáticamente la entrada contra SQL Injection
            cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("Usuario no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hashBD = reader["Contrasena"].ToString();

            // Verificar la contraseña en texto claro contra el hash almacenado
            bool esValida = BCrypt.Net.BCrypt.Verify(txtContrasena.Text, hashBD);

            if (!esValida)
            {
                MessageBox.Show("Contraseña incorrecta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(reader["Id"]);
            string nombre = reader["Nombre"].ToString();
            string usuario = reader["Usuario"].ToString();
            string rol = reader["Rol"].ToString();

            UserSession.IniciarSesion(id, nombre, usuario, rol);
            
            FrmPrincipal frm = new FrmPrincipal(rol);
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
