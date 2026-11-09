SET NOCOUNT ON;

-- IDs: Antonio=3, Ramiro=4, Laura=6, Martin=7, Maria=2, Carlos=5
-- Clientes: ConsumidorFinal=1, Roberto=5, Sofia=6, Diego=7, Elena=8, Lucas=9

-- ============================================================
-- CIERRES DE CAJA
-- ============================================================
-- Solo insertar si hay menos de 3 cierres
IF (SELECT COUNT(*) FROM CierresCaja) < 3
BEGIN
    INSERT INTO CierresCaja (IdUsuario,FechaApertura,FechaCierre,FondoInicial,VentasEfectivo,VentasQR,VentasTarjeta,IngresosVarios,GastosRetiros,TotalEsperado,TotalDeclarado,Diferencia,Observaciones,Estado)
    VALUES
    (3, DATEADD(DAY,-28,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-28,GETDATE())), 5000, 12500, 4300, 7800, 0, 1000, 28600, 28700,  100, 'Turno manana sin novedades', 'Cerrado'),
    (4, DATEADD(DAY,-21,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-21,GETDATE())), 5000,  9800, 6200, 5100, 0,  500, 25600, 25500, -100, 'Faltante leve - revisado',   'Cerrado'),
    (6, DATEADD(DAY,-14,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-14,GETDATE())), 5000, 15200, 8900,11300,500, 2000, 38900, 39200,  300, 'Sobrante - cliente pago de mas', 'Cerrado'),
    (7, DATEADD(DAY, -7,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY, -7,GETDATE())), 5000, 11000, 7500, 9200, 0, 1500, 31200, 31200,    0, 'Cierre exacto',               'Cerrado'),
    (3, DATEADD(DAY, -3,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY, -3,GETDATE())), 5000, 18500,12000,14500,1000,3000, 48000, 47800, -200, 'Faltante - revisados tickets','Cerrado'),
    (4, DATEADD(DAY, -1,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY, -1,GETDATE())), 5000, 13700, 9400,10800, 0, 1200, 37700, 37900,  200, 'Sobrante menor',              'Cerrado');
END

-- Capturamos IDs de los 2 primeros y 2 ultimos cierres para asociar ventas
DECLARE @c1 INT = (SELECT MIN(Id) FROM CierresCaja);
DECLARE @c2 INT = (SELECT MAX(Id) FROM CierresCaja);

-- ============================================================
-- VENTAS HISTORICAS (solo si hay menos de 15 ventas)
-- ============================================================
IF (SELECT COUNT(*) FROM Ventas) < 15
BEGIN

-- IDs productos mas usados
DECLARE @pCoca500   INT = (SELECT Id FROM Productos WHERE SKU='7790895000016');
DECLARE @pCoca15    INT = (SELECT Id FROM Productos WHERE SKU='7790895000023');
DECLARE @pPepsi500  INT = (SELECT Id FROM Productos WHERE SKU='7790335000012');
DECLARE @pAgua500   INT = (SELECT Id FROM Productos WHERE SKU='7791813000011');
DECLARE @pGatorade  INT = (SELECT Id FROM Productos WHERE SKU='7790580000014');
DECLARE @pQuilmes   INT = (SELECT Id FROM Productos WHERE SKU='7790110000017');
DECLARE @pAlfMilka  INT = (SELECT Id FROM Productos WHERE SKU='7622210449283');
DECLARE @pAlfJorg   INT = (SELECT Id FROM Productos WHERE SKU='7790580111011');
DECLARE @pRocklets  INT = (SELECT Id FROM Productos WHERE SKU='7790580222022');
DECLARE @pMarlboro  INT = (SELECT Id FROM Productos WHERE SKU='7790245000013');
DECLARE @pLucky     INT = (SELECT Id FROM Productos WHERE SKU='7790245000020');
DECLARE @pDerby     INT = (SELECT Id FROM Productos WHERE SKU='7790245000037');
DECLARE @pLays      INT = (SELECT Id FROM Productos WHERE SKU='7790310000016');
DECLARE @pDoritos   INT = (SELECT Id FROM Productos WHERE SKU='7790310000023');
DECLARE @pOreo      INT = (SELECT Id FROM Productos WHERE SKU='7790040000014');
DECLARE @pFernet    INT = (SELECT Id FROM Productos WHERE SKU='7791540000012');
DECLARE @pSprite    INT = (SELECT Id FROM Productos WHERE SKU='7790004000015');
DECLARE @pMentitas  INT = (SELECT Id FROM Productos WHERE SKU='7790580777077');

