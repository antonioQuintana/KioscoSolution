using System;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class VentaService
    {
        private readonly VentaRepository _repository;

        public VentaService()
        {
            _repository = new VentaRepository();
        }

        public int RegistrarVenta(Venta venta)
        {
            if (venta.Detalles == null || venta.Detalles.Count == 0)
                throw new ArgumentException("La venta no tiene productos.");

            if (venta.IdCliente <= 0)
                throw new ArgumentException("Cliente no válido.");

            return _repository.InsertarVenta(venta);
        }

        public System.Collections.Generic.List<Venta> ObtenerTodas()
        {
            return _repository.ObtenerTodas();
        }
    }
}
