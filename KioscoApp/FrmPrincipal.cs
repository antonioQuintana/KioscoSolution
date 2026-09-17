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
            BUsuarios.Visible = false;
            BProductos.Visible = false;
            BCategorias.Visible = false;
            BProveedores.Visible = false;
            BStock.Visible = false;
            BEstadisticas.Visible = false;

            BVenta.Visible = false;
            BClientes.Visible = false;
            BCierreCaja.Visible = false;

            // Mostramos según rol
            if (rolUsuario == "admin")
            {
                BUsuarios.Visible = true;
                BProductos.Visible = true;
                BCategorias.Visible = true;
                BProveedores.Visible = true;
                BStock.Visible = true;
                BEstadisticas.Visible = true;
            }
            else // vendedor
            {
                BVenta.Visible = true;
                BClientes.Visible = true;
                BCierreCaja.Visible = true;
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


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            UserSession.CerrarSesion();

            Form loginOriginal = Application.OpenForms["FrmLogin"];

            if (loginOriginal != null)
            {
                loginOriginal.Show();
            }

            this.FormClosed -= FrmPrincipal_FormClosed;
            this.Close();
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e) { }

        private void BUsuarios_Click_1(object sender, EventArgs e) { CargarUserControl(new UcABMUsuarios()); }

        private void BProductos_Click(object sender, EventArgs e){CargarUserControl(new UcABMProductos());}
        
        private void BCategorias_Click(object sender, EventArgs e){CargarUserControl(new UcABMCategorias());}
        
        private void BProveedores_Click(object sender, EventArgs e){CargarUserControl(new UcABMProveedores());}
        
        private void BStock_Click(object sender, EventArgs e){CargarUserControl(new UcMovimientosStock());}

        private void BEstadisticas_Click(object sender, EventArgs e){CargarUserControl(new UcEstadisticas());}
        
        private void BVenta_Click(object sender, EventArgs e){CargarUserControl(new UcVenta());}
        
        private void BClientes_Click(object sender, EventArgs e){CargarUserControl(new UcABMClientes());}
        
        private void BCierreCaja_Click(object sender, EventArgs e){CargarUserControl(new UcCierreCaja());}
                
    }
}

