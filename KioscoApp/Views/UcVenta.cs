using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Utils;
using KioscoApp.Views;

namespace KioscoApp
{
    public partial class UcVenta : UserControl
    {
        private readonly ProductoService _productoService;
        private readonly ClienteService _clienteService;
        private readonly List<VentaSuspendida> _ventasSuspendidas = new List<VentaSuspendida>();

        private class VentaSuspendida
        {
            public DateTime Fecha { get; set; } = DateTime.Now;
            public Cliente Cliente { get; set; }
            public List<DetalleVenta> Carrito { get; set; }
            public decimal DescuentoPorcentaje { get; set; }
            public decimal Total => Carrito.Sum(d => d.Subtotal) * (1 - DescuentoPorcentaje / 100);
        }

        private readonly VentaService _ventaService;
        private BindingList<DetalleVenta> _carrito;
        private Cliente _clienteActual;
        private decimal _descuentoPorcentaje = 0;

        public UcVenta()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            _clienteService = new ClienteService();
            _ventaService = new VentaService();
            _carrito = new BindingList<DetalleVenta>();

            ConfigurarUI();
            ConfigurarAyuda();

            _clienteActual = _clienteService.ObtenerConsumidorFinal() ?? new Cliente { Id = 1, Nombre = "Consumidor", Apellido = "Final", Dni = "00000000" };
            ActualizarUICliente();
            ActualizarTextoBotonSuspender();

            // Asegurar foco inicial al mostrar el control
            this.Load += (s, e) => EnfocarEscaner();
        }

        private void ConfigurarAyuda()
        {
            // Ajustar fuentes de botones de media columna para que luzcan prolijos junto a su ícono (?)
            var botonesMedios = new[] { btnConsultarPrecio, btnRetiroCaja, btnAplicarDescuento, btnSuspenderVenta, btnCobroQR, btnCobroTarjeta, btnCuentaCorriente, btnCancelarVenta };
            foreach (var b in botonesMedios)
            {
                if (b != null) b.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            }

            HelpTooltipHelper.AttachGroupBoxHelp(
                groupBoxCliente,
                "Formulario de Datos del Cliente",
                "En esta sección se identifica al comprador:\n" +
                "• Por defecto toda venta se emite a 'CONSUMIDOR FINAL'.\n" +
                "• Si desea vender fiado (Cuenta Corriente) o consultar la deuda de un cliente habitual, escriba su DNI y presione [Enter] o el botón 'Buscar [F10]'.\n" +
                "• Si el cliente seleccionado tiene deuda pendiente (texto naranja), puede hacer clic sobre su saldo para cobrarle un abono en el acto.");

            HelpTooltipHelper.AttachHelp(
                btnBuscarCliente,
                "Buscar Cliente por DNI [F10]",
                "Escriba el número de documento (DNI) en la caja de texto izquierda y presione este botón (o la tecla F10 / Enter) para asignar el cliente a la venta actual.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnNuevoCliente,
                "Registrar Nuevo Cliente",
                "Indica cómo dar de alta un nuevo cliente en el padrón (dirigiéndose a la pestaña 'Clientes' del menú principal) para habilitarle Cuenta Corriente.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                lblScanner,
                "Uso del Carrito de Compras",
                "La grilla inferior muestra todos los productos cargados en el ticket actual:\n" +
                "• Puede modificar manualmente la columna 'Cant.' haciendo clic en la celda.\n" +
                "• Seleccione una fila y presione [+] o [-] en el teclado para sumar o restar unidades.\n" +
                "• Presione la tecla [Supr / Delete] para quitar el artículo seleccionado del carrito.",
                shrinkControl: false,
                offsetX: 4);

            HelpTooltipHelper.AttachHelp(
                txtScanner,
                "Escáner, Búsqueda Predictiva y Multiplicador",
                "En este cuadro puede agregar productos de 3 formas:\n" +
                "1. ESCÁNER LÁSER: Escanee el código de barras o escriba el SKU exacto y presione [Enter].\n" +
                "2. BÚSQUEDA PREDICTIVA: Escriba al menos 2 letras del nombre del artículo, baje con la flecha [↓] y presione [Enter].\n" +
                "3. MULTIPLICADOR RÁPIDO: Escriba [Cantidad]*[Código] (ejemplo: 3*77912345) y presione [Enter] para cargar varias unidades de una sola vez.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnAutorizacion,
                "Autorización de Supervisor",
                "Permite solicitar la intervención o validación de un usuario con rol Supervisor para operaciones especiales en mostrador.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnConsultarPrecio,
                "Consultar Precio y Stock [F3]",
                "Escriba o escanee un código en la barra del escáner y presione este botón (o F3) para ver en pantalla su precio de venta y stock disponible sin agregarlo al carrito.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnRetiroCaja,
                "Retiro de Caja / Gasto [F9]",
                "Abre una ventana para asentar una salida de dinero en efectivo del cajón (por ejemplo, pago a un proveedor en mostrador o gastos menores). Se descontará automáticamente del efectivo esperado en el cierre.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnBuscarProducto,
                "Buscador del Catálogo [F1]",
                "Abre una ventana emergente con el catálogo completo de productos donde puede filtrar por nombre, SKU o categoría y agregar el artículo con doble clic.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnArticulosFrecuentes,
                "Artículos Frecuentes / Rápidos [F2]",
                "Muestra una botonera táctil/rápida con los artículos de mayor rotación para agregarlos al carrito con un solo clic sin necesidad de código de barras.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnAplicarDescuento,
                "Aplicar o Quitar Descuento (%)",
                "Aplica una bonificación del 10% sobre el subtotal del carrito actual. Si ya hay un descuento activo, vuelva a presionarlo para quitarlo.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnSuspenderVenta,
                "Suspender / Reanudar Venta [F4]",
                "• Si el carrito tiene productos: pone la compra actual 'en espera' y libera la pantalla de inmediato para atender a otro cliente urgente.\n" +
                "• Si el carrito está vacío y hay ventas en espera: recupera la última compra pausada con todos sus artículos para finalizar su cobro.",
                shrinkControl: true);

            HelpTooltipHelper.AttachFormHelp(
                this,
                "Totales a Pagar",
                "Muestra en tiempo real el Subtotal acumulado de los productos, el monto descontado por bonificaciones y el TOTAL neto a cobrar al cliente.",
                new Point(1018, 385));

            HelpTooltipHelper.AttachHelp(
                btnCobrar,
                "Cobrar en Efectivo [F5]",
                "Abre la Calculadora de Vuelto para cobros en dinero físico. Permite elegir billetes rápidos ($1.000, $2.000, $5.000, $10.000, $20.000 o Exacto), calcula el vuelto exacto y registra la venta.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnCobroQR,
                "Cobrar con MercadoPago / QR [F6]",
                "Registra la venta como cobrada mediante código QR o billetera virtual tras verificar que el pago del cliente haya sido acreditado.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnCobroTarjeta,
                "Cobrar con Tarjeta [F7]",
                "Registra la venta como abonada mediante Tarjeta de Débito o Crédito una vez aprobada la operación en la terminal Posnet.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnCuentaCorriente,
                "Cargar a Cuenta Corriente / Fiado [F8]",
                "Suma el total del ticket al saldo deudor del cliente registrado seleccionado arriba. Requiere haber buscado previamente un cliente por DNI (no disponible para Consumidor Final) y valida su límite de crédito.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnCancelarVenta,
                "Cancelar Ticket Actual [Esc]",
                "Vacía por completo el carrito de compras, restablece el cliente a Consumidor Final y deja el Punto de Venta limpio para una nueva operación.",
                shrinkControl: true);
        }

