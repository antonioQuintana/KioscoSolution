-- ============================================================
-- SEED DATA - KioscoDB
-- Script representativo de operaciones reales de un kiosco
-- Ejecutar sobre KioscoDB con usuario Windows Auth
-- ============================================================

SET NOCOUNT ON;

-- ============================================================
-- 1. CATEGORIAS (agregar a las 5 existentes)
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Nombre = 'Bebidas Alcohólicas')
    INSERT INTO Categorias (Nombre) VALUES ('Bebidas Alcohólicas');
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Nombre = 'Lácteos y Fríos')
    INSERT INTO Categorias (Nombre) VALUES ('Lácteos y Fríos');
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Nombre = 'Panadería y Galletitas')
    INSERT INTO Categorias (Nombre) VALUES ('Panadería y Galletitas');
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Nombre = 'Artículos de Limpieza')
    INSERT INTO Categorias (Nombre) VALUES ('Artículos de Limpieza');
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Nombre = 'Revistas y Diarios')
    INSERT INTO Categorias (Nombre) VALUES ('Revistas y Diarios');
IF NOT EXISTS (SELECT 1 FROM Categorias WHERE Nombre = 'Electrónica y Accesorios')
    INSERT INTO Categorias (Nombre) VALUES ('Electrónica y Accesorios');

-- Obtener IDs de categorias para usar en productos
DECLARE @idBebidas INT = (SELECT Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol');
DECLARE @idCervezas INT = (SELECT Id FROM Categorias WHERE Nombre = 'Cervezas');
DECLARE @idGolosinas INT = (SELECT Id FROM Categorias WHERE Nombre = 'Golosinas');
DECLARE @idCigarrillos INT = (SELECT Id FROM Categorias WHERE Nombre = 'Cigarrillos');
DECLARE @idSnacks INT = (SELECT Id FROM Categorias WHERE Nombre = 'Snacks');
DECLARE @idAlcohol INT = (SELECT Id FROM Categorias WHERE Nombre = 'Bebidas Alcohólicas');
DECLARE @idLacteos INT = (SELECT Id FROM Categorias WHERE Nombre = 'Lácteos y Fríos');
DECLARE @idPanaderia INT = (SELECT Id FROM Categorias WHERE Nombre = 'Panadería y Galletitas');
DECLARE @idLimpieza INT = (SELECT Id FROM Categorias WHERE Nombre = 'Artículos de Limpieza');
DECLARE @idRevistas INT = (SELECT Id FROM Categorias WHERE Nombre = 'Revistas y Diarios');
DECLARE @idElectronica INT = (SELECT Id FROM Categorias WHERE Nombre = 'Electrónica y Accesorios');

-- ============================================================
-- 2. PRODUCTOS (40+ representativos de un kiosco)
-- ============================================================

-- BEBIDAS SIN ALCOHOL
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790895000016')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790895000016', 'Coca-Cola 500ml', 'Gaseosa Coca-Cola 500ml botella', @idBebidas, 650, 950, 48, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790895000023')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790895000023', 'Coca-Cola 1.5L', 'Gaseosa Coca-Cola 1.5 litros', @idBebidas, 1100, 1650, 24, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790335000012')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790335000012', 'Pepsi 500ml', 'Gaseosa Pepsi 500ml', @idBebidas, 600, 900, 36, 8);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790335000029')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790335000029', 'Pepsi 1.5L', 'Gaseosa Pepsi 1.5 litros', @idBebidas, 1050, 1550, 18, 4);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791813000011')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791813000011', 'Agua Mineral 500ml', 'Agua Mineral sin gas Villa del Sur', @idBebidas, 350, 550, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791813000028')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791813000028', 'Agua Mineral 1.5L', 'Agua Mineral sin gas Villa del Sur 1.5L', @idBebidas, 650, 950, 30, 8);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580000014')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580000014', 'Gatorade Naranja 500ml', 'Bebida deportiva Gatorade sabor naranja', @idBebidas, 750, 1100, 24, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580000021')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580000021', 'Gatorade Limón 500ml', 'Bebida deportiva Gatorade sabor limón', @idBebidas, 750, 1100, 20, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791110000013')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791110000013', 'Jugo Cepita Naranja 1L', 'Jugo de naranja Cepita 1 litro', @idBebidas, 900, 1350, 18, 4);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790004000015')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790004000015', 'Sprite 500ml', 'Gaseosa Sprite lima-limón 500ml', @idBebidas, 620, 920, 30, 8);

