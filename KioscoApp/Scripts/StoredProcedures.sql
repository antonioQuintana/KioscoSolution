USE KioscoDB;
GO

-- =============================================================================
-- 1. CATEGORIAS (CategoriaRepository.cs)
-- =============================================================================

CREATE OR ALTER PROCEDURE sp_Categorias_ObtenerTodas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Descripcion
    FROM Categorias
    ORDER BY Nombre;
END
GO

CREATE OR ALTER PROCEDURE sp_Categorias_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Descripcion
    FROM Categorias
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Categorias_ExisteNombre
    @Nombre VARCHAR(100),
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM Categorias
    WHERE LOWER(Nombre) = LOWER(@Nombre) AND Id != @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Categorias_TieneProductosAsociados
    @IdCategoria INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM Productos
    WHERE IdCategoria = @IdCategoria;
END
GO

CREATE OR ALTER PROCEDURE sp_Categorias_Insertar
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Categorias (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END
GO

CREATE OR ALTER PROCEDURE sp_Categorias_Actualizar
    @Id INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Categorias
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Categorias_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Categorias
    WHERE Id = @Id;
END
GO

-- =============================================================================
-- 2. CLIENTES (ClienteRepository.cs)
-- =============================================================================

CREATE OR ALTER PROCEDURE sp_Clientes_ObtenerPorDni
    @Dni VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Clientes
    WHERE Dni = @Dni AND Activo = 1;
END
GO

CREATE OR ALTER PROCEDURE sp_Clientes_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Clientes
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Clientes_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Clientes
    WHERE Activo = 1 AND Dni != '00000000';
END
GO

CREATE OR ALTER PROCEDURE sp_Clientes_Insertar
    @Dni VARCHAR(20),
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Telefono VARCHAR(50) = NULL,
    @Email VARCHAR(100) = NULL,
    @Calle VARCHAR(100) = NULL,
    @Numero VARCHAR(20) = NULL,
    @Ciudad VARCHAR(100) = NULL,
    @Provincia VARCHAR(100) = NULL,
    @Nacimiento DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento)
    VALUES (@Dni, @Nombre, @Apellido, @Telefono, @Email, @Calle, @Numero, @Ciudad, @Provincia, @Nacimiento);
END
GO

CREATE OR ALTER PROCEDURE sp_Clientes_Actualizar
    @Id INT,
    @Dni VARCHAR(20),
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100),
    @Telefono VARCHAR(50) = NULL,
    @Email VARCHAR(100) = NULL,
    @Calle VARCHAR(100) = NULL,
    @Numero VARCHAR(20) = NULL,
    @Ciudad VARCHAR(100) = NULL,
    @Provincia VARCHAR(100) = NULL,
    @Nacimiento DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Clientes
    SET Dni = @Dni,
        Nombre = @Nombre,
        Apellido = @Apellido,
        Telefono = @Telefono,
        Email = @Email,
        Calle = @Calle,
        Numero = @Numero,
        Ciudad = @Ciudad,
        Provincia = @Provincia,
        Nacimiento = @Nacimiento
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Clientes_EliminarLogico
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Clientes
    SET Activo = 0
    WHERE Id = @Id;
END
GO

-- =============================================================================
-- 3. PRODUCTOS (ProductoRepository.cs y UcMovimientosStock.cs)
-- =============================================================================

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

CREATE OR ALTER PROCEDURE sp_Productos_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria,
           c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta,
           p.StockActual, p.StockMinimo
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
    WHERE p.Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Productos_ExisteSKU
    @SKU VARCHAR(50),
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM Productos
    WHERE LOWER(SKU) = LOWER(@SKU) AND Id != @Id;
END
GO

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

CREATE OR ALTER PROCEDURE sp_Productos_Actualizar
    @Id INT,
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
    UPDATE Productos
    SET SKU = @SKU,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        IdCategoria = @IdCategoria,
        PrecioCosto = @PrecioCosto,
        PrecioVenta = @PrecioVenta,
        StockActual = @StockActual,
        StockMinimo = @StockMinimo
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Productos_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Productos
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Productos_BuscarPorNombreOSKU
    @Term VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 10 p.Id, p.SKU, p.Nombre, p.Descripcion, p.IdCategoria,
                  c.Nombre AS CategoriaNombre, p.PrecioCosto, p.PrecioVenta,
                  p.StockActual, p.StockMinimo
    FROM Productos p
    INNER JOIN Categorias c ON p.IdCategoria = c.Id
    WHERE p.Nombre LIKE @Term OR p.SKU LIKE @Term
    ORDER BY p.Nombre;
