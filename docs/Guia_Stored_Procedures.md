# Guía Paso a Paso: Implementación de Procedimientos Almacenados (Stored Procedures) en KioscoApp

Esta guía detalla cómo migrar las consultas SQL en texto plano (actualmente dentro de tus clases `Repository`) hacia **Procedimientos Almacenados (Stored Procedures)** en SQL Server, manteniendo la arquitectura en capas de **KioscoApp** (`Views` -> `Business` -> `Data` -> `SQL Server`).

---

## ¿Por qué usar Procedimientos Almacenados?

1. **Seguridad:** Previene inyecciones SQL de forma nativa y permite restringir permisos en la base de datos.
2. **Rendimiento:** SQL Server compila y guarda en caché el plan de ejecución del procedimiento en la primera llamada.
3. **Mantenibilidad:** Si necesitás ajustar una consulta (por ejemplo, cambiar un cálculo en un reporte de `EstadisticasRepository`), podés hacerlo en SQL Server sin necesidad de recompilar la aplicación C#.
4. **Código más limpio:** Los archivos de la carpeta `Data/` quedan libres de bloques largos de cadenas SQL.

---

## Resumen del Flujo de Implementación

```
[1. SQL Server (SSMS)] ──> Crear el Stored Procedure (CREATE PROCEDURE ...)
         │
         ▼
[2. Capa Data (C#)]    ──> Cambiar el query por el nombre del SP y agregar:
                           cmd.CommandType = CommandType.StoredProcedure;
         │
         ▼
[3. Capa Business/UI]  ──> ¡No requiere cambios! Sigue llamando al mismo método del Repository.
```

---

## Paso 1: Crear los Procedimientos Almacenados en SQL Server

Debés ejecutar estos bloques en **SQL Server Management Studio (SSMS)** sobre tu base de datos `KioscoDB` (también te recomendamos guardarlos en un archivo nuevo como `KioscoApp/Scripts/StoredProcedures.sql`).

### Caso A: Consultas de Lectura (`SELECT` sin y con parámetros)
Tomaremos como ejemplo `ProductoRepository.cs`.

```sql
USE KioscoDB;
GO

-- 1. Obtener todos los productos
CREATE OR ALTER PROCEDURE sp_Productos_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria,
           c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta,
           p.StockActual, p.StockMinimo
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
    ORDER BY p.Nombre;
END
GO

-- 2. Obtener un producto por su SKU
CREATE OR ALTER PROCEDURE sp_Productos_ObtenerPorSKU
    @SKU VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria,
           c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta,
           p.StockActual, p.StockMinimo
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
    WHERE p.SKU = @SKU;
END
GO
```

### Caso B: Operaciones de Escritura (`INSERT`, `UPDATE`, `DELETE`)

```sql
USE KioscoDB;
GO

-- 3. Insertar un nuevo producto
CREATE OR ALTER PROCEDURE sp_Productos_Insertar
    @SKU VARCHAR(50),
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(255) = NULL,
    @IdCategoria INT,
    @PrecioCosto DECIMAL(18,2),
    @PrecioVenta DECIMAL(18,2),
    @StockActual INT,
    @StockMinimo INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Productos
        (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES
        (@SKU, @Nombre, @Descripcion, @IdCategoria, @PrecioCosto, @PrecioVenta, @StockActual, @StockMinimo);
END
GO
```

### Caso C: Devolver un ID generado (`OUTPUT` / `SCOPE_IDENTITY`)
En `VentaRepository.cs`, al insertar una cabecera de venta necesitás recuperar el `Id` generado para luego insertar los ítems en `DetallesVenta`.

```sql
USE KioscoDB;
GO

-- 4. Insertar cabecera de Venta y retornar el ID creado
CREATE OR ALTER PROCEDURE sp_Ventas_InsertarCabecera
    @IdCliente INT,
    @IdUsuario INT = NULL,
    @Total DECIMAL(18,2),
    @Descuento DECIMAL(18,2),
    @MetodoPago NVARCHAR(50),
    @Estado NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
    VALUES (@IdCliente, @IdUsuario, GETDATE(), @Total, @Descuento, @MetodoPago, @Estado);

    -- Devuelve el ID recién insertado como un entero
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- 5. Insertar detalle de venta y descontar stock en un mismo paso
CREATE OR ALTER PROCEDURE sp_Ventas_InsertarDetalleYDescontarStock
    @IdVenta INT,
    @IdProducto INT,
    @Cantidad INT,
    @PrecioUnitario DECIMAL(18,2),
    @Subtotal DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
    VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal);

    UPDATE Productos
    SET StockActual = StockActual - @Cantidad
    WHERE Id = @IdProducto;
END
GO
```