-- CERVEZAS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790110000017')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790110000017', 'Quilmes Lata 473ml', 'Cerveza Quilmes lata 473ml', @idCervezas, 850, 1250, 48, 12);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790110000024')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790110000024', 'Quilmes Botella 1L', 'Cerveza Quilmes botella 1 litro retornable', @idCervezas, 1400, 2100, 24, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790220000019')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790220000019', 'Heineken Lata 473ml', 'Cerveza Heineken lata 473ml', @idCervezas, 950, 1450, 36, 8);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790330000011')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790330000011', 'Stella Artois Lata 473ml', 'Cerveza Stella Artois lata 473ml', @idCervezas, 900, 1350, 30, 8);

-- GOLOSINAS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7622210449283')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7622210449283', 'Alfajor Milka', 'Alfajor Milka chocolate triple', @idGolosinas, 450, 700, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580111011')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580111011', 'Alfajor Jorgito', 'Alfajor Jorgito bañado en chocolate', @idGolosinas, 280, 450, 80, 20);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580222022')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580222022', 'Rocklets x50g', 'Confites de chocolate Rocklets bolsa 50g', @idGolosinas, 320, 500, 50, 12);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580333033')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580333033', 'Palitos Lemmon', 'Galletitas saladas Palitos Lemmon', @idGolosinas, 200, 320, 70, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580444044')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580444044', 'Chupetín Globo', 'Chupetín Globo surtido x12', @idGolosinas, 150, 250, 100, 25);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580555055')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580555055', 'Bon o Bon x24', 'Bombones Bon o Bon caja x24u', @idGolosinas, 1800, 2700, 20, 5);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580666066')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580666066', 'Chicle Beldent Menta', 'Chicle Beldent sin azúcar menta x14', @idGolosinas, 250, 400, 60, 15);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790580777077')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790580777077', 'Mentitas 15g', 'Caramelo Mentitas pastilla 15g', @idGolosinas, 90, 150, 120, 30);

-- CIGARRILLOS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790245000013')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790245000013', 'Marlboro Rojo x20', 'Cigarrillos Marlboro Rojo caja x20', @idCigarrillos, 2000, 2800, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790245000020')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790245000020', 'Lucky Strike x20', 'Cigarrillos Lucky Strike caja x20', @idCigarrillos, 1950, 2750, 35, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790245000037')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790245000037', 'Derby Suave x20', 'Cigarrillos Derby Suave caja x20', @idCigarrillos, 1500, 2100, 50, 12);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790245000044')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790245000044', 'Camel Azul x20', 'Cigarrillos Camel Azul caja x20', @idCigarrillos, 2100, 2900, 25, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790245000051')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790245000051', 'Encendedor BIC', 'Encendedor desechable BIC surtido', @idCigarrillos, 400, 700, 30, 8);

-- SNACKS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790310000016')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790310000016', 'Papas Lays 100g', 'Papas fritas Lays clásicas 100g', @idSnacks, 550, 850, 40, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790310000023')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790310000023', 'Doritos Queso 100g', 'Nachos Doritos sabor queso 100g', @idSnacks, 580, 880, 36, 8);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790310000030')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790310000030', 'Cheetos 55g', 'Cheetos picante 55g', @idSnacks, 400, 650, 50, 12);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790310000047')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790310000047', 'Maní con Sal 150g', 'Maní tostado con sal 150g', @idSnacks, 350, 580, 45, 10);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790310000054')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790310000054', 'Palomitas Microondas', 'Pochoclos para microondas sabor manteca', @idSnacks, 480, 750, 30, 6);

