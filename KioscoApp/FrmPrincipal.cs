using System;
using System.Windows.Forms;
using System.Drawing;

namespace KioscoApp
{
    public partial class FrmPrincipal : Form
    {
        private string rolUsuario;

        public FrmPrincipal(string rol)
        {
            InitializeComponent();
            rolUsuario = rol.ToLower();
            ConfigurarMenu();
            ThemeHelper.ApplyTheme(this);
        }

        private void ConfigurarMenu()
        {
            // Ocultamos todos los botones primero
            btnUsuarios.Visible = false;
            btnProductos.Visible = false;
            btnCategorias.Visible = false;
            btnProveedores.Visible = false;
            btnStock.Visible = false;
            btnEstadisticas.Visible = false;
            
            btnVenta.Visible = false;
            btnClientes.Visible = false;
            btnCierreCaja.Visible = false;

            // Mostramos según rol
            if (rolUsuario == "admin")
            {
                btnUsuarios.Visible = true;
                btnProductos.Visible = true;
                btnCategorias.Visible = true;
                btnProveedores.Visible = true;
                btnStock.Visible = true;
                btnEstadisticas.Visible = true;
            }
            else // vendedor
            {
                btnVenta.Visible = true;
                btnClientes.Visible = true;
                btnCierreCaja.Visible = true;
            }
        }

        private void CargarUserControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
            ThemeHelper.ApplyTheme(uc);
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // --- Eventos Click del Menú ---
        private void btnUsuarios_Click(object sender, EventArgs e) { CargarUserControl(new UcABMUsuarios()); }
        private void btnProductos_Click(object sender, EventArgs e) { CargarUserControl(new UcABMProductos()); }
        private void btnCategorias_Click(object sender, EventArgs e) { CargarUserControl(new UcABMCategorias()); }
        private void btnProveedores_Click(object sender, EventArgs e) { CargarUserControl(new UcABMProveedores()); }
        private void btnStock_Click(object sender, EventArgs e) { CargarUserControl(new UcMovimientosStock()); }
        private void btnEstadisticas_Click(object sender, EventArgs e) { CargarUserControl(new UcEstadisticas()); }
        private void btnVenta_Click(object sender, EventArgs e) { CargarUserControl(new UcVenta()); }
        private void btnClientes_Click(object sender, EventArgs e) { CargarUserControl(new UcABMClientes()); }
        private void btnCierreCaja_Click(object sender, EventArgs e) { CargarUserControl(new UcCierreCaja()); }
        
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            FrmLogin login = new FrmLogin();
            login.Show();
            this.Hide();
        }
    }
}

