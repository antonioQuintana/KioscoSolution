using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp
{
    public partial class UcABMCategorias : UserControl
    {
        private int categoriaIdSeleccionada = 0;
        private readonly ErrorProvider errorProvider = new ErrorProvider();
        private readonly CategoriaService _categoriaService;
        private List<Categoria>? _listaCategorias;

        public UcABMCategorias()
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            try
            {
                _listaCategorias = _categoriaService.ObtenerTodas();
                dgvCategorias.DataSource = null;
                dgvCategorias.DataSource = _listaCategorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Categoria ConstruirCategoriaDesdeForm()
        {
            return new Categoria
            {
                Id = categoriaIdSeleccionada,
                Nombre = txtNombre.Text.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcion.Text) ? null : txtDescripcion.Text.Trim()
            };
        }

        private bool MostrarErroresDeValidacion(ValidationResult resultado)
        {
            errorProvider.Clear();
            if (resultado.IsValid) return true;

            var controlMap = new Dictionary<string, Control>
            {
                { "Nombre", txtNombre },
                { "Descripcion", txtDescripcion },
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
            var categoria = ConstruirCategoriaDesdeForm();
            bool esEdicion = categoriaIdSeleccionada != 0;

            if (esEdicion)
            {
                var actual = _listaCategorias?.Find(c => c.Id == categoriaIdSeleccionada);
                if (actual != null && actual.Nombre == categoria.Nombre && actual.Descripcion == categoria.Descripcion)
                {
                    MessageBox.Show("No se realizó ningún cambio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            var resultado = _categoriaService.GuardarCategoria(categoria, esEdicion);
            if (!MostrarErroresDeValidacion(resultado)) return;

            MessageBox.Show(esEdicion ? "Categoría actualizada con éxito." : "Categoría registrada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Limpiar();
            CargarCategorias();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (categoriaIdSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una categoría para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea eliminar esta categoría?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var resultado = _categoriaService.EliminarCategoria(categoriaIdSeleccionada);
                if (!MostrarErroresDeValidacion(resultado)) return;

                MessageBox.Show("Categoría eliminada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarCategorias();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCategorias.Rows[e.RowIndex].DataBoundItem is Categoria categoria)
            {
                categoriaIdSeleccionada = categoria.Id;
                txtNombre.Text = categoria.Nombre;
                txtDescripcion.Text = categoria.Descripcion ?? string.Empty;
            }
        }

        private void Limpiar()
        {
            categoriaIdSeleccionada = 0;
            errorProvider.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtNombre.Focus();
        }
    }
}
