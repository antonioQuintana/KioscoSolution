using System;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcCierreCaja : UserControl
    {
        private readonly CajaService _cajaService;
        private ResumenCajaActual _resumen;
        private decimal _totalContado;
        private decimal _fondoInicialConfigurado = 0m;

        public UcCierreCaja()
        {
            InitializeComponent();
            _cajaService = new CajaService();
            _resumen = new ResumenCajaActual();
            
            ConfigurarEventos();
            CargarResumenTurno();
        }

        private void ConfigurarEventos()
        {
            // Eventos de conteo en tiempo real
            txtBilletes10000.TextChanged += (s, e) => CalcularArqueo();
            txtBilletes2000.TextChanged += (s, e) => CalcularArqueo();
            txtBilletes1000.TextChanged += (s, e) => CalcularArqueo();
            txtBilletes500.TextChanged += (s, e) => CalcularArqueo();
            txtBilletes200.TextChanged += (s, e) => CalcularArqueo();
            txtBilletes100.TextChanged += (s, e) => CalcularArqueo();
            txtMonedas.TextChanged += (s, e) => CalcularArqueo();

            // Restringir a solo números enteros en los billetes
            var billetesBoxes = new[] { txtBilletes10000, txtBilletes2000, txtBilletes1000, txtBilletes500, txtBilletes200, txtBilletes100 };
            foreach (var txt in billetesBoxes)
            {
                txt.KeyPress += (s, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                        e.Handled = true;
                };
            }

            // Monedas permite coma o punto decimal
            txtMonedas.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                    e.Handled = true;
            };

            // Permitir modificar el fondo de caja haciendo clic
            lblFondoCaja.Cursor = Cursors.Hand;
            lblFondoCaja.Click += LblFondoCaja_Click;

            // Botones de acción
            btnIngresoCaja.Click += BtnIngresoCaja_Click;
            btnRegistrarGasto.Click += BtnRegistrarGasto_Click;
            btnImprimirZ.Click += BtnImprimirZ_Click;
            btnCerrar.Click += BtnCerrar_Click;
        }

        private void LblFondoCaja_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmMovimientoCaja("Ingreso");
            modal.Text = "Configurar Fondo Inicial de Caja";
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                _fondoInicialConfigurado = modal.Monto;
                CargarResumenTurno();
            }
        }

        private void CargarResumenTurno()
        {
            try
            {
                _resumen = _cajaService.ObtenerResumenCajaActual(fondoInicial: _fondoInicialConfigurado);

                lblFondoCaja.Text = $"Fondo de Caja (Inicio): $ {_resumen.FondoInicial:N2}";
                lblVentasEfectivo.Text = $"(+) Ventas Efectivo: $ {_resumen.VentasEfectivo:N2}";
                lblVentasQR.Text = $"(+) Ventas QR: $ {_resumen.VentasQR:N2}";
                lblVentasTarjeta.Text = $"(+) Ventas Tarjeta: $ {_resumen.VentasTarjeta:N2}";
                lblGastos.Text = $"(-) Gastos / Retiros: $ {_resumen.GastosRetiros:N2}";
                lblEsperado.Text = $"Efectivo Esperado: $ {_resumen.EfectivoEsperado:N2}";

                CalcularArqueo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos de caja: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool HayConteoFisicoIngresado()
        {
            return !string.IsNullOrWhiteSpace(txtBilletes10000.Text) ||
                   !string.IsNullOrWhiteSpace(txtBilletes2000.Text) ||
                   !string.IsNullOrWhiteSpace(txtBilletes1000.Text) ||
                   !string.IsNullOrWhiteSpace(txtBilletes500.Text) ||
                   !string.IsNullOrWhiteSpace(txtBilletes200.Text) ||
                   !string.IsNullOrWhiteSpace(txtBilletes100.Text) ||
                   !string.IsNullOrWhiteSpace(txtMonedas.Text);
        }

        private void CalcularArqueo()
        {
            int.TryParse(txtBilletes10000.Text, out int b10000);
            int.TryParse(txtBilletes2000.Text, out int b2000);
            int.TryParse(txtBilletes1000.Text, out int b1000);
            int.TryParse(txtBilletes500.Text, out int b500);
            int.TryParse(txtBilletes200.Text, out int b200);
            int.TryParse(txtBilletes100.Text, out int b100);
            decimal.TryParse(txtMonedas.Text.Replace('.', ','), out decimal monedas);

            _totalContado = (b10000 * 10000m) +
                            (b2000 * 2000m) +
                            (b1000 * 1000m) +
                            (b500 * 500m) +
                            (b200 * 200m) +
                            (b100 * 100m) +
                            monedas;

            lblDeclarado.Text = $"Efectivo Contado: $ {_totalContado:N2}";

            // Si los campos de billetes están vacíos (por ejemplo, recién se abrió la pantalla o se acaba de cerrar el turno anterior),
            // no mostramos un faltante falso contra el fondo de caja.
            if (!HayConteoFisicoIngresado())
            {
                lblDiferencia.Text = "DIFERENCIA: $ 0.00";
                lblDiferencia.ForeColor = Color.DarkGray;
                return;
            }

            decimal diferencia = _totalContado - _resumen.EfectivoEsperado;

            if (diferencia == 0)
            {
                lblDiferencia.Text = "CAJA EXACTA: $ 0.00";
                lblDiferencia.ForeColor = Color.PaleGreen;
            }
            else if (diferencia > 0)
            {
                lblDiferencia.Text = $"SOBRANTE: $ {diferencia:N2}";
                lblDiferencia.ForeColor = Color.PaleGreen;
            }
            else
            {
                lblDiferencia.Text = $"FALTANTE: $ {Math.Abs(diferencia):N2}";
                lblDiferencia.ForeColor = Color.LightCoral;
            }
        }

        private void BtnIngresoCaja_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmMovimientoCaja("Ingreso");
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;
                    _cajaService.RegistrarIngreso(modal.Monto, modal.Motivo, idUsuario);
                    MessageBox.Show("Ingreso de efectivo registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarResumenTurno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar ingreso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRegistrarGasto_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmMovimientoCaja("Retiro");
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;
                    _cajaService.RegistrarRetiro(modal.Monto, modal.Motivo, idUsuario);
                    MessageBox.Show("Retiro / Gasto registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarResumenTurno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar retiro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnImprimirZ_Click(object? sender, EventArgs e)
        {
            decimal diferencia = _totalContado - _resumen.EfectivoEsperado;
            string estado = !HayConteoFisicoIngresado()
                ? "SIN CONTEO FÍSICO"
                : (diferencia == 0 ? "CAJA EXACTA" : (diferencia > 0 ? $"SOBRANTE (+${diferencia:N2})" : $"FALTANTE (-${Math.Abs(diferencia):N2})"));

            string ticket = 
                "====================================================\n" +
                "                  SISTEMA KIOSCO                    \n" +
                "              TICKET DE CIERRE DE CAJA (Z)          \n" +
                "====================================================\n" +
                $"Fecha/Hora Cierre : {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                $"Cajero a cargo     : {UserSession.Nombre ?? "Cajero"} ({UserSession.Usuario ?? "super"})\n" +
                $"Turno iniciado     : {_resumen.FechaPrimeraVenta?.ToString("dd/MM/yyyy HH:mm") ?? "Hoy (Nuevo turno)"}\n" +
                "----------------------------------------------------\n" +
                $"Cantidad Ventas    : {_resumen.CantidadVentas}\n" +
                $"(+) Ventas Efectivo: $ {_resumen.VentasEfectivo,14:N2}\n" +
                $"(+) Ventas Tarjeta : $ {_resumen.VentasTarjeta,14:N2}\n" +
                $"(+) Ventas QR      : $ {_resumen.VentasQR,14:N2}\n" +
                "----------------------------------------------------\n" +
                $"TOTAL VENTAS       : $ {_resumen.TotalVentas,14:N2}\n" +
                "----------------------------------------------------\n" +
                $"(+) Fondo Inicial  : $ {_resumen.FondoInicial,14:N2}\n" +
                $"(+) Ingresos Varios: $ {_resumen.IngresosVarios,14:N2}\n" +
                $"(-) Gastos / Retiro: $ {_resumen.GastosRetiros,14:N2}\n" +
                "----------------------------------------------------\n" +
                $"EFECTIVO ESPERADO  : $ {_resumen.EfectivoEsperado,14:N2}\n" +
                $"EFECTIVO CONTADO   : $ {_totalContado,14:N2}\n" +
                $"DIFERENCIA         : $ {diferencia,14:N2}\n" +
                $"RESULTADO ARQUEO   : {estado}\n" +
                "====================================================\n";

            MessageBox.Show(ticket, "Comprobante de Cierre Z", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCerrar_Click(object? sender, EventArgs e)
        {
            // Validar si el turno está vacío y no tiene movimientos
            if (!HayConteoFisicoIngresado() && _resumen.CantidadVentas == 0 && _resumen.GastosRetiros == 0 && _resumen.IngresosVarios == 0)
            {
                MessageBox.Show("El turno actual no posee ventas ni movimientos registrados para cerrar.", "Turno sin movimientos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!HayConteoFisicoIngresado())
            {
                var advertencia = MessageBox.Show(
                    "No ha ingresado el conteo de billetes en los casilleros.\n\n" +
                    "¿Desea cerrar el turno declarando $ 0,00 de efectivo contado?",
                    "Sin conteo ingresado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (advertencia != DialogResult.Yes) return;
            }

            decimal diferencia = _totalContado - _resumen.EfectivoEsperado;
            string estadoDiferencia = diferencia == 0 
                ? "Exacto ($ 0.00)" 
                : (diferencia > 0 ? $"Sobrante de $ {diferencia:N2}" : $"Faltante de $ {Math.Abs(diferencia):N2}");

            string mensajeConfirmacion = 
                "¿Está seguro de confirmar el Cierre de Caja?\n\n" +
                $"• Ventas Totales: $ {_resumen.TotalVentas:N2} ({_resumen.CantidadVentas} ventas)\n" +
                $"• Efectivo Esperado: $ {_resumen.EfectivoEsperado:N2}\n" +
                $"• Efectivo Declarado: $ {_totalContado:N2}\n" +
                $"• Diferencia de Caja: {estadoDiferencia}\n\n" +
                "Una vez confirmado, las ventas de este turno quedarán archivadas y comenzará el nuevo turno.";

            var resultado = MessageBox.Show(mensajeConfirmacion, "Confirmar Cierre de Caja (Z)", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado != DialogResult.Yes) return;

            try
            {
                int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;

                var nuevoCierre = new CierreCaja
                {
                    IdUsuario = idUsuario,
                    FechaApertura = _resumen.FechaPrimeraVenta ?? DateTime.Now,
                    FechaCierre = DateTime.Now,
                    FondoInicial = _resumen.FondoInicial,
                    VentasEfectivo = _resumen.VentasEfectivo,
                    VentasQR = _resumen.VentasQR,
                    VentasTarjeta = _resumen.VentasTarjeta,
                    IngresosVarios = _resumen.IngresosVarios,
                    GastosRetiros = _resumen.GastosRetiros,
                    TotalEsperado = _resumen.EfectivoEsperado,
                    TotalDeclarado = _totalContado,
                    Diferencia = diferencia,
                    Observaciones = $"Cierre realizado por {UserSession.Usuario ?? "Cajero"}",
                    Estado = "Cerrado"
                };

                int idCierre = _cajaService.ConfirmarCierre(nuevoCierre);

                MessageBox.Show($"¡Cierre de Caja N° {idCierre} guardado exitosamente!\n\nEl turno ha sido cerrado y archivado.", "Cierre Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Imprimir comprobante
                BtnImprimirZ_Click(sender, e);

                // Limpiar campos de conteo físico
                LimpiarConteo();

                // Recargar resumen con la caja reiniciada
                CargarResumenTurno();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cierre de caja: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarConteo()
        {
            txtBilletes10000.Clear();
            txtBilletes2000.Clear();
            txtBilletes1000.Clear();
            txtBilletes500.Clear();
            txtBilletes200.Clear();
            txtBilletes100.Clear();
            txtMonedas.Clear();
            _totalContado = 0m;
        }
    }
}
