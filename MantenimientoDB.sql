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
INSERT INTO Usuarios (Username, Password, Estado, IdRol) VALUES ('super', '123', 1, 3);

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

-- =======================================================
-- CRUD COMPLETO: EQUIPOS (Sprint 2)
-- =======================================================

-- 6. Insertar Equipo
CREATE PROCEDURE sp_InsertarEquipo
    @NombreEquipo VARCHAR(100),
    @Estado VARCHAR(20) = 'Activo'
AS
BEGIN
    -- Validación: no permitir nombres duplicados
    IF EXISTS (SELECT 1 FROM Equipos WHERE NombreEquipo = @NombreEquipo)
    BEGIN
        RAISERROR('Ya existe un equipo con ese nombre.', 16, 1);
        RETURN;
    END

    INSERT INTO Equipos (NombreEquipo, Estado) 
    VALUES (@NombreEquipo, @Estado);
END
GO

-- 7. Actualizar Equipo
CREATE PROCEDURE sp_ActualizarEquipo
    @IdEquipo INT,
    @NombreEquipo VARCHAR(100),
    @Estado VARCHAR(20)
AS
BEGIN
    -- Validación: no permitir nombres duplicados (excluyendo el equipo actual)
    IF EXISTS (SELECT 1 FROM Equipos WHERE NombreEquipo = @NombreEquipo AND IdEquipo <> @IdEquipo)
    BEGIN
        RAISERROR('Ya existe otro equipo con ese nombre.', 16, 1);
        RETURN;
    END

    UPDATE Equipos 
    SET NombreEquipo = @NombreEquipo, 
        Estado = @Estado 
    WHERE IdEquipo = @IdEquipo;
END
GO

-- 8. Eliminar Equipo (eliminación lógica: cambia estado a 'Eliminado')
CREATE PROCEDURE sp_EliminarEquipo
    @IdEquipo INT
AS
BEGIN
    -- Validación: no eliminar si tiene órdenes asociadas pendientes
    IF EXISTS (SELECT 1 FROM OrdenesTrabajo WHERE IdEquipo = @IdEquipo AND EstadoOrden = 'Pendiente')
    BEGIN
        RAISERROR('No se puede eliminar: el equipo tiene órdenes de trabajo pendientes.', 16, 1);
        RETURN;
    END

    UPDATE Equipos 
    SET Estado = 'Eliminado' 
    WHERE IdEquipo = @IdEquipo;
END
GO

-- 9. Listar Todos los Equipos (activos, inactivos y eliminados)
CREATE PROCEDURE sp_ListarTodosEquipos
AS
BEGIN
    SELECT IdEquipo, NombreEquipo, Estado 
    FROM Equipos 
    ORDER BY Estado, NombreEquipo;
END
GO

-- 10. Buscar Equipo por Nombre
CREATE PROCEDURE sp_BuscarEquipoPorNombre
    @Busqueda VARCHAR(100)
AS
BEGIN
    SELECT IdEquipo, NombreEquipo, Estado 
    FROM Equipos 
    WHERE NombreEquipo LIKE '%' + @Busqueda + '%'
    ORDER BY NombreEquipo;
END
GO

-- =======================================================
-- CRUD COMPLETO: USUARIOS (Sprint 2)
-- =======================================================

-- 11. Insertar Usuario
CREATE PROCEDURE sp_InsertarUsuario
    @Username VARCHAR(50),
    @Password VARCHAR(50),
    @Estado BIT = 1,
    @IdRol INT
AS
BEGIN
    -- Validación: no permitir usernames duplicados
    IF EXISTS (SELECT 1 FROM Usuarios WHERE Username = @Username)
    BEGIN
        RAISERROR('Ya existe un usuario con ese nombre de usuario.', 16, 1);
        RETURN;
    END

    -- Validación: verificar que el rol existe
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
    BEGIN
        RAISERROR('El rol especificado no existe.', 16, 1);
        RETURN;
    END

    INSERT INTO Usuarios (Username, Password, Estado, IdRol) 
    VALUES (@Username, @Password, @Estado, @IdRol);
