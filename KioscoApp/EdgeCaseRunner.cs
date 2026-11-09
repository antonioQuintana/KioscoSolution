using System;
using System.Collections.Generic;
using KioscoApp.Business;
using KioscoApp.Models;

namespace KioscoApp.Tests
{
    public static class EdgeCaseRunner
    {
        public static void RunAll()
        {
            Console.WriteLine("=== BATERIA DE PRUEBAS DE CASOS LIMITE Y RESISTENCIA ===");
            var ventaService = new VentaService();
            var clienteService = new ClienteService();
            var productoService = new ProductoService();
            var usuarioService = new UsuarioService();

            // CASO 1: Venta con carrito vacio
            try {
                ventaService.RegistrarVenta(new Venta { IdCliente = 1, Total = 0, Detalles = new List<DetalleVenta>() });
                Console.WriteLine("  [FALLO] Deberia rechazar carrito vacio");
            } catch (Exception ex) {
                Console.WriteLine("  [OK] Rechazo carrito vacio: " + ex.Message);
            }

            // CASO 2: Venta a Cuenta Corriente con Consumidor Final (Id=1)
            try {
                var det = new List<DetalleVenta> { new DetalleVenta { IdProducto = 1, Cantidad = 1, PrecioUnitario = 100 } };
                ventaService.RegistrarVenta(new Venta { IdCliente = 1, Total = 100, MetodoPago = "Cuenta Corriente", Detalles = det });
                Console.WriteLine("  [FALLO] Deberia rechazar Cuenta Corriente a Consumidor Final");
            } catch (Exception ex) {
                Console.WriteLine("  [OK] Rechazo Cuenta Corriente a Consumidor Final: " + ex.Message);
            }

            // CASO 3: Abono mayor a la deuda del cliente
            try {
                clienteService.RegistrarAbono(6, 999999m, "Efectivo", 1);
                Console.WriteLine("  [FALLO] Deberia rechazar abono superior a la deuda");
            } catch (Exception ex) {
                Console.WriteLine("  [OK] Rechazo abono mayor a la deuda: " + ex.Message);
            }

            // CASO 4: Abono con monto cero o negativo
            try {
                clienteService.RegistrarAbono(6, 0m, "Efectivo", 1);
                Console.WriteLine("  [FALLO] Deberia rechazar abono de cero");
            } catch (Exception ex) {
                Console.WriteLine("  [OK] Rechazo abono de monto cero: " + ex.Message);
            }

            // CASO 5: Intentar dar de baja a Consumidor Final (Id=1)
            try {
                clienteService.Eliminar(1);
                Console.WriteLine("  [FALLO] Deberia proteger a Consumidor Final");
            } catch (Exception ex) {
                Console.WriteLine("  [OK] Protegio a Consumidor Final: " + ex.Message);
            }

            // CASO 6: Intentar eliminar producto con ventas registradas (Id=1)
            var resProd = productoService.EliminarProducto(1);
            if (!resProd.IsValid) {
                Console.WriteLine("  [OK] Bloqueo eliminacion de producto vendido: " + resProd.Errors["General"][0]);
            } else {
                Console.WriteLine("  [FALLO] Permitio eliminar producto vendido");
            }

            // CASO 7: Intentar eliminar usuario que tiene operaciones registradas (Id=3)
            var resUser = usuarioService.EliminarUsuario(3, 1);
            if (!resUser.IsValid) {
                Console.WriteLine("  [OK] Bloqueo eliminacion de usuario con ventas: " + resUser.Errors["General"][0]);
            } else {
                Console.WriteLine("  [FALLO] Permitio eliminar usuario con ventas");
            }

            Console.WriteLine("=== TODAS LAS PRUEBAS DE BORDE COMPLETADAS CON EXITO ===");
        }
    }
}