        private void ActualizarUICliente()
        {
            if (lblClienteActual != null)
            {
                if (_clienteActual != null && _clienteActual.Id > 1)
                {
                    if (_clienteActual.SaldoDeudor > 0)
                    {
                        lblClienteActual.Text = $"Cliente: {_clienteActual.NombreCompleto.ToUpper()} | DEUDA PENDIENTE: $ {_clienteActual.SaldoDeudor:N2} (Clic aquí para cobrar deuda)";
                        lblClienteActual.ForeColor = Color.FromArgb(243, 156, 18);
                        lblClienteActual.Cursor = Cursors.Hand;
                    }
                    else
                    {
                        lblClienteActual.Text = $"Cliente: {_clienteActual.NombreCompleto.ToUpper()} | Al día (Saldo: $ 0.00)";
                        lblClienteActual.ForeColor = Color.LightSkyBlue;
                        lblClienteActual.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    lblClienteActual.Text = "Cliente: CONSUMIDOR FINAL";
                    lblClienteActual.ForeColor = Color.White;
                    lblClienteActual.Cursor = Cursors.Default;
                }
            }
        }

        private void ConfigurarUI()
        {
            dgvCarrito.AutoGenerateColumns = false;
            dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCarrito.MultiSelect = false;

            if (dgvCarrito.Columns.Count == 0)
            {
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cód/SKU", DataPropertyName = "Codigo", Width = 110, ReadOnly = true });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = "Descripcion", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "PrecioUnitario", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cant.", DataPropertyName = "Cantidad", Width = 80 });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
            }

            dgvCarrito.DataSource = _carrito;

