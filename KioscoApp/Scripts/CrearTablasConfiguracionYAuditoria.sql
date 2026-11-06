-- =========================================================
-- TABLAS Y PROCEDIMIENTOS DE CONFIGURACIÓN Y AUDITORÍA
-- =========================================================
USE KioscoDB;
GO

-- 1. Tabla de Configuración de Empresa / Kiosco
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ConfiguracionEmpresa')
BEGIN
    CREATE TABLE ConfiguracionEmpresa (
        Id INT PRIMARY KEY DEFAULT 1,
        NombreFantasia VARCHAR(150) NOT NULL DEFAULT 'KIOSCO EXPRESS',
        RazonSocial VARCHAR(150) NULL DEFAULT 'Kiosco Express S.A.',
        Cuit VARCHAR(30) NULL DEFAULT '20-12345678-9',
        Direccion VARCHAR(200) NULL DEFAULT 'Av. Principal 1234',
        Telefono VARCHAR(50) NULL DEFAULT '(011) 4567-8900',
        MensajePieTicket VARCHAR(300) NULL DEFAULT '¡Gracias por su compra! Conserve su ticket.',
        ImpresoraPredeterminada VARCHAR(150) NULL DEFAULT '',
        AnchoPapelMm INT NOT NULL DEFAULT 80,
        ImpresionAutomatica BIT NOT NULL DEFAULT 0,
        CortarPapel BIT NOT NULL DEFAULT 1
    );

    INSERT INTO ConfiguracionEmpresa (Id, NombreFantasia, RazonSocial, Cuit, Direccion, Telefono, MensajePieTicket, ImpresoraPredeterminada, AnchoPapelMm, ImpresionAutomatica, CortarPapel)
    VALUES (1, 'KIOSCO EXPRESS', 'Kiosco Express S.A.', '20-12345678-9', 'Av. Principal 1234', '(011) 4567-8900', '¡Gracias por su compra! Conserve su ticket.', '', 80, 0, 1);
END;
GO

-- 2. Tabla de Auditoría / Trazabilidad de Seguridad
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Auditoria')
BEGIN
    CREATE TABLE Auditoria (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        IdUsuario INT NULL,
        Usuario VARCHAR(50) NOT NULL,
        Rol VARCHAR(30) NOT NULL,
        Modulo VARCHAR(50) NOT NULL,
        Accion VARCHAR(100) NOT NULL,
        Detalle NVARCHAR(500) NULL,
        IpEquipo VARCHAR(100) NULL
    );
END;
GO

-- 3. Procedimientos Almacenados
CREATE OR ALTER PROCEDURE sp_Configuracion_Obtener
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM ConfiguracionEmpresa WHERE Id = 1)
    BEGIN
        INSERT INTO ConfiguracionEmpresa (Id, NombreFantasia, RazonSocial, Cuit, Direccion, Telefono, MensajePieTicket, ImpresoraPredeterminada, AnchoPapelMm, ImpresionAutomatica, CortarPapel)
        VALUES (1, 'KIOSCO EXPRESS', 'Kiosco Express S.A.', '20-12345678-9', 'Av. Principal 1234', '(011) 4567-8900', '¡Gracias por su compra! Conserve su ticket.', '', 80, 0, 1);
    END;

    SELECT TOP 1 
        Id,
        NombreFantasia,
        RazonSocial,
        Cuit,
        Direccion,
        Telefono,
        MensajePieTicket,
        ImpresoraPredeterminada,
        AnchoPapelMm,
        ImpresionAutomatica,
        CortarPapel
    FROM ConfiguracionEmpresa
    WHERE Id = 1;
END;
GO

CREATE OR ALTER PROCEDURE sp_Configuracion_Guardar
    @NombreFantasia VARCHAR(150),
    @RazonSocial VARCHAR(150) = NULL,
    @Cuit VARCHAR(30) = NULL,
    @Direccion VARCHAR(200) = NULL,
    @Telefono VARCHAR(50) = NULL,
    @MensajePieTicket VARCHAR(300) = NULL,
    @ImpresoraPredeterminada VARCHAR(150) = NULL,
    @AnchoPapelMm INT = 80,
    @ImpresionAutomatica BIT = 0,
    @CortarPapel BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM ConfiguracionEmpresa WHERE Id = 1)
    BEGIN
        UPDATE ConfiguracionEmpresa
        SET NombreFantasia = @NombreFantasia,
            RazonSocial = @RazonSocial,
            Cuit = @Cuit,
            Direccion = @Direccion,
            Telefono = @Telefono,
            MensajePieTicket = @MensajePieTicket,
            ImpresoraPredeterminada = @ImpresoraPredeterminada,
            AnchoPapelMm = @AnchoPapelMm,
            ImpresionAutomatica = @ImpresionAutomatica,
            CortarPapel = @CortarPapel
        WHERE Id = 1;
    END
    ELSE
    BEGIN
        INSERT INTO ConfiguracionEmpresa (Id, NombreFantasia, RazonSocial, Cuit, Direccion, Telefono, MensajePieTicket, ImpresoraPredeterminada, AnchoPapelMm, ImpresionAutomatica, CortarPapel)
        VALUES (1, @NombreFantasia, @RazonSocial, @Cuit, @Direccion, @Telefono, @MensajePieTicket, @ImpresoraPredeterminada, @AnchoPapelMm, @ImpresionAutomatica, @CortarPapel);
    END;
END;
GO

CREATE OR ALTER PROCEDURE sp_Auditoria_Registrar
    @IdUsuario INT = NULL,
    @Usuario VARCHAR(50),
    @Rol VARCHAR(30),
    @Modulo VARCHAR(50),
    @Accion VARCHAR(100),
    @Detalle NVARCHAR(500) = NULL,
    @IpEquipo VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (Fecha, IdUsuario, Usuario, Rol, Modulo, Accion, Detalle, IpEquipo)
    VALUES (GETDATE(), @IdUsuario, @Usuario, @Rol, @Modulo, @Accion, @Detalle, @IpEquipo);
END;
GO

CREATE OR ALTER PROCEDURE sp_Auditoria_Obtener
    @FechaDesde DATETIME = NULL,
    @FechaHasta DATETIME = NULL,
    @Modulo VARCHAR(50) = NULL,
    @Filtro VARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        Id,
        Fecha,
        Usuario,
        Rol,
        Modulo,
        Accion,
        Detalle,
        IpEquipo
    FROM Auditoria
    WHERE (@FechaDesde IS NULL OR Fecha >= @FechaDesde)
      AND (@FechaHasta IS NULL OR Fecha <= @FechaHasta)
      AND (@Modulo IS NULL OR @Modulo = '' OR Modulo = @Modulo)
      AND (@Filtro IS NULL OR @Filtro = '' OR (Usuario LIKE '%' + @Filtro + '%' OR Accion LIKE '%' + @Filtro + '%' OR Detalle LIKE '%' + @Filtro + '%'))
    ORDER BY Fecha DESC;
END;
GO
