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
('Buenos Aires'), ('Córdoba'), ('Santa Fe'), ('Mendoza'), ('Tucumán'), ('Corrientes'), ('Chaco');
GO

-- Insertar Ciudades
DECLARE @IdBA INT = (SELECT Id FROM Provincias WHERE Nombre = 'Buenos Aires');
DECLARE @IdCBA INT = (SELECT Id FROM Provincias WHERE Nombre = 'Córdoba');
DECLARE @IdCorrientes INT = (SELECT Id FROM Provincias WHERE Nombre = 'Corrientes');
DECLARE @IdChaco INT = (SELECT Id FROM Provincias WHERE Nombre = 'Chaco');
DECLARE @IdSantaFe INT = (SELECT Id FROM Provincias WHERE Nombre = 'Santa Fe');
DECLARE @IdMendoza INT = (SELECT Id FROM Provincias WHERE Nombre = 'Mendoza');
DECLARE @IdTucuman INT = (SELECT Id FROM Provincias WHERE Nombre = 'Tucumán');

INSERT INTO Ciudades (ProvinciaId, Nombre) VALUES 
(@IdBA, 'La Plata'), (@IdBA, 'Mar del Plata'), (@IdBA, 'Bahía Blanca'), (@IdBA, 'Quilmes'),
(@IdCBA, 'Córdoba Capital'), (@IdCBA, 'Villa Carlos Paz'), (@IdCBA, 'Río Cuarto'),
(@IdCorrientes, 'Corrientes Capital'), (@IdCorrientes, 'Goya'), (@IdCorrientes, 'Paso de los Libres'), (@IdCorrientes, 'Curuzú Cuatiá'), (@IdCorrientes, 'Mercedes'), (@IdCorrientes, 'Bella Vista'), (@IdCorrientes, 'Ituzaingó'),
(@IdChaco, 'Resistencia'), (@IdChaco, 'Presidencia Roque Sáenz Peña'), (@IdChaco, 'Villa Ángela'), (@IdChaco, 'Barranqueras'), (@IdChaco, 'Fontana'), (@IdChaco, 'General José de San Martín'), (@IdChaco, 'Juan José Castelli'),
(@IdSantaFe, 'Rosario'), (@IdSantaFe, 'Santa Fe Capital'), (@IdSantaFe, 'Rafaela'), (@IdSantaFe, 'Venado Tuerto'),
(@IdMendoza, 'Mendoza Capital'), (@IdMendoza, 'San Rafael'), (@IdMendoza, 'Godoy Cruz'), (@IdMendoza, 'Luján de Cuyo'),
(@IdTucuman, 'San Miguel de Tucumán'), (@IdTucuman, 'Tafí Viejo'), (@IdTucuman, 'Concepción'), (@IdTucuman, 'Yerba Buena');
GO

-- Insertar Usuarios
INSERT INTO Usuarios 
(Nombre, Apellido, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
VALUES 
('Juan', 'Pérez', 'admin', '.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'admin', 'admin@kiosco.com', '+54 9 11 1234 5678', 'Av. Corrientes', '1234', 'La Plata', 'Buenos Aires', 'Masculino', '1985-05-15'),
('María', 'Gómez', 'super', '.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'supervisor', 'super@kiosco.com', '+54 9 351 987 6543', 'Av. Colón', '456', 'Córdoba Capital', 'Córdoba', 'Femenino', '1990-10-20'),
('Antonio', 'Quintana', 'antonio', '.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'vendedor', 'antonio.quintana@kiosco.com', '+54 9 11 2233 4455', 'Florida', '999', 'Mar del Plata', 'Buenos Aires', 'Masculino', '1995-02-28'),
('Ramiro', 'Nuñez', 'ramiro', '.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii', 'Vendedor', 'ramiro.nunez@gmail.com', '+54 9 379 456 7890', 'San Juan', '890', 'Corrientes Capital', 'Corrientes', 'Masculino', '1998-03-08');
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
