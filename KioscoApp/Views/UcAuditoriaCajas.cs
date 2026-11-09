using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcAuditoriaCajas : UserControl
    {
        private enum VistaAuditoria
        {
            HistorialCierres,
            DescuadresEmpleado,
            MovimientosCaja,
            VentasAnuladas
        }

        private readonly CajaService _cajaService;
        private readonly UsuarioService _usuarioService;
        private VistaAuditoria _vistaActual = VistaAuditoria.HistorialCierres;

        // Data caches
        private List<CierreCaja> _listaCierres = new List<CierreCaja>();
        private List<DescuadreEmpleadoItem> _listaDescuadres = new List<DescuadreEmpleadoItem>();
        private List<MovimientoCaja> _listaMovimientos = new List<MovimientoCaja>();
        private List<VentaAnuladaItem> _listaAnuladas = new List<VentaAnuladaItem>();

        public UcAuditoriaCajas()
        {
            InitializeComponent();
            _cajaService = new CajaService();
            _usuarioService = new UsuarioService();

            InicializarFiltros();
            CargarCierres();
        }

        private void InicializarFiltros()
        {
            // Rango de fechas por defecto: Últimos 30 días hasta hoy
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;

            // Cargar combo de cajeros
            cmbCajero.Items.Clear();
            cmbCajero.Items.Add(new ComboBoxItem { Text = "[Todos los Cajeros]", Value = 0 });

            try
            {
                var usuarios = _usuarioService.ObtenerTodos();
                foreach (var u in usuarios)
                {
                    cmbCajero.Items.Add(new ComboBoxItem { Text = $"{u.NombreUsuario} ({u.Rol})", Value = u.Id });
                }
            }
            catch
            {
                // Si la BD no responde aún en el constructor, se manejará en filtros
            }

            cmbCajero.SelectedIndex = 0;
        }

        private class ComboBoxItem
        {
            public string Text { get; set; } = string.Empty;
            public int Value { get; set; }
            public override string ToString() => Text;
        }

        #region Métodos de Carga de Vistas

        private void CargarCierres()
        {
            _vistaActual = VistaAuditoria.HistorialCierres;
            lblVistaActual.Text = "Vista: Historial de Cierres de Caja (Z)";
            pnlFiltros.Visible = true;

            try
            {
                DateTime? desde = dtpDesde.Value.Date;
                DateTime? hasta = dtpHasta.Value.Date;
                int? idUsuario = null;

                if (cmbCajero.SelectedItem is ComboBoxItem item && item.Value > 0)
                {
                    idUsuario = item.Value;
                }

                _listaCierres = _cajaService.ObtenerHistorialCierres(desde, hasta, idUsuario);

                var displayList = _listaCierres.Select(c => new
                {
                    c.Id,
                    Apertura = c.FechaApertura.ToString("dd/MM/yyyy HH:mm"),
                    Cierre = c.FechaCierre.ToString("dd/MM/yyyy HH:mm"),
                    Cajero = c.NombreUsuario,
                    TotalVentas = $"$ {c.VentasTotales:N2}",
                    Efectivo = $"$ {c.VentasEfectivo:N2}",
                    QR_MP = $"$ {c.VentasQR:N2}",
                    Tarjeta = $"$ {c.VentasTarjeta:N2}",
                    Retiros = $"$ {c.GastosRetiros:N2}",
                    Esperado = $"$ {c.TotalEsperado:N2}",
                    Declarado = $"$ {c.TotalDeclarado:N2}",
                    Diferencia = $"$ {c.Diferencia:N2}",
                    c.Estado,
                    Observaciones = c.Observaciones ?? "-"
                }).ToList();

                dgvAuditoria.DataSource = displayList;

                // Resumen
                decimal totalVentas = _listaCierres.Sum(c => c.VentasTotales);
                decimal faltantes = _listaCierres.Where(c => c.Diferencia < 0).Sum(c => c.Diferencia);
                decimal sobrantes = _listaCierres.Where(c => c.Diferencia > 0).Sum(c => c.Diferencia);
                decimal neto = _listaCierres.Sum(c => c.Diferencia);

                lblResumenAuditoria.Text = $"Total Cierres: {_listaCierres.Count} | Total Ventas: $ {totalVentas:N2} | Faltantes Acum.: $ {faltantes:N2} | Sobrantes Acum.: $ {sobrantes:N2} | Descuadre Neto: $ {neto:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial de cierres: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDescuadresEmpleado()
        {
            _vistaActual = VistaAuditoria.DescuadresEmpleado;
            lblVistaActual.Text = "Vista: Descuadres Acumulados por Empleado";

            try
            {
                _listaDescuadres = _cajaService.ObtenerDescuadresPorEmpleado();

                var displayList = _listaDescuadres.Select(d => new
                {
                    d.Empleado,
                    d.TotalCierres,
                    TotalFaltantes = $"$ {d.TotalFaltantes:N2}",
                    TotalSobrantes = $"$ {d.TotalSobrantes:N2}",
                    DiferenciaNeta = $"$ {d.DiferenciaNeta:N2}",
                    d.Estado
                }).ToList();

                dgvAuditoria.DataSource = displayList;

                decimal totalFaltante = _listaDescuadres.Sum(d => d.TotalFaltantes);
                decimal totalSobrante = _listaDescuadres.Sum(d => d.TotalSobrantes);
                decimal balance = _listaDescuadres.Sum(d => d.DiferenciaNeta);

                lblResumenAuditoria.Text = $"Total Empleados con Cierres: {_listaDescuadres.Count} | Total Faltantes: $ {totalFaltante:N2} | Total Sobrantes: $ {totalSobrante:N2} | Descuadre Global: $ {balance:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular descuadres por empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMovimientosCaja()
        {
            _vistaActual = VistaAuditoria.MovimientosCaja;
            lblVistaActual.Text = "Vista: Auditoría de Retiros y Gastos de Caja";
            pnlFiltros.Visible = true;

            try
            {
                DateTime? desde = dtpDesde.Value.Date;
                DateTime? hasta = dtpHasta.Value.Date;
                int? idUsuario = null;

                if (cmbCajero.SelectedItem is ComboBoxItem item && item.Value > 0)
                {
                    idUsuario = item.Value;
                }

                _listaMovimientos = _cajaService.ObtenerMovimientosCaja(desde, hasta, idUsuario);

                var displayList = _listaMovimientos.Select(m => new
                {
                    m.Id,
                    Fecha = m.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    m.Tipo,
                    Monto = $"$ {m.Monto:N2}",
                    Responsable = m.NombreUsuario,
                    Concepto = m.Motivo,
                    CierreZ = m.IdCierreCaja.HasValue ? $"Cierre #{m.IdCierreCaja.Value}" : "Turno Abierto (Pendiente)"
                }).ToList();

                dgvAuditoria.DataSource = displayList;

                decimal totalRetiros = _listaMovimientos.Where(m => m.Tipo.Equals("Retiro", StringComparison.OrdinalIgnoreCase)).Sum(m => m.Monto);
                decimal totalIngresos = _listaMovimientos.Where(m => m.Tipo.Equals("Ingreso", StringComparison.OrdinalIgnoreCase)).Sum(m => m.Monto);
                int cantRetiros = _listaMovimientos.Count(m => m.Tipo.Equals("Retiro", StringComparison.OrdinalIgnoreCase));
                int cantIngresos = _listaMovimientos.Count(m => m.Tipo.Equals("Ingreso", StringComparison.OrdinalIgnoreCase));

                lblResumenAuditoria.Text = $"Total Movimientos: {_listaMovimientos.Count}  |  🔴 Retiros ({cantRetiros}): $ {totalRetiros:N2}  |  🟢 Ingresos ({cantIngresos}): $ {totalIngresos:N2}  |  Salida Neta: $ {(totalRetiros - totalIngresos):N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar movimientos de caja: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVentasAnuladas()
        {
            _vistaActual = VistaAuditoria.VentasAnuladas;
            lblVistaActual.Text = "Vista: Auditoría de Ventas Anuladas o Canceladas";

            try
            {
                _listaAnuladas = _cajaService.ObtenerVentasAnuladas();

                var displayList = _listaAnuladas.Select(a => new
                {
                    a.Id,
                    Fecha = a.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    a.Cajero,
                    Total = $"$ {a.Total:N2}",
                    a.MetodoPago,
                    a.Estado
                }).ToList();

                dgvAuditoria.DataSource = displayList;

                decimal totalPerdido = _listaAnuladas.Sum(a => a.Total);
                lblResumenAuditoria.Text = $"Total Ventas Anuladas/Canceladas: {_listaAnuladas.Count} | Monto Total de Operaciones Canceladas: $ {totalPerdido:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ventas anuladas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos de Botones Principales

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            CargarCierres();
        }

        private void btnArqueo_Click(object sender, EventArgs e)
        {
            try
            {
                var resumen = _cajaService.ObtenerResumenCajaActual();

                string msg =
                    "===============================================\n" +
                    "   ARQUEO SORPRESA DE CAJA (CORTE X)\n" +
                    "===============================================\n\n" +
                    $"• Fecha y Hora de Inspección: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                    $"• Primera Venta del Turno: {(resumen.FechaPrimeraVenta.HasValue ? resumen.FechaPrimeraVenta.Value.ToString("dd/MM/yyyy HH:mm") : "Sin ventas aún")}\n" +
                    $"• Cantidad de Ventas Emitidas: {resumen.CantidadVentas}\n\n" +
                    "--- FLUJO DE FONDOS DEL TURNO ACTUAL ---\n" +
                    $"• Fondo Inicial Registrado:       $ {resumen.FondoInicial:N2}\n" +
                    $"• Cobrado en Efectivo:            $ {resumen.VentasEfectivo:N2}\n" +
                    $"• Cobrado en QR / Mercado Pago:   $ {resumen.VentasQR:N2}\n" +
                    $"• Cobrado con Tarjeta de Débito:  $ {resumen.VentasTarjeta:N2}\n" +
                    $"-----------------------------------------------\n" +
                    $"• TOTAL FACTURADO EN TURNO:       $ {resumen.TotalVentas:N2}\n\n" +
                    "--- MOVIMIENTOS EXTRAORDINARIOS ---\n" +
                    $"• Ingresos Varios de Caja (+):    $ {resumen.IngresosVarios:N2}\n" +
                    $"• Retiros y Gastos de Caja (-):   $ {resumen.GastosRetiros:N2}\n" +
                    $"===============================================\n" +
                    $"• EFECTIVO ESPERADO EN CAJÓN:     $ {resumen.EfectivoEsperado:N2}\n" +
                    "===============================================\n\n" +
                    "El corte X no altera ni cierra el turno del cajero.";

                MessageBox.Show(msg, "Arqueo Sorpresa (Corte X)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar arqueo sorpresa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRetiro_Click(object sender, EventArgs e)
        {
            using var modal = new Views.FrmMovimientoCaja("Retiro");
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;
                    _cajaService.RegistrarRetiro(modal.Monto, $"[Supervisor] {modal.Motivo}", idUsuario);
                    MessageBox.Show(
                        $"Retiro de $ {modal.Monto:N2} registrado correctamente en la caja actual.\n\nConcepto: {modal.Motivo}",
                        "Retiro Registrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    if (_vistaActual == VistaAuditoria.HistorialCierres)
                    {
                        CargarCierres();
                    }
                    else if (_vistaActual == VistaAuditoria.MovimientosCaja)
                    {
                        CargarMovimientosCaja();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar el retiro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            CargarMovimientosCaja();
        }

        private void btnFaltantes_Click(object sender, EventArgs e)
        {
            CargarDescuadresEmpleado();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            CargarVentasAnuladas();
        }

        private void btnExportarCSV_Click(object sender, EventArgs e)
        {
            ExportarGridACSV();
        }

        private void btnActualizarVista_Click(object sender, EventArgs e)
        {
            btnFiltrar_Click(sender, e);
        }

        #endregion

        #region Filtros y Grid

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            switch (_vistaActual)
            {
                case VistaAuditoria.HistorialCierres:
                    CargarCierres();
                    break;
                case VistaAuditoria.DescuadresEmpleado:
                    CargarDescuadresEmpleado();
                    break;
                case VistaAuditoria.MovimientosCaja:
                    CargarMovimientosCaja();
                    break;
                case VistaAuditoria.VentasAnuladas:
                    CargarVentasAnuladas();
                    break;
            }
        }

        private void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            if (cmbCajero.Items.Count > 0) cmbCajero.SelectedIndex = 0;

            btnFiltrar_Click(sender, e);
        }

        private void dgvAuditoria_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvAuditoria.Columns[e.ColumnIndex].Name;

            // Formatear Diferencia / Descuadre en verde o rojo
            if (colName.Equals("Diferencia", StringComparison.OrdinalIgnoreCase) ||
                colName.Equals("DiferenciaNeta", StringComparison.OrdinalIgnoreCase) ||
                colName.Equals("TotalFaltantes", StringComparison.OrdinalIgnoreCase))
            {
                string valStr = e.Value?.ToString() ?? "";
                if (valStr.Contains("-"))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60); // Rojo brillante
                    e.CellStyle.Font = new Font(dgvAuditoria.Font, FontStyle.Bold);
                }
                else if (valStr != "$ 0,00" && valStr != "$ 0.00" && !string.IsNullOrWhiteSpace(valStr))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113); // Verde esmeralda
                    e.CellStyle.Font = new Font(dgvAuditoria.Font, FontStyle.Bold);
                }
            }
            // Formatear Tipo en Movimientos de Caja
            else if (colName.Equals("Tipo", StringComparison.OrdinalIgnoreCase))
            {
                string tipo = e.Value?.ToString() ?? "";
                if (tipo.Equals("Retiro", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                    e.CellStyle.Font = new Font(dgvAuditoria.Font, FontStyle.Bold);
                }
                else if (tipo.Equals("Ingreso", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113);
                    e.CellStyle.Font = new Font(dgvAuditoria.Font, FontStyle.Bold);
                }
            }
        }

        private void dgvAuditoria_DoubleClick(object sender, EventArgs e)
        {
            if (_vistaActual != VistaAuditoria.HistorialCierres || dgvAuditoria.CurrentRow == null) return;

            try
            {
                int idCierre = Convert.ToInt32(dgvAuditoria.CurrentRow.Cells["Id"].Value);
                var cierre = _listaCierres.FirstOrDefault(c => c.Id == idCierre);

                if (cierre != null)
                {
                    string detalle =
                        $"========================================\n" +
                        $"       DETALLE DEL CIERRE Z #{cierre.Id}\n" +
                        $"========================================\n\n" +
                        $"• Cajero Responsable: {cierre.NombreUsuario}\n" +
                        $"• Apertura: {cierre.FechaApertura:dd/MM/yyyy HH:mm}\n" +
                        $"• Cierre:   {cierre.FechaCierre:dd/MM/yyyy HH:mm}\n" +
                        $"• Estado:   {cierre.Estado}\n\n" +
                        $"--- DESGLOSE DE RECAUDACIÓN ---\n" +
                        $"• Ventas Efectivo:        $ {cierre.VentasEfectivo:N2}\n" +
                        $"• Ventas QR:              $ {cierre.VentasQR:N2}\n" +
                        $"• Ventas Tarjeta:         $ {cierre.VentasTarjeta:N2}\n" +
                        $"----------------------------------------\n" +
                        $"• TOTAL FACTURADO:        $ {cierre.VentasTotales:N2}\n\n" +
                        $"--- ARQUEO DE EFECTIVO ---\n" +
                        $"• Fondo Inicial de Caja:  $ {cierre.FondoInicial:N2}\n" +
                        $"• Ingresos Varios (+):    $ {cierre.IngresosVarios:N2}\n" +
                        $"• Retiros y Gastos (-):   $ {cierre.GastosRetiros:N2}\n" +
                        $"• EFECTIVO ESPERADO:      $ {cierre.TotalEsperado:N2}\n" +
                        $"• EFECTIVO DECLARADO:     $ {cierre.TotalDeclarado:N2}\n" +
                        $"----------------------------------------\n" +
                        $"• DIFERENCIA (Descuadre): $ {cierre.Diferencia:N2}\n\n" +
                        $"• Observaciones: {cierre.Observaciones ?? "Sin observaciones"}";

                    MessageBox.Show(detalle, $"Cierre de Caja #{cierre.Id}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                // Ignorar error al hacer doble click en celda no válida
            }
        }

        private void ExportarGridACSV()
        {
            if (dgvAuditoria.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos en la pantalla para exportar.", "Grid vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "Archivo CSV compatible con Excel (*.csv)|*.csv",
                    FileName = $"Auditoria_{_vistaActual}_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Exportar Reporte a Archivo CSV"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();

                // Encabezados
                var headers = new List<string>();
                foreach (DataGridViewColumn col in dgvAuditoria.Columns)
                {
                    headers.Add(col.HeaderText);
                }
                sb.AppendLine(string.Join(";", headers));

                // Filas
                foreach (DataGridViewRow row in dgvAuditoria.Rows)
                {
                    var cells = new List<string>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string val = cell.Value?.ToString() ?? "";
                        val = val.Replace(";", " ").Replace("\r", " ").Replace("\n", " ");
                        cells.Add(val);
                    }
                    sb.AppendLine(string.Join(";", cells));
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show(
                    $"Reporte exportado exitosamente con {dgvAuditoria.Rows.Count} registros a:\n\n{sfd.FileName}\n\nPuede abrirlo directamente en Microsoft Excel.",
                    "Exportación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar reporte a CSV: {ex.Message}", "Error de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
