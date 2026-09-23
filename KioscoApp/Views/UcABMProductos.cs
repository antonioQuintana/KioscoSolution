using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace KioscoApp
{
    public partial class UcABMProductos : UserControl
    {
        private int productoIdSeleccionado = 0;

        public UcABMProductos()
        {
            InitializeComponent();
            CargarCategorias();
            CargarProductos();
        }

        private void CargarCategorias()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Id, Nombre FROM Categorias ORDER BY Nombre";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbCategoria.DisplayMember = "Nombre";
                    cmbCategoria.ValueMember = "Id";
                    cmbCategoria.DataSource = dt;
                    cmbCategoria.SelectedIndex = -1;
                }
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
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, 
                                     c.Nombre AS Categoria, p.IdCategoria, 
                                     p.PrecioCosto, p.PrecioVenta, p.StockActual, p.StockMinimo
                                     FROM Productos p
                                     INNER JOIN Categorias c ON p.IdCategoria = c.Id";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvProductos.DataSource = dt;
                    dgvProductos.Columns["IdCategoria"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSKU.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("El SKU, Nombre y Categoría son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = productoIdSeleccionado == 0 
                        ? @"INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo) 
                            VALUES (@SKU, @Nombre, @Desc, @IdCat, @Costo, @Venta, @Stock, @StockMin)"
                        : @"UPDATE Productos SET SKU=@SKU, Nombre=@Nombre, Descripcion=@Desc, IdCategoria=@IdCat, 
                            PrecioCosto=@Costo, PrecioVenta=@Venta, StockActual=@Stock, StockMinimo=@StockMin 
                            WHERE Id=@Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SKU", txtSKU.Text.Trim());
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@Desc", txtDesc.Text.Trim());
                        cmd.Parameters.AddWithValue("@IdCat", cmbCategoria.SelectedValue);
                        cmd.Parameters.AddWithValue("@Costo", string.IsNullOrEmpty(txtCosto.Text) ? 0 : Convert.ToDecimal(txtCosto.Text));
                        cmd.Parameters.AddWithValue("@Venta", string.IsNullOrEmpty(txtVenta.Text) ? 0 : Convert.ToDecimal(txtVenta.Text));
                        cmd.Parameters.AddWithValue("@Stock", string.IsNullOrEmpty(txtStock.Text) ? 0 : Convert.ToInt32(txtStock.Text));
                        cmd.Parameters.AddWithValue("@StockMin", string.IsNullOrEmpty(txtStockMin.Text) ? 0 : Convert.ToInt32(txtStockMin.Text));
                        
                        if (productoIdSeleccionado != 0)
                            cmd.Parameters.AddWithValue("@Id", productoIdSeleccionado);
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Producto guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarProductos();
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("El SKU ingresado ya existe. Utilice un código único.", "Error de Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (productoIdSeleccionado == 0) return;

            if (MessageBox.Show("¿Está seguro que desea eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Productos WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", productoIdSeleccionado);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Limpiar();
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                productoIdSeleccionado = Convert.ToInt32(row.Cells["Id"].Value);
                txtSKU.Text = row.Cells["SKU"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtDesc.Text = row.Cells["Descripcion"].Value.ToString();
                cmbCategoria.SelectedValue = row.Cells["IdCategoria"].Value;
                txtCosto.Text = row.Cells["PrecioCosto"].Value.ToString();
                txtVenta.Text = row.Cells["PrecioVenta"].Value.ToString();
                txtStock.Text = row.Cells["StockActual"].Value.ToString();
                txtStockMin.Text = row.Cells["StockMinimo"].Value.ToString();
            }
        }

        private void Limpiar()
        {
            productoIdSeleccionado = 0;
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
            MessageBox.Show("Funcionalidad 'Descargar Excel' en desarrollo (Próximamente)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad 'Actualizar Precios con Excel' en desarrollo (Próximamente)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
