using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace KioscoApp
{
    public partial class UcABMUsuarios : UserControl
    {
        private int usuarioIdSeleccionado = 0;

        public UcABMUsuarios()
        {
            InitializeComponent();
            
        }

        private void FrmGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Id, Nombre, Usuario, Rol FROM Usuarios";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvUsuarios.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                usuarioIdSeleccionado = Convert.ToInt32(row.Cells["Id"].Value);
                txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
                txtUsuario.Text = row.Cells["Usuario"].Value?.ToString();
                txtContrasena.Text = ""; // No cargar contraseña por seguridad
                cmbRol.SelectedItem = row.Cells["Rol"].Value?.ToString();
            }
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Usuarios (Nombre, Usuario, Contrasena, Rol) VALUES (@Nombre, @Usuario, @Contrasena, @Rol)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        string hashPassword = BCrypt.Net.BCrypt.HashPassword(txtContrasena.Text);

                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@Contrasena", hashPassword);
                        cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem?.ToString() ?? "Vendedor");
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado == 0) return;
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Usuarios SET Nombre=@Nombre, Usuario=@Usuario, Rol=@Rol " + 
                                   (string.IsNullOrEmpty(txtContrasena.Text) ? "" : ", Contrasena=@Contrasena ") + 
                                   "WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        string hashPassword = BCrypt.Net.BCrypt.HashPassword(txtContrasena.Text);

                        cmd.Parameters.AddWithValue("@Id", usuarioIdSeleccionado);
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem?.ToString());
                        if (!string.IsNullOrEmpty(txtContrasena.Text))
                            cmd.Parameters.AddWithValue("@Contrasena", hashPassword);
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioIdSeleccionado == 0) return;
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Usuarios WHERE Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", usuarioIdSeleccionado);
                        cmd.ExecuteNonQuery();
                    }
                }
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            usuarioIdSeleccionado = 0;
            txtNombre.Clear();
            txtUsuario.Clear();
            txtContrasena.Clear();
            cmbRol.SelectedIndex = -1;
        }
    }
}

