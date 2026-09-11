using System;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmVendedorDashboard : Form
    {
        public FrmVendedorDashboard()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        private void BtnVenta_Click(object sender, EventArgs e)
        {
            FrmVenta frm = new FrmVenta();
            frm.ShowDialog();
        }
        
        private void BtnABMClientes_Click(object sender, EventArgs e)
        {
            FrmABMClientes frm = new FrmABMClientes();
            frm.ShowDialog();
        }
        
        private void BtnCierreCaja_Click(object sender, EventArgs e)
        {
            FrmCierreCaja frm = new FrmCierreCaja();
            frm.ShowDialog();
        }

        private void FrmVendedorDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