DECLARE @vId INT;

-- V1: hace 25 dias - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 3, DATEADD(DAY,-25,GETDATE()), 2600, 0, 'Efectivo', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pCoca500,2,950,1900),(@vId,@pAlfMilka,1,700,700);

-- V2: hace 24 dias - QR - Roberto Mansilla (Id=5)
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (5, 3, DATEADD(DAY,-24,GETDATE()), 5600, 0, 'QR', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pMarlboro,2,2800,5600);

-- V3: hace 23 dias - Tarjeta - Sofia Beltran (Id=6)
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (6, 4, DATEADD(DAY,-23,GETDATE()), 5450, 0, 'Tarjeta', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pQuilmes,2,1250,2500),(@vId,@pLays,1,850,850),(@vId,@pAlfMilka,1,700,700),(@vId,@pRocklets,1,500,500),(@vId,@pAgua500,1,550,550);

-- V4: hace 22 dias - Cuenta Corriente - Diego Acosta (Id=7)
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (7, 6, DATEADD(DAY,-22,GETDATE()), 6500, 0, 'Cuenta Corriente', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pFernet,1,6500,6500);

-- V5: hace 21 dias - ANULADA - Efectivo
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 4, DATEADD(DAY,-21,GETDATE()), 2100, 0, 'Efectivo', 'Anulada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pDerby,1,2100,2100);

-- V6: hace 19 dias - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 3, DATEADD(DAY,-19,GETDATE()), 3670, 0, 'Efectivo', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pLucky,1,2750,2750),(@vId,@pSprite,1,920,920);

-- V7: hace 17 dias - QR con descuento - Elena Vazquez (Id=8)
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (8, 6, DATEADD(DAY,-17,GETDATE()), 2480, 250, 'QR', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pDoritos,1,880,880),(@vId,@pOreo,1,1400,1400),(@vId,@pAlfJorg,1,450,450);

-- V8: hace 15 dias - Tarjeta - Lucas Herrera (Id=9)
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (9, 7, DATEADD(DAY,-15,GETDATE()), 8150, 0, 'Tarjeta', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pFernet,1,6500,6500),(@vId,@pCoca15,1,1650,1650);

-- V9: hace 13 dias - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 3, DATEADD(DAY,-13,GETDATE()), 1750, 0, 'Efectivo', 'Completada', @c1);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pAgua500,2,550,1100),(@vId,@pRocklets,1,500,500),(@vId,@pMentitas,1,150,150);

-- V10: hace 11 dias - QR - Roberto Mansilla
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (5, 4, DATEADD(DAY,-11,GETDATE()), 4850, 0, 'QR', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pDerby,1,2100,2100),(@vId,@pGatorade,1,1100,1100),(@vId,@pAlfMilka,1,700,700),(@vId,@pPepsi500,1,900,900);

-- V11: hace 10 dias - Tarjeta - Sofia Beltran
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (6, 6, DATEADD(DAY,-10,GETDATE()), 3800, 0, 'Tarjeta', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pQuilmes,2,1250,2500),(@vId,@pLays,1,850,850),(@vId,@pAlfJorg,1,450,450);

-- V12: hace 8 dias - Cuenta Corriente - Lucas Herrera
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (9, 7, DATEADD(DAY,-8,GETDATE()), 2800, 0, 'Cuenta Corriente', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pMarlboro,1,2800,2800);

-- V13: hace 6 dias - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 3, DATEADD(DAY,-6,GETDATE()), 5100, 0, 'Efectivo', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pOreo,2,1400,2800),(@vId,@pCoca500,2,950,1900),(@vId,@pAlfJorg,1,450,450);

