# Documentación Sprint 2 - Sistema de Mantenimiento Preventivo y Correctivo

Este documento detalla el cumplimiento exacto de los entregables requeridos para el **Sprint 2**, incluyendo el desarrollo del BackEnd, integración con la base de datos, pruebas funcionales y documentación técnica.

---

## 1. Resumen del Sprint 2

### Objetivo
Completar el desarrollo del sistema implementando el BackEnd funcional con autenticación por roles, CRUD completo para todas las entidades, reportes estadísticos, validaciones robustas, manejo de errores y aplicación de patrones de diseño GoF y principios SOLID.

### Entregables
- ✅ BackEnd funcional (Capa de Negocio + Capa de Datos)
- ✅ Autenticación por roles (Admin, Técnico, Supervisor)
- ✅ CRUD completo (Equipos, Usuarios, Órdenes)
- ✅ Integración con base de datos (25 procedimientos almacenados)
- ✅ Validaciones robustas (capa de negocio + procedimientos almacenados)
- ✅ Búsquedas y filtros (búsqueda en tiempo real, filtros múltiples)
- ✅ Reportes (4 tipos + función diferencial)
- ✅ Manejo de errores (estructurado con log)
- ✅ Patrones GoF (Singleton, Factory Method, Observer)
- ✅ Principios SOLID (SRP, OCP, DIP)
- ✅ Base de datos normalizada (3FN)
- ✅ Función diferencial (`fn_TiempoPromedioEntreMantenimientos`)
- ✅ Pruebas funcionales
- ✅ Manual de usuario
- ✅ README final

---

## 2. BackEnd Funcional

### Arquitectura en 3 Capas

#### Capa de Datos (DAL)
Única capa con acceso directo a SQL Server. Implementa el patrón **Singleton** en `CD_Conexion` para garantizar una única instancia de conexión.

| Clase | Responsabilidad |
|-------|----------------|
| `CD_Conexion` | Singleton de conexión a LocalDB |
| `CD_Usuarios` | CRUD de usuarios (5 métodos) |
| `CD_Equipos` | CRUD de equipos (6 métodos) |
| `CD_Ordenes` | CRUD de órdenes (8 métodos) |
| `CD_Reportes` | Generación de reportes (5 métodos) |

#### Capa de Negocio (BLL)
Orquesta validaciones y aplica reglas de negocio antes de delegar a la Capa de Datos.

| Clase | Responsabilidad |
|-------|----------------|
| `CN_Usuarios` | Validaciones de usuarios (6 métodos) |
| `CN_Equipos` | Validaciones de equipos (6 métodos) |
| `CN_Ordenes` | Validaciones de órdenes (8 métodos) |
| `CN_Reportes` | Validaciones de reportes (5 métodos) |
| `Validador` | Validaciones centralizadas (SRP) |
| `ManejadorErrores` | Manejo estructurado de errores (SRP) |

---

## 3. Autenticación por Roles

### Implementación
- Login con validación de credenciales mediante `sp_ValidarLogin`
- 3 roles definidos en la tabla `Roles`:
  - **Admin** (IdRol=1): Acceso total al sistema
  - **Técnico** (IdRol=2): Solo visualización y operación de órdenes
  - **Supervisor** (IdRol=3): Visualización, reportes y gestión de equipos

### Control de Permisos
Implementado en `Admin.cs` mediante `ConfigurarPermisos()`:

| Funcionalidad | Admin | Técnico | Supervisor |
|---------------|-------|---------|------------|
| Dashboard | ✅ | ✅ | ✅ |
| Nueva Orden | ✅ | ❌ | ❌ |
| Gestión Equipos | ✅ | ❌ | ✅ |
| Gestión Usuarios | ✅ | ❌ | ❌ |
| Reportes | ✅ | ❌ | ✅ |

---

## 4. CRUD Completo

