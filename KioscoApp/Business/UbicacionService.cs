using System.Collections.Generic;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class UbicacionService
    {
        private UbicacionRepository _repository;

        public UbicacionService()
        {
            _repository = new UbicacionRepository();
        }

        public List<Provincia> ObtenerProvincias()
        {
            return _repository.ObtenerProvincias();
        }

        public List<Ciudad> ObtenerCiudadesPorProvincia(int provinciaId)
        {
            return _repository.ObtenerCiudadesPorProvincia(provinciaId);
        }
    }
}
