USE KioscoDB;
GO

-- ==============================================================================
-- 1. IDENTIFICAR FECHAS DE LOS ÚLTIMOS 7 DÍAS
-- ==============================================================================
DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
DECLARE @FechaLunes DATE, @FechaMartes DATE, @FechaMiercoles DATE, 
        @FechaJueves DATE, @FechaViernes DATE, @FechaSabado DATE, @FechaDomingo DATE;

WITH Ultimos7Dias AS (
    SELECT DATEADD(DAY, -v.n, @Hoy) AS F
    FROM (VALUES (0),(1),(2),(3),(4),(5),(6)) AS v(n)
)
SELECT 
    @FechaLunes     = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 0 THEN F END),
    @FechaMartes    = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 1 THEN F END),
    @FechaMiercoles = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 2 THEN F END),
    @FechaJueves    = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 3 THEN F END),
    @FechaViernes   = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 4 THEN F END),
    @FechaSabado    = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 5 THEN F END),
    @FechaDomingo   = MAX(CASE WHEN (DATEDIFF(DAY, '19000101', F) % 7) = 6 THEN F END)
FROM Ultimos7Dias;

-- ==============================================================================
-- 2. VARIABLES DE CLIENTE, USUARIO Y PRODUCTOS (con fallback seguro)
-- ==============================================================================
DECLARE @IdCliente INT = (SELECT TOP 1 Id FROM Clientes WHERE Dni = '00000000');
DECLARE @IdUsuario INT = (SELECT TOP 1 Id FROM Usuarios ORDER BY Id ASC);
DECLARE @VentaId INT;

DECLARE @IdCoca1500  INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU IN ('7790002003', '7791234567890')), 1);
DECLARE @IdCoca500   INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790002001'), @IdCoca1500);
DECLARE @IdCervezaQ  INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790004001'), @IdCoca1500);
DECLARE @IdCervezaH  INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790004003'), @IdCoca1500);
DECLARE @IdFernet    INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790017001'), @IdCoca1500);
DECLARE @IdPapas     INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU IN ('7790014002', '7791111222233')), 3);
DECLARE @IdSaladix   INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790014001'), @IdPapas);
DECLARE @IdAlfajor   INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU IN ('7790003001', '7790987654321')), 2);
DECLARE @IdMarlboro  INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790001001'), @IdCoca1500);
DECLARE @IdSpeed     INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790016001'), @IdCoca1500);
DECLARE @IdAgua      INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790005001'), @IdCoca1500);

-- ==============================================================================
-- 1º PUESTO: VIERNES (Total recaudado: $96.000)
-- Fuerte salida de Cervezas, Fernet, Coca-Cola y Snacks
-- ==============================================================================
-- Venta Viernes 1 (20:30 hs) -> $66.000
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 30, DATEADD(hour, 20, CAST(@FechaViernes AS DATETIME))), 66000.00, 1200.00, 'MercadoPago / QR', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaQ, 18, 1600.00, 28800.00), -- 18 Cervezas Quilmes
    (@VentaId, @IdCoca1500,  6, 2400.00, 14400.00), -- 6 Coca-Cola 1.5L
    (@VentaId, @IdFernet,    6, 2800.00, 16800.00), -- 6 Latas Fernet
    (@VentaId, @IdPapas,     4, 1800.00,  7200.00); -- 4 Papas

-- Venta Viernes 2 (23:15 hs) -> $30.000
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 15, DATEADD(hour, 23, CAST(@FechaViernes AS DATETIME))), 30000.00, 200.00, 'Efectivo', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaH, 10, 2200.00, 22000.00), -- 10 Cervezas Heineken
    (@VentaId, @IdMarlboro,  2, 2600.00,  5200.00), -- 2 Marlboro
    (@VentaId, @IdSpeed,     2, 1500.00,  3000.00); -- 2 Speed

-- ==============================================================================
-- 2º PUESTO: SÁBADO (Total recaudado: $81.000)
-- Mucha cerveza, aperitivos, refrescos y previa
-- ==============================================================================
-- Venta Sábado 1 (19:45 hs) -> $50.000
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 45, DATEADD(hour, 19, CAST(@FechaSabado AS DATETIME))), 50000.00, 0.00, 'Tarjeta de Débito', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaQ, 12, 1600.00, 19200.00), -- 12 Cervezas Quilmes
    (@VentaId, @IdFernet,    5, 2800.00, 14000.00), -- 5 Latas Fernet
    (@VentaId, @IdCoca1500,  4, 2400.00,  9600.00), -- 4 Coca-Cola 1.5L
    (@VentaId, @IdPapas,     4, 1800.00,  7200.00); -- 4 Papas

-- Venta Sábado 2 (22:30 hs) -> $31.000
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 30, DATEADD(hour, 22, CAST(@FechaSabado AS DATETIME))), 31000.00, 800.00, 'MercadoPago / QR', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaH,  8, 2200.00, 17600.00), -- 8 Heineken
    (@VentaId, @IdCoca500,   4, 1500.00,  6000.00), -- 4 Coca 500ml
    (@VentaId, @IdMarlboro,  2, 2600.00,  5200.00), -- 2 Marlboro
    (@VentaId, @IdSpeed,     2, 1500.00,  3000.00); -- 2 Speed

