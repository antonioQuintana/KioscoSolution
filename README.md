# Sistema de Gestión para Kiosco (C# WinForms)

Un sistema de Punto de Venta (POS) y gestión integral para kioscos y minimercados, desarrollado en **C# (.NET 8)** con **Windows Forms** y **Microsoft SQL Server**.

---

## 🚀 Arquitectura y Organización del Código

El sistema implementa una **Arquitectura en Capas (N-Tier)** con separación estricta de responsabilidades:

```text
KioscoSolution/
├── docs/                        # Documentación técnica, manual de usuario y modelo de datos
│   ├── Guia_Stored_Procedures.md
│   ├── Manual_Usuario.md
│   └── Modelo_Datos.md
├── scripts/                     # Scripts de base de datos y lotes de datos
│   ├── seed_data.sql            # Lote de categorías, productos, usuarios y clientes
│   └── seed_cierres_ventas.sql  # Lote de ventas, detalles, cierres Z y movimientos
└── KioscoApp/
    ├── Business/                # Capa de Lógica de Negocio y Validaciones
    │   ├── AuditoriaService.cs
    │   ├── CajaService.cs
    │   ├── CategoriaService.cs
    │   ├── ClienteService.cs
    │   ├── ConfiguracionService.cs
    │   ├── ProductoService.cs
    │   ├── UbicacionService.cs
    │   ├── UsuarioService.cs
    │   └── VentaService.cs
    ├── Data/                    # Capa de Acceso a Datos (ADO.NET / Stored Procedures)
    │   ├── DatabaseHelper.cs
    │   ├── AuditoriaRepository.cs
    │   ├── CajaRepository.cs
    │   ├── CategoriaRepository.cs
    │   ├── ClienteRepository.cs
    │   ├── ConfiguracionRepository.cs
    │   ├── EstadisticasRepository.cs
    │   ├── ProductoRepository.cs
    │   ├── UbicacionRepository.cs
    │   ├── UsuarioRepository.cs
    │   └── VentaRepository.cs
    ├── Models/                  # Entidades de Dominio y DTOs
    ├── Views/                   # Formularios (Forms) y Controles de Usuario (UserControls)
    ├── Utils/                   # Utilidades transversales (UserSession, Logger, EdgeCaseRunner)
    ├── Scripts/                 # Esquema DDL inicial y Stored Procedures
    └── Program.cs               # Punto de entrada de la aplicación
```

---

## 👥 Perfiles y Control de Acceso por Roles (RBAC)

La aplicación cuenta con control de acceso estricto según el rol del usuario autenticado:

### 1. 🛒 Rol Vendedor
- **Punto de Venta (POS):** Escaneo de código de barras (SKU), multiplicador (`cant*código`), búsqueda predictiva de artículos, calculadora de vuelto en efectivo y cobro por QR, Tarjeta o Cuenta Corriente.
- **Ventas Suspendidas (`F4`):** Permite pausar una venta en espera para atender a otro cliente y reanudarla sin perder ítems.
- **Clientes y Cuenta Corriente:** Consulta y registro de clientes, límite de crédito y cobro de deuda pendiente (`FrmAbonoCuentaCorriente`).
- **Cierre de Caja (Arqueo X/Z):** Declaración física de billetes y monedas con cálculo automático de diferencias (sobrante/faltante).
- **Historial de Ventas:** Consulta y reimpresión de tickets del turno actual, con solicitud de anulación transaccional.

### 2. 👔 Rol Supervisor
- **Gestión de Catálogo:** Maestro de Productos (ABM) con control de costos, precios, margen y categorías.
- **Importación / Exportación Masiva:** Carga y actualización de precios mediante planillas Excel / CSV.
- **Control de Stock:** Alertas de stock crítico y agotado, ajuste manual con auditoría obligatoria.
- **Reporte de Inventario Valorizado:** Valuación global de mercadería a precio de costo vs. venta y ganancia potencial.
- **Auditoría de Cajas:** Consulta de Cierres Z históricos, reporte de descuadres por empleado, trazabilidad de egresos/retiros de caja y ventas anuladas.
- **Estadísticas Gráficas:** Dashboards interactivos con filtros de fechas (Top productos, ventas por día, franja horaria, categorías y medios de pago).

### 3. ⚙️ Rol Administrador
- **Gestión de Personal:** ABM de Usuarios, asignación de roles, reinicio de credenciales y validación de unicidad de DNI/Email/Usuario.
- **Datos de Empresa y Ticket:** Configuración de razón social, CUIT, mensaje de pie de comprobante y parámetros de impresión.
- **Visor de Logs del Sistema:** Monitoreo en tiempo real de registros operativos con filtros por nivel (`INFO`, `WARN`, `ERROR`), módulo y rango de fechas.
- **Trazabilidad y Auditoría:** Registro inmutable de eventos críticos (altas, modificaciones, eliminaciones y cierres de sesión).

---

## 🔐 Usuarios y Credenciales de Prueba

Todos los usuarios del lote de datos representativo poseen la contraseña: **`1234`** *(cifrada con BCrypt)*.

| Usuario | Nombre y Apellido | Rol Asignado |
| :--- | :--- | :--- |
| `admin` | Juan Pérez | **Administrador** |
| `mgomez` | María Gómez | **Supervisor** |
| `clopez` | Carlos López | **Supervisor** |
| `aquintana` | Antonio Quintana | **Vendedor** |
| `rnunez` | Ramiro Núñez | **Vendedor** |
| `lsanchez` | Laura Sánchez | **Vendedor** |
| `mfernandez` | Martín Fernández | **Vendedor** |
| `vrojas` | Valentina Rojas | **Vendedor** |

---

## 🛠️ Requisitos e Instalación

### Requisitos Previos
- **.NET 8.0 SDK** (o superior).
- **Microsoft SQL Server** (LocalDB o Express Edition) en `.\SQLEXPRESS`.
- Windows 10/11 (arquitectura WinForms).

### Configuración de la Base de Datos
1. Ejecutar el script base de esquema `KioscoApp/Scripts/KioscoDB.sql` y `KioscoApp/Scripts/StoredProcedures.sql`.
2. Para cargar el lote completo de datos de prueba (48 productos, 11 categorías, cierres de caja y ventas de los últimos 30 días), ejecutar:
   ```powershell
   sqlcmd -S ".\SQLEXPRESS" -d KioscoDB -E -i "scripts\seed_data.sql" -C
   sqlcmd -S ".\SQLEXPRESS" -d KioscoDB -E -i "scripts\seed_cierres_ventas.sql" -C
   ```

### Compilación y Ejecución
```bash
dotnet build
dotnet run --project KioscoApp
```

### Ejecución de Pruebas de Estrés y QA Automatizado
El sistema incluye un arnés de pruebas integrado para validar reglas de negocio y restricciones de integridad:
```powershell
& ".\KioscoApp\bin\Debug\net8.0-windows\KioscoApp.exe" --test-qa
```
Valida de forma autónoma:
1. Rechazo de venta con carrito vacío.
2. Bloqueo de cuenta corriente para Consumidor Final.
3. Rechazo de abonos superiores a la deuda del cliente.
4. Rechazo de abonos con monto cero o negativo.
5. Protección contra borrado del cliente base Consumidor Final.
6. Protección contra borrado de productos con historial de ventas (control FK).
7. Protección contra borrado de usuarios con operaciones registradas (control FK).
