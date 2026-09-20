CREATE DATABASE KioscoDB;
GO
USE KioscoDB;
GO

-- 1. Crear tabla Provincias y Ciudades (Catalogos)
CREATE TABLE Provincias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE Ciudades (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProvinciaId INT FOREIGN KEY REFERENCES Provincias(Id),
    Nombre NVARCHAR(100) NOT NULL
);
GO

-- 2. Crear tabla Usuarios con la nueva estructura extendida
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NULL,
    Calle NVARCHAR(100) NULL,
    Numero NVARCHAR(20) NULL,
    Ciudad NVARCHAR(100) NULL,
    Provincia NVARCHAR(100) NULL,
    Telefono NVARCHAR(50) NULL,
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

INSERT INTO Ciudades (ProvinciaId, Nombre) VALUES 
(@IdBA, 'La Plata'), (@IdBA, 'Mar del Plata'), (@IdBA, 'Bahía Blanca'), (@IdBA, 'Quilmes'),
(@IdCBA, 'Córdoba Capital'), (@IdCBA, 'Villa Carlos Paz'), (@IdCBA, 'Río Cuarto'),
(@IdCorrientes, 'Corrientes Capital'), (@IdCorrientes, 'Goya'), (@IdCorrientes, 'Paso de los Libres'), (@IdCorrientes, 'Curuzú Cuatiá'), (@IdCorrientes, 'Mercedes'), (@IdCorrientes, 'Bella Vista'), (@IdCorrientes, 'Ituzaingó'),
(@IdChaco, 'Resistencia'), (@IdChaco, 'Presidencia Roque Sáenz Peña'), (@IdChaco, 'Villa Ángela'), (@IdChaco, 'Barranqueras'), (@IdChaco, 'Fontana'), (@IdChaco, 'General José de San Martín'), (@IdChaco, 'Juan José Castelli');
GO

-- Insertar Usuarios por defecto
INSERT INTO Usuarios (Nombre, Usuario, Contrasena, Rol) VALUES 
('Administrador', 'admin', '$2a$11$afsMHec27ND2ytaCsjAXU.DWw6N9vyfTB7d79bYNSkZvck/Ftb3Ky', 'Admin'),
('Antonio', 'antonio', '$2a$11$XnMk5SnPN6SjUzV8MYn5r./E3Q.MgZzUWDWhZQQGV56G6zDnUHYKG', 'Vendedor');
GO
