# Manual de Usuario: Roles y Funcionalidades

El sistema **KioscoApp** opera bajo el principio de **Mínimo Privilegio**. Cada usuario accede únicamente a las herramientas requeridas para su rol operativo, evitando errores, fraudes y sobrecarga visual.

---

## 1. 🛒 Rol Vendedor (Cajero / Mostrador)
Perfil enfocado en la atención al público y velocidad de cobro.

### Funcionalidades:
- **Venta (Punto de Venta / POS):**
  - **Escáner:** Búsqueda instantánea por SKU o código de barra.
  - **Multiplicador:** Permite ingresar cantidades antes del código mediante el asterisco (ej. `3*7790895000016`).
  - **Búsqueda predictiva:** Filtrado interactivo en tiempo real al escribir el nombre del artículo.
  - **Ventas Suspendidas (`F4`):** Permite pausar una compra para atender a otro cliente y reanudarla con un solo clic.
  - **Cobro en Efectivo (`F5`):** Abre la calculadora de vuelto con botones rápidos de billetes y validación de importe suficiente.
  - **Cobro Electrónico (`F6` / `F7`):** Integración para cobro con QR (Mercado Pago) o Tarjeta Débito/Crédito.
  - **Cuenta Corriente / Fiado (`F8`):** Carga el saldo deudor al cliente registrado seleccionado (previa validación de límite de crédito).
- **Clientes (`F10`):**
  - Búsqueda por DNI, Nombre o Teléfono.
  - Alta rápida de clientes y consulta de deuda pendiente.
  - Cobranza de cuenta corriente (`FrmAbonoCuentaCorriente`).
- **Cierre de Caja (Arqueo X):**
  - Declaración del conteo de billetes y monedas.
  - Cálculo instantáneo de sobrante o faltante respecto al efectivo esperado.
- **Historial de Ventas del Turno:**
  - Consulta y reimpresión de tickets emitidos en el turno activo.
  - Solicitud de anulación de ticket con reversión automática de stock y saldos.

---

## 2. 👔 Rol Supervisor (Encargado / Gerente)
Controla el abastecimiento, fija precios y audita la operatoria de los cajeros.

### Funcionalidades:
- **Catálogo de Productos (ABM):**
  - Alta, edición y fijación de costos, precios y márgenes de ganancia.
  - Importación y exportación masiva en formato Excel / CSV.
- **Categorías:**
  - Administración de familias de productos con control de dependencias.
- **Control de Stock y Alertas:**
  - Identificación visual de productos con stock crítico o agotado.
  - Ajuste manual de inventario físico con registro de auditoría.
  - **Inventario Valorizado:** Consulta del valor total del stock a costo y venta con proyección de ganancia.
  - **Auditoría de Stock:** Trazabilidad de todas las modificaciones manuales de existencias.
- **Auditoría de Cajas:**
  - **Historial de Cierres Z:** Detalle de cada turno cerrado con doble clic para desglose de medios de pago.
  - **Descuadres por Empleado:** Balance acumulado de faltantes y sobrantes por cajero.
  - **Auditoría de Retiros/Gastos:** Registro de salidas de dinero para pago a proveedores y servicios.
  - **Ventas Anuladas:** Supervisión de anulaciones con detalle del motivo y cajero interviniente.
- **Estadísticas Comerciales:**
  - Filtros por rango de fechas en 6 dashboards:
    1. Productos más vendidos por categoría.
    2. Ventas totales por día de la semana.
    3. Ventas por franja horaria.
    4. Facturación por categoría.
    5. Facturación por método de pago.
    6. Cantidad de tickets por método de pago.

---

## 3. ⚙️ Rol Administrador (IT / Seguridad / Dueño)
Responsable de la seguridad informática, usuarios y configuración global.

### Funcionalidades:
- **Gestión de Usuarios (ABM):**
  - Alta de operadores, asignación de roles (`admin`, `supervisor`, `vendedor`).
  - Validación estricta de unicidad de DNI, usuario y correo electrónico.
  - Modificación y reseteo seguro de credenciales con cifrado BCrypt.
- **Configuración del Sistema:**
  - Datos comerciales de la empresa (Razón Social, CUIT, Dirección, Teléfono).
  - Parámetros del comprobante / ticket y leyenda al pie.
- **Visor de Logs de Seguridad y Diagnóstico:**
  - Filtros por rango de fechas (Hoy, 7 días, Todos).
  - Filtros por módulo del sistema (`Seguridad`, `Stock`, `Caja`, `Ventas`, `Configuracion`).
  - Filtros por nivel (`INFO`, `WARN`, `ERROR`).
  - Contadores dinámicos de eventos y exportación al portapapeles.
- **Trazabilidad y Auditoría Global:**
  - Registro cronológico de todas las operaciones críticas realizadas en la base de datos.
