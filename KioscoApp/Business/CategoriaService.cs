using System;
using System.Collections.Generic;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repository;

        public CategoriaService()
        {
            _repository = new CategoriaRepository();
        }

        public List<Categoria> ObtenerTodas()
        {
            return _repository.ObtenerTodas();
        }

        public Categoria? ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public ValidationResult GuardarCategoria(Categoria c, bool isEdit)
        {
            var result = ValidarCategoria(c, isEdit);
            if (!result.IsValid) return result;

            if (isEdit)
            {
                _repository.Actualizar(c);
            }
            else
            {
                _repository.Insertar(c);
            }

            return result;
        }

        public ValidationResult EliminarCategoria(int id)
        {
            var result = new ValidationResult();

            if (id <= 0)
            {
                result.AddError("General", "Categoría no válida para eliminar.");
                return result;
            }

            if (_repository.TieneProductosAsociados(id))
            {
                result.AddError("General", "No se puede eliminar la categoría porque hay productos asociados a ella.");
                return result;
            }

            _repository.Eliminar(id);
            return result;
        }

        private ValidationResult ValidarCategoria(Categoria c, bool isEdit)
        {
            var result = new ValidationResult();
            int idExcluir = isEdit ? c.Id : 0;

            if (string.IsNullOrWhiteSpace(c.Nombre))
            {
                result.AddError("Nombre", "El nombre de la categoría es obligatorio.");
            }
            else if (c.Nombre.Trim().Length < 3)
            {
                result.AddError("Nombre", "El nombre de la categoría debe tener al menos 3 caracteres.");
            }
            else if (_repository.ExisteNombre(c.Nombre, idExcluir))
            {
                result.AddError("Nombre", "Ya existe una categoría con ese nombre.");
            }

            return result;
        }
    }
}
