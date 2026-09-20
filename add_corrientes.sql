USE KioscoDB;
BEGIN TRY
    INSERT INTO Provincias (Nombre) VALUES ('Corrientes');
    DECLARE @IdCorrientes INT = (SELECT Id FROM Provincias WHERE Nombre = 'Corrientes');
    
    INSERT INTO Ciudades (ProvinciaId, Nombre) VALUES 
    (@IdCorrientes, 'Corrientes Capital'),
    (@IdCorrientes, 'Goya'),
    (@IdCorrientes, 'Paso de los Libres'),
    (@IdCorrientes, 'Curuzú Cuatiá'),
    (@IdCorrientes, 'Mercedes'),
    (@IdCorrientes, 'Bella Vista'),
    (@IdCorrientes, 'Ituzaingó');
    PRINT 'Agregado correctamente';
END TRY
BEGIN CATCH
    PRINT 'Error o la provincia ya existe';
END CATCH
