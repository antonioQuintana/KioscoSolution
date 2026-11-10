using System;
using System.Windows.Forms;
using System.Drawing;
using KioscoApp.Utils;

namespace KioscoApp
{
    public partial class FrmPrincipal : Form
    {
        private string rolUsuario;
        private Button? _botonMenuActivo;

        public FrmPrincipal(string rol)
        {
            InitializeComponent();

            rolUsuario = rol.ToLower();
            ThemeHelper.ApplyTheme(this);
            btnCerrarSesion.BackColor = Color.IndianRed;
            ConfigurarAyudaMenu();
            ConfigurarMenu();
        }

        private void ConfigurarAyudaMenu()
        {
            HelpTooltipHelper.AttachHelp(
                BUsuarios,
                "Módulo Usuarios (Administrador)",
                "Permite dar de alta nuevos operadores del sistema, editar sus datos personales, restablecer contraseñas y asignar su rol operativo (admin, supervisor o vendedor).",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BConfiguracion,
                "Módulo Configuración (Administrador)",
                "Accede a las herramientas críticas: Copias de Seguridad (Backup/Restore de SQL Server), Datos Fiscales del Ticket, Trazabilidad de Seguridad y Visor de Logs de Errores.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BProductos,
                "Módulo Catálogo de Productos (Supervisor)",
                "Permite crear, editar y dar de baja artículos del catálogo, calcular márgenes de ganancia y actualizar precios masivamente mediante plantillas Excel (CSV).",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BCategorias,
                "Módulo Categorías (Supervisor)",
                "Permite organizar los productos en rubros o familias comerciales (ej. Bebidas, Golosinas, Lácteos) para facilitar la búsqueda y las estadísticas.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BStock,
                "Módulo Control de Stock (Supervisor)",
                "Monitorea las existencias físicas con alertas visuales (Agotado / Bajo Stock), permite realizar ajustes manuales de inventario, ver el Inventario Valorizado y auditar movimientos.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BEstadisticas,
                "Módulo Estadísticas Comerciales (Supervisor)",
                "Presenta 6 gráficos interactivos con filtros por fechas para analizar ventas por categoría, días de mayor facturación, horarios pico y medios de pago.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BAuditoriaCajas,
                "Módulo Auditoría de Cajas (Supervisor)",
                "Permite inspeccionar la caja en vivo (Corte X / Arqueo Sorpresa), revisar el historial de Cierres Z, controlar descuadres por cajero, auditar retiros y supervisar anulaciones.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BVenta,
                "Módulo Punto de Venta / POS (Vendedor)",
                "Pantalla principal de facturación rápida. Permite escanear productos, buscar por nombre, suspender/reanudar compras y cobrar en Efectivo, QR, Tarjeta o Cuenta Corriente.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BClientes,
                "Módulo Clientes y Cuentas Corrientes (Vendedor)",
                "Administra el padrón de clientes habituales, permite consultar su límite de crédito y registrar pagos/abonos de deudas en Cuenta Corriente.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BCierreCaja,
                "Módulo Cierre de Caja (Vendedor)",
                "Permite declarar el fondo inicial de cambio, registrar ingresos o retiros de efectivo, realizar el conteo físico de billetes/monedas y confirmar el Cierre Z del turno.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                BHistorialVentas,
                "Módulo Historial de Ventas (Vendedor)",
                "Permite consultar los tickets emitidos en el turno actual o en fechas anteriores, ver el detalle de artículos, reimprimir comprobantes o anular ventas con reversión de stock.",
                shrinkControl: true);

            var tipCerrar = HelpTooltipHelper.CreateToolTip("Cerrar Sesión");
            tipCerrar.SetToolTip(btnCerrarSesion, "Cierra la sesión del usuario activo de forma segura y regresa a la pantalla de Inicio de Sesión para cambio de turno o cajero.");
            var badgeCerrar = HelpTooltipHelper.CreateHelpBadge(
                "Cerrar Sesión",
                "Haga clic en 'Cerrar Sesión' al finalizar su turno o ausentarse del mostrador para proteger su cuenta y permitir el ingreso de otro operador.",
                20);
            badgeCerrar.Location = new Point(220, pnlMenu.ClientSize.Height - 38);
            badgeCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pnlMenu.Controls.Add(badgeCerrar);
            badgeCerrar.BringToFront();
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
            BCierreCaja.Visible = false; 
            BHistorialVentas.Visible = false;

            // Mostramos según rol y cargamos la pantalla principal por defecto
            if (rolUsuario == "admin")
            {
                BUsuarios.Visible = true;
                BConfiguracion.Visible = true;
                CargarUserControl(new UcABMUsuarios(), BUsuarios);
            }
            else if (rolUsuario == "supervisor")
            {
                BProductos.Visible = true;
                BCategorias.Visible = true;
                BStock.Visible = true;
                BEstadisticas.Visible = true;
                BAuditoriaCajas.Visible = true;
                CargarUserControl(new UcABMProductos(), BProductos);
            }
            else // vendedor
            {
                BVenta.Visible = true;
                BClientes.Visible = true;
                BCierreCaja.Visible = true; 
                BHistorialVentas.Visible = true;
                CargarUserControl(new UcVenta(), BVenta);
            }
        }