END
GO

-- 12. Actualizar Usuario
CREATE PROCEDURE sp_ActualizarUsuario
    @IdUsuario INT,
    @Username VARCHAR(50),
    @Password VARCHAR(50),
    @Estado BIT,
    @IdRol INT
AS
BEGIN
    -- Validación: no permitir usernames duplicados (excluyendo el usuario actual)
    IF EXISTS (SELECT 1 FROM Usuarios WHERE Username = @Username AND IdUsuario <> @IdUsuario)
    BEGIN
        RAISERROR('Ya existe otro usuario con ese nombre de usuario.', 16, 1);
        RETURN;
    END

    -- Validación: verificar que el rol existe
    IF NOT EXISTS (SELECT 1 FROM Roles WHERE IdRol = @IdRol)
    BEGIN
        RAISERROR('El rol especificado no existe.', 16, 1);
        RETURN;
    END

    UPDATE Usuarios 
    SET Username = @Username, 
        Password = @Password, 
        Estado = @Estado, 
        IdRol = @IdRol 
    WHERE IdUsuario = @IdUsuario;
END
GO

-- 13. Eliminar Usuario (eliminación lógica: cambia estado a 0)
CREATE PROCEDURE sp_EliminarUsuario
    @IdUsuario INT
AS
BEGIN
    -- Validación: no eliminar al último admin
    DECLARE @AdminCount INT;
    SELECT @AdminCount = COUNT(*) FROM Usuarios WHERE IdRol = 1 AND Estado = 1;

    DECLARE @EsAdmin BIT;
    SELECT @EsAdmin = CASE WHEN IdRol = 1 THEN 1 ELSE 0 END FROM Usuarios WHERE IdUsuario = @IdUsuario;

    IF @EsAdmin = 1 AND @AdminCount <= 1
    BEGIN
        RAISERROR('No se puede eliminar: debe existir al menos un administrador.', 16, 1);
        RETURN;
    END

    -- Validación: no eliminar si tiene órdenes pendientes asignadas
    IF EXISTS (SELECT 1 FROM OrdenesTrabajo WHERE IdTecnico = @IdUsuario AND EstadoOrden = 'Pendiente')
    BEGIN
        RAISERROR('No se puede eliminar: el usuario tiene órdenes de trabajo pendientes.', 16, 1);
        RETURN;
    END

    UPDATE Usuarios 
    SET Estado = 0 
    WHERE IdUsuario = @IdUsuario;
END
GO

-- 14. Listar Todos los Usuarios
CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SELECT 
        u.IdUsuario, 
        u.Username, 
        u.Estado, 
        r.NombreRol AS Rol
    FROM Usuarios u
    INNER JOIN Roles r ON u.IdRol = r.IdRol
    ORDER BY u.Username;
END
GO

-- 15. Buscar Usuario por Nombre
CREATE PROCEDURE sp_BuscarUsuarioPorNombre
    @Busqueda VARCHAR(50)
AS
BEGIN
    SELECT 
        u.IdUsuario, 
        u.Username, 
        u.Estado, 
        r.NombreRol AS Rol
    FROM Usuarios u
    INNER JOIN Roles r ON u.IdRol = r.IdRol
    WHERE u.Username LIKE '%' + @Busqueda + '%'
    ORDER BY u.Username;
END
GO

-- =======================================================
-- CRUD COMPLETO: ÓRDENES DE TRABAJO (Sprint 2)
-- =======================================================

-- 16. Actualizar Estado de Orden
CREATE PROCEDURE sp_ActualizarEstadoOrden
    @IdOrden INT,
    @NuevoEstado VARCHAR(20)
AS
BEGIN
    -- Validación: solo permitir estados válidos
    IF @NuevoEstado NOT IN ('Pendiente', 'En Proceso', 'Completada', 'Cancelada')
    BEGIN
        RAISERROR('Estado no válido. Use: Pendiente, En Proceso, Completada o Cancelada.', 16, 1);
        RETURN;
    END

    UPDATE OrdenesTrabajo 
    SET EstadoOrden = @NuevoEstado 
    WHERE IdOrden = @IdOrden;
