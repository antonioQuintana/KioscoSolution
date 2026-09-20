USE KioscoDB;

-- Borrar usuarios actuales
DELETE FROM Usuarios;

-- Insertar los usuarios con TODOS los datos completos
INSERT INTO Usuarios 
(Nombre, Apellido, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
VALUES 
(
    'Juan', 'Pérez', 'admin', 
    '.DWw6N9vyfTB7d79bYNSkZvck/Ftb3Ky', 
    'Admin', 
    'admin@kiosco.com', '+54 9 11 1234 5678', 
    'Av. Corrientes', '1234', 
    'La Plata', 'Buenos Aires', 
    'Masculino', '1985-05-15'
),
(
    'Antonio', 'Quintana', 'antonio', 
    './E3Q.MgZzUWDWhZQQGV56G6zDnUHYKG', 
    'Vendedor', 
    'antonio@kiosco.com', '+54 9 351 987 6543', 
    'San Martín', '456', 
    'Córdoba Capital', 'Córdoba', 
    'Masculino', '1992-10-20'
),
(
    'María', 'López', 'maria', 
    '.DWw6N9vyfTB7d79bYNSkZvck/Ftb3Ky', -- Contraseña: admin123
    'Vendedor', 
    'maria.lopez@gmail.com', '+54 9 379 456 7890', 
    'San Juan', '890', 
    'Corrientes Capital', 'Corrientes', 
    'Femenino', '1998-03-08'
);
