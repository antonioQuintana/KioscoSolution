using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using KioscoApp.Business;
using KioscoApp.Utils;

namespace KioscoApp
{
    public partial class UcMovimientosStock : UserControl
    {
        private int idProductoSeleccionado = -1;
        private DataTable _dtStock = new DataTable();

        public UcMovimientosStock()
        {
            InitializeComponent();
            ConfigurarGrilla();
            ConfigurarAyuda();
            CargarStock();
        }

        private void ConfigurarAyuda()
        {
            HelpTooltipHelper.AttachHelp(
                lblTitulo,
                "Control de Inventario y Alertas de Stock",
                "Supervisa en tiempo real las existencias físicas de todos los productos con semáforo visual:\n" +
                "• 🔴 Rojo oscuro: Producto AGOTADO (Stock <= 0).\n" +
                "• 🟡 Amarillo/Dorado: Stock CRÍTICO (Stock Actual menor o igual al Stock Mínimo).\n" +
                "• Gris/Normal: Stock saludable.",
                shrinkControl: false,
                offsetX: 10);

            var tipBuscar = HelpTooltipHelper.CreateToolTip("Búsqueda de Artículos en Stock");
            tipBuscar.SetToolTip(txtBuscar, "Escriba el código SKU o parte del nombre del producto para filtrar el inventario inmediatamente.");

            HelpTooltipHelper.AttachHelp(
                chkSoloAlertas,
                "Filtro Rápido de Alertas de Reposición",
                "Marque esta casilla para ocultar los productos con stock suficiente y listar únicamente aquellos que están agotados o en nivel crítico para armar el pedido a proveedores.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachGroupBoxHelp(
                groupBoxModificar,
                "Formulario de Ajuste Manual de Stock",
                "Seleccione un producto en la tabla izquierda, ingrese el nuevo 'Stock Actual' (conteo de mercadería recibida o inventario físico) y el 'Stock Mínimo' de seguridad, y presione 'Guardar Cambios'.");

            HelpTooltipHelper.AttachHelp(
                btnGuardar,
                "Guardar Cambios de Stock",
                "Actualiza inmediatamente las existencias del producto seleccionado en la base de datos y deja asentado el movimiento en el historial de Auditoría de Stock.",
                shrinkControl: true);

            HelpTooltipHelper.AttachGroupBoxHelp(
                groupBoxHerramientasStock,
                "Reportes Financieros y Auditoría de Stock",
                "Acceda al cálculo de capital invertido en mercadería o consulte la trazabilidad completa de quién modificó el stock y cuándo.");

            HelpTooltipHelper.AttachHelp(
                btnInventarioValorizado,
                "Reporte de Inventario Valorizado",
                "Abre un informe financiero que calcula el valor total de la mercadería a Precio de Costo, la Proyección de Venta y la Ganancia Estimada por artículo y categoría, exportable a Excel.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnAuditoriaStock,
                "Historial y Auditoría de Ajustes",
                "Abre el registro cronológico de todos los ajustes manuales de stock realizados, indicando fecha, hora, usuario responsable, producto y valores asignados.",
                shrinkControl: true);
        }

        private void ConfigurarGrilla()
        {
            dgvStock.AutoGenerateColumns = false;
            dgvStock.Columns.Clear();
            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.MultiSelect = false;

            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id", Visible = false });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SKU", HeaderText = "SKU (Código)", FillWeight = 20 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Producto", FillWeight = 35 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Categoria", HeaderText = "Categoría", FillWeight = 20 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockActual", HeaderText = "Stock Actual", FillWeight = 12 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockMinimo", HeaderText = "Stock Mínimo", FillWeight = 13 });