END
GO

-- 17. Actualizar Orden Completa
CREATE PROCEDURE sp_ActualizarOrden
    @IdOrden INT,
    @IdEquipo INT,
    @IdTecnico INT,
    @TipoMantenimiento VARCHAR(50),
    @FechaProgramada DATETIME,
    @DescripcionFalla VARCHAR(MAX),
    @EstadoOrden VARCHAR(20)
AS
BEGIN
    -- Validación: estado válido
    IF @EstadoOrden NOT IN ('Pendiente', 'En Proceso', 'Completada', 'Cancelada')
    BEGIN
        RAISERROR('Estado no válido.', 16, 1);
        RETURN;
    END

    -- Validación: equipo debe existir y estar activo
    IF NOT EXISTS (SELECT 1 FROM Equipos WHERE IdEquipo = @IdEquipo AND Estado = 'Activo')
    BEGIN
        RAISERROR('El equipo seleccionado no existe o no está activo.', 16, 1);
        RETURN;
    END

    UPDATE OrdenesTrabajo 
    SET IdEquipo = @IdEquipo,
        IdTecnico = @IdTecnico,
        TipoMantenimiento = @TipoMantenimiento,
        FechaProgramada = @FechaProgramada,
        DescripcionFalla = @DescripcionFalla,
        EstadoOrden = @EstadoOrden
    WHERE IdOrden = @IdOrden;
END
GO

-- 18. Eliminar Orden
CREATE PROCEDURE sp_EliminarOrden
    @IdOrden INT
AS
BEGIN
    -- Validación: solo se pueden eliminar órdenes pendientes o canceladas
    DECLARE @EstadoActual VARCHAR(20);
    SELECT @EstadoActual = EstadoOrden FROM OrdenesTrabajo WHERE IdOrden = @IdOrden;

    IF @EstadoActual NOT IN ('Pendiente', 'Cancelada')
    BEGIN
        RAISERROR('Solo se pueden eliminar órdenes en estado Pendiente o Cancelada.', 16, 1);
        RETURN;
    END

    DELETE FROM OrdenesTrabajo WHERE IdOrden = @IdOrden;
END
GO

-- 19. Buscar Órdenes por Filtros Múltiples
CREATE PROCEDURE sp_BuscarOrdenes
    @Equipo VARCHAR(100) = NULL,
    @TipoMantenimiento VARCHAR(50) = NULL,
    @EstadoOrden VARCHAR(20) = NULL,
    @FechaDesde DATETIME = NULL,
    @FechaHasta DATETIME = NULL
AS
BEGIN
    SELECT 
        O.IdOrden AS 'Cod orden', 
        E.NombreEquipo AS 'Equipo Afectado', 
        O.TipoMantenimiento AS 'Tipo', 
        O.FechaProgramada AS 'Fecha Prog', 
        O.EstadoOrden AS 'Estado actual',
        U.Username AS 'Técnico'
    FROM OrdenesTrabajo O
    INNER JOIN Equipos E ON O.IdEquipo = E.IdEquipo
    INNER JOIN Usuarios U ON O.IdTecnico = U.IdUsuario
    WHERE (@Equipo IS NULL OR E.NombreEquipo LIKE '%' + @Equipo + '%')
      AND (@TipoMantenimiento IS NULL OR O.TipoMantenimiento = @TipoMantenimiento)
      AND (@EstadoOrden IS NULL OR O.EstadoOrden = @EstadoOrden)
      AND (@FechaDesde IS NULL OR O.FechaProgramada >= @FechaDesde)
      AND (@FechaHasta IS NULL OR O.FechaProgramada <= @FechaHasta)
    ORDER BY O.FechaProgramada DESC;
END
GO

-- 20. Obtener Detalle de una Orden
CREATE PROCEDURE sp_ObtenerDetalleOrden
    @IdOrden INT