### Equipos
| Operación | Método BLL | Método DAL | Procedimiento Almacenado |
|-----------|-----------|-----------|-------------------------|
| Crear | `InsertarEquipo()` | `InsertarEquipo()` | `sp_InsertarEquipo` |
| Listar todos | `ListarTodosEquipos()` | `ListarTodosEquipos()` | `sp_ListarTodosEquipos` |
| Listar activos | `ListarEquiposActivos()` | `ListarEquiposActivos()` | `sp_ListarEquiposActivos` |
| Buscar | `BuscarEquipoPorNombre()` | `BuscarEquipoPorNombre()` | `sp_BuscarEquipoPorNombre` |
| Actualizar | `ActualizarEquipo()` | `ActualizarEquipo()` | `sp_ActualizarEquipo` |
| Eliminar | `EliminarEquipo()` | `EliminarEquipo()` | `sp_EliminarEquipo` |

### Usuarios
| Operación | Método BLL | Método DAL | Procedimiento Almacenado |
|-----------|-----------|-----------|-------------------------|
| Crear | `InsertarUsuario()` | `InsertarUsuario()` | `sp_InsertarUsuario` |
| Listar | `ListarUsuarios()` | `ListarUsuarios()` | `sp_ListarUsuarios` |
| Buscar | `BuscarUsuarioPorNombre()` | `BuscarUsuarioPorNombre()` | `sp_BuscarUsuarioPorNombre` |
| Actualizar | `ActualizarUsuario()` | `ActualizarUsuario()` | `sp_ActualizarUsuario` |
| Eliminar | `EliminarUsuario()` | `EliminarUsuario()` | `sp_EliminarUsuario` |

### Órdenes de Trabajo
| Operación | Método BLL | Método DAL | Procedimiento Almacenado |
|-----------|-----------|-----------|-------------------------|
| Crear | `InsertarOrden()` | `InsertarOrden()` | `sp_InsertarOrden` |
| Listar | `ListarOrdenes()` | `ListarOrdenes()` | `sp_ListarOrdenes` |
| Buscar | `BuscarOrdenes()` | `BuscarOrdenes()` | `sp_BuscarOrdenes` |
| Ver detalle | `ObtenerDetalleOrden()` | `ObtenerDetalleOrden()` | `sp_ObtenerDetalleOrden` |
| Cambiar estado | `ActualizarEstadoOrden()` | `ActualizarEstadoOrden()` | `sp_ActualizarEstadoOrden` |
| Actualizar | `ActualizarOrden()` | `ActualizarOrden()` | `sp_ActualizarOrden` |
| Eliminar | `EliminarOrden()` | `EliminarOrden()` | `sp_EliminarOrden` |
| Contar alertas | `ContarAlertasProximas()` | `ContarAlertasProximas()` | `sp_ContarAlertasProximas` |

---

## 5. Procedimientos Almacenados

Total: **25 procedimientos almacenados + 1 función diferencial**

### Autenticación
| SP | Descripción |
|----|-------------|
| `sp_ValidarLogin` | Valida credenciales y retorna el rol del usuario |

### Equipos
| SP | Descripción |
|----|-------------|
| `sp_ListarEquiposActivos` | Lista solo equipos activos |
| `sp_ListarTodosEquipos` | Lista todos los equipos (activos, inactivos, eliminados) |
| `sp_InsertarEquipo` | Inserta un nuevo equipo con validación de duplicados |
| `sp_ActualizarEquipo` | Actualiza nombre y estado con validación de duplicados |
| `sp_EliminarEquipo` | Eliminación lógica (cambia estado a 'Eliminado') |
| `sp_BuscarEquipoPorNombre` | Búsqueda parcial con LIKE |

### Usuarios
| SP | Descripción |
|----|-------------|
| `sp_ListarUsuarios` | Lista usuarios con nombre de rol (JOIN) |
| `sp_InsertarUsuario` | Inserta usuario con validación de duplicados y rol válido |
| `sp_ActualizarUsuario` | Actualiza usuario con validaciones |
| `sp_EliminarUsuario` | Eliminación lógica con validación de último admin |
| `sp_BuscarUsuarioPorNombre` | Búsqueda parcial con LIKE |