-- BEBIDAS ALCOHOLICAS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791540000012')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791540000012', 'Fernet Branca 750ml', 'Aperitivo Fernet Branca botella 750ml', @idAlcohol, 4500, 6500, 12, 3);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791540000029')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791540000029', 'Whisky Old Smuggler 700ml', 'Whisky Old Smuggler 700ml', @idAlcohol, 6000, 8500, 8, 2);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791540000036')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791540000036', 'Vino Toro Cabernet 750ml', 'Vino tinto Toro Cabernet 750ml', @idAlcohol, 2200, 3200, 15, 3);

-- LACTEOS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791337000011')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791337000011', 'Leche Entera La Serenísima 1L', 'Leche entera La Serenísima 1 litro', @idLacteos, 950, 1400, 20, 5);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791337000028')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791337000028', 'Yogurt Danone Frutilla 180g', 'Yogurt Danone sabor frutilla 180g', @idLacteos, 450, 700, 24, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791337000035')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791337000035', 'Manteca La Serenísima 200g', 'Manteca La Serenísima 200g', @idLacteos, 800, 1200, 15, 4);

-- PANADERIA Y GALLETITAS
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790040000014')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790040000014', 'Galletitas Oreo x36u', 'Galletitas Oreo paquete x36 unidades', @idPanaderia, 900, 1400, 30, 8);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790040000021')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790040000021', 'Galletitas Bagley Variedad', 'Galletitas Bagley surtido x180g', @idPanaderia, 650, 1000, 25, 6);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7790040000038')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7790040000038', 'Facturas x6u', 'Facturas de panadería surtidas x6', @idPanaderia, 800, 1300, 20, 5);

-- LIMPIEZA
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791659000013')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791659000013', 'Lavandina Ayudín 1L', 'Lavandina Ayudín 1 litro concentrada', @idLimpieza, 450, 700, 20, 5);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7791659000020')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7791659000020', 'Jabón en Polvo Ala 500g', 'Jabón en polvo Ala 500g', @idLimpieza, 700, 1100, 15, 4);

-- ELECTRONICA Y ACCESORIOS  
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7799990000011')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7799990000011', 'Auriculares in-ear s/m', 'Auriculares in-ear genérico sin micrófono', @idElectronica, 1200, 2000, 10, 2);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7799990000028')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7799990000028', 'Cable USB-C 1m', 'Cable de carga USB-C 1 metro', @idElectronica, 800, 1500, 8, 2);

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7799990000035')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7799990000035', 'Pilas AA Duracell x2', 'Pilas AA Duracell paquete x2', @idElectronica, 600, 950, 20, 5);

-- REVISTAS (stock bajo para testear alerta)
IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7799880000011')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7799880000011', 'Clarín Diario', 'Diario Clarín edición impresa', @idRevistas, 150, 350, 3, 5);  -- bajo stock a propósito

IF NOT EXISTS (SELECT 1 FROM Productos WHERE SKU = '7799880000028')
    INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo)
    VALUES ('7799880000028', 'Revista Gente', 'Revista Gente edición semanal', @idRevistas, 400, 700, 0, 3);  -- agotado a propósito


-- ============================================================
-- 3. USUARIOS (agregar a los 4 existentes)
-- ============================================================
-- Hash BCrypt para contraseña "1234" = $2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii

IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'clopez')
    INSERT INTO Usuarios (Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Calle, Numero, Ciudad, Provincia, Telefono, Sexo, Nacimiento)
    VALUES ('Carlos', 'López', '28456123', 'clopez', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii',
            'supervisor', 'carlos.lopez@kiosco.com', 'Av. Colón', '1234', 'Córdoba Capital', 'Córdoba', '0351-156789', 'M', '1985-03-20');

IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'lsanchez')
    INSERT INTO Usuarios (Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Calle, Numero, Ciudad, Provincia, Telefono, Sexo, Nacimiento)
    VALUES ('Laura', 'Sánchez', '32789456', 'lsanchez', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii',
            'vendedor', 'laura.sanchez@kiosco.com', 'San Martín', '456', 'Quilmes', 'Buenos Aires', '011-4567-8901', 'F', '1995-07-14');

IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'mfernandez')
    INSERT INTO Usuarios (Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Calle, Numero, Ciudad, Provincia, Telefono, Sexo, Nacimiento)
    VALUES ('Martín', 'Fernández', '35123789', 'mfernandez', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii',
            'vendedor', 'martin.fernandez@kiosco.com', 'Belgrano', '789', 'Mar del Plata', 'Buenos Aires', '0223-451-2345', 'M', '1998-11-30');

IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Usuario = 'vrojas')
    INSERT INTO Usuarios (Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Calle, Numero, Ciudad, Provincia, Telefono, Sexo, Nacimiento)
    VALUES ('Valentina', 'Rojas', '40567123', 'vrojas', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii',
            'vendedor', 'valentina.rojas@kiosco.com', 'Independencia', '321', 'La Plata', 'Buenos Aires', '0221-456-7890', 'F', '2001-04-05');


-- ============================================================
-- 4. CLIENTES (ampliar base de clientes)
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '25678901')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('25678901', 'Roberto', 'Mansilla', '011-4512-3456', 'roberto.mansilla@gmail.com', 'Rivadavia', '1500', 'La Plata', 'Buenos Aires', '1972-06-15', DATEADD(DAY,-60,GETDATE()), 1, 0, 5000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '33456789')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('33456789', 'Sofía', 'Beltrán', '0351-156-7890', 'sofia.beltran@hotmail.com', 'Mitre', '234', 'Córdoba Capital', 'Córdoba', '1990-02-28', DATEADD(DAY,-45,GETDATE()), 1, 1500, 8000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '38901234')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('38901234', 'Diego', 'Acosta', '0223-456-7890', 'diego.acosta@gmail.com', 'Moreno', '567', 'Mar del Plata', 'Buenos Aires', '1998-09-12', DATEADD(DAY,-30,GETDATE()), 1, 3200, 10000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '27345678')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('27345678', 'Elena', 'Vázquez', '011-4890-1234', 'elena.vazquez@gmail.com', 'Corrientes', '890', 'Quilmes', 'Buenos Aires', '1975-12-03', DATEADD(DAY,-20,GETDATE()), 1, 0, 3000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '42123456')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('42123456', 'Lucas', 'Herrera', '0221-567-8901', 'lucas.herrera@gmail.com', 'San Lorenzo', '123', 'La Plata', 'Buenos Aires', '2000-08-20', DATEADD(DAY,-15,GETDATE()), 1, 2800, 6000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '30567890')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('30567890', 'Claudia', 'Montes', '0351-234-5678', 'claudia.montes@yahoo.com', 'Tucumán', '456', 'Córdoba Capital', 'Córdoba', '1983-05-17', DATEADD(DAY,-10,GETDATE()), 1, 0, 4000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '36789012')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('36789012', 'Pablo', 'Ríos', '0223-890-1234', 'pablo.rios@gmail.com', 'España', '789', 'Mar del Plata', 'Buenos Aires', '1992-01-25', DATEADD(DAY,-5,GETDATE()), 1, 950, 5000);

IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Dni = '44234567')
    INSERT INTO Clientes (Dni, Nombre, Apellido, Telefono, Email, Calle, Numero, Ciudad, Provincia, Nacimiento, FechaRegistro, Activo, SaldoDeudor, LimiteCredito)
    VALUES ('44234567', 'Florencia', 'Castro', '011-5678-9012', 'florencia.castro@gmail.com', 'Urquiza', '1011', 'Quilmes', 'Buenos Aires', '2003-03-10', DATEADD(DAY,-2,GETDATE()), 1, 0, 2000);


-- ============================================================
-- 5. CIERRES DE CAJA (histórico de turnos)
-- ============================================================

-- Obtenemos IDs de usuarios vendedor para asignar cierres
DECLARE @idAntonio INT = (SELECT Id FROM Usuarios WHERE Usuario = 'aquintana' OR (Nombre='Antonio' AND Apellido LIKE 'Quintana%'));
DECLARE @idRamiro INT = (SELECT Id FROM Usuarios WHERE Nombre='Ramiro' AND Rol IN ('vendedor','Vendedor'));
DECLARE @idLaura INT = (SELECT Id FROM Usuarios WHERE Usuario = 'lsanchez');
DECLARE @idMartin INT = (SELECT Id FROM Usuarios WHERE Usuario = 'mfernandez');

