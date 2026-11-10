USE KioscoDB;
GO

SET NOCOUNT ON;

-- ==============================================================================
-- SIMULACIÓN DE 10.000 VENTAS AL CONSUMIDOR FINAL (DNI: 00000000)
-- Rango: Desde el 1 de Julio del año actual hasta la fecha de hoy
-- Jerarquía de días con mayores ventas (de mayor a menor):
--   1º Sábado    (2.500 ventas)
--   2º Viernes   (2.100 ventas)
--   3º Jueves    (1.650 ventas)
--   4º Miércoles (1.300 ventas)
--   5º Martes    (1.050 ventas)
--   6º Domingo   (  800 ventas)
--   7º Lunes     (  600 ventas)
--   TOTAL:       10.000 ventas
-- ==============================================================================

-- 1. Asegurar que exista el Consumidor Final (DNI: 00000000)
IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '00000000')
BEGIN
    INSERT INTO Clientes (Dni, Nombre, Apellido, Activo)
    VALUES ('00000000', 'Consumidor', 'Final', 1);
END

DECLARE @IdCliente INT = (SELECT TOP 1 Id FROM Clientes WHERE Dni = '00000000');
DECLARE @FechaInicio DATE = DATEFROMPARTS(YEAR(GETDATE()), 7, 1); -- 1 de Julio de este año
DECLARE @FechaFin DATE = CAST(GETDATE() AS DATE);                 -- Fecha de hoy

-- 2. Preparar tablas temporales de Usuarios y Productos para selección aleatoria sin huecos de ID
IF OBJECT_ID('tempdb..#UsuariosTemp') IS NOT NULL DROP TABLE #UsuariosTemp;
SELECT 
    ROW_NUMBER() OVER (ORDER BY Id) AS Idx,
    Id AS IdUsuario
INTO #UsuariosTemp
FROM Usuarios;

