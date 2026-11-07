using System;
using System.Collections.Generic;
using System.Linq;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class VentaService
    {
        private readonly VentaRepository _repository;
        private readonly ClienteRepository _clienteRepository;

        public VentaService()
        {
            _repository = new VentaRepository();
            _clienteRepository = new ClienteRepository();
        }

        public int RegistrarVenta(Venta venta)
        {
            if (venta == null)
                throw new ArgumentNullException(nameof(venta), "La venta no puede ser nula.");

            if (venta.Detalles == null || venta.Detalles.Count == 0)
                throw new ArgumentException("El carrito de la venta no contiene ningún producto.", nameof(venta));

            if (venta.IdCliente <= 0)
                throw new ArgumentException("Debe asociar un cliente válido a la venta.", nameof(venta));

            if (venta.Total < 0)
                throw new ArgumentException("El total de la venta no puede ser negativo.", nameof(venta));

            // Validar rigurosamente cada ítem del detalle
            foreach (var d in venta.Detalles)
            {
                if (d.IdProducto <= 0)
                    throw new ArgumentException("Se detectó un producto con identificador no válido en el ticket.");

                if (d.Cantidad <= 0)
                    throw new ArgumentException($"La cantidad para el producto '{d.Descripcion}' debe ser mayor a 0.");

                if (d.PrecioUnitario < 0)
                    throw new ArgumentException($"El precio unitario del producto '{d.Descripcion}' no puede ser negativo.");
            }

            // Validaciones rigurosas para Cuenta Corriente (Fiado)
            if (string.Equals(venta.MetodoPago, "Cuenta Corriente", StringComparison.OrdinalIgnoreCase))
            {
                if (venta.IdCliente <= 1)
                {
                    throw new InvalidOperationException("No se permite venta a Cuenta Corriente para el cliente 'Consumidor Final'. Debe registrar o seleccionar un cliente con cuenta abierta.");
                }

                var cliente = _clienteRepository.ObtenerPorId(venta.IdCliente);
                if (cliente == null || !cliente.Activo)
                {
                    throw new InvalidOperationException("El cliente seleccionado no existe o se encuentra inactivo en el sistema.");
                }

                decimal saldoResultante = cliente.SaldoDeudor + venta.Total;
                if (cliente.LimiteCredito > 0 && saldoResultante > cliente.LimiteCredito)
                {
                    decimal exceso = saldoResultante - cliente.LimiteCredito;
                    throw new InvalidOperationException(
                        $"Límite de crédito superado:\n\n" +
                        $"• Saldo deudor actual: $ {cliente.SaldoDeudor:N2}\n" +
                        $"• Importe de esta compra: $ {venta.Total:N2}\n" +
                        $"• Saldo resultante: $ {saldoResultante:N2}\n" +
                        $"• Límite autorizado: $ {cliente.LimiteCredito:N2}\n\n" +
                        $"Excede por: $ {exceso:N2}. Solicite autorización al supervisor.");
                }
            }

            return _repository.InsertarVenta(venta);
        }

        public List<Venta> ObtenerTodas()
        {
            return _repository.ObtenerTodas();
        }

        public List<Venta> FiltrarVentas(DateTime? desde = null, DateTime? hasta = null, string? filtro = null, bool soloTurnoActual = false)
        {
            return _repository.FiltrarVentas(desde, hasta, filtro, soloTurnoActual);
        }

        public void AnularVenta(int idVenta, string motivo, int? idUsuario)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El ID de la venta a anular es inválido.", nameof(idVenta));

            _repository.AnularVenta(idVenta, motivo, idUsuario);

            AuditoriaService.Registrar("Ventas", "Anulación de Venta",
                $"Ticket #{idVenta} anulado. Motivo: '{motivo ?? "Sin motivo especificado"}'");
        }

        public List<DetalleVenta> ObtenerDetallesPorVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException("El ID de la venta debe ser mayor a 0.", nameof(idVenta));

            return _repository.ObtenerDetallesPorVenta(idVenta);
        }
    }
}