### Órdenes
| SP | Descripción |
|----|-------------|
| `sp_InsertarOrden` | Inserta nueva orden |
| `sp_ListarOrdenes` | Lista órdenes con nombre de equipo |
| `sp_ActualizarEstadoOrden` | Cambia solo el estado de una orden |
| `sp_ActualizarOrden` | Actualiza todos los campos de una orden |
| `sp_EliminarOrden` | Elimina solo órdenes Pendientes o Canceladas |
| `sp_BuscarOrdenes` | Búsqueda con filtros múltiples (equipo, tipo, estado, fechas) |
| `sp_ObtenerDetalleOrden` | Obtiene detalle completo de una orden |
| `sp_ContarAlertasProximas` | Cuenta mantenimientos en los próximos 7 días |

### Reportes
| SP | Descripción |
|----|-------------|
| `sp_ReporteHistoricoPorEquipo` | Histórico de mantenimientos de un equipo |
| `sp_ReporteResumenPorTecnico` | Estadísticas por técnico (totales, completadas, pendientes) |
| `sp_ReporteOrdenesPorEstado` | Conteo de órdenes agrupadas por estado |
| `sp_ReportePorTipoMantenimiento` | Conteo por tipo con porcentaje |

### Función Diferencial
| Función | Descripción |
|---------|-------------|
| `fn_TiempoPromedioEntreMantenimientos(@IdEquipo)` | Calcula días promedio entre mantenimientos completados usando `LAG()` |

---

## 6. Validaciones Implementadas

### Capa de Negocio
| Entidad | Validación | Mensaje |
|---------|-----------|---------|
| Usuarios | Username no vacío | "El nombre de usuario no puede estar vacío." |
| Usuarios | Username ≤ 50 caracteres | "El nombre de usuario no puede exceder los 50 caracteres." |
| Usuarios | Password no vacío | "La contraseña no puede estar vacía." |
| Usuarios | Rol válido (1-3) | "El rol seleccionado no es válido." |
| Equipos | Nombre no vacío | "El nombre del equipo no puede estar vacío." |
| Equipos | Nombre ≤ 100 caracteres | "El nombre del equipo no puede exceder los 100 caracteres." |
| Equipos | Estado válido | "El estado debe ser 'Activo' o 'Inactivo'." |
| Órdenes | Descripción no vacía | "La descripción de la falla no puede estar vacía." |
| Órdenes | Fecha no en pasado | "La fecha programada no puede ser menor a la fecha actual." |
| Órdenes | Tipo válido | "El tipo de mantenimiento debe ser 'Preventivo' o 'Correctivo'." |
| Órdenes | Estado válido | "Estado no válido. Use: Pendiente, En Proceso, Completada o Cancelada." |
| Órdenes | Fecha desde ≤ fecha hasta | "La fecha inicial no puede ser mayor a la fecha final." |

### Procedimientos Almacenados
| SP | Validación |
|----|-----------|
| `sp_InsertarEquipo` | No permite nombres duplicados |
| `sp_ActualizarEquipo` | No permite nombres duplicados (excluyendo el actual) |
| `sp_EliminarEquipo` | No elimina si tiene órdenes pendientes |
| `sp_InsertarUsuario` | No permite usernames duplicados, verifica rol existente |
| `sp_EliminarUsuario` | No elimina al último admin, verifica órdenes pendientes |
| `sp_ActualizarEstadoOrden` | Solo permite estados válidos |
| `sp_ActualizarOrden` | Verifica equipo activo, estado válido |
| `sp_EliminarOrden` | Solo permite eliminar órdenes Pendientes o Canceladas |

---

## 7. Búsquedas y Filtros

### Búsqueda en Tiempo Real
- **Equipos**: `txtBusqueda_TextChanged` en `GestionEquipos.cs` filtra mientras escribe
- **Usuarios**: `txtBusqueda_TextChanged` en `GestionUsuarios.cs` filtra mientras escribe