        private void CargarUserControl(UserControl uc, Button? botonMenu = null)
        {
            // Si la pantalla actual ya es del mismo tipo, no recargar para evitar parpadeos y pérdida de estado
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0].GetType() == uc.GetType())
            {
                if (botonMenu != null) ResaltarBotonActivo(botonMenu);
                return;
            }

            pnlContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            
            // Aplicar el tema correspondiente al nuevo UserControl
            ThemeHelper.ApplyTheme(uc);
            
            pnlContent.Controls.Add(uc);

            // Fix: Asegurar que los textos en las celdas de las grillas sean legibles
            foreach (Control c in uc.Controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.DefaultCellStyle.ForeColor = Color.Black;
                    dgv.ForeColor = Color.Black;
                }
            }

            if (botonMenu != null) ResaltarBotonActivo(botonMenu);
        }

        private void ResaltarBotonActivo(Button? boton)
        {
            if (boton == null) return;

            var botones = new[] 
            { 
                BUsuarios, BConfiguracion, BProductos, BCategorias, BStock, 
                BEstadisticas, BAuditoriaCajas, BVenta, BClientes, BCierreCaja, BHistorialVentas 
            };

            foreach (var b in botones)
            {
                if (b != null && b.Visible)
                {
                    b.BackColor = ThemeHelper.ButtonColor;
                    b.ForeColor = Color.White;
                }
            }

            _botonMenuActivo = boton;
            _botonMenuActivo.BackColor = Color.FromArgb(0, 122, 204); // Resaltado azul profesional de pestaña activa
            _botonMenuActivo.ForeColor = Color.White;
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

        private void BUsuarios_Click_1(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcABMUsuarios) return;
            CargarUserControl(new UcABMUsuarios(), BUsuarios); 
        }

        private void BConfiguracion_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcConfiguracion) return;
            CargarUserControl(new UcConfiguracion(), BConfiguracion); 
        }

        private void BProductos_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcABMProductos) return;
            CargarUserControl(new UcABMProductos(), BProductos); 
        }

        private void BCategorias_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcABMCategorias) return;
            CargarUserControl(new UcABMCategorias(), BCategorias); 
        }

        private void BStock_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcMovimientosStock) return;
            CargarUserControl(new UcMovimientosStock(), BStock); 
        }

        private void BEstadisticas_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcEstadisticas) return;
            CargarUserControl(new UcEstadisticas(), BEstadisticas); 
        }

        private void BAuditoriaCajas_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcAuditoriaCajas) return;
            CargarUserControl(new UcAuditoriaCajas(), BAuditoriaCajas); 
        }

        private void BVenta_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcVenta) return;
            CargarUserControl(new UcVenta(), BVenta); 
        }

        private void BClientes_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcABMClientes) return;
            CargarUserControl(new UcABMClientes(), BClientes); 
        }

        private void BCierreCaja_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcCierreCaja) return;
            CargarUserControl(new UcCierreCaja(), BCierreCaja); 
        }

        private void BHistorialVentas_Click(object sender, EventArgs e) 
        { 
            if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is UcHistorialVentas) return;
            CargarUserControl(new UcHistorialVentas(), BHistorialVentas); 
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
    }
}