END
GO

CREATE OR ALTER PROCEDURE sp_Productos_FiltrarStock
    @Filtro VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT p.Id, p.SKU, p.Nombre, c.Nombre AS Categoria, p.StockActual, p.StockMinimo 
    FROM Productos p
    LEFT JOIN Categorias c ON p.IdCategoria = c.Id
    WHERE p.Nombre LIKE @Filtro OR p.SKU LIKE @Filtro
    ORDER BY p.Nombre;
END
GO

CREATE OR ALTER PROCEDURE sp_Productos_ActualizarStock
    @Id INT,
    @Actual INT,
    @Minimo INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Productos
    SET StockActual = @Actual,
        StockMinimo = @Minimo
    WHERE Id = @Id;
END
GO

-- =============================================================================
-- 4. UBICACIONES (UbicacionRepository.cs)
-- =============================================================================

CREATE OR ALTER PROCEDURE sp_Ubicaciones_ObtenerProvincias
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre
    FROM Provincias
    ORDER BY Nombre;
END
GO

CREATE OR ALTER PROCEDURE sp_Ubicaciones_ObtenerCiudadesPorProvincia
    @ProvinciaId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, ProvinciaId, Nombre
    FROM Ciudades
    WHERE ProvinciaId = @ProvinciaId
    ORDER BY Nombre;
END
GO

-- =============================================================================
-- 5. USUARIOS (UsuarioRepository.cs)
-- =============================================================================

CREATE OR ALTER PROCEDURE sp_Usuarios_ObtenerTodos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Apellido, Dni, Usuario, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento
    FROM Usuarios;
END
GO

CREATE OR ALTER PROCEDURE sp_Usuarios_ObtenerPorUsuario
    @Usuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento
    FROM Usuarios
    WHERE Usuario = @Usuario;
END
GO

CREATE OR ALTER PROCEDURE sp_Usuarios_ExisteDatoUnico
    @Campo VARCHAR(50),
    @Valor VARCHAR(100),
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM Usuarios
    WHERE Id != @Id
      AND (
          (@Campo = 'Usuario'  AND Usuario  = @Valor) OR
          (@Campo = 'Email'    AND Email    = @Valor) OR
          (@Campo = 'Telefono' AND Telefono = @Valor) OR
          (@Campo = 'Dni'      AND Dni      = @Valor)
      );
END
GO

