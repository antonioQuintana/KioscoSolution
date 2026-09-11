using System;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmAdminDashboard : Form
    {
        public FrmAdminDashboard()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        private void BtnGestionUsuarios_Click(object sender, EventArgs e)
        {
            FrmGestionUsuarios frmGestion = new FrmGestionUsuarios();
            frmGestion.ShowDialog();
        }
        
        private void BtnABMProductos_Click(object sender, EventArgs e)
        {
            FrmABMProductos frm = new FrmABMProductos();
            frm.ShowDialog();
        }
        
        private void BtnEstadisticas_Click(object sender, EventArgs e)
        {
            FrmEstadisticas frm = new FrmEstadisticas();
            frm.ShowDialog();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            FrmABMProveedores frm = new FrmABMProveedores();
            frm.ShowDialog();
        }

        private void BtnCategorias_Click(object sender, EventArgs e)
        {
            FrmABMCategorias frm = new FrmABMCategorias();
            frm.ShowDialog();
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {
            FrmMovimientosStock frm = new FrmMovimientosStock();
            frm.ShowDialog();
        }

        private void FrmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