            // Eventos de controles
            txtScanner.KeyPress += TxtScanner_KeyPress;
            txtScanner.TextChanged += TxtScanner_TextChanged;
            txtScanner.KeyDown += TxtScanner_KeyDown;

            dgvCarrito.CellValueChanged += DgvCarrito_CellValueChanged;
            dgvCarrito.UserDeletedRow += DgvCarrito_UserDeletedRow;
            dgvCarrito.KeyDown += DgvCarrito_KeyDown;

            if (btnCancelarVenta != null) btnCancelarVenta.Click += BtnCancelarVenta_Click;
            if (btnCobrar != null) btnCobrar.Click += BtnCobrar_Click;
            if (btnCobroQR != null) btnCobroQR.Click += BtnCobroQR_Click;
            if (btnCobroTarjeta != null) btnCobroTarjeta.Click += BtnCobroTarjeta_Click;
            if (btnBuscarProducto != null) btnBuscarProducto.Click += BtnBuscarProducto_Click;
            if (btnConsultarPrecio != null) btnConsultarPrecio.Click += BtnConsultarPrecio_Click;
            if (btnAplicarDescuento != null) btnAplicarDescuento.Click += BtnAplicarDescuento_Click;
            if (btnSuspenderVenta != null) btnSuspenderVenta.Click += BtnSuspenderVenta_Click;
            if (btnBuscarCliente != null) btnBuscarCliente.Click += BtnBuscarCliente_Click;
            if (btnNuevoCliente != null) btnNuevoCliente.Click += BtnNuevoCliente_Click;
            if (btnRetiroCaja != null) btnRetiroCaja.Click += BtnRetiroCaja_Click;
            if (btnCuentaCorriente != null) btnCuentaCorriente.Click += BtnCuentaCorriente_Click;
            if (btnArticulosFrecuentes != null) btnArticulosFrecuentes.Click += BtnArticulosFrecuentes_Click;

            if (txtBusquedaCliente != null) txtBusquedaCliente.KeyPress += TxtBusquedaCliente_KeyPress;
            ConfigurarAutocompletadoClientes();
            if (lblClienteActual != null) lblClienteActual.Click += LblClienteActual_Click;

            if (lstSugerencias != null)
            {
                lstSugerencias.KeyDown += LstSugerencias_KeyDown;
                lstSugerencias.DoubleClick += LstSugerencias_DoubleClick;
            }