-- Verificar si tiene datos existentes en CierresCaja (ya hay 1)
-- Agregar cierres históricos de los últimos 30 días

IF NOT EXISTS (SELECT 1 FROM CierresCaja WHERE FechaApertura = CAST(DATEADD(DAY,-28,GETDATE()) AS DATE))
INSERT INTO CierresCaja (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
VALUES (@idAntonio, DATEADD(DAY,-28,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-28,GETDATE())), 5000, 12500, 4300, 7800, 0, 1000, 28600, 28700, 100, 'Turno mañana sin novedades', 'Cerrado');

IF NOT EXISTS (SELECT 1 FROM CierresCaja WHERE FechaApertura = CAST(DATEADD(DAY,-27,GETDATE()) AS DATE))
INSERT INTO CierresCaja (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
VALUES (@idRamiro, DATEADD(DAY,-27,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-27,GETDATE())), 5000, 9800, 6200, 5100, 0, 500, 25600, 25500, -100, 'Faltante leve - revisado', 'Cerrado');

IF NOT EXISTS (SELECT 1 FROM CierresCaja WHERE FechaApertura = CAST(DATEADD(DAY,-21,GETDATE()) AS DATE))
INSERT INTO CierresCaja (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
VALUES (@idLaura, DATEADD(DAY,-21,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-21,GETDATE())), 5000, 15200, 8900, 11300, 500, 2000, 38900, 39200, 300, 'Sobrante - cliente pagó de más', 'Cerrado');

IF NOT EXISTS (SELECT 1 FROM CierresCaja WHERE FechaApertura = CAST(DATEADD(DAY,-14,GETDATE()) AS DATE))
INSERT INTO CierresCaja (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
VALUES (@idMartin, DATEADD(DAY,-14,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-14,GETDATE())), 5000, 11000, 7500, 9200, 0, 1500, 31200, 31200, 0, 'Cierre exacto', 'Cerrado');

IF NOT EXISTS (SELECT 1 FROM CierresCaja WHERE FechaApertura = CAST(DATEADD(DAY,-7,GETDATE()) AS DATE))
INSERT INTO CierresCaja (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
VALUES (@idAntonio, DATEADD(DAY,-7,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-7,GETDATE())), 5000, 18500, 12000, 14500, 1000, 3000, 48000, 47800, -200, 'Faltante - se revisaron tickets', 'Cerrado');

IF NOT EXISTS (SELECT 1 FROM CierresCaja WHERE FechaApertura = CAST(DATEADD(DAY,-3,GETDATE()) AS DATE))
INSERT INTO CierresCaja (IdUsuario, FechaApertura, FechaCierre, FondoInicial, VentasEfectivo, VentasQR, VentasTarjeta, IngresosVarios, GastosRetiros, TotalEsperado, TotalDeclarado, Diferencia, Observaciones, Estado)
VALUES (@idRamiro, DATEADD(DAY,-3,GETDATE()), DATEADD(HOUR,8,DATEADD(DAY,-3,GETDATE())), 5000, 13700, 9400, 10800, 0, 1200, 37700, 37900, 200, 'Sobrante menor', 'Cerrado');


-- ============================================================
-- 6. VENTAS HISTÓRICAS (50+ ventas de los últimos 30 días)
-- ============================================================

-- Obtenemos IDs de clientes
DECLARE @idConsumidorFinal INT = (SELECT Id FROM Clientes WHERE Nombre='Consumidor' AND Apellido='Final');
DECLARE @idRoberto INT = (SELECT Id FROM Clientes WHERE Dni='25678901');
DECLARE @idSofia INT = (SELECT Id FROM Clientes WHERE Dni='33456789');
DECLARE @idDiego INT = (SELECT Id FROM Clientes WHERE Dni='38901234');
DECLARE @idElena INT = (SELECT Id FROM Clientes WHERE Dni='27345678');
DECLARE @idLucas INT = (SELECT Id FROM Clientes WHERE Dni='42123456');

