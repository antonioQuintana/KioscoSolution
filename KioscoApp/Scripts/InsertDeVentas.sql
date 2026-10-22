USE KioscoDB;
GO

-- Variables auxiliares para IDs generales
DECLARE @IdCliente INT = (SELECT TOP 1 Id FROM Clientes WHERE Dni = '00000000');
DECLARE @IdUsuario INT = (SELECT TOP 1 Id FROM Usuarios ORDER BY Id ASC);
DECLARE @VentaId INT;

-- Variables para IDs de productos (con fallback en caso de que algún SKU no exista aún)
DECLARE @IdCoca INT     = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU IN ('7790002001', '7791234567890')), 1);
DECLARE @IdAlfajor INT  = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU IN ('7790003001', '7790987654321')), 2);
DECLARE @IdPapas INT    = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU IN ('7790014002', '7791111222233')), 3);
DECLARE @IdCerveza INT  = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790004001'), @IdCoca);
DECLARE @IdMarlboro INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790001001'), @IdCoca);
DECLARE @IdSpeed INT    = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790016001'), @IdCoca);
DECLARE @IdGolosina INT = ISNULL((SELECT TOP 1 Id FROM Productos WHERE SKU = '7790006001'), @IdAlfajor);

-- ==============================================================================
-- VENTA 1: Hace 6 días (Bebida + Snack simple) - Pago Efectivo
-- Total: $ 2.600
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 14, DATEADD(day, -6, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 2600.00, 0.00, 'Efectivo', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdCoca,  1, 1500.00, 1500.00),
    (@VentaId, @IdPapas, 1, 1100.00, 1100.00);


-- ==============================================================================
-- VENTA 2: Hace 5 días (Varios alfajores y golosinas) - MercadoPago / QR
-- Total: $ 2.450 (Subtotal $2.600 - Descuento $150)
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 18, DATEADD(day, -5, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 2450.00, 150.00, 'MercadoPago / QR', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdAlfajor,  3, 400.00, 1200.00),
    (@VentaId, @IdGolosina, 4, 350.00, 1400.00);


-- ==============================================================================
-- VENTA 3: Hace 4 días (Cervezas y snacks para previa) - Tarjeta de Débito
-- Total: $ 6.600
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 21, DATEADD(day, -4, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 6600.00, 0.00, 'Tarjeta de Débito', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdCerveza, 3, 1600.00, 4800.00),
    (@VentaId, @IdPapas,   1, 1800.00, 1800.00);


-- ==============================================================================
-- VENTA 4: Hace 3 días (Cigarrillos + Energizante) - Efectivo
-- Total: $ 4.100
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 11, DATEADD(day, -3, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 4100.00, 0.00, 'Efectivo', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdMarlboro, 1, 2600.00, 2600.00),
    (@VentaId, @IdSpeed,    1, 1500.00, 1500.00);


-- ==============================================================================
-- VENTA 5: Hace 2 días (Compra grande de gaseosas y golosinas) - Transferencia
-- Total: $ 9.500 (Subtotal $10.000 - Descuento $500)
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 16, DATEADD(day, -2, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 9500.00, 500.00, 'Transferencia', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdCoca,    4, 1500.00, 6000.00),
    (@VentaId, @IdAlfajor, 5,  400.00, 2000.00),
    (@VentaId, @IdSpeed,   1, 1500.00, 1500.00),
    (@VentaId, @IdGolosina, 2, 250.00,  500.00);


-- ==============================================================================
-- VENTA 6: Ayer (Papas + Cervezas) - Tarjeta de Crédito
-- Total: $ 5.000
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, 20, DATEADD(day, -1, CAST(CAST(GETDATE() AS DATE) AS DATETIME))), 5000.00, 0.00, 'Tarjeta de Crédito', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdCerveza, 2, 1600.00, 3200.00),
    (@VentaId, @IdPapas,   1, 1800.00, 1800.00);


-- ==============================================================================
-- VENTA 7: Hoy (Golosina al paso + Gaseosa) - Efectivo
-- Total: $ 1.900
-- ==============================================================================
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES (@IdCliente, @IdUsuario, DATEADD(hour, -2, GETDATE()), 1900.00, 0.00, 'Efectivo', 'Completada');

SET @VentaId = SCOPE_IDENTITY();

INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES 
    (@VentaId, @IdCoca,    1, 1500.00, 1500.00),
    (@VentaId, @IdAlfajor, 1,  400.00,  400.00);
GO