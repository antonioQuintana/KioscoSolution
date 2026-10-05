using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcVenta : UserControl
    {
        private readonly ProductoService _productoService;
        private BindingList<DetalleVenta> _carrito;

        public UcVenta()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            _carrito = new BindingList<DetalleVenta>();

            ConfigurarUI();
        }

        private void ConfigurarUI()
        {
            dgvCarrito.AutoGenerateColumns = false;
            
            // Si el DataGridView no tiene columnas diseñadas, las creamos. 
            // Si ya las tiene en el diseñador, esto no hará daño si los DataPropertyName coinciden.
            if (dgvCarrito.Columns.Count == 0)
            {
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cód/SKU", DataPropertyName = "Codigo", Width = 100, ReadOnly = true });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Descripción", DataPropertyName = "Descripcion", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", DataPropertyName = "PrecioUnitario", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cant.", DataPropertyName = "Cantidad", Width = 80 });
                dgvCarrito.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", DataPropertyName = "Subtotal", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }, ReadOnly = true });
            }

            dgvCarrito.DataSource = _carrito;
            
            // Eventos
            txtScanner.KeyPress += TxtScanner_KeyPress;
            dgvCarrito.CellValueChanged += DgvCarrito_CellValueChanged;
            dgvCarrito.UserDeletedRow += DgvCarrito_UserDeletedRow;

            if (btnCancelarVenta != null) btnCancelarVenta.Click += BtnCancelarVenta_Click;
            if (btnCobrar != null) btnCobrar.Click += BtnCobrar_Click;
            if (btnCobroQR != null) btnCobroQR.Click += BtnCobroQR_Click;
            if (btnCobroTarjeta != null) btnCobroTarjeta.Click += BtnCobroTarjeta_Click;
            if (btnBuscarProducto != null) btnBuscarProducto.Click += BtnBuscarProducto_Click;
            if (btnConsultarPrecio != null) btnConsultarPrecio.Click += BtnConsultarPrecio_Click;
            if (btnAplicarDescuento != null) btnAplicarDescuento.Click += BtnAplicarDescuento_Click;
            if (btnSuspenderVenta != null) btnSuspenderVenta.Click += BtnSuspenderVenta_Click;
            
            txtScanner.TextChanged += TxtScanner_TextChanged;
            txtScanner.KeyDown += TxtScanner_KeyDown;
            if (lstSugerencias != null)
            {
                lstSugerencias.KeyDown += LstSugerencias_KeyDown;
                lstSugerencias.DoubleClick += LstSugerencias_DoubleClick;
            }

            ActualizarTotales();
        }

        private void TxtScanner_TextChanged(object sender, EventArgs e)
        {
            string term = txtScanner.Text.Trim();
            if (term.Length >= 2)
            {
                var sugerencias = _productoService.BuscarPorNombreOSKU(term);
                if (sugerencias.Any())
                {
                    lstSugerencias.DataSource = sugerencias;
                    lstSugerencias.DisplayMember = "Nombre"; // Mostrar solo el nombre (o podemos formatearlo)
                    lstSugerencias.ValueMember = "SKU";
                    lstSugerencias.Visible = true;
                    lstSugerencias.BringToFront();
                }
                else
                {
                    lstSugerencias.Visible = false;
                }
            }
            else
            {
                lstSugerencias.Visible = false;
            }
        }

        private void TxtScanner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && lstSugerencias.Visible && lstSugerencias.Items.Count > 0)
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
                txtScanner.Focus();
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
                AgregarAlCarrito(sku);
            }
            lstSugerencias.Visible = false;
            txtScanner.Clear();
            txtScanner.Focus();
        }

        private void TxtScanner_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Detectar si el usuario presionó Enter (como un lector de códigos de barras)
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (lstSugerencias.Visible && lstSugerencias.Items.Count > 0)
                {
                    // Si la lista está visible, elegir el primero por defecto o no hacer nada
                    lstSugerencias.SelectedIndex = 0;
                    SeleccionarSugerencia();
                }
                else
                {
                    string sku = txtScanner.Text.Trim();
                    if (!string.IsNullOrEmpty(sku))
                    {
                        AgregarAlCarrito(sku);
                    }
                    txtScanner.Clear();
                    txtScanner.Focus();
                }
            }
        }

        private void AgregarAlCarrito(string sku)
        {
            var producto = _productoService.ObtenerPorSKU(sku);
            if (producto == null)
            {
                MessageBox.Show($"No se encontró ningún producto con el SKU: {sku}", "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemExistente = _carrito.FirstOrDefault(d => d.IdProducto == producto.Id);
            if (itemExistente != null)
            {
                itemExistente.Cantidad += 1;
                // Forzar actualización de la fila para que recalcule subtotal
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
                    Cantidad = 1
                });
            }

            ActualizarTotales();
        }

        private decimal _descuentoPorcentaje = 0;

        private void DgvCarrito_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Forzar que el grid se redibuje para actualizar la columna calculada "Subtotal"
                dgvCarrito.InvalidateRow(e.RowIndex);
                ActualizarTotales();
            }
        }

        private void DgvCarrito_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ActualizarTotales();
        }

        private void ActualizarTotales()
        {
            decimal subtotal = _carrito.Sum(d => d.Subtotal);
            decimal descuento = subtotal * (_descuentoPorcentaje / 100);
            decimal total = subtotal - descuento;

            if (lblSubtotal != null)
            {
                lblSubtotal.Text = $"$ {subtotal:N2}";
            }
            if (lblTotal != null)
            {
                lblTotal.Text = $"$ {total:N2}";
            }
        }

        private void BtnCancelarVenta_Click(object sender, EventArgs e)
        {
            if (_carrito.Count > 0)
            {
                var result = MessageBox.Show("¿Está seguro que desea cancelar la venta actual?", "Cancelar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CancelarVenta();
                }
            }
        }

        private void CancelarVenta()
        {
            _carrito.Clear();
            _descuentoPorcentaje = 0;
            ActualizarTotales();
            txtScanner.Clear();
            txtScanner.Focus();
        }

        private void BtnCobrar_Click(object sender, EventArgs e)
        {
            ProcesarCobro("Efectivo");
        }

        private void BtnCobroQR_Click(object sender, EventArgs e)
        {
            ProcesarCobro("MercadoPago / QR");
        }

        private void BtnCobroTarjeta_Click(object sender, EventArgs e)
        {
            ProcesarCobro("Tarjeta (Débito/Crédito)");
        }

        private void ProcesarCobro(string metodoPago)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Escanee productos antes de cobrar.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal subtotal = _carrito.Sum(d => d.Subtotal);
            decimal total = subtotal - (subtotal * (_descuentoPorcentaje / 100));
            
            // Aquí en el futuro se guardará en la tabla Ventas y DetallesVenta
            string msg = $"Venta registrada con éxito.\n\nMétodo: {metodoPago}\nTotal Cobrado: $ {total:N2}";
            if (_descuentoPorcentaje > 0) msg += $"\n(Incluye descuento del {_descuentoPorcentaje}%)";

            MessageBox.Show(msg, "Venta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            CancelarVenta(); // Limpia la pantalla para el siguiente cliente
        }

        private void BtnConsultarPrecio_Click(object sender, EventArgs e)
        {
            string sku = txtScanner.Text.Trim();
            if (string.IsNullOrEmpty(sku))
            {
                MessageBox.Show("Primero escanee o escriba un código en el buscador para consultar su precio.", "Consultar Precio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtScanner.Focus();
                return;
            }

            var producto = _productoService.ObtenerPorSKU(sku);
            if (producto != null)
            {
                MessageBox.Show($"Producto: {producto.Nombre}\nPrecio: $ {producto.PrecioVenta:N2}", "Consulta de Precio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtScanner.Clear();
                txtScanner.Focus();
            }
            else
            {
                MessageBox.Show($"No se encontró el producto con código: {sku}", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAplicarDescuento_Click(object sender, EventArgs e)
        {
            if (_carrito.Count == 0)
            {
                MessageBox.Show("Agregue productos al carrito antes de aplicar un descuento.", "Carrito vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_descuentoPorcentaje > 0)
            {
                var remove = MessageBox.Show($"Ya existe un descuento del {_descuentoPorcentaje}%. ¿Desea quitarlo?", "Quitar Descuento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            txtScanner.Focus();
        }

        private void BtnSuspenderVenta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La funcionalidad de 'Suspender Venta' permitirá guardar este carrito temporalmente para atender a otro cliente.\n¡Próximamente!", "Próximamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBuscarProducto_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La funcionalidad de 'Búsqueda Manual de Productos' se implementará pronto.\nPor ahora usa el buscador predictivo escribiendo en la barra de escáner.", "Próximamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

