using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Views;

namespace KioscoApp
{
    public partial class UcABMClientes : UserControl
    {
        private readonly ClienteService _clienteService;
        private BindingList<Cliente> _listaClientes;
        private int _clienteIdEdicion = 0;
        private Button btnAbonarCuenta;

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
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;

            dgvClientes.Columns.Clear();
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "DNI", DataPropertyName = "Dni", Width = 90 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "Nombre", Width = 110 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Apellido", DataPropertyName = "Apellido", Width = 110 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { 
                HeaderText = "Saldo Deudor", 
                DataPropertyName = "SaldoDeudor", 
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { 
                HeaderText = "Límite Crédito", 
                DataPropertyName = "LimiteCredito", 
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Teléfono", DataPropertyName = "Telefono", Width = 100 });
            dgvClientes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Width = 140 });

            dgvClientes.DataSource = _listaClientes;
            dgvClientes.CellFormatting += DgvClientes_CellFormatting;
            dgvClientes.DoubleClick += DgvClientes_DoubleClick;

            // Botón de Cobranza de Deuda (Abono)
            btnAbonarCuenta = new Button
            {
                Text = "Cobrar / Abonar Deuda",
                Location = new Point(20, dgvClientes.Bottom + 10),
                Size = new Size(220, 36),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAbonarCuenta.FlatAppearance.BorderSize = 0;
            btnAbonarCuenta.Click += BtnAbonarCuenta_Click;
            Controls.Add(btnAbonarCuenta);

            if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += BtnLimpiar_Click;
            if (btnEditar != null) btnEditar.Click += BtnEditar_Click;
            if (btnEliminar != null) btnEliminar.Click += BtnEliminar_Click;
            
            // Llenar combos de provincias
            if (cmbProvincia != null && cmbProvincia.Items.Count == 0)
            {
                cmbProvincia.Items.AddRange(new object[] { "Buenos Aires", "Ciudad Autónoma de Buenos Aires", "Córdoba", "Santa Fe", "Mendoza", "Entre Ríos", "Tucumán" });
            }
        }

        private void DgvClientes_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _listaClientes.Count) return;

            var cliente = _listaClientes[e.RowIndex];
            if (cliente.SaldoDeudor > 0)
            {
                // Si la columna es SaldoDeudor, formatear en rojo y negrita
                if (dgvClientes.Columns[e.ColumnIndex].DataPropertyName == "SaldoDeudor")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);
                    e.CellStyle.Font = new Font(dgvClientes.Font, FontStyle.Bold);
                }
            }
        }

        private void DgvClientes_DoubleClick(object? sender, EventArgs e)
        {
            AbrirModalCobroDeuda();
        }

        private void BtnAbonarCuenta_Click(object? sender, EventArgs e)
        {
            AbrirModalCobroDeuda();
        }

        private void AbrirModalCobroDeuda()
        {
            if (dgvClientes.CurrentRow?.DataBoundItem is Cliente cliente)
            {
                if (cliente.SaldoDeudor <= 0)
                {
                    MessageBox.Show(
                        $"El cliente {cliente.NombreCompleto} se encuentra al día (Saldo Deudor: $ 0.00).\n\nNo posee saldo pendiente de pago.",
                        "Sin Deuda Pendiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                using var modal = new FrmAbonoCuentaCorriente(cliente);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    CargarClientes();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un cliente de la grilla para registrar el cobro de su cuenta corriente.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                    Nacimiento = dtpNacimiento != null ? dtpNacimiento.Value : (DateTime?)null,
                    LimiteCredito = 15000m
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
                if (cliente.Id <= 1)
                {
                    MessageBox.Show(
                        "No se puede eliminar al cliente 'Consumidor Final' ya que es el cliente predeterminado del sistema.",
                        "Operación No Permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (cliente.SaldoDeudor > 0)
                {
                    MessageBox.Show(
                        $"No se puede dar de baja al cliente {cliente.NombreCompleto} porque mantiene un saldo deudor pendiente de $ {cliente.SaldoDeudor:N2}.\n\nDebe regularizar el pago de su cuenta antes de eliminarlo.",
                        "Cliente con Deuda Pendiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

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
