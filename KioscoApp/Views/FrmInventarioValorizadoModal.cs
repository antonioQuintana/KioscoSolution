using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Utils;

namespace KioscoApp.Views
{
    public class FrmInventarioValorizadoModal : Form
    {
        private readonly CajaService _cajaService;
        private List<InventarioValorizadoItem> _todosLosItems = new List<InventarioValorizadoItem>();

        private TextBox txtBuscar = null!;
        private ComboBox cmbCategoria = null!;
        private DataGridView dgvInventario = null!;
        private Label lblMetricas = null!;

        public FrmInventarioValorizadoModal()
        {
            _cajaService = new CajaService();
            InitializeCustomComponents();
            CargarDatos();
        }

        private void InitializeCustomComponents()
        {
            Text = "Reporte de Inventario Valorizado del Negocio";
            ClientSize = new Size(1020, 680);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(28, 28, 30);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5F);
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };

            var lblTitulo = new Label
            {
                Text = "💰 Inventario Valorizado y Ganancia Potencial en Mercadería",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113),
                Location = new Point(20, 15),
                AutoSize = true
            };

            // Panel de filtros
            var pnlFiltros = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(980, 50),
                BackColor = Color.FromArgb(38, 38, 42),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblBuscar = new Label
            {
                Text = "Buscar:",
                Location = new Point(12, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White
            };

            txtBuscar = new TextBox
            {
                Location = new Point(70, 11),
                Size = new Size(230, 25),
                BackColor = Color.FromArgb(50, 50, 55),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBuscar.TextChanged += (s, e) => AplicarFiltro();

            var lblCat = new Label
            {
                Text = "Categoría:",
                Location = new Point(320, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White
            };

            cmbCategoria = new ComboBox
            {
                Location = new Point(400, 11),
                Size = new Size(180, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(50, 50, 55),
                ForeColor = Color.White
            };
            cmbCategoria.SelectedIndexChanged += (s, e) => AplicarFiltro();

            var btnLimpiar = new Button
            {
                Text = "Limpiar",
                Location = new Point(595, 10),
                Size = new Size(80, 28),
                BackColor = Color.FromArgb(80, 80, 85),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLimpiar.Click += (s, e) =>
            {
                txtBuscar.Clear();
                if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            };

            var btnExportarCSV = new Button
            {
                Text = "📥 Exportar CSV",
                Location = new Point(830, 10),
                Size = new Size(135, 28),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnExportarCSV.Click += (s, e) => ExportarACSV();

            pnlFiltros.Controls.AddRange(new Control[] { lblBuscar, txtBuscar, lblCat, cmbCategoria, btnLimpiar, btnExportarCSV });

            // DataGridView
            dgvInventario = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(980, 460),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(32, 32, 36),
                BorderStyle = BorderStyle.Fixed3D
            };

            dgvInventario.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgvInventario.DefaultCellStyle.ForeColor = Color.White;
            dgvInventario.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvInventario.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvInventario.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 32);
            dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvInventario.EnableHeadersVisualStyles = false;
            dgvInventario.RowHeadersVisible = false;

            // Barra de métricas
            lblMetricas = new Label
            {
                Location = new Point(20, 580),
                Size = new Size(980, 30),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 180, 255),
                Text = "Calculando métricas..."
            };

            // Botón Cerrar
            var btnCerrar = new Button
            {
                Text = "Cerrar [Esc]",
                Location = new Point(850, 620),
                Size = new Size(150, 42),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnCerrar.Click += (s, e) => Close();

            Controls.AddRange(new Control[] { lblTitulo, pnlFiltros, dgvInventario, lblMetricas, btnCerrar });

            HelpTooltipHelper.AttachHelp(lblTitulo, "Inventario Valorizado",
                "Calcula en tiempo real el capital invertido en mercadería (Stock × Precio Costo), el ingreso proyectado (Stock × Precio Venta) y la ganancia potencial estimada.");
            HelpTooltipHelper.AttachHelp(btnLimpiar, "Filtros de Inventario",
                "Escriba parte del nombre o código SKU y filtre por categoría para recalcular instantáneamente las métricas financieras del subconjunto seleccionado.");
            HelpTooltipHelper.AttachHelp(btnExportarCSV, "Exportar a Excel (CSV)",
                "Descarga el listado valorizado actual en un archivo CSV compatible con Microsoft Excel para análisis contable.", shrinkControl: true);
        }

        private void CargarDatos()
        {
            try
            {
                _todosLosItems = _cajaService.ObtenerInventarioValorizado();

                // Llenar combo categorias
                cmbCategoria.Items.Clear();
                cmbCategoria.Items.Add("[Todas las categorías]");
                var categorias = _todosLosItems.Select(i => i.Categoria).Distinct().OrderBy(c => c);
                foreach (var cat in categorias)
                {
                    cmbCategoria.Items.Add(cat);
                }
                cmbCategoria.SelectedIndex = 0;

                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inventario valorizado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltro()
        {
            string busqueda = txtBuscar.Text.Trim();
            string categoria = cmbCategoria.SelectedItem?.ToString() ?? "[Todas las categorías]";

            var filtrados = _todosLosItems.Where(i =>
            {
                if (categoria != "[Todas las categorías]" && !i.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                    return false;

                if (!string.IsNullOrEmpty(busqueda) &&
                    !i.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase) &&
                    !i.SKU.Contains(busqueda, StringComparison.OrdinalIgnoreCase))
                    return false;

                return true;
            }).ToList();

            var display = filtrados.Select(i => new
            {
                i.SKU,
                Producto = i.Nombre,
                i.Categoria,
                Stock = i.StockActual,
                CostoUnit = $"$ {i.PrecioCosto:N2}",
                VentaUnit = $"$ {i.PrecioVenta:N2}",
                InversionCosto = $"$ {i.ValorCostoTotal:N2}",
                ProyeccionVenta = $"$ {i.ValorVentaTotal:N2}",
                Ganancia = $"$ {i.GananciaPotencial:N2}"
            }).ToList();

            dgvInventario.DataSource = display;

            int articulos = filtrados.Count;
            int stockTotal = filtrados.Sum(i => i.StockActual);
            decimal inversionTotal = filtrados.Sum(i => i.ValorCostoTotal);
            decimal proyeccionTotal = filtrados.Sum(i => i.ValorVentaTotal);
            decimal gananciaTotal = proyeccionTotal - inversionTotal;

            lblMetricas.Text = $"Artículos: {articulos}  |  Stock Físico: {stockTotal:N0} u.  |  💵 Costo Invertido: $ {inversionTotal:N2}  |  🏷️ Proyección Venta: $ {proyeccionTotal:N2}  |  📈 Ganancia Estimada: $ {gananciaTotal:N2}";
        }

        private void ExportarACSV()
        {
            if (dgvInventario.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "Archivo CSV compatible con Excel (*.csv)|*.csv",
                    FileName = $"Inventario_Valorizado_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Exportar Inventario Valorizado"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var sb = new StringBuilder();
                var headers = new List<string>();
                foreach (DataGridViewColumn col in dgvInventario.Columns)
                {
                    headers.Add(col.HeaderText);
                }
                sb.AppendLine(string.Join(";", headers));

                foreach (DataGridViewRow row in dgvInventario.Rows)
                {
                    var cells = new List<string>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string val = cell.Value?.ToString() ?? "";
                        cells.Add(val.Replace(";", " ").Replace("\r", " ").Replace("\n", " "));
                    }
                    sb.AppendLine(string.Join(";", cells));
                }

                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                MessageBox.Show(
                    $"Reporte exportado exitosamente a:\n\n{sfd.FileName}\n\nPuede abrirlo en Excel para análisis financiero.",
                    "Exportación Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