DECLARE @TotalUsuarios INT = (SELECT COUNT(*) FROM #UsuariosTemp);

IF OBJECT_ID('tempdb..#ProductosTemp') IS NOT NULL DROP TABLE #ProductosTemp;
SELECT 
    ROW_NUMBER() OVER (ORDER BY Id) AS Idx,
    Id AS IdProducto,
    PrecioVenta
INTO #ProductosTemp
FROM Productos;

DECLARE @TotalProductos INT = (SELECT COUNT(*) FROM #ProductosTemp);

IF @TotalProductos = 0
BEGIN
    RAISERROR('No hay productos cargados en la tabla Productos. Ejecute primero InsertDeProductos.sql.', 16, 1);
    RETURN;
END

-- 3. Generar el calendario de fechas desde el 1 de Julio hasta hoy clasificado por día de la semana
-- Nota: DATEDIFF(DAY, '19000101', Fecha) % 7 es independiente de @@DATEFIRST / idioma:
-- 0 = Lunes, 1 = Martes, 2 = Miércoles, 3 = Jueves, 4 = Viernes, 5 = Sábado, 6 = Domingo
IF OBJECT_ID('tempdb..#CalendarioDias') IS NOT NULL DROP TABLE #CalendarioDias;

;WITH Numeros AS (
    SELECT TOP (DATEDIFF(DAY, @FechaInicio, @FechaFin) + 1)
        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS n
    FROM sys.all_objects a
    CROSS JOIN sys.all_objects b
),
Fechas AS (
    SELECT 
        DATEADD(DAY, n, @FechaInicio) AS Fecha,
        DATEDIFF(DAY, '19000101', DATEADD(DAY, n, @FechaInicio)) % 7 AS DiaSemana
    FROM Numeros
)
SELECT 
    Fecha,
    DiaSemana,
    ROW_NUMBER() OVER (PARTITION BY DiaSemana ORDER BY Fecha) AS NumFechaEnDiaSemana,
    COUNT(*) OVER (PARTITION BY DiaSemana) AS TotalFechasDiaSemana
INTO #CalendarioDias
FROM Fechas;

-- 4. Tabla temporal de semillas aleatorias materializadas por día y tabla de ventas simuladas
IF OBJECT_ID('tempdb..#SemillaDia') IS NOT NULL DROP TABLE #SemillaDia;
CREATE TABLE #SemillaDia (
    RowNum INT NOT NULL,
    PuestoJerarquia INT NOT NULL,
    NombreDia VARCHAR(20) NOT NULL,
    DiaSemana INT NOT NULL,
    UmbralEstratificado INT NOT NULL,
    MinProductos INT NOT NULL,
    RangoProductos INT NOT NULL,
    MaxUnidadesPorProducto INT NOT NULL,
    RndFecha INT NOT NULL,
    RndFranjaHora INT NOT NULL,
    RndHoraDentro INT NOT NULL,
    RndMinuto INT NOT NULL,
    RndSegundo INT NOT NULL,
    RndUsuario INT NOT NULL,
    RndMetodoPago INT NOT NULL,
    RndCantProd INT NOT NULL,
    RndDescuento INT NOT NULL,
    RndSeedProd INT NOT NULL,
    RndSeedCant INT NOT NULL
);

-- ==============================================================================
-- CONSULTA 1 (1º PUESTO JERARQUÍA): SÁBADO -> 2.500 VENTAS
-- Mayor volumen de tickets, entre 2 y 5 productos distintos por venta, hasta 5 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (2500)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    1 AS PuestoJerarquia, 'Sábado' AS NombreDia, 5 AS DiaSemana,
    2250 AS UmbralEstratificado, 2 AS MinProductos, 4 AS RangoProductos, 5 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSULTA 2 (2º PUESTO JERARQUÍA): VIERNES -> 2.100 VENTAS
-- Entre 2 y 4 productos distintos por venta, hasta 4 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (2100)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    2 AS PuestoJerarquia, 'Viernes' AS NombreDia, 4 AS DiaSemana,
    1890 AS UmbralEstratificado, 2 AS MinProductos, 3 AS RangoProductos, 4 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSULTA 3 (3º PUESTO JERARQUÍA): JUEVES -> 1.650 VENTAS
-- Entre 1 y 4 productos distintos por venta, hasta 4 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (1650)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    3 AS PuestoJerarquia, 'Jueves' AS NombreDia, 3 AS DiaSemana,
    1485 AS UmbralEstratificado, 1 AS MinProductos, 4 AS RangoProductos, 4 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSULTA 4 (4º PUESTO JERARQUÍA): MIÉRCOLES -> 1.300 VENTAS
-- Entre 1 y 3 productos distintos por venta, hasta 3 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (1300)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    4 AS PuestoJerarquia, 'Miércoles' AS NombreDia, 2 AS DiaSemana,
    1170 AS UmbralEstratificado, 1 AS MinProductos, 3 AS RangoProductos, 3 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSULTA 5 (5º PUESTO JERARQUÍA): MARTES -> 1.050 VENTAS
-- Entre 1 y 3 productos distintos por venta, hasta 3 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (1050)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    5 AS PuestoJerarquia, 'Martes' AS NombreDia, 1 AS DiaSemana,
    945 AS UmbralEstratificado, 1 AS MinProductos, 3 AS RangoProductos, 3 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSULTA 6 (6º PUESTO JERARQUÍA): DOMINGO -> 800 VENTAS
-- Entre 1 y 3 productos distintos por venta, hasta 2 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (800)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    6 AS PuestoJerarquia, 'Domingo' AS NombreDia, 6 AS DiaSemana,
    720 AS UmbralEstratificado, 1 AS MinProductos, 3 AS RangoProductos, 2 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSULTA 7 (7º PUESTO JERARQUÍA): LUNES -> 600 VENTAS
-- Entre 1 y 2 productos distintos por venta, hasta 2 uds c/u
-- ==============================================================================
INSERT INTO #SemillaDia
SELECT TOP (600)
    ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum,
    7 AS PuestoJerarquia, 'Lunes' AS NombreDia, 0 AS DiaSemana,
    540 AS UmbralEstratificado, 1 AS MinProductos, 2 AS RangoProductos, 2 AS MaxUnidadesPorProducto,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())) % 60, ABS(CHECKSUM(NEWID())),
    ABS(CHECKSUM(NEWID())) % 100, ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID())) % 100,
    ABS(CHECKSUM(NEWID())), ABS(CHECKSUM(NEWID()))
FROM sys.all_columns a CROSS JOIN sys.all_columns b;

