using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace KioscoApp
{
    public partial class UcMovimientosStock : UserControl
    {
        private int idProductoSeleccionado = -1;

        public UcMovimientosStock()
        {
            InitializeComponent();
            ConfigurarGrilla();
            CargarStock();
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
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvStock.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarStock(txtBuscar.Text);
        }

        private void dgvStock_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStock.CurrentRow != null)
            {
                idProductoSeleccionado = Convert.ToInt32(dgvStock.CurrentRow.Cells[0].Value);
                lblProductoSeleccionado.Text = dgvStock.CurrentRow.Cells[2].Value.ToString();
                numStockActual.Value = Convert.ToDecimal(dgvStock.CurrentRow.Cells[4].Value);
                numStockMinimo.Value = Convert.ToDecimal(dgvStock.CurrentRow.Cells[5].Value);
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
    }
}
