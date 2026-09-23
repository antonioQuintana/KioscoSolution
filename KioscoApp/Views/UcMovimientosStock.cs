using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class UcMovimientosStock : UserControl
    {
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=KioscoDB;Integrated Security=True;";
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

            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id", Visible = false });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SKU", HeaderText = "SKU (Código)", Width = 150 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Producto", Width = 250 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Categoria", HeaderText = "Categoría", Width = 150 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockActual", HeaderText = "Stock Actual", Width = 100 });
            dgvStock.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockMinimo", HeaderText = "Stock Mínimo", Width = 100 });
        }

        private void CargarStock(string filtro = "")
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                        SELECT p.Id, p.SKU, p.Nombre, c.Nombre AS Categoria, p.StockActual, p.StockMinimo 
                        FROM Productos p
                        LEFT JOIN Categorias c ON p.IdCategoria = c.Id
                        WHERE p.Nombre LIKE @Filtro OR p.SKU LIKE @Filtro
                        ORDER BY p.Nombre";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
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

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE Productos SET StockActual = @Actual, StockMinimo = @Minimo WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Actual", (int)numStockActual.Value);
                        cmd.Parameters.AddWithValue("@Minimo", (int)numStockMinimo.Value);
                        cmd.Parameters.AddWithValue("@Id", idProductoSeleccionado);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Stock actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarStock(txtBuscar.Text); // Recargar
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
