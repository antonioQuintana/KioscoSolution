USE master;
GO

-- Si la base de datos existe, la forzamos a cerrar conexiones y la borramos
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'KioscoDB')
BEGIN
    ALTER DATABASE KioscoDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE KioscoDB;
END
GO

CREATE DATABASE KioscoDB;
GO
USE KioscoDB;
GO

-- 1. Crear tabla Provincias
CREATE TABLE Provincias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- 2. Crear tabla Ciudades
CREATE TABLE Ciudades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProvinciaId INT FOREIGN KEY REFERENCES Provincias(Id),
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- 3. Crear tabla Usuarios con TODOS los campos
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NULL,
    Dni NVARCHAR(20) NULL UNIQUE,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NULL UNIQUE,
    Calle NVARCHAR(100) NULL,
    Numero NVARCHAR(20) NULL,
    Ciudad NVARCHAR(100) NULL,
    Provincia NVARCHAR(100) NULL,
    Telefono NVARCHAR(50) NULL UNIQUE,
    Sexo NVARCHAR(20) NULL,
    Nacimiento DATE NULL
);
GO

-- ==========================================
-- SEEDERS (Datos iniciales)
-- ==========================================

-- Insertar Provincias
INSERT INTO Provincias (Nombre) VALUES 
('Buenos Aires'), ('CÃ³rdoba'), ('Santa Fe'), ('Mendoza'), ('TucumÃ¡n'), ('Corrientes'), ('Chaco');
GO

-- Insertar Ciudades
DECLARE @IdBA INT = (SELECT Id FROM Provincias WHERE Nombre = 'Buenos Aires');
DECLARE @IdCBA INT = (SELECT Id FROM Provincias WHERE Nombre = 'CÃ³rdoba');
DECLARE @IdCorrientes INT = (SELECT Id FROM Provincias WHERE Nombre = 'Corrientes');
DECLARE @IdChaco INT = (SELECT Id FROM Provincias WHERE Nombre = 'Chaco');
DECLARE @IdSantaFe INT = (SELECT Id FROM Provincias WHERE Nombre = 'Santa Fe');
DECLARE @IdMendoza INT = (SELECT Id FROM Provincias WHERE Nombre = 'Mendoza');
DECLARE @IdTucuman INT = (SELECT Id FROM Provincias WHERE Nombre = 'TucumÃ¡n');

INSERT INTO Ciudades (ProvinciaId, Nombre) VALUES 
(@IdBA, 'La Plata'), (@IdBA, 'Mar del Plata'), (@IdBA, 'BahÃ­a Blanca'), (@IdBA, 'Quilmes'),
(@IdCBA, 'CÃ³rdoba Capital'), (@IdCBA, 'Villa Carlos Paz'), (@IdCBA, 'RÃ­o Cuarto'),
(@IdCorrientes, 'Corrientes Capital'), (@IdCorrientes, 'Goya'), (@IdCorrientes, 'Paso de los Libres'), (@IdCorrientes, 'CuruzÃº CuatiÃ¡'), (@IdCorrientes, 'Mercedes'), (@IdCorrientes, 'Bella Vista'), (@IdCorrientes, 'ItuzaingÃ³'),
(@IdChaco, 'Resistencia'), (@IdChaco, 'Presidencia Roque SÃ¡enz PeÃ±a'), (@IdChaco, 'Villa Ãngela'), (@IdChaco, 'Barranqueras'), (@IdChaco, 'Fontana'), (@IdChaco, 'General JosÃ© de San MartÃ­n'), (@IdChaco, 'Juan JosÃ© Castelli'),
(@IdSantaFe, 'Rosario'), (@IdSantaFe, 'Santa Fe Capital'), (@IdSantaFe, 'Rafaela'), (@IdSantaFe, 'Venado Tuerto'),
(@IdMendoza, 'Mendoza Capital'), (@IdMendoza, 'San Rafael'), (@IdMendoza, 'Godoy Cruz'), (@IdMendoza, 'LujÃ¡n de Cuyo'),
(@IdTucuman, 'San Miguel de TucumÃ¡n'), (@IdTucuman, 'TafÃ­ Viejo'), (@IdTucuman, 'ConcepciÃ³n'), (@IdTucuman, 'Yerba Buena');
GO

