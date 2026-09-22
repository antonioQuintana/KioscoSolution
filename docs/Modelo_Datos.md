# Modelo de Datos (Esquema Relacional)

La base de datos `KioscoDB` está diseñada para Microsoft SQL Server. A continuación se detalla la estructura y el diccionario de las tablas principales que rigen el sistema.

## 1. Tabla: `Usuarios`
Almacena el personal que opera el sistema y gestiona el control de acceso (Login) usando cifrado BCrypt.

| Columna | Tipo de Dato | Restricciones | Descripción |
|---------|-------------|---------------|-------------|
| **Id** | `INT` | `PK, IDENTITY(1,1)` | Identificador único del empleado. |
| **Nombre** | `NVARCHAR(50)` | `NOT NULL` | Nombre de pila. |
| **Apellido** | `NVARCHAR(50)` | `NOT NULL` | Apellidos paternos/maternos. |
| **Usuario** | `NVARCHAR(50)` | `NOT NULL, UNIQUE` | Nombre de usuario (username) para el login. |
| **Contrasena** | `NVARCHAR(255)`| `NOT NULL` | Hash BCrypt (Nunca texto plano). |
| **Rol** | `NVARCHAR(20)` | `NOT NULL` | Nivel de permiso: 'Admin', 'Supervisor', 'Vendedor'. |
| **FechaCreacion** | `DATETIME` | `DEFAULT GETDATE()` | Registro de alta en el sistema. |

---

## 2. Tabla: `Categorias`
Maestro de los rubros o familias a las que pertenecen los productos del kiosco.

| Columna | Tipo de Dato | Restricciones | Descripción |
|---------|-------------|---------------|-------------|
| **Id** | `INT` | `PK, IDENTITY(1,1)` | Identificador único de categoría. |
| **Nombre** | `NVARCHAR(100)`| `NOT NULL, UNIQUE` | Nombre del rubro (Ej: "Golosinas"). |
| **Descripcion** | `NVARCHAR(255)`| `NULL` | Detalle o notas adicionales sobre la categoría. |
| **Estado** | `BIT` | `DEFAULT 1` | `1` = Activa, `0` = Inactiva (Borrado Lógico). |

---

## 3. Tabla: `Productos`
Maestro del inventario. Todo lo que el kiosco tiene disponible para la venta.

| Columna | Tipo de Dato | Restricciones | Descripción |
|---------|-------------|---------------|-------------|
| **Id** | `INT` | `PK, IDENTITY(1,1)` | ID lógico interno (Opcional usar SKU primario). |
| **SKU** | `NVARCHAR(50)` | `NOT NULL, UNIQUE` | Código de Barras principal. |
| **Nombre** | `NVARCHAR(150)`| `NOT NULL` | Descripción que sale en el ticket de compra. |
| **IdCategoria** | `INT` | `FK -> Categorias(Id)`| Relación con el rubro del artículo. |
| **PrecioCosto** | `DECIMAL(10,2)`| `NOT NULL` | Valor de compra al proveedor (Sin IVA / Con IVA). |
| **PrecioVenta** | `DECIMAL(10,2)`| `NOT NULL` | Valor final que se cobra al cliente. |
| **StockActual** | `INT` | `DEFAULT 0` | Unidades físicas existentes en caja/depósito. |
| **StockMinimo** | `INT` | `DEFAULT 5` | Alerta visual cuando las existencias bajan de este número. |

---

## Próximas Tablas Proyectadas (Módulo de Ventas y Clientes)

*(Nota: Estas tablas aún no han sido materializadas en KioscoDB.sql, corresponden a la proyección lógica de los próximos módulos).*

### `Clientes`
- **Id** (`PK`)
- **DNI_Telefono** (`NVARCHAR`, `UNIQUE`) -> Usado para búsquedas en caja.
- **Nombre_RazonSocial** (`NVARCHAR`)
- **CuentaCorriente_Saldo** (`DECIMAL`) -> Para la funcionalidad de "Fiado".

### `Ventas` (Cabecera del Ticket)
- **Id_Comprobante** (`PK`)
- **IdUsuario** (`FK -> Usuarios`) -> Cajero que emitió la venta (fundamental para Historial y Arqueo).
- **IdCliente** (`FK -> Clientes, NULL`)
- **FechaHora** (`DATETIME`)
- **Total** (`DECIMAL`)
- **MedioDePago** (`NVARCHAR`: 'Efectivo', 'Tarjeta', 'QR', 'CtaCte')

### `Venta_Detalle` (Cuerpo del Ticket)
- **Id_Detalle** (`PK`)
- **Id_Comprobante** (`FK -> Ventas`)
- **Id_Producto** (`FK -> Productos`)
- **Cantidad** (`INT`)
- **PrecioUnitarioCobrado** (`DECIMAL`) -> Histórico, por si el PrecioVenta del producto cambia mañana.
- **Subtotal** (`DECIMAL`)