### Filtros Múltiples
- **Órdenes**: Filtro por equipo (texto), tipo (combo), estado (combo) en `OrdenDeTrabajo.cs`
- **Dashboard**: Filtro por tipo y estado en `Admin.cs` con `DataView.RowFilter`

---

## 8. Reportes

### Pestaña 1: Histórico por Equipo
- Selecciona un equipo del ComboBox
- Muestra todas las órdenes de ese equipo ordenadas por fecha
- **Función diferencial**: Calcula y muestra el tiempo promedio entre mantenimientos

### Pestaña 2: Resumen por Técnico
- Tabla con estadísticas por técnico:
  - Total de órdenes
  - Completadas, Pendientes, En Proceso, Canceladas

### Pestaña 3: Órdenes por Estado
- Conteo de órdenes agrupadas por estado

### Pestaña 4: Por Tipo de Mantenimiento
- Conteo de órdenes por tipo (Preventivo/Correctivo)
- Porcentaje de cada tipo sobre el total

---

## 9. Manejo de Errores

### Estrategia
1. **Capa de Negocio**: Lanza `Exception` con mensajes descriptivos para validaciones fallidas
2. **Capa de Datos**: Captura `SqlException` y relanza con contexto ("Error al insertar el equipo: ...")
3. **Presentación**: Captura excepciones y muestra `MessageBox` con mensaje amigable
4. **Log**: `ManejadorErrores.Registrar()` escribe en `errores.log` con timestamp, tipo, mensaje y stack trace

### Tipos de Error Detectados
| Tipo | Detección | Mensaje al Usuario |
|------|-----------|-------------------|
| SqlException | `EsErrorDeBaseDeDatos()` | "Ocurrió un problema con la base de datos..." |
| ArgumentException | `EsErrorDeValidacion()` | Muestra el mensaje de validación directamente |
| Error de conexión | `esErrorDeConexion()` | "No se pudo conectar con la base de datos..." |
| Otros | Default | "Ocurrió un error inesperado..." |

---

## 10. Patrones GoF

### Singleton (`CD_Conexion`)
```csharp
public class CD_Conexion
{
    private static CD_Conexion _instancia;
    private CD_Conexion() { ... }
    public static CD_Conexion Instancia { get { ... } }
}
```
**Beneficio**: Evita múltiples conexiones simultáneas, optimiza recursos.

### Factory Method (`FabricaFormularios`)
```csharp
public static Form CrearFormulario(TipoFormulario tipo, string rolUsuario)
{
    if (!TienePermiso(tipo, rolUsuario)) return null;
    switch (tipo) { ... }
}
```
**Beneficio**: Centraliza la creación de formularios y controla permisos en un solo lugar.

### Observer (`GestorEventos` + `ISuscriptor`)
```csharp
public interface ISuscriptor { void Actualizar(string tipoEvento, object datos = null); }
public static class GestorEventos {
    public static void Suscribirse(ISuscriptor s) { ... }
    public static void Notificar(TipoEvento evento, object datos = null) { ... }
}
```
**Beneficio**: Desacopla formularios. Cuando `OrdenDeTrabajo` guarda, el `Dashboard` se refresca automáticamente sin pasar referencias.

---

## 11. Principios SOLID

### SRP (Single Responsibility Principle)
- `Validador`: Solo valida datos
- `ManejadorErrores`: Solo maneja errores
- `CD_Conexion`: Solo gestiona la conexión
- Cada clase de datos/negocio tiene una única responsabilidad

### OCP (Open-Closed Principle)
- `FabricaFormularios`: Se puede agregar un nuevo tipo de formulario sin modificar el código que los consume
- `GestorEventos`: Se pueden agregar nuevos tipos de eventos sin modificar la lógica de notificación

### DIP (Dependency Inversion Principle)
- `Admin` depende de `ISuscriptor` (abstracción), no de formularios concretos
- Las capas de negocio y datos están desacopladas

