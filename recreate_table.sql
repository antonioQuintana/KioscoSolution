USE KioscoDB;
IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL
    DROP TABLE dbo.Usuarios;

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Usuario NVARCHAR(50) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,
    Rol NVARCHAR(20) NOT NULL
);

INSERT INTO Usuarios (Nombre, Usuario, Contrasena, Rol) VALUES ('Administrador', 'admin', '.DWw6N9vyfTB7d79bYNSkZvck/Ftb3Ky', 'Admin');
INSERT INTO Usuarios (Nombre, Usuario, Contrasena, Rol) VALUES ('Antonio', 'antonio', './E3Q.MgZzUWDWhZQQGV56G6zDnUHYKG', 'Vendedor');
