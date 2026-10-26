using System;
using System.Collections.Generic;
using KioscoApp.Data;
using KioscoApp.Models;

namespace KioscoApp.Business
{
    public class CajaService
    {
        private readonly CajaRepository _cajaRepository;

        public CajaService()
        {
            _cajaRepository = new CajaRepository();
        }

        public ResumenCajaActual ObtenerResumenCajaActual(decimal fondoInicial = 0m)
        {
            return _cajaRepository.ObtenerResumenCajaActual(fondoInicial);
        }

        public void RegistrarIngreso(decimal monto, string motivo, int? idUsuario)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto a ingresar debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("Debe especificar un motivo para el ingreso de dinero.");

            var mov = new MovimientoCaja
            {
                IdUsuario = idUsuario,
                Tipo = "Ingreso",
                Monto = monto,
                Motivo = motivo.Trim()
            };

            _cajaRepository.RegistrarMovimiento(mov);
        }

        public void RegistrarRetiro(decimal monto, string motivo, int? idUsuario)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto a retirar debe ser mayor a 0.");

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("Debe especificar un motivo o concepto para el retiro de dinero.");

            var mov = new MovimientoCaja
            {
                IdUsuario = idUsuario,
                Tipo = "Retiro",
                Monto = monto,
                Motivo = motivo.Trim()
            };

            _cajaRepository.RegistrarMovimiento(mov);
        }

        public int ConfirmarCierre(CierreCaja cierre)
        {
            if (cierre.TotalDeclarado < 0)
                throw new ArgumentException("El efectivo contado no puede ser negativo.");

            return _cajaRepository.RegistrarCierreCaja(cierre);
        }

        public List<CierreCaja> ObtenerHistorialCierres()
        {
            return _cajaRepository.ObtenerHistorialCierres();
        }
    }
}
