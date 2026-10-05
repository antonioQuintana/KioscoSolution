using System;
using System.ComponentModel;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcABMClientes : UserControl
    {
        private readonly ClienteService _clienteService;
        private BindingList<Cliente> _listaClientes;
        private int _clienteIdEdicion = 0;

        public UcABMClientes()
        {
            InitializeComponent();
            _clienteService = new ClienteService();
            _listaClientes = new BindingList<Cliente>();

            ConfigurarUI();
            CargarClientes();
        }

        private void ConfigurarUI()
        {
            dgvClientes.AutoGenerateColumns = false;
            if (dgvClientes.Columns.Count == 0)
            {
                dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "DNI", DataPropertyName = "Dni" });
                dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "Nombre" });
                dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Apellido", DataPropertyName = "Apellido" });
                dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Teléfono", DataPropertyName = "Telefono" });
                dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email" });
            }
            dgvClientes.DataSource = _listaClientes;

            if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += BtnLimpiar_Click;
            if (btnEditar != null) btnEditar.Click += BtnEditar_Click;
            if (btnEliminar != null) btnEliminar.Click += BtnEliminar_Click;
            
            // Llenar combos de ejemplo
            if (cmbProvincia != null) cmbProvincia.Items.AddRange(new object[] { "Buenos Aires", "Córdoba", "Santa Fe" });
            if (cmbCiudad != null) cmbCiudad.Items.AddRange(new object[] { "Capital", "La Plata", "Mar del Plata" });
        }

        private void CargarClientes()
        {
            _listaClientes.Clear();
            var clientes = _clienteService.ObtenerTodos();
            foreach (var c in clientes)
            {
                _listaClientes.Add(c);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            _clienteIdEdicion = 0;
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtCalle.Clear();
            txtNumero.Clear();
            if (cmbProvincia != null) cmbProvincia.SelectedIndex = -1;
            if (cmbCiudad != null) cmbCiudad.SelectedIndex = -1;
            if (dtpNacimiento != null) dtpNacimiento.Value = DateTime.Now;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var cliente = new Cliente
                {
                    Id = _clienteIdEdicion,
                    Dni = txtDNI.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Calle = txtCalle.Text.Trim(),
                    Numero = txtNumero.Text.Trim(),
                    Provincia = cmbProvincia?.SelectedItem?.ToString(),
                    Ciudad = cmbCiudad?.SelectedItem?.ToString(),
                    Nacimiento = dtpNacimiento != null ? dtpNacimiento.Value : (DateTime?)null
                };

                _clienteService.Guardar(cliente);
                MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimpiarFormulario();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow?.DataBoundItem is Cliente cliente)
            {
                _clienteIdEdicion = cliente.Id;
                txtDNI.Text = cliente.Dni;
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtTelefono.Text = cliente.Telefono;
                txtEmail.Text = cliente.Email;
                txtCalle.Text = cliente.Calle;
                txtNumero.Text = cliente.Numero;
                if (cmbProvincia != null) cmbProvincia.SelectedItem = cliente.Provincia;
                if (cmbCiudad != null) cmbCiudad.SelectedItem = cliente.Ciudad;
                if (dtpNacimiento != null && cliente.Nacimiento.HasValue) dtpNacimiento.Value = cliente.Nacimiento.Value;
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la lista para editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow?.DataBoundItem is Cliente cliente)
            {
                var confirm = MessageBox.Show($"¿Desea eliminar al cliente {cliente.NombreCompleto}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        _clienteService.Eliminar(cliente.Id);
                        CargarClientes();
                        LimpiarFormulario();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