-- ==============================================================================
-- CONSTRUIR LAS 10.000 CABECERAS DE VENTAS CON FECHA, HORA, VENDEDOR Y MÉTODO DE PAGO
-- ==============================================================================
IF OBJECT_ID('tempdb..#VentasSimuladas') IS NOT NULL DROP TABLE #VentasSimuladas;
CREATE TABLE #VentasSimuladas (
    SimVentaId INT IDENTITY(1,1) PRIMARY KEY,
    PuestoJerarquia INT NOT NULL,
    NombreDia VARCHAR(20) NOT NULL,
    DiaSemana INT NOT NULL,
    FechaVenta DATETIME NOT NULL,
    IdCliente INT NOT NULL,
    IdUsuario INT NULL,
    MetodoPago NVARCHAR(50) NOT NULL,
    CantProductosDistintos INT NOT NULL,
    MaxUnidadesPorProducto INT NOT NULL,
    PorcentajeDescuento DECIMAL(5,2) NOT NULL,
    SeedProducto INT NOT NULL,
    SeedCantidad INT NOT NULL
);

INSERT INTO #VentasSimuladas (
    PuestoJerarquia, NombreDia, DiaSemana, FechaVenta, IdCliente, IdUsuario,
    MetodoPago, CantProductosDistintos, MaxUnidadesPorProducto, PorcentajeDescuento, SeedProducto, SeedCantidad
)
SELECT 
    s.PuestoJerarquia,
    s.NombreDia,
    s.DiaSemana,
    DATEADD(SECOND, s.RndSegundo,
        DATEADD(MINUTE, s.RndMinuto,
            DATEADD(HOUR,
                CASE 
                    WHEN s.RndFranjaHora < 8  THEN s.RndHoraDentro % 6           -- 00:00 a 05:59 (8%)
                    WHEN s.RndFranjaHora < 28 THEN 6 + (s.RndHoraDentro % 6)     -- 06:00 a 11:59 (20%)
                    WHEN s.RndFranjaHora < 60 THEN 12 + (s.RndHoraDentro % 6)    -- 12:00 a 17:59 (32%)
                    ELSE 18 + (s.RndHoraDentro % 6)                              -- 18:00 a 23:59 (40%)
                END,
                CAST(c.Fecha AS DATETIME)
            )
        )
    ) AS FechaVenta,
    @IdCliente,
    u.IdUsuario,
    CASE 
        WHEN s.RndMetodoPago < 38 THEN N'Efectivo'
        WHEN s.RndMetodoPago < 70 THEN N'MercadoPago / QR'
        WHEN s.RndMetodoPago < 85 THEN N'Tarjeta de D' + NCHAR(233) + N'bito'
        WHEN s.RndMetodoPago < 94 THEN N'Transferencia'
        ELSE N'Tarjeta de Cr' + NCHAR(233) + N'dito'
    END AS MetodoPago,
    s.MinProductos + (s.RndCantProd % s.RangoProductos) AS CantProductosDistintos,
    s.MaxUnidadesPorProducto,
    CASE 
        WHEN s.RndDescuento < 10 THEN 5.00 
        WHEN s.RndDescuento < 14 THEN 10.00 
        ELSE 0.00 
    END AS PorcentajeDescuento,
    s.RndSeedProd,
    s.RndSeedCant
FROM #SemillaDia s
INNER JOIN #CalendarioDias c 
    ON c.DiaSemana = s.DiaSemana
   AND c.NumFechaEnDiaSemana = CASE 
        WHEN s.RowNum <= s.UmbralEstratificado THEN ((s.RowNum - 1) % c.TotalFechasDiaSemana) + 1
        ELSE (s.RndFecha % c.TotalFechasDiaSemana) + 1
       END
LEFT JOIN #UsuariosTemp u 
    ON u.Idx = (s.RndUsuario % NULLIF(@TotalUsuarios, 0)) + 1;

-- Ajustar cualquier hora del día de hoy que haya quedado posterior al instante actual
UPDATE #VentasSimuladas
SET FechaVenta = DATEADD(MINUTE, -(ABS(SeedProducto) % 180 + 5), GETDATE())
WHERE FechaVenta > GETDATE();

-- ==============================================================================
-- 5. GENERAR LOS DETALLES DE CADA VENTA Y CALCULAR TOTALES EXACTOS
-- ==============================================================================
IF OBJECT_ID('tempdb..#DetallesSimulados') IS NOT NULL DROP TABLE #DetallesSimulados;

SELECT 
    v.SimVentaId,
    p.IdProducto,
    1 + ((ABS(v.SeedCantidad + l.NumLinea * 13) % v.MaxUnidadesPorProducto)) AS Cantidad,
    p.PrecioVenta AS PrecioUnitario,
    CAST((1 + ((ABS(v.SeedCantidad + l.NumLinea * 13) % v.MaxUnidadesPorProducto))) * p.PrecioVenta AS DECIMAL(18,2)) AS Subtotal