-- V14: hace 4 dias - QR con descuento supervisor - Diego Acosta
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (7, 4, DATEADD(DAY,-4,GETDATE()), 9700, 500, 'QR', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pFernet,1,6500,6500),(@vId,@pQuilmes,2,1250,2500),(@vId,@pLays,1,850,850);

-- V15: hace 2 dias - Tarjeta - Consumidor Final
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 6, DATEADD(DAY,-2,GETDATE()), 2100, 0, 'Tarjeta', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pDerby,1,2100,2100);

-- V16: ayer - Efectivo - Elena Vazquez
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (8, 7, DATEADD(DAY,-1,GETDATE()), 3200, 0, 'Efectivo', 'Completada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pGatorade,2,1100,2200),(@vId,@pDoritos,1,880,880),(@vId,@pMentitas,1,150,150);

-- V17: ayer - ANULADA - Efectivo
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 3, DATEADD(HOUR,-3,GETDATE()), 2800, 0, 'Efectivo', 'Anulada', @c2);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pMarlboro,1,2800,2800);

-- V18: hoy - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (1, 3, DATEADD(MINUTE,-90,GETDATE()), 1700, 0, 'Efectivo', 'Completada', NULL);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pCoca500,1,950,950),(@vId,@pAlfMilka,1,700,700),(@vId,@pMentitas,1,150,150);

-- V19: hoy - QR - Roberto Mansilla
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (5, 6, DATEADD(MINUTE,-60,GETDATE()), 1650, 0, 'QR', 'Completada', NULL);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pCoca15,1,1650,1650);

-- V20: hoy - Tarjeta - Sofia Beltran
INSERT INTO Ventas (IdCliente,IdUsuario,Fecha,Total,Descuento,MetodoPago,Estado,IdCierreCaja)
VALUES (6, 4, DATEADD(MINUTE,-30,GETDATE()), 2500, 0, 'Tarjeta', 'Completada', NULL);
SET @vId = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta,IdProducto,Cantidad,PrecioUnitario,Subtotal) VALUES (@vId,@pQuilmes,2,1250,2500);

END -- IF ventas < 15


-- ============================================================
-- MOVIMIENTOS DE CAJA
-- ============================================================
IF (SELECT COUNT(*) FROM MovimientosCaja) < 3
BEGIN
    INSERT INTO MovimientosCaja (IdUsuario,Fecha,Tipo,Monto,Motivo,IdCierreCaja)
    VALUES
    (2, DATEADD(DAY,-27,GETDATE()), 'Retiro',  5000, 'Pago proveedor golosinas',    @c1),
    (2, DATEADD(DAY,-20,GETDATE()), 'Ingreso', 2000, 'Reposicion fondo de caja',    @c1),
    (5, DATEADD(DAY,-13,GETDATE()), 'Retiro',  3500, 'Pago servicios luz/agua',     @c1),
    (2, DATEADD(DAY, -6,GETDATE()), 'Retiro',  8000, 'Compra mercaderia urgente',   @c2),
    (5, DATEADD(DAY, -2,GETDATE()), 'Ingreso', 1500, 'Devolucion proveedor',        @c2),
    (2, DATEADD(DAY, -1,GETDATE()), 'Retiro',  2500, 'Gastos varios - limpieza',    @c2);
END

-- ============================================================
-- REPORTE FINAL
-- ============================================================
SELECT 'Categorias'    AS Tabla, COUNT(*) AS Registros FROM Categorias   UNION ALL
SELECT 'Productos',    COUNT(*) FROM Productos                            UNION ALL
SELECT 'Usuarios',     COUNT(*) FROM Usuarios                             UNION ALL
SELECT 'Clientes',     COUNT(*) FROM Clientes                             UNION ALL
SELECT 'Ventas',       COUNT(*) FROM Ventas                               UNION ALL
SELECT 'DetallesVenta',COUNT(*) FROM DetallesVenta                        UNION ALL
SELECT 'CierresCaja',  COUNT(*) FROM CierresCaja                          UNION ALL
SELECT 'MovimientosCaja', COUNT(*) FROM MovimientosCaja;