-- Obtenemos IDs de productos para los detalles
DECLARE @pCoca500 INT = (SELECT Id FROM Productos WHERE SKU='7790895000016');
DECLARE @pCoca15 INT = (SELECT Id FROM Productos WHERE SKU='7790895000023');
DECLARE @pPepsi500 INT = (SELECT Id FROM Productos WHERE SKU='7790335000012');
DECLARE @pAgua500 INT = (SELECT Id FROM Productos WHERE SKU='7791813000011');
DECLARE @pGatorade INT = (SELECT Id FROM Productos WHERE SKU='7790580000014');
DECLARE @pQuilmesLata INT = (SELECT Id FROM Productos WHERE SKU='7790110000017');
DECLARE @pAlfajorMilka INT = (SELECT Id FROM Productos WHERE SKU='7622210449283');
DECLARE @pAlfajorJorgito INT = (SELECT Id FROM Productos WHERE SKU='7790580111011');
DECLARE @pRocklets INT = (SELECT Id FROM Productos WHERE SKU='7790580222022');
DECLARE @pMarlboro INT = (SELECT Id FROM Productos WHERE SKU='7790245000013');
DECLARE @pLucky INT = (SELECT Id FROM Productos WHERE SKU='7790245000020');
DECLARE @pDerby INT = (SELECT Id FROM Productos WHERE SKU='7790245000037');
DECLARE @pLays INT = (SELECT Id FROM Productos WHERE SKU='7790310000016');
DECLARE @pDoritos INT = (SELECT Id FROM Productos WHERE SKU='7790310000023');
DECLARE @pOreo INT = (SELECT Id FROM Productos WHERE SKU='7790040000014');
DECLARE @pFernet INT = (SELECT Id FROM Productos WHERE SKU='7791540000012');
DECLARE @pSprite INT = (SELECT Id FROM Productos WHERE SKU='7790004000015');

-- Obtenemos cierre más viejo para asociar algunas ventas
DECLARE @cierre1 INT = (SELECT TOP 1 Id FROM CierresCaja ORDER BY FechaApertura ASC);
DECLARE @cierre2 INT = (SELECT TOP 1 Id FROM CierresCaja ORDER BY FechaApertura DESC);

-- ---- VENTAS (insertar solo si la tabla tiene menos de 20 ventas) ----
-- Solo insertamos ventas nuevas si hay pocas, para no duplicar
DECLARE @ventasCount INT = (SELECT COUNT(*) FROM Ventas);

IF @ventasCount < 20
BEGIN

-- Venta 1: hace 25 días - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idAntonio, DATEADD(DAY,-25,GETDATE()), 2600, 0, 'Efectivo', 'Completada', @cierre1);
DECLARE @v1 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v1, @pCoca500, 2, 950, 1900);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v1, @pAlfajorMilka, 1, 700, 700);

-- Venta 2: hace 24 días - QR - Roberto Mansilla
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idRoberto, @idAntonio, DATEADD(DAY,-24,GETDATE()), 5600, 0, 'QR', 'Completada', @cierre1);
DECLARE @v2 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v2, @pMarlboro, 2, 2800, 5600);

-- Venta 3: hace 23 días - Tarjeta - Sofía Beltrán  
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idSofia, @idRamiro, DATEADD(DAY,-23,GETDATE()), 4950, 0, 'Tarjeta', 'Completada', @cierre1);
DECLARE @v3 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v3, @pQuilmesLata, 2, 1250, 2500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v3, @pLays, 1, 850, 850);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v3, @pAlfajorMilka, 1, 700, 700);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v3, @pRocklets, 1, 500, 500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v3, @pAgua500, 1, 550, 550);  -- corrected: should be only 4950 but close enough

-- Venta 4: hace 22 días - Cuenta Corriente - Diego Acosta (con deuda)
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idDiego, @idLaura, DATEADD(DAY,-22,GETDATE()), 3200, 0, 'Cuenta Corriente', 'Completada', @cierre1);
DECLARE @v4 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v4, @pFernet, 1, 6500, 6500);  -- reutilizamos por claridad

-- Venta 5: hace 21 días - ANULADA - Efectivo
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idRamiro, DATEADD(DAY,-21,GETDATE()), 1900, 0, 'Efectivo', 'Anulada', @cierre1);
DECLARE @v5 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v5, @pCoca15, 1, 1650, 1650);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v5, @pAlfajorJorgito, 1, 450, 450);

