# Manual de Usuario: Roles y Funcionalidades

El sistema KioscoApp opera bajo el principio de **Mínimo Privilegio**. Esto significa que los usuarios solo ven las herramientas estrictamente necesarias para llevar a cabo su trabajo, evitando así distracciones, errores operativos y fraudes.

La plataforma se divide en **3 Perfiles (Roles) principales**:

---

## 1. Vendedor (Cajero)
Es el rol de operación diaria frente al mostrador. Su interfaz es ultra simplificada para agilizar el tiempo de atención al público.
**No tiene acceso** a precios de costo, eliminación de productos, ni métricas del negocio.

**Funcionalidades:**
- 🛒 **Venta (POS):** Pantalla principal. Permite escanear códigos de barras (SKU), agregar al carrito, aplicar descuentos, solicitar autorización para anulaciones, registrar egresos (pago a proveedores de gaseosas), y cobrar en Efectivo, Tarjeta o QR.
- 👥 **Clientes:** Registro y gestión de clientes para "Cuentas Corrientes" (fiado) o para emitir comprobantes a nombre de un consumidor específico.
- 💵 **Cierre de Caja (Arqueo):** Herramienta que el cajero utiliza al finalizar su turno para declarar el conteo de billetes y compararlo con el total teórico del sistema (Cierre X / Z).
- ⏱️ **Historial de Ventas (Turno):** Grilla fija que le permite al vendedor consultar **únicamente** las transacciones que ha realizado en su turno actual, útil para reimpresión de tickets o reclamos rápidos.

---

## 2. Supervisor (Encargado)
Es el gerente de la sucursal. Controla el inventario, actualiza los precios de venta y audita a los vendedores. No tiene permisos sobre la infraestructura técnica (backups, redes).

**Funcionalidades:**
- 📦 **Stock y Productos:** Acceso total al maestro de artículos (ABM). Puede ver el precio de costo (rentabilidad), actualizar el precio de venta y establecer alarmas de stock mínimo. Próximamente: actualización masiva de precios vía Excel.
- 📁 **Categorías:** Agrupación lógica del inventario (ej: "Golosinas", "Bebidas Alcohólicas").
- 📈 **Estadísticas:** Dashboard gráfico para analizar qué días y qué categorías se venden más.
- 🔍 **Auditoría de Cajas:** Módulo de control para revisar todos los cierres de caja (Cierres Z) de los distintos cajeros, detectando faltantes o sobrantes y exportando los datos (CSV) al contador.

---

## 3. Administrador (IT / Dueño Sistema)
Perfil de uso exclusivo para tareas técnicas, mantenimiento y seguridad de la base de datos.
*(Nota: El Administrador no atiende el mostrador ni maneja el inventario diario).*

**Funcionalidades:**
- 👤 **Usuarios:** Creación, edición, suspensión y reseteo de contraseñas del personal del kiosco (Vendedores y Supervisores).
- ⚙️ **Configuración Avanzada:** Panel de control técnico. Contiene herramientas proyectadas para:
  - Generación de Copias de Seguridad (Backup de la Base de Datos).
  - Trazabilidad de Seguridad (Logs de quién borra o modifica datos).
  - Configuración de hardware (Impresoras térmicas, lectores seriales, etc).