### Caso D: Reportes Complejos (`EstadisticasRepository.cs`)
Las consultas largas con CTEs (`WITH ...`) o subconsultas son las que más se benefician al pasar a procedimientos almacenados:

```sql
USE KioscoDB;
GO

-- 6. Reporte de ventas por franja horaria
CREATE OR ALTER PROCEDURE sp_Estadisticas_VentasPorHora
AS
BEGIN
    SET NOCOUNT ON;

    WITH FranjasHorarias AS (
        SELECT 1 AS Orden, '00:00 - 06:00 hs' AS FranjaHoraria, 0 AS HoraInicio, 5 AS HoraFin
        UNION ALL
        SELECT 2, '06:00 - 12:00 hs', 6, 11
        UNION ALL
        SELECT 3, '12:00 - 18:00 hs', 12, 17
        UNION ALL
        SELECT 4, '18:00 - 00:00 hs', 18, 23
    )
    SELECT 
        f.FranjaHoraria,
        COUNT(v.Id) AS CantidadVentas,
        ISNULL(SUM(v.Total), 0) AS TotalFacturado
    FROM FranjasHorarias f
    LEFT JOIN Ventas v 
        ON DATEPART(HOUR, v.Fecha) BETWEEN f.HoraInicio AND f.HoraFin
        AND v.Estado = 'Completada'
    GROUP BY f.Orden, f.FranjaHoraria
    ORDER BY f.Orden ASC;
END
GO
```

---

## Paso 2: Consumir los Procedimientos desde C# (Capa `Data`)

Para que `SqlCommand` entienda que le estás pasando el nombre de un procedimiento almacenado y no una consulta `SELECT` común, hay **dos requisitos clave** en tus archivos de `KioscoApp/Data/`:

1. Agregar el namespace **`using System.Data;`** arriba del todo en el archivo `.cs` (para poder usar `CommandType`).
2. Configurar la propiedad **`cmd.CommandType = CommandType.StoredProcedure;`** antes de ejecutar el comando.

### Ejemplo 1: Modificando `ProductoRepository.cs`

#### Antes (Consulta en texto plano):
```csharp
public Producto? ObtenerPorSKU(string sku)
{
    using var connection = DatabaseHelper.GetConnection();
    connection.Open();

    const string query = @"SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria,
                                  c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta,
                                  p.StockActual, p.StockMinimo
                           FROM Productos p
                           INNER JOIN Categorias c ON p.IdCategoria = c.Id
                           WHERE p.SKU = @SKU";

    using var cmd = new SqlCommand(query, connection);
    cmd.Parameters.AddWithValue("@SKU", sku.Trim());
    using var reader = cmd.ExecuteReader();

    return reader.Read() ? MapProducto(reader) : null;
}
```

#### Después (Usando Stored Procedure):
```csharp
using System;
using System.Collections.Generic;
using System.Data; // <-- 1. IMPORTANTE: Agregar System.Data
using KioscoApp.Models;
using Microsoft.Data.SqlClient;

// ... dentro de ProductoRepository:

public Producto? ObtenerPorSKU(string sku)
{
    using var connection = DatabaseHelper.GetConnection();
    connection.Open();

    // 2. Pasamos el nombre exacto del procedimiento almacenado
    using var cmd = new SqlCommand("sp_Productos_ObtenerPorSKU", connection);
    
    // 3. Indicamos que es un Stored Procedure
    cmd.CommandType = CommandType.StoredProcedure;

    // 4. Los parámetros se pasan exactamente igual que antes
    cmd.Parameters.AddWithValue("@SKU", sku.Trim());

    using var reader = cmd.ExecuteReader();
    return reader.Read() ? MapProducto(reader) : null;
}
```

---

### Ejemplo 2: Modificando `Insertar` en `ProductoRepository.cs`

```csharp
public void Insertar(Producto p)
{
    using var connection = DatabaseHelper.GetConnection();
    connection.Open();

    using var cmd = new SqlCommand("sp_Productos_Insertar", connection);
    cmd.CommandType = CommandType.StoredProcedure;

    // Tu método auxiliar SetCommandParameters funciona sin cambios
    // siempre y cuando los nombres de parámetros (@SKU, @Nombre, etc.) coincidan con el SP
    SetCommandParameters(cmd, p);
    
    cmd.ExecuteNonQuery();
}
```