            ActualizarTotales();
        }

        private void LblClienteActual_Click(object? sender, EventArgs e)
        {
            if (_clienteActual != null && _clienteActual.Id > 1 && _clienteActual.SaldoDeudor > 0)
            {
                using var modal = new FrmAbonoCuentaCorriente(_clienteActual);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    ActualizarUICliente();
                }
                EnfocarEscaner();
            }
        }

        private void EnfocarEscaner()
        {
            if (txtScanner != null && txtScanner.CanFocus)
            {
                txtScanner.Focus();
                txtScanner.SelectAll();
            }
        }

        #region Atajos de Teclado Globales (ProcessCmdKey)

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    BtnBuscarProducto_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F2:
                    BtnArticulosFrecuentes_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F3:
                    BtnConsultarPrecio_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F4:
                    BtnSuspenderVenta_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F5:
                    BtnCobrar_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F6:
                    BtnCobroQR_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F7:
                    BtnCobroTarjeta_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F8:
                    BtnCuentaCorriente_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F9:
                    BtnRetiroCaja_Click(this, EventArgs.Empty);
                    return true;
                case Keys.F10:
                    if (txtBusquedaCliente != null)
                    {
                        txtBusquedaCliente.Focus();
                        txtBusquedaCliente.SelectAll();
                    }
                    return true;
                case Keys.Escape:
                    if (lstSugerencias != null && lstSugerencias.Visible)
                    {
                        lstSugerencias.Visible = false;
                        EnfocarEscaner();
                        return true;
                    }
                    if (_carrito.Count > 0)
                    {
                        BtnCancelarVenta_Click(this, EventArgs.Empty);
                        return true;
                    }
                    txtScanner.Clear();
                    EnfocarEscaner();
                    return true;
                case Keys.Delete:
                    // Quitar ítem del carrito si no estamos escribiendo en los textboxes
                    if (!txtScanner.Focused && !(txtBusquedaCliente?.Focused ?? false))
                    {
                        EliminarItemSeleccionado();
                        return true;
                    }
                    break;
                case Keys.Add:
                case Keys.Oemplus:
                    // Incrementar cantidad del ítem seleccionado si no estamos escribiendo en el escáner
                    if (!txtScanner.Focused && !(txtBusquedaCliente?.Focused ?? false))
                    {
                        ModificarCantidadSeleccionada(1);
                        return true;
                    }
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    // Decrementar cantidad del ítem seleccionado
                    if (!txtScanner.Focused && !(txtBusquedaCliente?.Focused ?? false))
                    {
                        ModificarCantidadSeleccionada(-1);
                        return true;
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Búsqueda Predictiva y Escáner con Multiplicador

        private void TxtScanner_TextChanged(object sender, EventArgs e)
        {
            string term = txtScanner.Text.Trim();

            // Si contiene asterisco para multiplicar, no mostrar sugerencias de texto para no entorpecer
            if (term.Contains('*'))
            {
                if (lstSugerencias != null) lstSugerencias.Visible = false;
                return;
            }

            if (term.Length >= 2 && lstSugerencias != null)
            {
                var sugerencias = _productoService.BuscarPorNombreOSKU(term);
                if (sugerencias.Any())
                {
                    lstSugerencias.DataSource = sugerencias;
                    lstSugerencias.DisplayMember = "Nombre";
                    lstSugerencias.ValueMember = "SKU";
                    lstSugerencias.Visible = true;
                    lstSugerencias.BringToFront();
                }
                else
                {
                    lstSugerencias.Visible = false;
                }
            }
            else if (lstSugerencias != null)
            {
                lstSugerencias.Visible = false;
            }
        }

        private void TxtScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lstSugerencias != null && lstSugerencias.Visible && lstSugerencias.Items.Count > 0)
            {
                lstSugerencias.Focus();
                lstSugerencias.SelectedIndex = 0;
            }
        }

        private void LstSugerencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstSugerencias.SelectedItem != null)
            {
                e.Handled = true;
                SeleccionarSugerencia();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lstSugerencias.Visible = false;
                EnfocarEscaner();
            }
        }

        private void LstSugerencias_DoubleClick(object sender, EventArgs e)
        {
            if (lstSugerencias.SelectedItem != null)
            {
                SeleccionarSugerencia();
            }
        }

        private void SeleccionarSugerencia()
        {
            var sku = lstSugerencias.SelectedValue?.ToString();
            if (!string.IsNullOrEmpty(sku))
            {
                AgregarAlCarrito(sku, 1);
            }
            lstSugerencias.Visible = false;
            txtScanner.Clear();
            EnfocarEscaner();
        }

        private void TxtScanner_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (lstSugerencias != null && lstSugerencias.Visible && lstSugerencias.Items.Count > 0)
                {
                    lstSugerencias.SelectedIndex = 0;
                    SeleccionarSugerencia();
                    return;
                }

                string input = txtScanner.Text.Trim();
                if (string.IsNullOrEmpty(input)) return;

                int cantidad = 1;
                string sku = input;

                // Soporte para multiplicador por teclado: cant*código (ej: 3*77912345 o 5*coca)
                if (input.Contains('*'))
                {
                    var partes = input.Split('*');
                    if (partes.Length == 2 && int.TryParse(partes[0].Trim(), out int cantParsed) && cantParsed > 0)
                    {
                        cantidad = cantParsed;
                        sku = partes[1].Trim();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Formato de multiplicador no válido.\n\nUtilice el formato: [cantidad]*[código]\nEjemplo: 3*77912345 (para agregar 3 unidades).",
                            "Formato Incorrecto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        txtScanner.SelectAll();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(sku))
                {
                    AgregarAlCarrito(sku, cantidad);
                }

                txtScanner.Clear();
                EnfocarEscaner();
            }
        }

        #endregion

        #region Manejo del Carrito y Validación Rigurosa de Stock

        private void AgregarProducto(Producto producto, int cantidad = 1)
        {
            if (producto == null || string.IsNullOrEmpty(producto.SKU)) return;
            AgregarAlCarrito(producto.SKU, cantidad);
        }

        private void AgregarAlCarrito(string sku, int cantidad = 1)
        {
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad a agregar debe ser mayor a 0.", "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var producto = _productoService.ObtenerPorSKU(sku);
            if (producto == null)
            {
                // Si no coincide con SKU exacto, intentar por nombre
                var coincidencia = _productoService.BuscarPorNombreOSKU(sku);
                if (coincidencia.Count == 1)
                {
                    producto = coincidencia[0];
                }
                else
                {
                    MessageBox.Show($"No se encontró ningún producto con el código o SKU: {sku}", "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Validación rigurosa de Stock en tiempo real
            var itemExistente = _carrito.FirstOrDefault(d => d.IdProducto == producto.Id);
            int enCarritoActual = itemExistente?.Cantidad ?? 0;
            int totalRequerido = enCarritoActual + cantidad;

            if (totalRequerido > producto.StockActual)
            {
                string advertencia =
                    $"ALERTA DE STOCK INSUFICIENTE:\n\n" +
                    $"• Producto: {producto.Nombre}\n" +
                    $"• Stock en Sistema: {producto.StockActual} unidades\n" +
                    $"• Total Solicitado: {totalRequerido} unidades ({enCarritoActual} en carrito + {cantidad} nuevos)\n\n" +
                    $"¿Desea agregarlo de todas formas y registrar la venta con stock deficitario para regularizar luego?";

                var respuesta = MessageBox.Show(advertencia, "Alerta de Stock Físico", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta != DialogResult.Yes)
                {
                    return;
                }
            }

            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
                dgvCarrito.Refresh();
            }
            else
            {
                _carrito.Add(new DetalleVenta
                {
                    IdProducto = producto.Id,
                    Codigo = producto.SKU,
                    Descripcion = producto.Nombre,
                    PrecioUnitario = producto.PrecioVenta,
                    Cantidad = cantidad
                });
            }

            ActualizarTotales();
            EnfocarEscaner();
        }

        private void DgvCarrito_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _carrito.Count)
            {
                var item = _carrito[e.RowIndex];

                if (item.Cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a 0. Para quitar el producto presione Supr.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    item.Cantidad = 1;
                    dgvCarrito.Refresh();
                }
                else
                {
                    // Validar stock al editar cantidad manual en la grilla
                    var producto = _productoService.ObtenerPorId(item.IdProducto);
                    if (producto != null && item.Cantidad > producto.StockActual)
                    {
                        var confirm = MessageBox.Show(
                            $"La cantidad ingresada ({item.Cantidad}) supera el stock disponible ({producto.StockActual}) de '{item.Descripcion}'.\n\n¿Desea mantenerla de todas formas?",
                            "Alerta de Stock",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (confirm != DialogResult.Yes)
                        {
                            item.Cantidad = Math.Max(1, producto.StockActual);
                            dgvCarrito.Refresh();
                        }
                    }
                }

                dgvCarrito.InvalidateRow(e.RowIndex);
                ActualizarTotales();
            }
        }

        private void DgvCarrito_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ActualizarTotales();
            EnfocarEscaner();
        }

        private void DgvCarrito_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                EliminarItemSeleccionado();
            }
        }

        private void EliminarItemSeleccionado()
        {
            if (dgvCarrito.CurrentRow != null && dgvCarrito.CurrentRow.Index >= 0 && dgvCarrito.CurrentRow.Index < _carrito.Count)
            {
                var item = _carrito[dgvCarrito.CurrentRow.Index];
                var confirm = MessageBox.Show($"¿Desea quitar '{item.Descripcion}' del carrito?", "Quitar producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    _carrito.RemoveAt(dgvCarrito.CurrentRow.Index);
                    ActualizarTotales();
                    EnfocarEscaner();
                }
            }
        }

        private void ModificarCantidadSeleccionada(int delta)
        {
            if (dgvCarrito.CurrentRow != null && dgvCarrito.CurrentRow.Index >= 0 && dgvCarrito.CurrentRow.Index < _carrito.Count)
            {
                var item = _carrito[dgvCarrito.CurrentRow.Index];
                int nuevaCant = item.Cantidad + delta;

                if (nuevaCant <= 0)
                {
                    EliminarItemSeleccionado();
                }
                else
                {
                    // Validar si sube cantidad
                    if (delta > 0)
                    {
                        var producto = _productoService.ObtenerPorId(item.IdProducto);
                        if (producto != null && nuevaCant > producto.StockActual)
                        {
                            var confirm = MessageBox.Show(
                                $"Stock superado: solo quedan {producto.StockActual} unidades en sistema.\n\n¿Continuar sumando?",
                                "Stock Insuficiente",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (confirm != DialogResult.Yes) return;
                        }
                    }

                    item.Cantidad = nuevaCant;
                    dgvCarrito.Refresh();
                    ActualizarTotales();
                }
            }
        }

        private void ActualizarTotales()
        {
            decimal subtotal = _carrito.Sum(d => d.Subtotal);
            decimal descuento = subtotal * (_descuentoPorcentaje / 100);
            decimal total = subtotal - descuento;

            if (lblSubtotal != null)
            {
                lblSubtotal.Text = $"Subtotal: $ {subtotal:N2}\nDescuento ({_descuentoPorcentaje}%): $ {descuento:N2}";
            }
            if (lblTotal != null)
            {
                lblTotal.Text = $"TOTAL: $ {total:N2}";
            }
        }

        #endregion

        #region Cobro y Procesamiento de Venta con Validación Rigurosa

        private void BtnCobrar_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Escanee productos antes de cobrar.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnfocarEscaner();
                return;
            }

            decimal subtotal = _carrito.Sum(d => d.Subtotal);
            decimal descuentoMonto = subtotal * (_descuentoPorcentaje / 100);
            decimal total = subtotal - descuentoMonto;

            // Abrir calculadora de vuelto profesional para cobro en efectivo
            using var modal = new FrmCobroEfectivo(total);
            if (modal.ShowDialog(this) != DialogResult.OK)
            {
                EnfocarEscaner();
                return;
            }

            ProcesarCobro("Efectivo", modal.MontoAbonado, modal.Vuelto);
        }

        private void BtnCobroQR_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Escanee productos antes de cobrar.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnfocarEscaner();
                return;
            }

            decimal total = _carrito.Sum(d => d.Subtotal) * (1 - _descuentoPorcentaje / 100);
            var confirm = MessageBox.Show(
                $"Total a Cobrar por Mercado Pago / QR: $ {total:N2}\n\n¿El cliente ya escaneó el código y el pago figura acreditado?",
                "Confirmar Cobro QR",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ProcesarCobro("MercadoPago / QR", total, 0m);
            }
        }

        private void BtnCobroTarjeta_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Escanee productos antes de cobrar.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnfocarEscaner();
                return;
            }

            decimal total = _carrito.Sum(d => d.Subtotal) * (1 - _descuentoPorcentaje / 100);
            var confirm = MessageBox.Show(
                $"Total a Cobrar por Tarjeta de Débito/Crédito: $ {total:N2}\n\n¿La transacción fue aprobada en la terminal / posnet?",
                "Confirmar Cobro con Tarjeta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                ProcesarCobro("Tarjeta (Débito/Crédito)", total, 0m);
            }
        }

        private void BtnCuentaCorriente_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Escanee productos antes de cobrar.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnfocarEscaner();
                return;
            }

            if (_clienteActual == null || _clienteActual.Id <= 1 || _clienteActual.Dni == "00000000")
            {
                MessageBox.Show(
                    "Para cobrar con Cuenta Corriente debe seleccionar un cliente registrado (no disponible para Consumidor Final).\n\nBusque al cliente por DNI arriba a la derecha [F10].",
                    "Cliente Requerido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                if (txtBusquedaCliente != null)
                {
                    txtBusquedaCliente.Focus();
                    txtBusquedaCliente.SelectAll();
                }
                return;
            }

            decimal subtotal = _carrito.Sum(d => d.Subtotal);
            decimal descuentoMonto = subtotal * (_descuentoPorcentaje / 100);
            decimal total = subtotal - descuentoMonto;

            decimal nuevoSaldo = _clienteActual.SaldoDeudor + total;
            if (_clienteActual.LimiteCredito > 0 && nuevoSaldo > _clienteActual.LimiteCredito)
            {
                var confirmacion = MessageBox.Show(
                    $"¡Atención! Esta compra supera el límite de crédito del cliente:\n\n" +
                    $"• Límite de Crédito: $ {_clienteActual.LimiteCredito:N2}\n" +
                    $"• Saldo Deudor Actual: $ {_clienteActual.SaldoDeudor:N2}\n" +
                    $"• Compra Actual: $ {total:N2}\n" +
                    $"• Nuevo Saldo Resultante: $ {nuevoSaldo:N2}\n\n" +
                    "¿Desea autorizar y registrar la venta fiada de todas formas?",
                    "Límite de Crédito Excedido",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes) return;
            }
            else
            {
                var confirmacion = MessageBox.Show(
                    $"¿Confirma registrar esta venta fiada a Cuenta Corriente para {_clienteActual.NombreCompleto}?\n\n" +
                    $"• Total a cargar: $ {total:N2}\n" +
                    $"• Saldo Deudor Actual: $ {_clienteActual.SaldoDeudor:N2}\n" +
                    $"• Nuevo Saldo Deudor: $ {nuevoSaldo:N2}",
                    "Confirmar Cuenta Corriente",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes) return;
            }

            ProcesarCobro("Cuenta Corriente", total, 0m);
        }

        private void ProcesarCobro(string metodoPago, decimal abonado, decimal vuelto)
        {
            decimal subtotal = _carrito.Sum(d => d.Subtotal);
            decimal descuentoMonto = subtotal * (_descuentoPorcentaje / 100);
            decimal total = subtotal - descuentoMonto;

            var nuevaVenta = new Venta
            {
                IdCliente = _clienteActual.Id,
                IdUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null,
                Total = total,
                Descuento = descuentoMonto,
                MetodoPago = metodoPago,
                Estado = "Completada",
                Detalles = _carrito.ToList()
            };

            try
            {
                int idVenta = _ventaService.RegistrarVenta(nuevaVenta);

                string msg = $"¡Venta #{idVenta} Registrada con Éxito!\n\n" +
                             $"• Método de Pago: {metodoPago}\n" +
                             $"• Total Cobrado:  $ {total:N2}\n";

                if (metodoPago == "Efectivo")
                {
                    msg += $"• Abonó con:      $ {abonado:N2}\n" +
                           $"----------------------------------------\n" +
                           $"• ENTREGAR VUELTO: $ {vuelto:N2}";
                }
                else if (metodoPago == "Cuenta Corriente")
                {
                    msg += $"• Nuevo saldo del cliente: $ {(_clienteActual.SaldoDeudor + total):N2}";
                }

                MessageBox.Show(msg, "Operación Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar pantalla y resetear para el siguiente cliente
                CancelarVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la venta:\n\n{ex.Message}", "Error de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelarVenta_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count > 0)
            {
                var result = MessageBox.Show(
                    "¿Está seguro que desea cancelar la venta actual y vaciar el carrito?",
                    "Cancelar Venta",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    CancelarVenta();
                }
            }
            else
            {
                CancelarVenta();
            }
        }

        private void CancelarVenta()
        {
            _carrito.Clear();
            _descuentoPorcentaje = 0;
            _clienteActual = _clienteService.ObtenerConsumidorFinal() ?? new Cliente { Id = 1, Nombre = "Consumidor", Apellido = "Final", Dni = "00000000" };
            ActualizarUICliente();
            ActualizarTotales();
            ActualizarTextoBotonSuspender();
            if (txtScanner != null) txtScanner.Clear();
            if (txtBusquedaCliente != null) txtBusquedaCliente.Clear();
            EnfocarEscaner();
        }

        #endregion

        #region Suspender y Reanudar Venta

        private void BtnSuspenderVenta_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count > 0)
            {
                var confirm = MessageBox.Show(
                    "¿Desea suspender la venta actual para atender a otro cliente?\n\nEl ticket quedará en espera y la pantalla se liberará de inmediato.",
                    "Suspender Venta [F4]",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                _ventasSuspendidas.Add(new VentaSuspendida
                {
                    Cliente = _clienteActual,
                    Carrito = _carrito.Select(d => new DetalleVenta
                    {
                        IdProducto = d.IdProducto,
                        Codigo = d.Codigo,
                        Descripcion = d.Descripcion,
                        PrecioUnitario = d.PrecioUnitario,
                        Cantidad = d.Cantidad
                    }).ToList(),
                    DescuentoPorcentaje = _descuentoPorcentaje
                });

                MessageBox.Show(
                    $"Venta guardada en espera (Ticket #{_ventasSuspendidas.Count}).\nPantalla libre para el siguiente cliente.",
                    "Venta Suspendida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _carrito.Clear();
                _descuentoPorcentaje = 0;
                _clienteActual = _clienteService.ObtenerConsumidorFinal() ?? new Cliente { Id = 1, Nombre = "Consumidor", Apellido = "Final", Dni = "00000000" };
                ActualizarUICliente();
                ActualizarTotales();
                ActualizarTextoBotonSuspender();
                EnfocarEscaner();
            }
            else
            {
                if (_ventasSuspendidas.Count == 0)
                {
                    MessageBox.Show("No hay ninguna venta en espera actualmente.", "Sin ventas en espera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EnfocarEscaner();
                    return;
                }

                // Recuperar la última venta suspendida
                var venta = _ventasSuspendidas.Last();
                _ventasSuspendidas.RemoveAt(_ventasSuspendidas.Count - 1);

                _clienteActual = venta.Cliente ?? _clienteService.ObtenerConsumidorFinal() ?? new Cliente { Id = 1, Nombre = "Consumidor", Apellido = "Final" };
                ActualizarUICliente();

                _carrito.Clear();
                foreach (var item in venta.Carrito)
                {
                    _carrito.Add(item);
                }

                _descuentoPorcentaje = venta.DescuentoPorcentaje;
                ActualizarTotales();
                ActualizarTextoBotonSuspender();

                MessageBox.Show(
                    $"Venta recuperada con éxito ({venta.Carrito.Count} artículos).\nTotal a Cobrar: $ {venta.Total:N2}",
                    "Venta Reanudada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                EnfocarEscaner();
            }
        }

        private void ActualizarTextoBotonSuspender()
        {
            if (btnSuspenderVenta != null)
            {
                btnSuspenderVenta.Text = _ventasSuspendidas.Count > 0
                    ? $"Reanudar Venta ({_ventasSuspendidas.Count}) [F4]"
                    : "Suspender Venta [F4]";
                btnSuspenderVenta.BackColor = _ventasSuspendidas.Count > 0 ? Color.Orange : Color.DarkSeaGreen;
            }
        }

        #endregion

        #region Modales Complementarios y Utilidades

        private void BtnBuscarProducto_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmBuscarProductoModal();
            if (modal.ShowDialog(this) == DialogResult.OK && modal.ProductoSeleccionado != null)
            {
                AgregarProducto(modal.ProductoSeleccionado);
            }
            EnfocarEscaner();
        }

        private void BtnArticulosFrecuentes_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmArticulosFrecuentesModal();
            if (modal.ShowDialog(this) == DialogResult.OK && modal.ProductoSeleccionado != null)
            {
                AgregarProducto(modal.ProductoSeleccionado);
            }
            EnfocarEscaner();
        }

        private void BtnConsultarPrecio_Click(object? sender, EventArgs e)
        {
            string sku = txtScanner.Text.Trim();
            if (string.IsNullOrEmpty(sku))
            {
                MessageBox.Show("Escanee o escriba un código en el buscador para consultar su precio.", "Consultar Precio [F3]", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EnfocarEscaner();
                return;
            }

            var producto = _productoService.ObtenerPorSKU(sku) ?? _productoService.BuscarPorNombreOSKU(sku).FirstOrDefault();
            if (producto != null)
            {
                MessageBox.Show(
                    $"• Producto: {producto.Nombre}\n" +
                    $"• SKU: {producto.SKU}\n" +
                    $"• Categoría: {producto.CategoriaNombre}\n" +
                    $"• Stock Actual: {producto.StockActual} unidades\n\n" +
                    $"PRECIO DE VENTA: $ {producto.PrecioVenta:N2}",
                    "Consulta de Precio",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtScanner.Clear();
                EnfocarEscaner();
            }
            else
            {
                MessageBox.Show($"No se encontró ningún producto con el código: {sku}", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnfocarEscaner();
            }
        }

        private void BtnAplicarDescuento_Click(object? sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("Agregue productos al carrito antes de aplicar un descuento.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnfocarEscaner();
                return;
            }

            if (_descuentoPorcentaje > 0)
            {
                var remove = MessageBox.Show($"Actualmente hay un descuento del {_descuentoPorcentaje}%. ¿Desea quitarlo?", "Quitar Descuento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (remove == DialogResult.Yes)
                {
                    _descuentoPorcentaje = 0;
                    ActualizarTotales();
                }
            }
            else
            {
                var res = MessageBox.Show("¿Desea aplicar un 10% de descuento a esta compra?", "Aplicar Descuento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    _descuentoPorcentaje = 10;
                    ActualizarTotales();
                }
            }
            EnfocarEscaner();
        }

        private void BtnBuscarCliente_Click(object? sender, EventArgs e)
        {
            BuscarCliente();
        }

        private void TxtBusquedaCliente_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BuscarCliente();
            }
        }

        private void BuscarCliente()
        {
            string dni = txtBusquedaCliente.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                _clienteActual = _clienteService.ObtenerConsumidorFinal() ?? new Cliente { Id = 1, Nombre = "Consumidor", Apellido = "Final", Dni = "00000000" };
                ActualizarUICliente();
                EnfocarEscaner();
                return;
            }

            var cliente = _clienteService.ObtenerPorDni(dni);
            if (cliente != null)
            {
                _clienteActual = cliente;
                ActualizarUICliente();
                EnfocarEscaner();
            }
            else
            {
                MessageBox.Show($"No se encontró ningún cliente activo con el DNI {dni}.", "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBusquedaCliente.SelectAll();
                txtBusquedaCliente.Focus();
            }
        }

        private void BtnNuevoCliente_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Diríjase a la pestaña 'Clientes' en el menú principal para registrar un nuevo cliente.", "Nuevo Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EnfocarEscaner();
        }

        private void BtnRetiroCaja_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmMovimientoCaja("Retiro");
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    int? idUsuario = UserSession.IdUsuario > 0 ? UserSession.IdUsuario : null;
                    var cajaService = new Business.CajaService();
                    cajaService.RegistrarRetiro(modal.Monto, modal.Motivo, idUsuario);
                    MessageBox.Show(
                        $"Retiro de $ {modal.Monto:N2} registrado en caja correctamente.\n\nMotivo: {modal.Motivo}",
                        "Retiro Registrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar retiro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            EnfocarEscaner();
        }

        private void ConfigurarAutocompletadoClientes()
        {
            if (txtBusquedaCliente != null)
            {
                var data = new AutoCompleteStringCollection();
                var clientes = _clienteService.ObtenerTodos();
                foreach (var c in clientes)
                {
                    data.Add(c.Dni);
                }

                txtBusquedaCliente.AutoCompleteCustomSource = data;
                txtBusquedaCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtBusquedaCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        #endregion
    }
}
