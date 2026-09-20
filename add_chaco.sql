USE KioscoDB;
BEGIN TRY
    INSERT INTO Provincias (Nombre) VALUES ('Chaco');
    DECLARE @IdChaco INT = (SELECT Id FROM Provincias WHERE Nombre = 'Chaco');
    
    INSERT INTO Ciudades (ProvinciaId, Nombre) VALUES 
    (@IdChaco, 'Resistencia'),
    (@IdChaco, 'Presidencia Roque Sáenz Peña'),
    (@IdChaco, 'Villa Ángela'),
    (@IdChaco, 'Barranqueras'),
    (@IdChaco, 'Fontana'),
    (@IdChaco, 'General José de San Martín'),
    (@IdChaco, 'Juan José Castelli');
    PRINT 'Agregado correctamente';
END TRY
BEGIN CATCH
    PRINT 'Error o la provincia ya existe';
END CATCH
