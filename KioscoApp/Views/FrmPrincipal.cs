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
            tableLayoutPanel1.Controls.Add(pnlMenu, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlContent, 1, 0);

            // Ocultamos todos los botones primero
            BUsuarios.Visible = false;
            BConfiguracion.Visible = false;
            
            BProductos.Visible = false;
            BCategorias.Visible = false;
            BStock.Visible = false;
            BEstadisticas.Visible = false;
            BAuditoriaCajas.Visible = false;

            BVenta.Visible = false;
            BClientes.Visible = false;
            BCierreCaja.Visible = false; BHistorialVentas.Visible = false;

            // Mostramos según rol
            if (rolUsuario == "admin")
            {
                BUsuarios.Visible = true;
                BConfiguracion.Visible = true;
            }
            else if (rolUsuario == "supervisor")
            {
                BProductos.Visible = true;
                BCategorias.Visible = true;
                BStock.Visible = true;
                BEstadisticas.Visible = true;
                BAuditoriaCajas.Visible = true;
            }
            else // vendedor
            {
                BVenta.Visible = true;
                BClientes.Visible = true;
                BCierreCaja.Visible = true; BHistorialVentas.Visible = true;
            }
        }

        private void CargarUserControl(UserControl uc)
        {
            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(uc);
            // Fix: WinForms hereda el color blanco del FrmPrincipal, y oculta el texto en las grillas de fondo blanco.
            foreach (Control c in uc.Controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.ForeColor = Color.Black;
                }
            }
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

        private void BConfiguracion_Click(object sender, EventArgs e) { CargarUserControl(new UcConfiguracion()); }

        private void BProductos_Click(object sender, EventArgs e) { CargarUserControl(new UcABMProductos()); }

        private void BCategorias_Click(object sender, EventArgs e) { CargarUserControl(new UcABMCategorias()); }


        private void BStock_Click(object sender, EventArgs e) { CargarUserControl(new UcMovimientosStock()); }

        private void BEstadisticas_Click(object sender, EventArgs e) { CargarUserControl(new UcEstadisticas()); }

        private void BAuditoriaCajas_Click(object sender, EventArgs e) { CargarUserControl(new UcAuditoriaCajas()); }

        private void BVenta_Click(object sender, EventArgs e) { CargarUserControl(new UcVenta()); }

        private void BClientes_Click(object sender, EventArgs e) { CargarUserControl(new UcABMClientes()); }

        private void BCierreCaja_Click(object sender, EventArgs e) { CargarUserControl(new UcCierreCaja()); }
        private void BHistorialVentas_Click(object sender, EventArgs e) { CargarUserControl(new UcHistorialVentas()); }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        
    }
}

