using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcABMProductos : UserControl
    {
        private int productoIdSeleccionado = 0;
        private readonly ErrorProvider errorProvider = new ErrorProvider();
        private readonly ProductoService _productoService;
        private readonly CategoriaService _categoriaService;
        private List<Producto>? _listaProductos;
        private List<Categoria>? _listaCategorias;

        public UcABMProductos()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            _categoriaService = new CategoriaService();
            ConfigurarValidacionesEntrada();
            CargarCategorias();
            CargarProductos();
        }

        private void ConfigurarValidacionesEntrada()
        {
            txtSKU.MaxLength = 50;
            txtNombre.MaxLength = 100;
            txtDesc.MaxLength = 255;

            // Restringir ingreso de caracteres no numéricos en costos/precios
            txtCosto.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                    e.Handled = true;
            };

            txtVenta.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                    e.Handled = true;
            };

            txtStock.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };

            txtStockMin.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
        }

        private void CargarCategorias()
        {
            try
            {
                _listaCategorias = _categoriaService.ObtenerTodas();
                cmbCategoria.DataSource = null;
                cmbCategoria.DisplayMember = "Nombre";
                cmbCategoria.ValueMember = "Id";
                cmbCategoria.DataSource = _listaCategorias;
                cmbCategoria.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                _listaProductos = _productoService.ObtenerTodos();
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = _listaProductos;

                if (dgvProductos.Columns["IdCategoria"] != null)
                    dgvProductos.Columns["IdCategoria"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Producto ConstruirProductoDesdeForm()
        {
            decimal.TryParse(txtCosto.Text.Replace('.', ','), out decimal costo);
            decimal.TryParse(txtVenta.Text.Replace('.', ','), out decimal venta);
            int.TryParse(txtStock.Text, out int stock);
            int.TryParse(txtStockMin.Text, out int stockMin);

            int idCat = cmbCategoria.SelectedValue is int catId ? catId : 0;

            return new Producto
            {
                Id = productoIdSeleccionado,
                SKU = txtSKU.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(txtDesc.Text) ? null : txtDesc.Text.Trim(),
                IdCategoria = idCat,
                CategoriaNombre = cmbCategoria.Text,
                PrecioCosto = costo,
                PrecioVenta = venta,
                StockActual = stock,
                StockMinimo = stockMin
            };
        }

        private bool MostrarErroresDeValidacion(ValidationResult resultado)
        {
            errorProvider.Clear();
            if (resultado.IsValid) return true;

            var controlMap = new Dictionary<string, Control>
            {
                { "SKU", txtSKU },
                { "Nombre", txtNombre },
                { "Descripcion", txtDesc },
                { "Categoria", cmbCategoria },
                { "PrecioCosto", txtCosto },
                { "PrecioVenta", txtVenta },
                { "StockActual", txtStock },
                { "StockMinimo", txtStockMin },
                { "General", this }
            };

            List<string> mensajesError = new List<string>();

            foreach (var error in resultado.Errors)
            {
                if (controlMap.TryGetValue(error.Key, out Control ctrl) && ctrl != this)
                {
                    errorProvider.SetError(ctrl, error.Value);
                }
                mensajesError.Add($"- {error.Key}: {error.Value}");
            }

            MessageBox.Show("Por favor, complete o corrija los campos marcados en rojo.\n\nDetalles:\n" + string.Join("\n", mensajesError), "Validación de Formulario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var producto = ConstruirProductoDesdeForm();
            bool esEdicion = productoIdSeleccionado != 0;

            if (esEdicion)
            {
                var actual = _listaProductos?.Find(p => p.Id == productoIdSeleccionado);
                if (actual != null &&
                    actual.SKU == producto.SKU &&
                    actual.Nombre == producto.Nombre &&
                    actual.Descripcion == producto.Descripcion &&
                    actual.IdCategoria == producto.IdCategoria &&
                    actual.PrecioCosto == producto.PrecioCosto &&
                    actual.PrecioVenta == producto.PrecioVenta &&
                    actual.StockActual == producto.StockActual &&
                    actual.StockMinimo == producto.StockMinimo)
                {
                    MessageBox.Show("No se realizó ningún cambio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            var resultado = _productoService.GuardarProducto(producto, esEdicion);
            if (!MostrarErroresDeValidacion(resultado)) return;

            MessageBox.Show(esEdicion ? "Producto actualizado con éxito." : "Producto registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Limpiar();
            CargarProductos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (productoIdSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var resultado = _productoService.EliminarProducto(productoIdSeleccionado);
                if (!MostrarErroresDeValidacion(resultado)) return;

                MessageBox.Show("Producto eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarProductos();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Rows[e.RowIndex].DataBoundItem is Producto producto)
            {
                productoIdSeleccionado = producto.Id;
                txtSKU.Text = producto.SKU;
                txtNombre.Text = producto.Nombre;
                txtDesc.Text = producto.Descripcion ?? string.Empty;
                cmbCategoria.SelectedValue = producto.IdCategoria;
                txtCosto.Text = producto.PrecioCosto.ToString("0.##");
                txtVenta.Text = producto.PrecioVenta.ToString("0.##");
                txtStock.Text = producto.StockActual.ToString();
                txtStockMin.Text = producto.StockMinimo.ToString();
            }
        }

        private void Limpiar()
        {
            productoIdSeleccionado = 0;
            errorProvider.Clear();
            txtSKU.Clear();
            txtNombre.Clear();
            txtDesc.Clear();
            cmbCategoria.SelectedIndex = -1;
            txtCosto.Clear();
            txtVenta.Clear();
            txtStock.Clear();
            txtStockMin.Clear();
            txtSKU.Focus();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "Archivo CSV compatible con Excel (*.csv)|*.csv",
                    FileName = $"Catalogo_Productos_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Exportar Catálogo de Productos a Excel / CSV"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var productos = _productoService.ObtenerTodos();
                var sb = new StringBuilder();

                // Encabezados estándar con punto y coma para compatibilidad directa con Excel en español
                sb.AppendLine("SKU;Nombre;Categoria;PrecioCosto;PrecioVenta;StockActual;StockMinimo;Descripcion");

                foreach (var p in productos)
                {
                    string categoria = p.CategoriaNombre ?? "";
                    string desc = (p.Descripcion ?? "").Replace(";", " ").Replace("\r", " ").Replace("\n", " ");
                    sb.AppendLine($"{p.SKU};{p.Nombre};{categoria};{p.PrecioCosto:F2};{p.PrecioVenta:F2};{p.StockActual};{p.StockMinimo};{desc}");
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show(
                    $"Se exportaron {productos.Count} productos exitosamente a:\n\n{sfd.FileName}\n\nPuede abrir el archivo directamente con Microsoft Excel para consultar o actualizar precios de forma masiva.",
                    "Exportación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar productos a Excel: {ex.Message}", "Error de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using var ofd = new OpenFileDialog
                {
                    Filter = "Archivos CSV o Texto (*.csv;*.txt)|*.csv;*.txt|Todos los archivos (*.*)|*.*",
                    Title = "Seleccionar archivo CSV / Excel para actualizar precios y productos"
                };

                if (ofd.ShowDialog() != DialogResult.OK) return;

                var lineas = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                if (lineas.Length <= 1)
                {
                    MessageBox.Show("El archivo seleccionado no contiene filas de datos o está vacío.", "Archivo sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int actualizados = 0;
                int creados = 0;
                int errores = 0;

                var categorias = _categoriaService.ObtenerTodas();
                int defaultCatId = categorias.FirstOrDefault()?.Id ?? 1;

                // Detectar separador (; o ,)
                char sep = lineas[0].Contains(';') ? ';' : ',';

                for (int i = 1; i < lineas.Length; i++)
                {
                    string linea = lineas[i].Trim();
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] partes = linea.Split(sep);
                    if (partes.Length < 4)
                    {
                        errores++;
                        continue;
                    }

                    string sku = partes[0].Trim();
                    string nombre = partes.Length > 1 ? partes[1].Trim() : "";

                    if (string.IsNullOrWhiteSpace(sku))
                    {
                        errores++;
                        continue;
                    }

                    // Parsear precios tolerando coma o punto
                    string costoStr = partes[3].Replace("$", "").Trim().Replace(',', '.');
                    decimal.TryParse(costoStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precioCosto);

                    decimal precioVenta = 0;
                    if (partes.Length > 4)
                    {
                        string ventaStr = partes[4].Replace("$", "").Trim().Replace(',', '.');
                        decimal.TryParse(ventaStr, NumberStyles.Any, CultureInfo.InvariantCulture, out precioVenta);
                    }

                    int stock = 0;
                    if (partes.Length > 5) int.TryParse(partes[5].Trim(), out stock);

                    int stockMin = 5;
                    if (partes.Length > 6) int.TryParse(partes[6].Trim(), out stockMin);

                    var existente = _productoService.ObtenerPorSKU(sku);
                    if (existente != null)
                    {
                        if (!string.IsNullOrWhiteSpace(nombre)) existente.Nombre = nombre;
                        if (precioCosto > 0) existente.PrecioCosto = precioCosto;
                        if (precioVenta > 0) existente.PrecioVenta = precioVenta;
                        if (stock > 0) existente.StockActual = stock;
                        if (stockMin > 0) existente.StockMinimo = stockMin;

                        _productoService.GuardarProducto(existente, true);
                        actualizados++;
                    }
                    else
                    {
                        // Nuevo producto si tiene nombre y precio de venta
                        if (!string.IsNullOrWhiteSpace(nombre) && precioVenta > 0)
                        {
                            var nuevo = new Producto
                            {
                                SKU = sku,
                                Nombre = nombre,
                                IdCategoria = defaultCatId,
                                PrecioCosto = precioCosto,
                                PrecioVenta = precioVenta,
                                StockActual = stock,
                                StockMinimo = stockMin,
                                                            };
                            _productoService.GuardarProducto(nuevo, false);
                            creados++;
                        }
                        else
                        {
                            errores++;
                        }
                    }
                }

                CargarProductos();

                MessageBox.Show(
                    $"Importación masiva completada con éxito:\n\n" +
                    $"• Productos actualizados (Precios / Stock): {actualizados}\n" +
                    $"• Nuevos productos dados de alta: {creados}\n" +
                    $"• Filas omitidas con formato incorrecto: {errores}",
                    "Actualización Masiva Completa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo Excel / CSV: {ex.Message}", "Error de Importación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
