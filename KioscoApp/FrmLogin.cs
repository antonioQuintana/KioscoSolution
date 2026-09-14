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
            string rol = txtUsuario.Text.ToLower() == "admin" ? "admin" : "vendedor";
            FrmPrincipal frm = new FrmPrincipal(rol);
            frm.Show();
            this.Hide();
        }
    }
}