-- Insertar Usuarios
INSERT INTO Usuarios 
(Nombre, Apellido, Dni, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
VALUES 
('Juan', 'PÃ©rez', '11111111', 'admin', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'admin', 'admin@kiosco.com', '+54 9 11 1234 5678', 'Av. Corrientes', '1234', 'La Plata', 'Buenos Aires', 'Masculino', '1985-05-15'),
('MarÃ­a', 'GÃ³mez', '22222222', 'super', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'supervisor', 'super@kiosco.com', '+54 9 351 987 6543', 'Av. ColÃ³n', '456', 'CÃ³rdoba Capital', 'CÃ³rdoba', 'Femenino', '1990-10-20'),
('Antonio', 'Quintana', '33333333', 'antonio', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'vendedor', 'antonio.quintana@kiosco.com', '+54 9 11 2233 4455', 'Florida', '999', 'Mar del Plata', 'Buenos Aires', 'Masculino', '1995-02-28'),
('Ramiro', 'NuÃ±ez', '44444444', 'ramiro', '$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'Vendedor', 'ramiro.nunez@gmail.com', '+54 9 379 456 7890', 'San Juan', '890', 'Corrientes Capital', 'Corrientes', 'Masculino', '1998-03-08');
GO

-- =============================================
-- TABLA CATEGORIAS
-- =============================================
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NULL
);
GO

-- =============================================
-- TABLA PRODUCTOS
-- =============================================
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SKU NVARCHAR(50) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    IdCategoria INT NOT NULL FOREIGN KEY REFERENCES Categorias(Id),
    PrecioCosto DECIMAL(18,2) NOT NULL DEFAULT 0,
    PrecioVenta DECIMAL(18,2) NOT NULL DEFAULT 0,
    StockActual INT NOT NULL DEFAULT 0,
    StockMinimo INT NOT NULL DEFAULT 0
);
GO

-- =============================================
-- INSERTAR CATEGORIAS DE EJEMPLO
-- =============================================
INSERT INTO Categorias (Nombre, Descripcion) VALUES 
('Bebidas sin alcohol', 'Gaseosas, aguas, jugos'),
('Cervezas', 'Cervezas en lata y botella'),
('Golosinas', 'Alfajores, caramelos, chocolates'),
('Cigarrillos', 'Atados y accesorios'),
('Snacks', 'Papas fritas, chizitos, palitos');
GO
-- =============================================
-- INSERTAR PRODUCTOS DE EJEMPLO
-- =============================================
DECLARE @IdBebidas INT = (SELECT Id FROM Categorias WHERE Nombre = 'Bebidas sin alcohol');
DECLARE @IdGolosinas INT = (SELECT Id FROM Categorias WHERE Nombre = 'Golosinas');
DECLARE @IdSnacks INT = (SELECT Id FROM Categorias WHERE Nombre = 'Snacks');

INSERT INTO Productos (SKU, Nombre, Descripcion, IdCategoria, PrecioCosto, PrecioVenta, StockActual, StockMinimo) VALUES 
('7791234567890', 'Coca Cola 2L', 'Gaseosa Cola 2 Litros', @IdBebidas, 1500, 2200, 50, 10),
('7790987654321', 'Alfajor Jorgito', 'Alfajor de chocolate', @IdGolosinas, 300, 500, 100, 20),
('7791111222233', 'Papas Lays 150g', 'Papas fritas clasicas', @IdSnacks, 800, 1300, 30, 5);
GO
-- =============================================
-- TABLA CLIENTES
-- =============================================
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Dni NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(50) NULL,
    Email NVARCHAR(100) NULL,
    Calle NVARCHAR(100) NULL,
    Numero NVARCHAR(20) NULL,
    Ciudad NVARCHAR(100) NULL,
    Provincia NVARCHAR(100) NULL,
    Nacimiento DATE NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    Activo BIT NOT NULL DEFAULT 1
);
GO

-- Insertar Consumidor Final por defecto
INSERT INTO Clientes (Dni, Nombre, Apellido, Activo) VALUES ('00000000', 'Consumidor', 'Final', 1);
GO

-- =============================================
-- TABLA VENTAS
-- =============================================
CREATE TABLE Ventas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Clientes(Id),
    IdUsuario INT NULL FOREIGN KEY REFERENCES Usuarios(Id), -- Vendedor que la hizo
    Fecha Varchar(50) NOT NULL, -- O DATETIME
    Total DECIMAL(18,2) NOT NULL DEFAULT 0,
    Descuento DECIMAL(18,2) NOT NULL DEFAULT 0,
    MetodoPago NVARCHAR(50) NOT NULL,
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Completada'
);
GO
ALTER TABLE Ventas ALTER COLUMN Fecha DATETIME;
GO

-- =============================================
-- TABLA DETALLES_VENTA
-- =============================================
CREATE TABLE DetallesVenta (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL FOREIGN KEY REFERENCES Ventas(Id),
    IdProducto INT NOT NULL FOREIGN KEY REFERENCES Productos(Id),
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL
);
GO

