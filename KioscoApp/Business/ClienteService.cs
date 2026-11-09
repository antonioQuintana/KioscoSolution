using System;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;

        public ClienteService()
        {
            _repository = new ClienteRepository();
        }

        public Cliente ObtenerConsumidorFinal()
        {
            return _repository.ObtenerPorDni("00000000");
        }

        public Cliente ObtenerPorDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return null;
            return _repository.ObtenerPorDni(dni.Trim());
        }

        public System.Collections.Generic.List<Cliente> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public void Guardar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Dni)) throw new ArgumentException("El DNI es obligatorio.");
            if (string.IsNullOrWhiteSpace(cliente.Nombre)) throw new ArgumentException("El Nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(cliente.Apellido)) throw new ArgumentException("El Apellido es obligatorio.");

            if (cliente.Id == 0)
            {
                var existente = _repository.ObtenerPorDni(cliente.Dni);
                if (existente != null) throw new ArgumentException("Ya existe un cliente con ese DNI.");
                _repository.Insertar(cliente);
            }
            else
            {
                _repository.Actualizar(cliente);
            }
        }

        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("ID no válido");
            if (id <= 1) throw new InvalidOperationException("No se permite eliminar al cliente base 'Consumidor Final'.");
            _repository.EliminarLogico(id);
        }

        public void RegistrarAbono(int idCliente, decimal monto, string metodoPago, int? idUsuario)
        {
            if (idCliente <= 1)
                throw new ArgumentException("No se puede registrar abono al cliente Consumidor Final.");

            if (monto <= 0)
                throw new ArgumentException("El monto a abonar debe ser mayor a $ 0.00.");

            var cliente = _repository.ObtenerPorId(idCliente);
            if (cliente == null || !cliente.Activo)
                throw new ArgumentException("El cliente seleccionado no existe o está inactivo.");

            if (cliente.SaldoDeudor <= 0)
                throw new InvalidOperationException($"El cliente '{cliente.NombreCompleto}' no posee deuda pendiente (Saldo actual: $ 0.00).");

            if (monto > cliente.SaldoDeudor)
                throw new InvalidOperationException(
                    $"El importe a abonar ($ {monto:N2}) supera la deuda total pendiente ($ {cliente.SaldoDeudor:N2}) de {cliente.NombreCompleto}.");

            _repository.RegistrarAbonoCuentaCorriente(idCliente, monto, metodoPago, idUsuario);
        }
    }
}
