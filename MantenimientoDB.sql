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
