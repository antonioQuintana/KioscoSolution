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
            // Login rápido mockeado para la navegación
            if (txtUsuario.Text.ToLower() == "admin")
            {
                FrmAdminDashboard adminDash = new FrmAdminDashboard();
                adminDash.Show();
                this.Hide();
            }
            else
            {
                FrmVendedorDashboard vendDash = new FrmVendedorDashboard();
                vendDash.Show();
                this.Hide();
            }
        }
    }
}