INTO #DetallesSimulados
FROM #VentasSimuladas v
CROSS APPLY (
    SELECT NumLinea
    FROM (VALUES (1), (2), (3), (4), (5)) AS Lineas(NumLinea)
    WHERE Lineas.NumLinea <= v.CantProductosDistintos
) l
INNER JOIN #ProductosTemp p
    ON p.Idx = ((v.SeedProducto + (l.NumLinea - 1) * 7) % @TotalProductos) + 1;

-- Calcular Subtotal, Descuento y Total por cada venta simulada
IF OBJECT_ID('tempdb..#TotalesPorVenta') IS NOT NULL DROP TABLE #TotalesPorVenta;

SELECT 
    d.SimVentaId,
    SUM(d.Subtotal) AS SubtotalBruto,
    CAST(ROUND(SUM(d.Subtotal) * (v.PorcentajeDescuento / 100.0), 2) AS DECIMAL(18,2)) AS DescuentoMonto,
    CAST(SUM(d.Subtotal) - ROUND(SUM(d.Subtotal) * (v.PorcentajeDescuento / 100.0), 2) AS DECIMAL(18,2)) AS TotalFinal
INTO #TotalesPorVenta
FROM #DetallesSimulados d
INNER JOIN #VentasSimuladas v ON v.SimVentaId = d.SimVentaId
GROUP BY d.SimVentaId, v.PorcentajeDescuento;

-- ==============================================================================
-- 6. INSERTAR EN TABLAS REALES (Ventas y DetallesVenta) EN TRANSACCIÓN
-- ==============================================================================
IF OBJECT_ID('tempdb..#MapaIdsVentas') IS NOT NULL DROP TABLE #MapaIdsVentas;
CREATE TABLE #MapaIdsVentas (
    SimVentaId INT PRIMARY KEY,
    IdVentaReal INT NOT NULL
);

BEGIN TRANSACTION;

MERGE INTO Ventas AS Target
USING (
    SELECT 
        v.SimVentaId,
        v.IdCliente,
        v.IdUsuario,
        v.FechaVenta,
        t.TotalFinal,
        t.DescuentoMonto,
        v.MetodoPago
    FROM #VentasSimuladas v
    INNER JOIN #TotalesPorVenta t ON t.SimVentaId = v.SimVentaId
) AS Source
ON 1 = 0
WHEN NOT MATCHED THEN
    INSERT (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
    VALUES (Source.IdCliente, Source.IdUsuario, Source.FechaVenta, Source.TotalFinal, Source.DescuentoMonto, Source.MetodoPago, 'Completada')
OUTPUT Source.SimVentaId, INSERTED.Id
INTO #MapaIdsVentas (SimVentaId, IdVentaReal);

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
SELECT 
    m.IdVentaReal,
    d.IdProducto,
    d.Cantidad,
    d.PrecioUnitario,
    d.Subtotal
FROM #DetallesSimulados d
INNER JOIN #MapaIdsVentas m ON m.SimVentaId = d.SimVentaId
ORDER BY m.IdVentaReal;

COMMIT TRANSACTION;

-- ==============================================================================
-- 7. RESUMEN DE VERIFICACIÓN DE LA JERARQUÍA DE DÍAS INSERTADOS
-- ==============================================================================
;WITH UnidadesPorVenta AS (
    SELECT SimVentaId, SUM(Cantidad) AS Unidades
    FROM #DetallesSimulados
    GROUP BY SimVentaId
)
SELECT 
    v.PuestoJerarquia AS [Puesto],
    v.NombreDia AS [Dia de la Semana],
    COUNT(v.SimVentaId) AS [Cantidad de Ventas],
    SUM(u.Unidades) AS [Unidades Vendidas],
    SUM(t.TotalFinal) AS [Total Facturado ($)]
FROM #VentasSimuladas v
INNER JOIN #TotalesPorVenta t ON t.SimVentaId = v.SimVentaId
INNER JOIN UnidadesPorVenta u ON u.SimVentaId = v.SimVentaId
GROUP BY v.PuestoJerarquia, v.NombreDia
ORDER BY v.PuestoJerarquia ASC;

DROP TABLE #MapaIdsVentas;
DROP TABLE #TotalesPorVenta;
DROP TABLE #DetallesSimulados;
DROP TABLE #VentasSimuladas;
DROP TABLE #SemillaDia;
DROP TABLE #CalendarioDias;
DROP TABLE #ProductosTemp;
DROP TABLE #UsuariosTemp;
GO
