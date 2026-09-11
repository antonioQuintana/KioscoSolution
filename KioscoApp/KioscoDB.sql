CREATE DATABASE KioscoDB;
GO
USE KioscoDB;
GO
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) NOT NULL
);
GO
INSERT INTO Usuarios (Nombre, Usuario, Contrasena, Rol) VALUES ('Administrador', 'admin', 'admin123', 'Admin');
GO
