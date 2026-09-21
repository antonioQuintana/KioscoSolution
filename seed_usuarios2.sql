USE KioscoDB;
DELETE FROM Usuarios;
INSERT INTO Usuarios 
(Nombre, Apellido, Usuario, Contrasena, Rol, Email, Telefono, Calle, Numero, Ciudad, Provincia, Sexo, Nacimiento) 
VALUES 
(
    'Juan', 'Pérez', 'admin', 
    '$2a$11$afsMHec27ND2ytaCsjAXU.DWw6N9vyfTB7d79bYNSkZvck/Ftb3Ky', 
    'Admin', 
    'admin@kiosco.com', '+54 9 11 1234 5678', 
    'Av. Corrientes', '1234', 
    'La Plata', 'Buenos Aires', 
    'Masculino', '1985-05-15'
),
(
    'Antonio', 'Quintana', 'antonio', 
    '$2a$11$XnMk5SnPN6SjUzV8MYn5r./E3Q.MgZzUWDWhZQQGV56G6zDnUHYKG', 
    'Vendedor', 
    'antonio@kiosco.com', '+54 9 351 987 6543', 
    'San Martín', '456', 
    'Córdoba Capital', 'Córdoba', 
    'Masculino', '1992-10-20'
),
(
    'María', 'López', 'maria', 
    '$2a$11$afsMHec27ND2ytaCsjAXU.DWw6N9vyfTB7d79bYNSkZvck/Ftb3Ky', 
    'Vendedor', 
    'maria.lopez@gmail.com', '+54 9 379 456 7890', 
    'San Juan', '890', 
    'Corrientes Capital', 'Corrientes', 
    'Femenino', '1998-03-08'
);
