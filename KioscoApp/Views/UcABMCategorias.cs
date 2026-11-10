using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KioscoApp.Business;
using KioscoApp.Models;
using KioscoApp.Utils;

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
            ConfigurarAyuda();
            CargarCategorias();
        }

        private void ConfigurarAyuda()
        {
            HelpTooltipHelper.AttachHelp(
                lblTitulo,
                "Gestión de Categorías de Productos",
                "Permite administrar los rubros o familias de artículos del comercio (ej. Bebidas, Golosinas, Cigarrillos, Almacén).\n\n" +
                "• Seleccione cualquier fila en la tabla izquierda para editar su nombre o descripción.\n" +
                "• Use el formulario derecho para crear nuevas categorías o eliminar rubros en desuso.",
                shrinkControl: false,
                offsetX: 10);

            HelpTooltipHelper.AttachGroupBoxHelp(
                gbFormulario,
                "Formulario de Datos de la Categoría",
                "Ingrese el Nombre único de la categoría (obligatorio) y opcionalmente una breve Descripción sobre el tipo de mercadería que agrupa.");

            HelpTooltipHelper.AttachHelp(
                lblNombre,
                "Nombre del Rubro / Categoría",
                "Escriba un nombre claro y representativo (por ejemplo: 'Bebidas sin Alcohol' o 'Snacks Salados'). No se permiten nombres duplicados.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                lblDesc,
                "Descripción de la Categoría",
                "Campo opcional para detallar qué productos integran esta familia comercial.",
                shrinkControl: false,
                offsetX: 6);

            HelpTooltipHelper.AttachHelp(
                btnGuardar,
                "Guardar Categoría",
                "Registra una nueva categoría si el formulario estaba limpio, o guarda los cambios realizados sobre la categoría seleccionada.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnEliminar,
                "Eliminar Categoría",
                "Elimina la categoría seleccionada en la grilla.\n" +
                "Nota: Por integridad de datos, el sistema impide borrar categorías que tengan productos asociados actualmente.",
                shrinkControl: true);

            HelpTooltipHelper.AttachHelp(
                btnLimpiar,
                "Limpiar Formulario",
                "Blanquea los campos de texto y deselecciona la fila actual para permitir el alta de una nueva categoría.",
                shrinkControl: true);
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
