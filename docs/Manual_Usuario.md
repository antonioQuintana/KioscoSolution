# 📘 MANUAL DE USUARIO INTEGRAL — KioscoApp (.NET 8)

**Sistema de Punto de Venta (POS), Gestión de Inventario, Auditoría de Cajas y Administración Comercial**  
*Versión 2.0 — Incluye Sistema de Ayuda Contextual Interactiva (`?`)*

---

## 📑 Índice de Contenidos

1. [Introducción y Arquitectura de Roles (Mínimo Privilegio)](#1-introducción-y-arquitectura-de-roles-mínimo-privilegio)
2. [Sistema de Ayuda Contextual Interactiva (`?`)](#2-sistema-de-ayuda-contextual-interactiva-)
3. [Acceso al Sistema (`FrmLogin`) y Menú Principal (`FrmPrincipal`)](#3-acceso-al-sistema-frmlogin-y-menú-principal-frmprincipal)
4. [Módulo Operativo — Rol Vendedor](#4-módulo-operativo--rol-vendedor)
   - [4.1. Punto de Venta (`UcVenta`)](#41-punto-de-venta-ucventa)
   - [4.2. Ventana de Cobro en Efectivo y Vuelto (`FrmCobroEfectivo`)](#42-ventana-de-cobro-en-efectivo-y-vuelto-frmcobroefectivo)
   - [4.3. Buscador Avanzado de Productos (`FrmBuscarProductoModal`)](#43-buscador-avanzado-de-productos-frmbuscarproductomodal)
   - [4.4. Artículos Frecuentes sin Código (`FrmArticulosFrecuentesModal`)](#44-artículos-frecuentes-sin-código-frmarticulosfrecuentesmodal)
   - [4.5. Ingresos y Retiros Manuales de Caja (`FrmMovimientoCaja`)](#45-ingresos-y-retiros-manuales-de-caja-frmmovimientocaja)
   - [4.6. Gestión de Clientes y Cuentas Corrientes (`UcABMClientes`)](#46-gestión-de-clientes-y-cuentas-corrientes-ucabmclientes)
   - [4.7. Cobro de Deuda en Cuenta Corriente (`FrmAbonoCuentaCorriente`)](#47-cobro-de-deuda-en-cuenta-corriente-frmabonocuentacorriente)
   - [4.8. Apertura, Arqueo y Cierre de Caja Z (`UcCierreCaja`)](#48-apertura-arqueo-y-cierre-de-caja-z-uccierrecaja)
   - [4.9. Historial de Ventas y Reimpresión (`UcHistorialVentas` y `FrmDetalleVentaModal`)](#49-historial-de-ventas-y-reimpresión-uchistorialventas-y-frmdetalleventamodal)
5. [Módulo Gerencial — Rol Supervisor](#5-módulo-gerencial--rol-supervisor)
   - [5.1. Catálogo de Productos e Importación/Exportación Excel (`UcABMProductos`)](#51-catálogo-de-productos-e-importaciónexportación-excel-ucabmproductos)
   - [5.2. Gestión de Categorías y Rubros (`UcABMCategorias`)](#52-gestión-de-categorías-y-rubros-ucabmcategorias)
   - [5.3. Control y Ajustes de Stock (`UcMovimientosStock`)](#53-control-y-ajustes-de-stock-ucmovimientosstock)
   - [5.4. Reporte de Inventario Valorizado (`FrmInventarioValorizadoModal`)](#54-reporte-de-inventario-valorizado-frminventariovalorizadomodal)
   - [5.5. Auditoría de Ajustes de Stock (`FrmAuditoriaStockModal`)](#55-auditoría-de-ajustes-de-stock-frmauditoriastockmodal)
   - [5.6. Estadísticas Comerciales y Dashboards (`UcEstadisticas`)](#56-estadísticas-comerciales-y-dashboards-ucestadisticas)
   - [5.7. Auditoría y Control de Cajas (`UcAuditoriaCajas`)](#57-auditoría-y-control-de-cajas-ucauditoriacajas)
6. [Módulo de Seguridad y Sistema — Rol Administrador](#6-módulo-de-seguridad-y-sistema--rol-administrador)
   - [6.1. Gestión de Usuarios y Roles (`UcABMUsuarios`)](#61-gestión-de-usuarios-y-roles-ucabmusuarios)
   - [6.2. Configuración General, Backups y Restauración (`UcConfiguracion`)](#62-configuración-general-backups-y-restauración-ucconfiguracion)
   - [6.3. Datos Fiscales y Plantilla de Ticket (`FrmDatosEmpresaModal`)](#63-datos-fiscales-y-plantilla-de-ticket-frmdatosempresamodal)
   - [6.4. Configuración de Impresora Térmica (`FrmConfigurarImpresoraModal`)](#64-configuración-de-impresora-térmica-frmconfigurarimpresoramodal)
   - [6.5. Trazabilidad de Seguridad y Auditoría (`FrmTrazabilidadModal`)](#65-trazabilidad-de-seguridad-y-auditoría-frmtrazabilidadmodal)
   - [6.6. Visor de Registros de Diagnóstico y Logs (`FrmLogsModal`)](#66-visor-de-registros-de-diagnóstico-y-logs-frmlogsmodal)

---

## 1. Introducción y Arquitectura de Roles (Mínimo Privilegio)

**KioscoApp** está diseñado bajo el principio de **Mínimo Privilegio (Segregación de Funciones)**. Cada cuenta de usuario posee un único rol asignado que determina estrictamente qué módulos aparecen en la barra superior de navegación:

| Rol | Propósito Principal | Módulos Habilitados |
| :--- | :--- | :--- |
| **Vendedor** | Operación diaria de mostrador, cobros y arqueo de turno | Punto de Venta, Clientes y Cta. Cte., Cierre de Caja, Historial de Ventas |
| **Supervisor** | Administración comercial, inventario, precios y auditoría contable | Productos, Categorías, Control de Stock, Estadísticas, Auditoría de Cajas |
| **Administrador** | Seguridad informática, cuentas de usuario, periféricos y base de datos | Gestión de Usuarios, Configuración del Sistema (Backups, Impresora, Datos Empresa, Logs, Trazabilidad) |

### Credenciales Iniciales por Defecto
- **Administrador:** Usuario `admin` / Contraseña `admin123`
- **Supervisor:** Usuario `supervisor` / Contraseña `super123`
- **Vendedor:** Usuario `vendedor` / Contraseña `venta123`

---

## 2. Sistema de Ayuda Contextual Interactiva (`?`)

Todas las pantallas, paneles, tablas y botones de acción del sistema incorporan el **signo de pregunta interactivo (`?`)** junto al elemento o en el encabezado de cada sección:

1. **Al pasar el cursor del ratón por encima (Hover):**
   - El ícono circular `?` cambia de color **azul (`#007ACC`)** a **dorado (`#F1C40F`)** y el puntero se transforma en el cursor de ayuda.
   - Se despliega de forma instantánea un **cartel emergente (ToolTip)** con título informativo (`ℹ️ Guía de Uso`) explicando qué hace esa pantalla, qué datos deben ingresarse en el campo o qué acción ejecuta el botón.
   - También puede pasar el ratón directamente sobre el botón o cuadro de texto acompañado por el signo `?` para visualizar el mismo cartel.
2. **Al hacer clic sobre el ícono (`?`):**
   - Si desea leer la explicación sin mantener el ratón quieto, al hacer clic izquierdo sobre cualquier signo `?` se abrirá una ventana fija con el texto completo de ayuda.

---

## 3. Acceso al Sistema (`FrmLogin`) y Menú Principal (`FrmPrincipal`)

### 3.1. Pantalla de Inicio de Sesión (`FrmLogin`)
1. **Usuario:** Ingrese su nombre de usuario asignado (no distingue mayúsculas de minúsculas).
2. **Contraseña:** Ingrese su clave secreta. Puede pulsar `Enter` en el campo de usuario para saltar a la contraseña, y `Enter` nuevamente para iniciar sesión.
3. **Ver contraseña:** Marque o desmarque esta casilla para mostrar u ocultar los caracteres escritos y verificar que no haya errores de tipeo.
4. **Botón Ingresar:** Valida las credenciales y carga el entorno correspondiente a su rol.

### 3.2. Ventana Principal (`FrmPrincipal`)
- **Barra de Navegación Superior:** Muestra únicamente los botones de los módulos permitidos para su rol. El módulo activo se resalta con una barra inferior celeste.
- **Indicador de Usuario Activo (derecha):** Muestra en todo momento el nombre y rol del operador conectado.
- **Botón `Ayuda (F1)`:** Abre este Manual de Usuario desde cualquier pantalla del sistema, o pulsando la tecla `F1` (salvo cuando está dentro del Punto de Venta, donde `F1` posiciona el foco en el lector de códigos).
- **Botón `Cerrar Sesión`:** Cierra el turno actual de pantalla y regresa de forma segura a `FrmLogin`.

---

## 4. Módulo Operativo — Rol Vendedor

### 4.1. Punto de Venta (`UcVenta`)
Es la pantalla central de facturación rápida diseñada para operar 100% con teclado o con ratón.

#### A. Carga de Productos al Carrito
1. **Lector de Código de Barras / SKU (`F1`):** Con el cursor en el cuadro *"Código de Producto"*, escanee el código de barras o digite el código SKU y presione `Enter`.
2. **Multiplicador Rápido de Cantidad (`*`):** Para cargar varias unidades de un mismo artículo en un solo paso, escriba `CANTIDAD*CODIGO` (por ejemplo: `3*779123456`) y presione `Enter`, o ajuste el campo numérico **Cantidad** antes de escanear.
3. **Buscador por Nombre (`F10`):** Si el artículo no tiene etiqueta legible, pulse `F10` o el botón **Buscar Producto** para buscarlo por descripción o categoría.
4. **Artículos Frecuentes (`F7`):** Abre una grilla de botones rápidos con los productos de mostrador más vendidos.

#### B. Manejo del Carrito de Compras y Ventas en Espera
- **Modificar Cantidades (`+` / `-`):** Seleccione una fila de la grilla y presione las teclas `+` o `-` del teclado para sumar o restar unidades (el sistema valida automáticamente el stock disponible).
- **Quitar Producto (`Supr`):** Seleccione el ítem y presione `Delete/Supr` o el botón **Quitar Producto**.
- **Cancelar Venta (`Esc`):** Vacía por completo el carrito actual tras confirmar.
- **Pestañas de Ventas Simultáneas (`Venta 1 (F8)`, `Venta 2 (F9)`, `+`):** Permiten dejar en espera la compra de un cliente, abrir una segunda o tercera pestaña para cobrarle a otro cliente de inmediato, y luego regresar a la primera pestaña sin perder ningún artículo cargado.

#### C. Selección de Cliente, Descuento y Medio de Pago
1. **Cliente:** Por defecto se selecciona *"Consumidor Final"*. Si el cliente comprará al fiado (**Cuenta Corriente**), selecciónelo en el desplegable o busque su DNI/Nombre; el sistema mostrará su saldo deudor y su límite de crédito disponible.
2. **Descuento Manual (`%` o `$`):** Permite aplicar una bonificación porcentual (con botón rápido **10%**) o un monto fijo en pesos sobre el subtotal. El total se recalcula en vivo.
3. **Método de Pago:**
   - **Efectivo:** Al confirmar con `F5`, abre la calculadora de billetes y vuelto (`FrmCobroEfectivo`).
   - **Transferencia / MercadoPago / QR / Tarjeta Débito / Crédito (`F6` para alternar):** Registra el cobro electrónico directo.
   - **Cuenta Corriente (Fiado):** Solo habilitado si seleccionó un cliente registrado y el monto no supera su límite de crédito.
4. **Finalizar Venta (`F5`):** Registra la venta, descuenta el stock de forma transaccional, impacta en la caja abierta e imprime el ticket si la impresión automática está activa.

#### D. Tabla Resumen de Atajos de Teclado del POS (`UcVenta`)
| Tecla | Acción en el Punto de Venta |
| :---: | :--- |
| `F1` | Enfocar el campo de lectura de Código de Barras / SKU |
| `F2` | Abrir ventana de **Ingreso de Dinero / Cambio** a Caja |
| `F3` | Abrir ventana de **Retiro / Gasto / Pago a Proveedor** de Caja |
| `F5` | **Finalizar y Cobrar** la venta actual |
| `F6` | Alternar rápidamente entre los **Métodos de Pago** |
| `F7` | Abrir panel de **Artículos Rápidos / Frecuentes** |
| `F8` / `F9` | Cambiar entre **Venta 1** y **Venta 2** (Ventas en Espera) |
| `F10` | Abrir el **Buscador Avanzado de Productos** por nombre |
| `+` / `-` | Aumentar o disminuir la cantidad del producto seleccionado en el carrito |
| `Supr` | Eliminar del carrito el producto seleccionado |
| `Esc` | Cancelar y vaciar el carrito actual |

---

### 4.2. Ventana de Cobro en Efectivo y Vuelto (`FrmCobroEfectivo`)
Se abre automáticamente al finalizar (`F5`) una venta con método de pago **Efectivo**:
1. **Total a Pagar:** Muestra en grande el importe final del ticket.
2. **Monto Recibido (`Abona con`):** Escriba el monto que entrega el cliente o utilice los **botones de billetes rápidos** (`Pago Exacto`, `$1.000`, `$2.000`, `$5.000`, `$10.000`, `$20.000`).
3. **Cálculo de Vuelto en Vivo:** Si el monto recibido cubre el total, muestra en verde el **Vuelto a Entregar**; si falta dinero, indica en rojo cuánto resta cubrir y bloquea la confirmación.
4. **Confirmar (`Enter`):** Cierra la operación y emite el comprobante. Puede cancelar y volver al carrito con `Esc`.

---

### 4.3. Buscador Avanzado de Productos (`FrmBuscarProductoModal`)
1. **Filtro en Tiempo Real:** Escriba cualquier parte del nombre o código SKU y filtre opcionalmente por **Categoría**.
2. **Semáforo Visual de Stock:**
   - **Gris tachado:** Sin stock (`0` unidades).
   - **Amarillo:** Stock en nivel crítico (menor o igual al mínimo).
   - **Blanco/Normal:** Stock saludable disponible.
3. **Selección Rápida:** Use las flechas `Arriba`/`Abajo` para desplazarse desde el mismo cuadro de búsqueda, ajuste la **Cantidad a agregar** y presione `Enter` (o doble clic) para enviarlo al carrito.

---

### 4.4. Artículos Frecuentes sin Código (`FrmArticulosFrecuentesModal`)
- Presenta tarjetas táctiles grandes con los productos de mayor rotación del kiosco (con precio y stock disponible).
- Ajuste la **Cantidad** en la parte superior y haga clic sobre el botón del producto deseado para sumarlo inmediatamente al carrito.

---

### 4.5. Ingresos y Retiros Manuales de Caja (`FrmMovimientoCaja`)
Se accede desde el POS con `F2` (Ingreso) o `F3` (Retiro/Egreso):
1. **Monto (`$`):** Importe en efectivo que ingresa o sale del cajón físico.
2. **Motivo / Concepto:** Escriba el detalle o elija una plantilla rápida (ej. *"Refuerzo de cambio / sencillo"*, *"Pago a proveedor en efectivo"*, *"Retiro parcial de seguridad"*).
3. **Registrar Movimiento (`Enter`):** Guarda el movimiento con fecha, hora y usuario para que se compute automáticamente en el Arqueo de Caja Z.

---

### 4.6. Gestión de Clientes y Cuentas Corrientes (`UcABMClientes`)
Permite administrar la cartera de clientes del comercio y sus saldos de cuenta corriente:
1. **Buscador y Filtros Rápidos:** Busque por DNI, nombre o apellido. Use el filtro **"Con Deuda"** para listar únicamente los clientes morosos.
2. **Alta / Edición de Cliente:** Complete **DNI** (único, de 7 a 10 dígitos), **Nombre**, **Apellido**, **Teléfono**, **Email**, **Dirección** y el **Límite de Crédito ($)** máximo autorizado para compras al fiado. Haga clic en **Guardar**.
3. **Cobrar Deuda (`$ Cobrar Cta. Cte.`):** Seleccione un cliente con saldo pendiente en la tabla y pulse este botón para registrar una entrega de dinero parcial o total.
4. **Eliminar / Limpiar:** Permite dar de baja clientes sin deuda activa o limpiar el formulario para una nueva carga.

---

### 4.7. Cobro de Deuda en Cuenta Corriente (`FrmAbonoCuentaCorriente`)
1. Muestra el nombre del cliente y su **Deuda Actual** en rojo.
2. **Monto a Abonar:** Ingrese la suma que entrega el cliente o pulse los botones rápidos (**Saldar 100%**, **50%**, **$5.000**, **$10.000**).
3. **Medio de Pago del Abono:** Indique si el cliente abona su deuda en *Efectivo* (suma al efectivo físico de la caja actual), *Transferencia*, *MercadoPago* o *Tarjeta*.
4. **Confirmar Cobro (`Enter`):** Descuenta inmediatamente el saldo deudor del cliente e impacta el ingreso en la caja del turno.

---

### 4.8. Apertura, Arqueo y Cierre de Caja Z (`UcCierreCaja`)
Gestiona el ciclo completo del dinero de cada turno de trabajo:
1. **Apertura de Caja:** Si no hay una caja abierta, ingrese el **Monto Inicial (Fondo de Cambio)** con el que recibe el cajón y pulse **Abrir Caja**.
2. **Panel de Resumen del Turno:** Mientras la caja está abierta, muestra en tiempo real:
   - Fondo Inicial, Ventas en Efectivo, Ventas por Transferencia/Tarjeta/QR, Ventas en Cuenta Corriente, Cobros de Cta. Cte., Ingresos Manuales y Retiros/Gastos.
   - **Efectivo Esperado en Cajón:** Fórmula exacta (`Monto Inicial + Ventas Efectivo + Cobros Cta. Cte. Efectivo + Ingresos Manuales - Retiros`).
3. **Calculadora de Arqueo Físico:** Cuente los billetes y monedas del cajón e ingrese el total en **Efectivo Real Contado**. El sistema calculará en vivo la **Diferencia de Arqueo** (*Caja Exacta*, *Sobrante* en azul o *Faltante* en rojo).
4. **Cerrar Caja (Cierre Z):** Registra el cierre definitivo del turno y genera el reporte de auditoría.

---

### 4.9. Historial de Ventas y Reimpresión (`UcHistorialVentas` y `FrmDetalleVentaModal`)
1. **Búsqueda de Tickets:** Filtre las ventas por rango de fechas (**Desde / Hasta**), **Método de Pago**, **Estado** (*Completada* o *Anulada*) o busque por N° de Ticket / Cliente.
2. **Ver Detalle y Reimprimir (`FrmDetalleVentaModal`):** Seleccione una venta y pulse **Ver Detalle** (o haga doble clic) para visualizar todos los renglones vendidos y utilizar el botón **📋 Copiar / Imprimir Ticket**.
3. **Anular Venta:** Si se cometió un error en una venta activa del turno, selecciónela y pulse **Anular Venta**. El sistema solicita el motivo obligatorio de anulación, **restituye automáticamente el stock** de todos los artículos involucrados y descuenta el importe de la recaudación de caja.

---

## 5. Módulo Gerencial — Rol Supervisor

### 5.1. Catálogo de Productos e Importación/Exportación Excel (`UcABMProductos`)
1. **Alta y Edición de Artículos:**
   - **Código de Barras / SKU:** Identificador único del producto.
   - **Nombre y Descripción:** Denominación comercial que aparecerá en pantalla y en el ticket.
   - **Categoría:** Rubro al que pertenece el artículo.
   - **Precio de Costo y Precio de Venta:** El sistema calcula y muestra automáticamente el **Margen de Ganancia (%)** para evitar errores de marcación.
   - **Stock Actual y Stock Mínimo:** Definen las existencias actuales y el umbral de alerta de reposición.
2. **Filtros de Inventario:** Busque por texto libre, filtre por categoría o marque **"Solo Bajo Stock"** para identificar qué mercadería debe pedir a los proveedores.
3. **Exportar e Importar Excel (`.xlsx` / `.csv`):**
   - **Exportar:** Descarga todo el catálogo actual a una planilla Excel para actualizar precios o analizar costos.
   - **Importar:** Permite cargar o actualizar cientos de productos y precios de forma masiva desde una planilla externa.

---

### 5.2. Gestión de Categorías y Rubros (`UcABMCategorias`)
- Permite crear, renombrar o dar de baja las familias de productos (*Bebidas*, *Golosinas*, *Cigarrillos*, *Almacén*, *Lácteos*, etc.).
- El sistema protege la integridad referencial impidiendo eliminar categorías que tengan productos asociados.

---

### 5.3. Control y Ajustes de Stock (`UcMovimientosStock`)
Pantalla especializada para registrar la recepción de pedidos de proveedores o bajas por roturas/vencimientos:
1. **Selección de Producto:** Busque el artículo en la grilla superior para ver su stock actual y su stock mínimo.
2. **Tipo de Movimiento:**
   - **Entrada (+):** Suma unidades al inventario (ej. llegada de mercadería de proveedor).
   - **Salida / Merma (-):** Descuenta unidades (ej. producto vencido, envase roto, consumo interno).
3. **Cantidad y Motivo Obligatorio:** Indique las unidades y explique la razón del ajuste. Todo movimiento queda grabado de forma inmutable con usuario, fecha y hora.
4. **Accesos Rápidos de Auditoría:** Desde esta misma pantalla puede abrir el **💰 Inventario Valorizado** y la **📋 Auditoría de Stock**.

---

### 5.4. Reporte de Inventario Valorizado (`FrmInventarioValorizadoModal`)
- Calcula al instante el valor financiero de toda la mercadería almacenada en el local:
  - **Costo Invertido:** $\sum (\text{Stock Actual} \times \text{Precio de Costo})$
  - **Proyección de Venta:** $\sum (\text{Stock Actual} \times \text{Precio de Venta})$
  - **Ganancia Potencial Estimada:** $\text{Proyección de Venta} - \text{Costo Invertido}$
- Permite filtrar por categoría o texto y exportar el cuadro financiero a Excel (**📥 Exportar CSV**).

---

### 5.5. Auditoría de Ajustes de Stock (`FrmAuditoriaStockModal`)
- Muestra la bitácora histórica de todas las modificaciones manuales de inventario realizadas desde `UcMovimientosStock` o `UcABMProductos`.
- Permite filtrar por rango de fechas y buscar por operador, producto o motivo del ajuste, con exportación directa a CSV.

---

### 5.6. Estadísticas Comerciales y Dashboards (`UcEstadisticas`)
Contiene **6 paneles analíticos independientes**, cada uno con su propio filtro de fechas (**Desde / Hasta**):
1. **Productos Más Vendidos:** Ranking de los artículos líderes en unidades despachadas.
2. **Recaudación por Categoría:** Gráfico comparativo del peso de cada rubro en la facturación.
3. **Evolución de Ventas por Período:** Curva temporal de ingresos diarios o mensuales.
4. **Distribución por Métodos de Pago:** Proporción cobrada en Efectivo, Transferencia, MercadoPago, Tarjetas y Cuenta Corriente.
5. **Rendimiento por Vendedor / Cajero:** Comparativa de tickets emitidos y monto facturado por cada operador.
6. **Horarios Pico de Mayor Demanda:** Análisis por franja horaria para optimizar turnos de personal.

---

### 5.7. Auditoría y Control de Cajas (`UcAuditoriaCajas`)
Centro de control financiero para supervisar todos los turnos de caja del negocio, organizado en **5 pestañas especializadas**:
1. **Corte X (Caja en Vivo):** Monitorea en tiempo real la caja que se encuentra abierta en ese instante sin necesidad de cerrarla.
2. **Historial de Cierres Z:** Lista todos los turnos cerrados con su monto inicial, recaudación esperada, efectivo declarado por el cajero y diferencia de arqueo.
3. **Auditoría de Descuadres:** Filtra automáticamente los cierres que registraron **Faltantes** o **Sobrantes** de dinero para detectar desvíos.
4. **Retiros y Gastos de Caja:** Detalla todas las salidas manuales de efectivo (`F3`) e ingresos (`F2`) con su concepto y responsable.
5. **Auditoría de Ventas Anuladas:** Lista todos los tickets dados de baja, quién los anuló y por qué motivo.
- Incluye botón **Exportar Reporte CSV** para descargar cualquier pestaña a Excel.

---

## 6. Módulo de Seguridad y Sistema — Rol Administrador

### 6.1. Gestión de Usuarios y Roles (`UcABMUsuarios`)
1. **Listado y Búsqueda de Personal:** Visualice todas las cuentas registradas con su DNI, Nombre, Rol y Estado (*Activo* / *Inactivo*).
2. **Alta y Modificación de Cuentas:**
   - Complete **DNI**, **Nombre**, **Apellido**, **Email**, **Dirección** (**Calle** y **Número**), **Nombre de Usuario**, **Contraseña** y seleccione el **Rol** (`vendedor`, `supervisor` o `admin`).
   - Las contraseñas se almacenan protegidas mediante hash criptográfico **SHA-256**.
3. **Activar / Desactivar Usuarios:** Si un empleado deja de trabajar o cambia de puesto, puede desactivar su cuenta desmarcando la casilla **Usuario Activo** sin perder el historial contable de sus ventas pasadas.

---

### 6.2. Configuración General, Backups y Restauración (`UcConfiguracion`)
1. **Apariencia Visual (Temas):** Cambie en tiempo real entre **Modo Oscuro Profesional (Dark)** y **Modo Claro Clásico (Light)**.
2. **Copia de Seguridad de Base de Datos (`Generar Backup .BAK`):**
   - Ejecuta un respaldo completo en caliente de la base de datos SQL Server (`KioscoDB`) en la carpeta que usted elija. Se recomienda realizar backups periódicos en un pendrive o disco externo.
3. **Restauración del Sistema (`Restaurar Base de Datos`):**
   - Permite recuperar el sistema completo a partir de un archivo `.bak` previo en caso de migración de PC o contingencia.
4. **Accesos a Herramientas de Administración:** Botones directos a **Datos de la Empresa**, **Configurar Impresora**, **Trazabilidad de Auditoría** y **Visor de Logs**.

---

### 6.3. Datos Fiscales y Plantilla de Ticket (`FrmDatosEmpresaModal`)
- Permite personalizar el **Nombre de Fantasía** (título principal del comprobante), **Razón Social**, **CUIT**, **Dirección del Local**, **Teléfono / WhatsApp** y el **Mensaje de Pie de Ticket**.
- Cuenta con un panel derecho de **Vista Previa en Tiempo Real** que dibuja exactamente cómo saldrá impreso el ticket térmico de 40 columnas mientras usted escribe.

---

### 6.4. Configuración de Impresora Térmica (`FrmConfigurarImpresoraModal`)
1. **Selección de Impresora:** Elija su comandera térmica instalada en Windows (USB, Serial o Red).
2. **Ancho de Papel:** Seleccione entre rollo estándar de **80 mm** (40-48 columnas) o rollo compacto de **58 mm** (32 columnas).
3. **Automatización:** Active o desactive la **Impresión automática al cobrar con `[F5]`** y el **Corte automático de papel**.
4. **🖨️ Imprimir Ticket de Prueba:** Envía una página de diagnóstico para verificar la conexión física antes de guardar.

---

### 6.5. Trazabilidad de Seguridad y Auditoría (`FrmTrazabilidadModal`)
- Bitácora forense centralizada que registra todas las operaciones críticas del sistema (inicios de sesión exitosos y fallidos, creación/baja de usuarios, backups, restauraciones, cierres de caja, anulaciones de ventas y cambios de configuración).
- Permite filtrar por rango de fechas, módulo (`Seguridad`, `BaseDeDatos`, `Configuracion`, `Caja`, `Clientes`, `Stock`, `Ventas`) y texto libre, con exportación a **CSV**.

---

### 6.6. Visor de Registros de Diagnóstico y Logs (`FrmLogsModal`)
- Herramienta técnica para inspeccionar el archivo de registro interno de la aplicación:
  - Filtre eventos por **Fecha**, **Nivel de Severidad** (`ERROR`, `WARN`, `INFO`), **Módulo** o palabra clave.
  - Visualice contadores en tiempo real de errores, advertencias y eventos informativos.
  - Utilice los botones **📋 Copiar Logs al Portapapeles**, **📂 Abrir en Bloc de Notas** o **🗑️ Limpiar Archivo de Logs**.
