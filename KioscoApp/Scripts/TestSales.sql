USE KioscoDB;
GO

-- Insertar Clientes
INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia) 
VALUES ('11223344', 'Juan', 'Perez', '351222333', 'juan@test.com', 'Av. Colon', '1234', 'Cordoba', 'Cordoba');

INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia) 
VALUES ('55667788', 'Maria', 'Gomez', '351555666', 'maria@test.com', 'Gral. Paz', '432', 'Cordoba', 'Cordoba');

-- Insertar Ventas
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES ((SELECT Id FROM Clientes WHERE Dni = '00000000'), 1, DATEADD(hour, -2, GETDATE()), 4500.00, 0, 'Efectivo', 'Completada');

INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES ((SELECT Id FROM Clientes WHERE Dni = '11223344'), 1, DATEADD(minute, -30, GETDATE()), 7200.00, 0, 'MercadoPago / QR', 'Completada');

INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado)
VALUES ((SELECT Id FROM Clientes WHERE Dni = '55667788'), 1, DATEADD(minute, -5, GETDATE()), 1150.00, 150.00, 'Tarjeta (Débito/Crédito)', 'Completada');

-- Insertar Detalles (Simulados referenciando los IDs)
-- Venta 1
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES ((SELECT TOP 1 Id FROM Ventas ORDER BY Id ASC), 1, 1, 4500.00, 4500.00);

-- Venta 2
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES ((SELECT TOP 1 Id FROM Ventas WHERE Total = 7200.00), 2, 2, 3600.00, 7200.00);

-- Venta 3
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES ((SELECT TOP 1 Id FROM Ventas WHERE Total = 1150.00), 3, 1, 1300.00, 1300.00);
GO
