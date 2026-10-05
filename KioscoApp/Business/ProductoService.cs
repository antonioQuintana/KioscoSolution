using System;
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

        public Producto ObtenerPorSKU(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) return null;
            return _repository.ObtenerPorSKU(sku.Trim());
        }

        public System.Collections.Generic.List<Producto> BuscarPorNombreOSKU(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return new System.Collections.Generic.List<Producto>();
            return _repository.BuscarPorNombreOSKU(term.Trim());
        }
    }
}