CREATE OR ALTER PROCEDURE sp_Usuarios_Insertar
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100) = NULL,
    @Dni VARCHAR(20) = NULL,
    @Usuario VARCHAR(50),
    @Contrasena VARCHAR(100),
    @Rol VARCHAR(20),
    @Email VARCHAR(100) = NULL,
    @Telefono VARCHAR(50) = NULL,
    @Calle VARCHAR(100) = NULL,
    @Numero VARCHAR(20) = NULL,
    @Ciudad VARCHAR(100) = NULL,
    @Provincia VARCHAR(100) = NULL,
    @Sexo VARCHAR(20) = NULL,
    @Nacimiento DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Usuarios 
        (Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
    VALUES 
        (@Nombre, @Apellido, @Dni, @Usuario, @Contrasena, @Rol, @Email, @Telefono, @Calle, @Numero, @Ciudad, @Provincia, @Sexo, @Nacimiento);
END
GO

CREATE OR ALTER PROCEDURE sp_Usuarios_Actualizar
    @Id INT,
    @Nombre VARCHAR(100),
    @Apellido VARCHAR(100) = NULL,
    @Dni VARCHAR(20) = NULL,
    @Usuario VARCHAR(50),
    @Rol VARCHAR(20),
    @Email VARCHAR(100) = NULL,
    @Telefono VARCHAR(50) = NULL,
    @Calle VARCHAR(100) = NULL,
    @Numero VARCHAR(20) = NULL,
    @Ciudad VARCHAR(100) = NULL,
    @Provincia VARCHAR(100) = NULL,
    @Sexo VARCHAR(20) = NULL,
    @Nacimiento DATE = NULL,
    @Contrasena VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Usuarios
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Dni = @Dni,
        Usuario = @Usuario,
        Rol = @Rol,
        Email = @Email,
        Telefono = @Telefono,
        Calle = @Calle,
        Numero = @Numero,
        Ciudad = @Ciudad,
        Provincia = @Provincia,
        Sexo = @Sexo,
        Nacimiento = @Nacimiento,
        Contrasena = CASE WHEN @Contrasena IS NOT NULL AND @Contrasena != '' THEN @Contrasena ELSE Contrasena END
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE sp_Usuarios_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Usuarios
    WHERE Id = @Id;
END
GO

-- =============================================================================
-- 6. VENTAS (VentaRepository.cs)
-- =============================================================================

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
    OUTPUT INSERTED.Id
    VALUES (@IdCliente, @IdUsuario, GETDATE(), @Total, @Descuento, @MetodoPago, @Estado);
END
GO

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

CREATE OR ALTER PROCEDURE sp_Ventas_ObtenerTodas
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 50 v.*, c.Dni AS DniCliente, c.Nombre AS NombreCliente, c.Apellido AS ApellidoCliente
    FROM Ventas v
    INNER JOIN Clientes c ON v.IdCliente = c.Id
    ORDER BY v.Fecha DESC;
END
GO

-- =============================================================================
-- 7. ESTADISTICAS (EstadisticasRepository.cs)
-- =============================================================================

CREATE OR ALTER PROCEDURE sp_Estadisticas_ListarMasVendidos
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.nombre AS nombre_categoria,
        p.nombre AS nombre_producto,
        SUM(dv.cantidad) AS cantidad_total_vendida
    FROM 
        categorias c
        INNER JOIN productos p ON c.id = p.idcategoria
        INNER JOIN detallesventa dv ON p.id = dv.idproducto
    GROUP BY 
        c.id,
        c.nombre,
        p.id,
        p.nombre
    HAVING 
        SUM(dv.cantidad) = (
            SELECT TOP 1 SUM(dv_sub.cantidad)
            FROM productos p_sub
            INNER JOIN detallesventa dv_sub ON p_sub.id = dv_sub.idproducto
            WHERE p_sub.idcategoria = c.id
            GROUP BY p_sub.id
            ORDER BY SUM(dv_sub.cantidad) DESC
        );
END
GO

CREATE OR ALTER PROCEDURE sp_Estadisticas_ListarVentasPorDias
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        CAST(Fecha AS DATE) AS Dia,
        DATENAME(WEEKDAY, Fecha) AS NombreDia,
        COUNT(Id) AS CantidadVentas,
        SUM(Total) AS TotalIngresos
    FROM Ventas
    WHERE Estado = 'Completada'
      AND Fecha >= DATEADD(DAY, -7, CAST(GETDATE() AS DATE))
    GROUP BY 
        CAST(Fecha AS DATE), 
        DATENAME(WEEKDAY, Fecha)
    ORDER BY 
        Dia ASC;
END
GO

CREATE OR ALTER PROCEDURE sp_Estadisticas_ListarVentasPorHora
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
    GROUP BY 
        f.Orden, 
        f.FranjaHoraria
    ORDER BY 
        f.Orden ASC;
END
GO

CREATE OR ALTER PROCEDURE sp_Estadisticas_ListarVentasPorCategoria
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.Nombre AS Categoria,
        COUNT(DISTINCT v.Id) AS TotalVentas,
        ISNULL(SUM(dv.Cantidad), 0) AS UnidadesVendidas,
        ISNULL(SUM(dv.Subtotal), 0) AS TotalFacturado
    FROM Categorias c
    LEFT JOIN Productos p 
        ON p.IdCategoria = c.Id
    LEFT JOIN DetallesVenta dv 
        ON dv.IdProducto = p.Id
    LEFT JOIN Ventas v 
        ON v.Id = dv.IdVenta 
        AND v.Estado = 'Completada'
    GROUP BY 
        c.Id, 
        c.Nombre
    ORDER BY 
        TotalFacturado DESC;
END
GO

CREATE OR ALTER PROCEDURE sp_Estadisticas_ListarMetodosMasUsados
AS
BEGIN
    SET NOCOUNT ON;
    WITH ArticulosPorVenta AS (
        SELECT 
            IdVenta, 
            SUM(Cantidad) AS CantidadArticulos
        FROM DetallesVenta
        GROUP BY IdVenta
    )
    SELECT 
        LTRIM(RTRIM(v.MetodoPago)) AS MetodoPago,
        COUNT(v.Id) AS CantidadVentas,
        ISNULL(SUM(apv.CantidadArticulos), 0) AS UnidadesVendidas,
        SUM(v.Total) AS TotalFacturado,
        ROUND(AVG(v.Total), 2) AS TicketPromedio
    FROM Ventas v
    LEFT JOIN ArticulosPorVenta apv 
        ON v.Id = apv.IdVenta
    WHERE v.Estado = 'Completada'
    GROUP BY 
        LTRIM(RTRIM(v.MetodoPago))
    ORDER BY 
        TotalFacturado DESC;
END
GO