AS
BEGIN
    SELECT 
        O.IdOrden,
        E.NombreEquipo AS 'Equipo',
        U.Username AS 'Técnico',
        O.TipoMantenimiento AS 'Tipo',
        O.FechaProgramada AS 'Fecha Programada',
        O.DescripcionFalla AS 'Descripción',
        O.EstadoOrden AS 'Estado'
    FROM OrdenesTrabajo O
    INNER JOIN Equipos E ON O.IdEquipo = E.IdEquipo
    INNER JOIN Usuarios U ON O.IdTecnico = U.IdUsuario
    WHERE O.IdOrden = @IdOrden;
END
GO

-- =======================================================
-- REPORTES (Sprint 2)
-- =======================================================

-- 21. Reporte: Histórico de Mantenimientos por Equipo
CREATE PROCEDURE sp_ReporteHistoricoPorEquipo
    @IdEquipo INT
AS
BEGIN
    SELECT 
        O.IdOrden AS 'N° Orden',
        O.TipoMantenimiento AS 'Tipo',
        O.FechaProgramada AS 'Fecha',
        O.DescripcionFalla AS 'Descripción',
        O.EstadoOrden AS 'Estado',
        U.Username AS 'Técnico'
    FROM OrdenesTrabajo O
    INNER JOIN Usuarios U ON O.IdTecnico = U.IdUsuario
    WHERE O.IdEquipo = @IdEquipo
    ORDER BY O.FechaProgramada DESC;
END
GO

-- 22. Reporte: Resumen por Técnico
CREATE PROCEDURE sp_ReporteResumenPorTecnico
AS
BEGIN
    SELECT 
        U.Username AS 'Técnico',
        COUNT(O.IdOrden) AS 'Total Órdenes',
        SUM(CASE WHEN O.EstadoOrden = 'Completada' THEN 1 ELSE 0 END) AS 'Completadas',
        SUM(CASE WHEN O.EstadoOrden = 'Pendiente' THEN 1 ELSE 0 END) AS 'Pendientes',
        SUM(CASE WHEN O.EstadoOrden = 'En Proceso' THEN 1 ELSE 0 END) AS 'En Proceso',
        SUM(CASE WHEN O.EstadoOrden = 'Cancelada' THEN 1 ELSE 0 END) AS 'Canceladas'
    FROM Usuarios U
    LEFT JOIN OrdenesTrabajo O ON U.IdUsuario = O.IdTecnico
    WHERE U.IdRol IN (2, 3) -- Técnicos y Supervisores
    GROUP BY U.Username
    ORDER BY COUNT(O.IdOrden) DESC;
END
GO

-- 23. Reporte: Órdenes por Estado (Dashboard)
CREATE PROCEDURE sp_ReporteOrdenesPorEstado
AS
BEGIN
    SELECT 
        EstadoOrden AS 'Estado',
        COUNT(*) AS 'Cantidad'
    FROM OrdenesTrabajo
    GROUP BY EstadoOrden;
END
GO

-- 24. Reporte: Órdenes por Tipo de Mantenimiento
CREATE PROCEDURE sp_ReportePorTipoMantenimiento
AS
BEGIN
    SELECT 
        TipoMantenimiento AS 'Tipo',
        COUNT(*) AS 'Cantidad',
        CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM OrdenesTrabajo) AS DECIMAL(5,2)) AS 'Porcentaje'
    FROM OrdenesTrabajo
    GROUP BY TipoMantenimiento;
END
GO

-- 25. Función Diferencial: Calcular tiempo promedio entre mantenimientos
CREATE FUNCTION fn_TiempoPromedioEntreMantenimientos(@IdEquipo INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @PromedioDias FLOAT;
    
    SELECT @PromedioDias = AVG(DATEDIFF(DAY, FechaAnterior, FechaActual))
    FROM (
        SELECT 
            FechaProgramada AS FechaActual,
            LAG(FechaProgramada) OVER (ORDER BY FechaProgramada) AS FechaAnterior
        FROM OrdenesTrabajo
        WHERE IdEquipo = @IdEquipo AND EstadoOrden = 'Completada'
    ) AS SubConsulta
    WHERE FechaAnterior IS NOT NULL;

    RETURN ISNULL(@PromedioDias, 0);
END
GO

