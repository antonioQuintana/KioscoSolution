USE KioscoDB;

-- 1. Crear tabla de Provincias
IF OBJECT_ID('dbo.Provincias', 'U') IS NULL
BEGIN
    CREATE TABLE Provincias (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL UNIQUE
    );
    INSERT INTO Provincias (Nombre) VALUES ('Buenos Aires'), ('Córdoba'), ('Santa Fe'), ('Mendoza'), ('Tucumán');
END

-- 2. Crear tabla de Ciudades
IF OBJECT_ID('dbo.Ciudades', 'U') IS NULL
BEGIN
    CREATE TABLE Ciudades (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProvinciaId INT FOREIGN KEY REFERENCES Provincias(Id),
        Nombre NVARCHAR(100) NOT NULL
    );
    -- Insertar ciudades de ejemplo
    DECLARE @IdBA INT = (SELECT Id FROM Provincias WHERE Nombre = 'Buenos Aires');
    DECLARE @IdCBA INT = (SELECT Id FROM Provincias WHERE Nombre = 'Córdoba');
    
    INSERT INTO Ciudades (ProvinciaId, Nombre) VALUES 
    (@IdBA, 'La Plata'), (@IdBA, 'Mar del Plata'), (@IdBA, 'Bahía Blanca'), (@IdBA, 'Quilmes'),
    (@IdCBA, 'Córdoba Capital'), (@IdCBA, 'Villa Carlos Paz'), (@IdCBA, 'Río Cuarto');
END

-- 3. Actualizar la tabla Usuarios
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Apellido' AND Object_ID = Object_ID(N'Usuarios'))
BEGIN
    ALTER TABLE Usuarios ADD 
        Apellido NVARCHAR(100) NULL,
        Email NVARCHAR(100) NULL,
        Calle NVARCHAR(100) NULL,
        Numero NVARCHAR(20) NULL,
        Ciudad NVARCHAR(100) NULL,
        Provincia NVARCHAR(100) NULL,
        Telefono NVARCHAR(50) NULL,
        Sexo NVARCHAR(20) NULL,
        Nacimiento DATE NULL;
END