---

### Ejemplo 3: Uso con Transacciones en `VentaRepository.cs`

Podés seguir utilizando transacciones de C# (`SqlTransaction`) combinadas con procedimientos almacenados sin ningún problema:

```csharp
using System;
using System.Collections.Generic;
using System.Data; // <-- Necesario para CommandType.StoredProcedure
using Microsoft.Data.SqlClient;
using KioscoApp.Models;

namespace KioscoApp.Data
{
    public class VentaRepository
    {
        public int InsertarVenta(Venta venta)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Insertar la Venta principal usando SP
                using var cmdVenta = new SqlCommand("sp_Ventas_InsertarCabecera", connection, transaction);
                cmdVenta.CommandType = CommandType.StoredProcedure;
                cmdVenta.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario ?? (object)DBNull.Value);
                cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                cmdVenta.Parameters.AddWithValue("@Descuento", venta.Descuento);
                cmdVenta.Parameters.AddWithValue("@MetodoPago", venta.MetodoPago);
                cmdVenta.Parameters.AddWithValue("@Estado", venta.Estado);

                int idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                venta.Id = idVenta;

                // 2. Insertar detalles y descontar stock usando SP
                foreach (var detalle in venta.Detalles)
                {
                    using var cmdDetalle = new SqlCommand("sp_Ventas_InsertarDetalleYDescontarStock", connection, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmdDetalle.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmdDetalle.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmdDetalle.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                    cmdDetalle.ExecuteNonQuery();
                }

                transaction.Commit();
                return idVenta;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al registrar la venta en la base de datos.", ex);
            }
        }
    }
}
```

---

## Paso 3: Buenas Prácticas y Recomendaciones para tu Proyecto

1. **Convención de Nombres:**
   Usá un formato estándar para encontrar rápido tus procedimientos en SSMS, por ejemplo:
   `sp_<Entidad>_<Accion>`
   * `sp_Productos_ObtenerTodos`
   * `sp_Clientes_Insertar`
   * `sp_Usuarios_ObtenerPorNombreUsuario`
   * `sp_Estadisticas_ListarMasVendidos`
   *(Nota: Evitá el prefijo `sp_` únicamente si tenés miles de procedimientos de sistema, o usá `usp_` [User Stored Procedure], aunque en proyectos académicos o PyME `sp_` es totalmente aceptado).*

2. **Usá siempre `CREATE OR ALTER PROCEDURE`:**
   A partir de SQL Server 2016 SP1, `CREATE OR ALTER` te permite ejecutar el mismo script `.sql` varias veces sin que falle diciendo *"There is already an object named 'sp_...' in the database"*.

3. **Incluí `SET NOCOUNT ON;` al principio del bloque `BEGIN`:**
   Evita que SQL Server envíe mensajes internos de `"X rows affected"` por la red en cada instrucción dentro del SP, mejorando ligeramente el rendimiento en ADO.NET.

4. **Mantené tus Scripts en el Repositorio Git:**
   Creá un archivo llamado `KioscoApp/Scripts/StoredProcedures.sql` y guardá ahí todos los `CREATE OR ALTER PROCEDURE`. Si algún compañero de equipo clona el proyecto o recreás la base de datos con `KioscoDB.sql`, solo tendrán que correr `StoredProcedures.sql` a continuación.

---

## Checklist Rápido para Migrar cada Método

- [ ] **1.** Copiar el `SELECT / INSERT / UPDATE / DELETE` del método en `KioscoApp/Data/...Repository.cs`.
- [ ] **2.** Armar el `CREATE OR ALTER PROCEDURE sp_Entidad_Metodo` en SSMS declarando las variables `@Parametro TIPO` arriba del `AS`.
- [ ] **3.** Ejecutar el script en SSMS (`F5`) verificando que se cree en `KioscoDB -> Programmability -> Stored Procedures`.
- [ ] **4.** En el archivo `.cs`, verificar que esté `using System.Data;`.
- [ ] **5.** Reemplazar el texto SQL por `"sp_Entidad_Metodo"` en el `new SqlCommand(...)`.
- [ ] **6.** Agregar la línea `cmd.CommandType = CommandType.StoredProcedure;`.
- [ ] **7.** Verificar que los nombres de las columnas devueltas (`reader["NombreColumna"]`) y los parámetros (`@Id`, `@SKU`, etc.) coincidan exactamente con los del SP.