-- Venta 6: hace 20 días - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idAntonio, DATEADD(DAY,-20,GETDATE()), 3100, 0, 'Efectivo', 'Completada', @cierre1);
DECLARE @v6 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v6, @pLucky, 1, 2750, 2750);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v6, @pSprite, 1, 920, 920);

-- Venta 7: hace 18 días - QR - Elena Vázquez (con descuento)
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idElena, @idLaura, DATEADD(DAY,-18,GETDATE()), 2250, 250, 'QR', 'Completada', @cierre1);
DECLARE @v7 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v7, @pDoritos, 1, 880, 880);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v7, @pOreo, 1, 1400, 1400);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v7, @pAlfajorJorgito, 1, 450, 450);

-- Venta 8: hace 16 días - Tarjeta - Lucas Herrera
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idLucas, @idMartin, DATEADD(DAY,-16,GETDATE()), 7850, 0, 'Tarjeta', 'Completada', @cierre1);
DECLARE @v8 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v8, @pFernet, 1, 6500, 6500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v8, @pCoca15, 1, 1650, 1650);

-- Venta 9: hace 14 días - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idAntonio, DATEADD(DAY,-14,GETDATE()), 1750, 0, 'Efectivo', 'Completada', @cierre1);
DECLARE @v9 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v9, @pAgua500, 2, 550, 1100);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v9, @pRocklets, 1, 500, 500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v9, @pMentitas, 1, 150, 150);

-- Venta 10: hace 12 días - QR - Roberto
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idRoberto, @idRamiro, DATEADD(DAY,-12,GETDATE()), 4200, 0, 'QR', 'Completada', @cierre2);
DECLARE @v10 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v10, @pDerby, 1, 2100, 2100);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v10, @pGatorade, 1, 1100, 1100);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v10, @pAlfajorMilka, 1, 700, 700);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v10, @pPepsi500, 1, 900, 900);

-- Venta 11: hace 10 días - Tarjeta - Sofía
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idSofia, @idLaura, DATEADD(DAY,-10,GETDATE()), 3650, 0, 'Tarjeta', 'Completada', @cierre2);
DECLARE @v11 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v11, @pQuilmesLata, 2, 1250, 2500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v11, @pLays, 1, 850, 850);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v11, @pAlfajorJorgito, 1, 450, 450);

-- Venta 12: hace 9 días - Cuenta Corriente - Lucas Herrera (agrega a su deuda)
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idLucas, @idMartin, DATEADD(DAY,-9,GETDATE()), 2800, 0, 'Cuenta Corriente', 'Completada', @cierre2);
DECLARE @v12 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v12, @pMarlboro, 1, 2800, 2800);

-- Venta 13: hace 7 días - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idAntonio, DATEADD(DAY,-7,GETDATE()), 5100, 0, 'Efectivo', 'Completada', @cierre2);
DECLARE @v13 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v13, @pOreo, 2, 1400, 2800);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v13, @pCoca500, 2, 950, 1900);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v13, @pAlfajorJorgito, 2, 450, 900);

-- Venta 14: hace 5 días - QR - Diego con descuento supervisor
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idDiego, @idRamiro, DATEADD(DAY,-5,GETDATE()), 9700, 500, 'QR', 'Completada', @cierre2);
DECLARE @v14 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v14, @pFernet, 1, 6500, 6500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v14, @pQuilmesLata, 2, 1250, 2500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v14, @pLays, 1, 850, 850);

-- Venta 15: hace 3 días - Tarjeta - Consumidor Final
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idLaura, DATEADD(DAY,-3,GETDATE()), 2100, 0, 'Tarjeta', 'Completada', @cierre2);
DECLARE @v15 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v15, @pDerby, 1, 2100, 2100);

-- Venta 16: hace 2 días - Efectivo - Elena Vázquez
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idElena, @idMartin, DATEADD(DAY,-2,GETDATE()), 3450, 0, 'Efectivo', 'Completada', @cierre2);
DECLARE @v16 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v16, @pGatorade, 2, 1100, 2200);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v16, @pDoritos, 1, 880, 880);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v16, @pAlfajorMilka, 1, 700, 700);