-- ==============================================================================
-- 3º PUESTO: JUEVES (Total recaudado: $54.000)
-- ==============================================================================
-- Venta Jueves 1 (19:00 hs) -> $38.000
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 19, CAST(@FechaJueves AS DATETIME)), 38000.00, 300.00, 'Tarjeta de Crédito', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaQ, 10, 1600.00, 16000.00),
    (@VentaId, @IdCoca500,   6, 1500.00,  9000.00),
    (@VentaId, @IdSaladix,   5, 1100.00,  5500.00),
    (@VentaId, @IdMarlboro,  3, 2600.00,  7800.00);

-- Venta Jueves 2 (21:30 hs) -> $16.000
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 30, DATEADD(hour, 21, CAST(@FechaJueves AS DATETIME))), 16000.00, 0.00, 'Efectivo', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaH,  5, 2200.00, 11000.00),
    (@VentaId, @IdPapas,     2, 1800.00,  3600.00),
    (@VentaId, @IdAlfajor,   3,  400.00,  1400.00);

-- ==============================================================================
-- 4º PUESTO: MIÉRCOLES (Total recaudado: $38.000)
-- ==============================================================================
-- Venta Miércoles 1 (13:15 hs) -> $13.200
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 15, DATEADD(hour, 13, CAST(@FechaMiercoles AS DATETIME))), 13200.00, 0.00, 'Efectivo', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCoca500,   4, 1500.00,  6000.00),
    (@VentaId, @IdAlfajor,   5,  400.00,  2000.00),
    (@VentaId, @IdMarlboro,  2, 2600.00,  5200.00);

-- Venta Miércoles 2 (18:45 hs) -> $24.800
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 45, DATEADD(hour, 18, CAST(@FechaMiercoles AS DATETIME))), 24800.00, 400.00, 'MercadoPago / QR', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCervezaQ,  6, 1600.00,  9600.00),
    (@VentaId, @IdCoca1500,  3, 2400.00,  7200.00),
    (@VentaId, @IdPapas,     3, 1800.00,  5400.00),
    (@VentaId, @IdSpeed,     2, 1500.00,  3000.00);

-- ==============================================================================
-- 5º PUESTO: MARTES (Total recaudado: $28.000)
-- ==============================================================================
-- Venta Martes 1 (11:00 hs) -> $10.600
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 11, CAST(@FechaMartes AS DATETIME)), 10600.00, 0.00, 'Transferencia', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdAgua,      4,  950.00,  3800.00),
    (@VentaId, @IdAlfajor,   4,  400.00,  1600.00),
    (@VentaId, @IdMarlboro,  2, 2600.00,  5200.00);

-- Venta Martes 2 (17:30 hs) -> $17.400
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 30, DATEADD(hour, 17, CAST(@FechaMartes AS DATETIME))), 17400.00, 700.00, 'Efectivo', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCoca500,   4, 1500.00,  6000.00),
    (@VentaId, @IdSaladix,   4, 1100.00,  4400.00),
    (@VentaId, @IdSpeed,     3, 1500.00,  4500.00),
    (@VentaId, @IdCervezaQ,  2, 1600.00,  3200.00);

-- ==============================================================================
-- 6º PUESTO: LUNES (Total recaudado: $19.000)
-- ==============================================================================
-- Venta Lunes 1 (09:30 hs) -> $10.200
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 30, DATEADD(hour, 9, CAST(@FechaLunes AS DATETIME))), 10200.00, 0.00, 'MercadoPago / QR', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdSpeed,     4, 1500.00,  6000.00),
    (@VentaId, @IdAlfajor,   4,  400.00,  1600.00),
    (@VentaId, @IdMarlboro,  1, 2600.00,  2600.00);

-- Venta Lunes 2 (15:00 hs) -> $8.800
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 15, CAST(@FechaLunes AS DATETIME)), 8800.00, 200.00, 'Efectivo', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdAgua,      4,  950.00,  3800.00),
    (@VentaId, @IdCoca500,   2, 1500.00,  3000.00),
    (@VentaId, @IdSaladix,   2, 1100.00,  2200.00);

-- ==============================================================================
-- 7º PUESTO: DOMINGO (Total recaudado: $9.500)
-- ==============================================================================
-- Venta Domingo 1 (16:30 hs) -> $5.400
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(minute, 30, DATEADD(hour, 16, CAST(@FechaDomingo AS DATETIME))), 5400.00, 0.00, 'Efectivo', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCoca1500,  1, 2400.00,  2400.00),
    (@VentaId, @IdPapas,     1, 1800.00,  1800.00),
    (@VentaId, @IdAlfajor,   3,  400.00,  1200.00);

-- Venta Domingo 2 (20:00 hs) -> $4.100
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 20, CAST(@FechaDomingo AS DATETIME)), 4100.00, 0.00, 'Tarjeta de Débito', 'Completada');
SET @VentaId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES
    (@VentaId, @IdCoca500,   1, 1500.00,  1500.00),
    (@VentaId, @IdMarlboro,  1, 2600.00,  2600.00);
GO

UPDATE Ventas
SET MetodoPago = 'MercadoPago / QR'
WHERE LTRIM(RTRIM(MetodoPago)) = 'Mercado Pago';
GO

UPDATE Ventas
SET MetodoPago = 'Tarjeta de Débito'
WHERE LTRIM(RTRIM(MetodoPago)) = 'Tarjeta de Debito';
GO
