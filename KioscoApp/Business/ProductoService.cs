using System;
using System.Collections.Generic;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class ProductoService
    {
        private readonly ProductoRepository _repository;

        public ProductoService()
        {
            _repository = new ProductoRepository();
        }

        public List<Producto> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Producto? ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public ValidationResult GuardarProducto(Producto p, bool isEdit)
        {
            var result = ValidarProducto(p, isEdit);
            if (!result.IsValid) return result;

            if (isEdit)
            {
                _repository.Actualizar(p);
            }
            else
            {
                _repository.Insertar(p);
            }

            return result;
        }

        public ValidationResult EliminarProducto(int id)
        {
            var result = new ValidationResult();

            if (id <= 0)
            {
                result.AddError("General", "Producto no válido para eliminar.");
                return result;
            }

            _repository.Eliminar(id);
            return result;
        }

        private ValidationResult ValidarProducto(Producto p, bool isEdit)
        {
            var result = new ValidationResult();
            int idExcluir = isEdit ? p.Id : 0;

            // SKU
            if (string.IsNullOrWhiteSpace(p.SKU))
            {
                result.AddError("SKU", "El código SKU es obligatorio.");
            }
            else if (_repository.ExisteSKU(p.SKU, idExcluir))
            {
                result.AddError("SKU", "El SKU ingresado ya pertenece a otro producto.");
            }

            // Nombre
            if (string.IsNullOrWhiteSpace(p.Nombre))
            {
                result.AddError("Nombre", "El nombre del producto es obligatorio.");
            }
            else if (p.Nombre.Trim().Length < 2)
            {
                result.AddError("Nombre", "El nombre del producto debe tener al menos 2 caracteres.");
            }

            // Categoría
            if (p.IdCategoria <= 0)
            {
                result.AddError("Categoria", "Debe seleccionar una categoría válida.");
            }

            // Precios
            if (p.PrecioCosto < 0)
            {
                result.AddError("PrecioCosto", "El precio de costo no puede ser negativo.");
            }

            if (p.PrecioVenta < 0)
            {
                result.AddError("PrecioVenta", "El precio de venta no puede ser negativo.");
            }

            // Stocks
            if (p.StockActual < 0)
            {
                result.AddError("StockActual", "El stock actual no puede ser negativo.");
            }

            if (p.StockMinimo < 0)
            {
                result.AddError("StockMinimo", "El stock mínimo no puede ser negativo.");
            }

            return result;
        }
    }
}