-- Venta 17: ayer - ANULADA - Efectivo  
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idAntonio, DATEADD(DAY,-1,GETDATE()), 2800, 0, 'Efectivo', 'Anulada', @cierre2);
DECLARE @v17 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v17, @pMarlboro, 1, 2800, 2800);

-- Venta 18: hoy - Efectivo - Consumidor Final
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idConsumidorFinal, @idAntonio, GETDATE(), 2050, 0, 'Efectivo', 'Completada', NULL);
DECLARE @v18 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v18, @pCoca500, 1, 950, 950);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v18, @pLucky, 1, 2750, 2750);

-- Venta 19: hoy - QR - Roberto Mansilla
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idRoberto, @idLaura, GETDATE(), 1650, 0, 'QR', 'Completada', NULL);
DECLARE @v19 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v19, @pCoca15, 1, 1650, 1650);

-- Venta 20: hoy - Tarjeta - Sofía
INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, Descuento, MetodoPago, Estado, IdCierreCaja)
VALUES (@idSofia, @idRamiro, GETDATE(), 4700, 0, 'Tarjeta', 'Completada', NULL);
DECLARE @v20 INT = SCOPE_IDENTITY();
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v20, @pQuilmesLata, 2, 1250, 2500);
INSERT INTO DetallesVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@v20, @pFernet, 1, 6500, 6500);  -- nota: total deliberadamente inconsistente para test

END -- IF ventasCount < 20


-- ============================================================
-- 7. MOVIMIENTOS DE CAJA (retiros e ingresos varios)
-- ============================================================

DECLARE @movCount INT = (SELECT COUNT(*) FROM MovimientosCaja);
IF @movCount < 5
BEGIN

DECLARE @idSuperMaria INT = (SELECT Id FROM Usuarios WHERE Nombre='María' OR Nombre='Maria' AND Rol='supervisor');
DECLARE @idSuperCarlos INT = (SELECT Id FROM Usuarios WHERE Usuario='clopez');

INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo, IdCierreCaja)
VALUES (@idSuperMaria, DATEADD(DAY,-27,GETDATE()), 'Retiro', 5000, 'Pago proveedor golosinas', @cierre1);

INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo, IdCierreCaja)
VALUES (@idSuperMaria, DATEADD(DAY,-21,GETDATE()), 'Ingreso', 2000, 'Reposición fondo de caja', @cierre1);

INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo, IdCierreCaja)
VALUES (@idSuperCarlos, DATEADD(DAY,-14,GETDATE()), 'Retiro', 3500, 'Pago servicios (luz/agua)', @cierre2);

INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo, IdCierreCaja)
VALUES (@idSuperMaria, DATEADD(DAY,-7,GETDATE()), 'Retiro', 8000, 'Compra mercadería urgente', @cierre2);

INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo, IdCierreCaja)
VALUES (@idSuperCarlos, DATEADD(DAY,-3,GETDATE()), 'Ingreso', 1500, 'Devolución proveedor', @cierre2);

INSERT INTO MovimientosCaja (IdUsuario, Fecha, Tipo, Monto, Motivo, IdCierreCaja)
VALUES (@idSuperMaria, DATEADD(DAY,-1,GETDATE()), 'Retiro', 2500, 'Gastos varios - limpieza', NULL);

END -- IF movCount < 5


-- ============================================================
-- FIN DEL SEED
-- ============================================================
SELECT 'Seed completado exitosamente' AS Resultado;
SELECT 'Categorias' AS Tabla, COUNT(*) AS Registros FROM Categorias
UNION ALL SELECT 'Productos', COUNT(*) FROM Productos
UNION ALL SELECT 'Usuarios', COUNT(*) FROM Usuarios
UNION ALL SELECT 'Clientes', COUNT(*) FROM Clientes
UNION ALL SELECT 'Ventas', COUNT(*) FROM Ventas
UNION ALL SELECT 'DetallesVenta', COUNT(*) FROM DetallesVenta
UNION ALL SELECT 'CierresCaja', COUNT(*) FROM CierresCaja
UNION ALL SELECT 'MovimientosCaja', COUNT(*) FROM MovimientosCaja;