            dgvStock.CellFormatting += DgvStock_CellFormatting;
        }

        private void DgvStock_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvStock.Rows.Count) return;

            var row = dgvStock.Rows[e.RowIndex];
            if (row.Cells[4].Value != null && row.Cells[5].Value != null &&
                int.TryParse(row.Cells[4].Value.ToString(), out int actual) &&
                int.TryParse(row.Cells[5].Value.ToString(), out int minimo))
            {
                if (actual <= 0)
                {
                    // Quiebre de stock (Agotado)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(70, 25, 25);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 170, 170);
                }
                else if (actual <= minimo)
                {
                    // Stock crítico / bajo mínimo
                    row.DefaultCellStyle.BackColor = Color.FromArgb(60, 50, 20);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 225, 130);
                }
                else
                {
                    // Normal
                    row.DefaultCellStyle.BackColor = Color.FromArgb(47, 54, 64);
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void CargarStock(string filtro = "")
        {
            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_Productos_FiltrarStock", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            _dtStock = new DataTable();
                            da.Fill(_dtStock);
                            AplicarFiltroLocal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltroLocal()
        {
            if (_dtStock == null) return;

            var dv = _dtStock.DefaultView;
            if (chkSoloAlertas != null && chkSoloAlertas.Checked)
            {
                dv.RowFilter = "StockActual <= StockMinimo";
            }
            else
            {
                dv.RowFilter = "";
            }

            dgvStock.DataSource = dv;

            // Calcular contadores
            int total = _dtStock.Rows.Count;
            int agotados = 0;
            int criticos = 0;

            foreach (DataRow row in _dtStock.Rows)
            {
                if (row["StockActual"] != DBNull.Value && row["StockMinimo"] != DBNull.Value)
                {
                    int act = Convert.ToInt32(row["StockActual"]);
                    int min = Convert.ToInt32(row["StockMinimo"]);
                    if (act <= 0) agotados++;
                    else if (act <= min) criticos++;
                }
            }

            if (lblResumenStock != null)
            {
                lblResumenStock.Text = $"Total artículos: {total}  |  🔴 Agotados: {agotados}  |  🟡 Bajo Stock: {criticos}  |  Mostrando en grilla: {dv.Count}";
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarStock(txtBuscar.Text);
        }

        private void chkSoloAlertas_CheckedChanged(object? sender, EventArgs e)
        {
            AplicarFiltroLocal();
        }

        private void dgvStock_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStock.CurrentRow != null && dgvStock.CurrentRow.Cells[0].Value != null)
            {
                idProductoSeleccionado = Convert.ToInt32(dgvStock.CurrentRow.Cells[0].Value);
                lblProductoSeleccionado.Text = dgvStock.CurrentRow.Cells[2].Value?.ToString() ?? string.Empty;
                numStockActual.Value = Convert.ToDecimal(dgvStock.CurrentRow.Cells[4].Value ?? 0);
                numStockMinimo.Value = Convert.ToDecimal(dgvStock.CurrentRow.Cells[5].Value ?? 0);
            }
            else
            {
                idProductoSeleccionado = -1;
                lblProductoSeleccionado.Text = "Ningún producto seleccionado";
                numStockActual.Value = 0;
                numStockMinimo.Value = 0;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == -1)
            {
                MessageBox.Show("Seleccione un producto de la grilla para modificar su stock.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stockActual = (int)numStockActual.Value;
            int stockMinimo = (int)numStockMinimo.Value;

            if (stockMinimo < 0)
            {
                MessageBox.Show("El stock mínimo de seguridad no puede ser negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numStockMinimo.Focus();
                return;
            }

            if (stockActual < 0)
            {
                var confirm = MessageBox.Show(
                    "Ha ingresado un valor de stock negativo.\n\n¿Confirma registrar un saldo negativo para regularizarlo luego de un inventario físico?",
                    "Confirmar Stock Negativo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes) return;
            }

            try
            {
                using (SqlConnection con = DatabaseHelper.GetConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_Productos_ActualizarStock", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Actual", stockActual);
                        cmd.Parameters.AddWithValue("@Minimo", stockMinimo);
                        cmd.Parameters.AddWithValue("@Id", idProductoSeleccionado);
                        cmd.ExecuteNonQuery();
                    }
                }

                AuditoriaService.Registrar("Stock", "Ajuste de Stock",
                    $"Producto: '{lblProductoSeleccionado.Text}', Stock Actual: {stockActual}, Stock Mínimo: {stockMinimo}");

                MessageBox.Show(
                    $"Stock de '{lblProductoSeleccionado.Text}' actualizado exitosamente:\n\n• Stock Actual: {stockActual}\n• Stock Mínimo: {stockMinimo}",
                    "Stock Guardado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarStock(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInventarioValorizado_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmInventarioValorizadoModal();
            modal.ShowDialog(this);
        }

        private void btnAuditoriaStock_Click(object? sender, EventArgs e)
        {
            using var modal = new Views.FrmAuditoriaStockModal();
            modal.ShowDialog(this);
        }
    }
}
