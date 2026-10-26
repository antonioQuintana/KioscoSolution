using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Views
{
    public class FrmBuscarProductoModal : Form
    {
        public Producto? ProductoSeleccionado { get; private set; }

        private readonly ProductoService _productoService;
        private List<Producto> _catalogo;
        private TextBox txtBuscar;
        private DataGridView dgvProductos;
        private Button btnAgregar;
        private Button btnCerrar;

        public FrmBuscarProductoModal()
        {
            _productoService = new ProductoService();
            _catalogo = new List<Producto>();

            this.Text = "Catálogo de Productos - Búsqueda Rápida";
            this.Size = new Size(800, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(45, 45, 48);
            this.ForeColor = Color.White;

            InicializarComponentes();
            CargarProductos();
        }

        private void InicializarComponentes()
        {
            var lblBuscar = new Label
            {
                Text = "🔍 Buscar por Nombre o SKU:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtBuscar = new TextBox
            {
                Location = new Point(250, 18),
                Size = new Size(350, 26),
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White
            };
            txtBuscar.TextChanged += (s, e) => Filtrar();

            dgvProductos = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(745, 340),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.Black
            };
            dgvProductos.CellDoubleClick += (s, e) => SeleccionarYConfirmar();

            btnAgregar = new Button
            {
                Text = "➕ Agregar al Carrito",
                Location = new Point(480, 415),
                Size = new Size(160, 35),
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAgregar.Click += (s, e) => SeleccionarYConfirmar();

            btnCerrar = new Button
            {
                Text = "Cerrar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(650, 415),
                Size = new Size(115, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtBuscar);
            this.Controls.Add(dgvProductos);
            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnCerrar);

            this.CancelButton = btnCerrar;
        }

        private void CargarProductos()
        {
            try
            {
                _catalogo = _productoService.ObtenerTodos() ?? new List<Producto>();

                dgvProductos.AutoGenerateColumns = false;
                dgvProductos.Columns.Clear();
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SKU", HeaderText = "SKU", FillWeight = 20 });
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nombre", HeaderText = "Producto", FillWeight = 40 });
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CategoriaNombre", HeaderText = "Categoría", FillWeight = 20 });
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioVenta", HeaderText = "Precio", FillWeight = 15, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight } });
                dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StockActual", HeaderText = "Stock", FillWeight = 12, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });

                Filtrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Filtrar()
        {
            string texto = txtBuscar.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(texto))
            {
                dgvProductos.DataSource = _catalogo.ToList();
            }
            else
            {
                dgvProductos.DataSource = _catalogo
                    .Where(p => (p.Nombre?.ToLower().Contains(texto) ?? false) ||
                                (p.SKU?.ToLower().Contains(texto) ?? false) ||
                                (p.CategoriaNombre?.ToLower().Contains(texto) ?? false))
                    .ToList();
            }
        }

        private void SeleccionarYConfirmar()
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is Producto prod)
            {
                ProductoSeleccionado = prod;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
