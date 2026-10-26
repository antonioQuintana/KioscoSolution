-- =============================================
-- CREACIÓN DE TABLAS PARA CIERRE Y MOVIMIENTOS DE CAJA
-- =============================================
USE KioscoDB;
GO

-- 1. Tabla de Cierres de Caja (Cierre Z)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CierresCaja')
BEGIN
    CREATE TABLE CierresCaja (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NULL FOREIGN KEY REFERENCES Usuarios(Id),
        FechaApertura DATETIME NOT NULL DEFAULT GETDATE(),
        FechaCierre DATETIME NOT NULL DEFAULT GETDATE(),
        FondoInicial DECIMAL(18,2) NOT NULL DEFAULT 0,
        VentasEfectivo DECIMAL(18,2) NOT NULL DEFAULT 0,
        VentasQR DECIMAL(18,2) NOT NULL DEFAULT 0,
        VentasTarjeta DECIMAL(18,2) NOT NULL DEFAULT 0,
        IngresosVarios DECIMAL(18,2) NOT NULL DEFAULT 0,
        GastosRetiros DECIMAL(18,2) NOT NULL DEFAULT 0,
        TotalEsperado DECIMAL(18,2) NOT NULL DEFAULT 0,
        TotalDeclarado DECIMAL(18,2) NOT NULL DEFAULT 0,
        Diferencia DECIMAL(18,2) NOT NULL DEFAULT 0,
        Observaciones NVARCHAR(255) NULL,
        Estado NVARCHAR(50) NOT NULL DEFAULT 'Cerrado'
    );
END
GO

-- 2. Tabla de Movimientos de Caja (Ingresos / Retiros)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MovimientosCaja')
BEGIN
    CREATE TABLE MovimientosCaja (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NULL FOREIGN KEY REFERENCES Usuarios(Id),
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        Tipo NVARCHAR(20) NOT NULL, -- 'Ingreso' o 'Retiro'
        Monto DECIMAL(18,2) NOT NULL,
        Motivo NVARCHAR(200) NOT NULL,
        IdCierreCaja INT NULL FOREIGN KEY REFERENCES CierresCaja(Id)
    );
END
GO

-- 3. Vincular Ventas a CierresCaja si la columna no existe
IF NOT EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Ventas' AND COLUMN_NAME = 'IdCierreCaja'
)
BEGIN
    ALTER TABLE Ventas ADD IdCierreCaja INT NULL FOREIGN KEY REFERENCES CierresCaja(Id);
END
GO