---

## 12. Función Diferencial

### `fn_TiempoPromedioEntreMantenimientos(@IdEquipo)`

**Descripción**: Calcula el tiempo promedio (en días) entre mantenimientos completados de un equipo específico.

**Implementación SQL**:
```sql
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
```

**Técnica**: Usa `LAG()` (window function) para obtener la fecha anterior y calcular la diferencia en días entre mantenimientos consecutivos.

---

## 13. Pruebas Funcionales

### Casos de Prueba

| # | Caso | Entrada Esperada | Resultado |
|---|------|-----------------|-----------|
| 1 | Login correcto | admin/123 | Acceso al Dashboard como Admin ✅ |
| 2 | Login incorrecto | admin/456 | Mensaje "Usuario o contraseña incorrectos" ✅ |
| 3 | Crear orden válida | Equipo seleccionado, descripción, fecha futura | Orden creada exitosamente ✅ |
| 4 | Crear orden sin equipo | Sin seleccionar equipo | Mensaje "Selecciona un equipo" ✅ |
| 5 | Crear orden sin descripción | Descripción vacía | Mensaje "Ingresa una descripción" ✅ |
| 6 | Crear orden con fecha pasada | Fecha < hoy | Mensaje "La fecha programada no puede ser menor..." ✅ |
| 7 | Crear equipo duplicado | Nombre existente | Mensaje "Ya existe un equipo con ese nombre" ✅ |
| 8 | Eliminar equipo con órdenes pendientes | Equipo con órdenes Pendientes | Mensaje "No se puede eliminar..." ✅ |
| 9 | Eliminar último admin | Último usuario Admin | Mensaje "Debe existir al menos un administrador" ✅ |
| 10 | Cambiar estado de orden | Seleccionar orden + nuevo estado | Estado actualizado correctamente ✅ |
| 11 | Búsqueda de equipos | Texto parcial | Filtra resultados en tiempo real ✅ |
| 12 | Reporte histórico | Equipo seleccionado | Muestra historial + promedio días ✅ |
| 13 | Permiso Técnico | Login como tecnico | Botones Admin ocultos ✅ |

---

## 14. Evidencia de Trabajo en GitHub

### Commits del Sprint 2

| Commit | Descripción |
|--------|-------------|
| `feat: agregar CRUD completo de stored procedures...` | Fase 1: 20 SPs + 1 función diferencial |
| `feat: completar Capa de Datos con CRUD...` | Fase 2: 20 métodos en 4 clases DAL |
| `feat: completar Capa de Negocio con validaciones...` | Fase 3: 20 métodos con validaciones en 4 clases BLL |
| `feat: agregar formularios de GestionEquipos...` | Fase 4: 4 formularios nuevos/mejorados |
| `feat: implementar patrones GoF (Factory, Observer)...` | Fase 5: 4 clases de patrones + integración |
| `docs: actualizar README y documentación Sprint 2` | Fase 6: Documentación completa |

### Ramas
- `master`: Rama principal con el código completo del proyecto

---

## 15. Conclusiones

El Sprint 2 completó exitosamente todos los entregables requeridos:
- ✅ BackEnd funcional con arquitectura en 3 capas
- ✅ Autenticación por roles con permisos diferenciados
- ✅ CRUD completo para Equipos, Usuarios y Órdenes
- ✅ 25 procedimientos almacenados + 1 función diferencial
- ✅ Validaciones robustas en capa de negocio y base de datos
- ✅ Búsquedas en tiempo real y filtros múltiples
- ✅ 4 tipos de reportes estadísticos
- ✅ Manejo estructurado de errores con log
- ✅ 3 patrones GoF implementados (Singleton, Factory, Observer)
- ✅ Principios SOLID aplicados (SRP, OCP, DIP)
- ✅ Base de datos normalizada hasta 3FN
- ✅ Documentación técnica y manual de usuario
