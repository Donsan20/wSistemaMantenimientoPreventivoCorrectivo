-- SCRIPT PARA CREAR LA BASE DE DATOS Y TABLAS (Para el profesor o compañeros)
-- Ejecutar en SQL Server Management Studio (SSMS) o Visual Studio.

CREATE DATABASE MantenimientoDB;
GO

USE MantenimientoDB;
GO

CREATE TABLE Roles (
    IdRol INT PRIMARY KEY IDENTITY(1,1),
    NombreRol VARCHAR(50) NOT NULL
);

CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1, -- 1 para Activo, 0 para Inactivo
    IdRol INT NOT NULL,
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);

CREATE TABLE Equipos (
    IdEquipo INT PRIMARY KEY IDENTITY(1,1),
    NombreEquipo VARCHAR(100) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Activo'
);

CREATE TABLE OrdenesTrabajo (
    IdOrden INT PRIMARY KEY IDENTITY(1,1),
    IdEquipo INT NOT NULL,
    IdTecnico INT NOT NULL,
    TipoMantenimiento VARCHAR(50) NOT NULL,
    FechaProgramada DATETIME NOT NULL,
    DescripcionFalla VARCHAR(MAX) NOT NULL,
    EstadoOrden VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    FOREIGN KEY (IdEquipo) REFERENCES Equipos(IdEquipo),
    FOREIGN KEY (IdTecnico) REFERENCES Usuarios(IdUsuario)
);

-- DATOS DE PRUEBA (Opcional, para que puedan probar el login)
INSERT INTO Roles (NombreRol) VALUES ('Admin'), ('Tecnico'), ('Super');

-- Insertamos un admin (Password: 123)
INSERT INTO Usuarios (Username, Password, Estado, IdRol) VALUES ('admin', '123', 1, 1);
INSERT INTO Usuarios (Username, Password, Estado, IdRol) VALUES ('tecnico', '123', 1, 2);

-- Insertamos algunos equipos de prueba
INSERT INTO Equipos (NombreEquipo, Estado) VALUES ('Motor Eléctrico Principal', 'Activo');
INSERT INTO Equipos (NombreEquipo, Estado) VALUES ('Banda Transportadora B', 'Activo');
INSERT INTO Equipos (NombreEquipo, Estado) VALUES ('Torno CNC', 'Inactivo');
GO

-- =======================================================
-- PROCEDIMIENTOS ALMACENADOS (Stored Procedures)
-- Agregados para cumplir con Arquitectura de N Capas
-- =======================================================

GO

-- 1. Validar Login
CREATE PROCEDURE sp_ValidarLogin
    @Usuario VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    SELECT r.NombreRol 
    FROM Usuarios u 
    INNER JOIN Roles r ON u.IdRol = r.IdRol 
    WHERE u.Username = @Usuario AND u.Password = @Password AND u.Estado = 1;
END
GO

-- 2. Listar Equipos Activos
CREATE PROCEDURE sp_ListarEquiposActivos
AS
BEGIN
    SELECT IdEquipo, NombreEquipo 
    FROM Equipos 
    WHERE Estado = 'Activo';
END
GO

-- 3. Insertar Orden de Trabajo
CREATE PROCEDURE sp_InsertarOrden
    @IdEquipo INT,
    @IdTecnico INT,
    @TipoMantenimiento VARCHAR(50),
    @FechaProgramada DATETIME,
    @DescripcionFalla VARCHAR(MAX)
AS
BEGIN
    INSERT INTO OrdenesTrabajo (IdEquipo, IdTecnico, TipoMantenimiento, FechaProgramada, DescripcionFalla) 
    VALUES (@IdEquipo, @IdTecnico, @TipoMantenimiento, @FechaProgramada, @DescripcionFalla);
END
GO

-- 4. Listar todas las Ordenes (Para el Dashboard)
CREATE PROCEDURE sp_ListarOrdenes
AS
BEGIN
    SELECT 
        O.IdOrden AS 'Cod orden', 
        E.NombreEquipo AS 'Equipo Afectado', 
        O.TipoMantenimiento AS 'Tipo', 
        O.FechaProgramada AS 'Fecha Prog', 
        O.EstadoOrden AS 'Estado actual'
    FROM OrdenesTrabajo O
    INNER JOIN Equipos E ON O.IdEquipo = E.IdEquipo;
END
GO

-- 5. Contar Alertas Próximas (Para el Panel)
CREATE PROCEDURE sp_ContarAlertasProximas
AS
BEGIN
    SELECT COUNT(*) 
    FROM OrdenesTrabajo 
    WHERE EstadoOrden = 'Pendiente' 
    AND FechaProgramada BETWEEN GETDATE() AND DATEADD(day, 7, GETDATE());
END
GO

