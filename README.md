# Sistema de Mantenimiento Preventivo y Correctivo

Sistema de escritorio desarrollado en C# con Windows Forms para la gestión integral de mantenimientos industriales. Permite digitalizar el control de maquinaria, agendar órdenes de trabajo (preventivas y correctivas), controlar el historial de cada equipo y generar reportes estadísticos.

---

## Tecnologías Utilizadas

| Tecnología | Versión |
|------------|---------|
| Lenguaje | C# |
| Framework | .NET Framework 4.7.2 |
| Interfaz | Windows Forms |
| Base de datos | SQL Server LocalDB |
| IDE | Visual Studio 2022 |
| Control de versiones | Git + GitHub |

---

## Arquitectura del Sistema

El sistema implementa una **Arquitectura en 3 Capas (N-Tier)**:

```
┌─────────────────────────────────────────────────┐
│           Capa de Presentación (UI)              │
│  Form1, Admin, OrdenDeTrabajo, GestionEquipos,   │
│  GestionUsuarios, Reportes                       │
└──────────────────────┬──────────────────────────┘
                       │ delega
┌──────────────────────▼──────────────────────────┐
│            Capa de Negocio (BLL)                 │
│  CN_Usuarios, CN_Equipos, CN_Ordenes,            │
│  CN_Reportes, Validador, ManejadorErrores        │
└──────────────────────┬──────────────────────────┘
                       │ delega
┌──────────────────────▼──────────────────────────┐
│             Capa de Datos (DAL)                  │
│  CD_Conexion, CD_Usuarios, CD_Equipos,           │
│  CD_Ordenes, CD_Reportes                         │
└──────────────────────┬──────────────────────────┘
                       │ ejecuta
┌──────────────────────▼──────────────────────────┐
│        SQL Server LocalDB (.mdf)                 │
│  MantenimientoDB.mdf + Procedimientos Almacenados│
└─────────────────────────────────────────────────┘
```

---

## Patrones de Diseño Implementados

### Patrones GoF

| Patrón | Clase | Descripción |
|--------|-------|-------------|
| **Singleton** | `CD_Conexion` | Garantiza una única instancia de conexión a la base de datos |
| **Factory Method** | `FabricaFormularios` | Crea formularios según el rol del usuario con control de permisos |
| **Observer** | `GestorEventos` + `ISuscriptor` | Notifica cambios entre formularios (ej: al crear una orden, el Dashboard se refresca automáticamente) |

### Principios SOLID

| Principio | Aplicación |
|-----------|-----------|
| **SRP** | `Validador` y `ManejadorErrores` tienen una sola responsabilidad |
| **OCP** | `FabricaFormularios` permite agregar nuevos formularios sin modificar el código consumidor |
| **DIP** | Las capas dependen de abstracciones, no de implementaciones concretas |

---

## Características del Sistema

### Autenticación y Roles
- Login con validación de credenciales contra base de datos
- 3 roles con permisos diferenciados:
  - **Admin**: acceso total al sistema
  - **Técnico**: solo visualización y operación de órdenes
  - **Supervisor**: visualización, reportes y gestión de equipos

### CRUD Completo
- **Equipos**: crear, listar, buscar, actualizar, eliminar (lógica)
- **Usuarios**: crear, listar, buscar, actualizar, eliminar (lógica)
- **Órdenes de trabajo**: crear, listar, buscar, editar, cambiar estado, eliminar

### Reportes
- Histórico de mantenimientos por equipo
- Resumen de órdenes por técnico
- Órdenes agrupadas por estado
- Órdenes por tipo de mantenimiento con porcentajes
- **Función diferencial**: cálculo del tiempo promedio entre mantenimientos de un equipo

### Seguridad y Validaciones
- Uso exclusivo de **Procedimientos Almacenados** (prevención de inyección SQL)
- Validaciones en capa de negocio (campos obligatorios, estados válidos, fechas lógicas)
- Validaciones en procedimientos almacenados (duplicados, integridad referencial)
- Manejo estructurado de errores con log a archivo
- Eliminación lógica (no se borran registros físicamente)

---

## Estructura del Proyecto

```
wSistemaMantenimientoPreventivoCorrectivo/
├── CapaDatos/
│   ├── CD_Conexion.cs          # Singleton de conexión
│   ├── CD_Usuarios.cs          # Acceso a datos de usuarios
│   ├── CD_Equipos.cs           # Acceso a datos de equipos
│   ├── CD_Ordenes.cs           # Acceso a datos de órdenes
│   └── CD_Reportes.cs          # Acceso a datos de reportes
├── CapaNegocio/
│   ├── CN_Usuarios.cs          # Reglas de negocio de usuarios
│   ├── CN_Equipos.cs           # Reglas de negocio de equipos
│   ├── CN_Ordenes.cs           # Reglas de negocio de órdenes
│   └── CN_Reportes.cs          # Reglas de negocio de reportes
├── Form1.cs                    # Login
├── Admin.cs                    # Dashboard principal
├── OrdenDeTrabajo.cs           # CRUD de órdenes
├── GestionEquipos.cs           # CRUD de equipos
├── GestionUsuarios.cs          # CRUD de usuarios
├── Reportes.cs                 # Módulo de reportes
├── Validador.cs                # Validaciones centralizadas
├── ManejadorErrores.cs         # Manejo de errores
├── FabricaFormularios.cs       # Factory Method (GoF)
├── ObservadorEventos.cs        # Observer Pattern (GoF)
├── AuxiliarInterfaz.cs         # Helpers de interfaz
├── ConexionBD.cs               # Helper de conexión
├── MantenimientoDB.sql         # Script completo de la base de datos
├── MantenimientoDB.mdf         # Base de datos LocalDB
└── Docs/
    ├── Documentacion_Sprint1.md
    ├── Documentacion_Sprint2.md
    └── ManualUsuario.md
```

---

## Base de Datos

### Tablas

| Tabla | Descripción |
|-------|-------------|
| `Roles` | Tipos de usuario (Admin, Tecnico, Super) |
| `Usuarios` | Usuarios del sistema con rol asignado |
| `Equipos` | Equipos de maquinaria con estado |
| `OrdenesTrabajo` | Órdenes de mantenimiento vinculadas a equipos y técnicos |

### Procedimientos Almacenados (25 total)

| Categoría | Procedimientos |
|-----------|---------------|
| **Autenticación** | `sp_ValidarLogin` |
| **Equipos** | `sp_ListarEquiposActivos`, `sp_ListarTodosEquipos`, `sp_InsertarEquipo`, `sp_ActualizarEquipo`, `sp_EliminarEquipo`, `sp_BuscarEquipoPorNombre` |
| **Usuarios** | `sp_ListarUsuarios`, `sp_InsertarUsuario`, `sp_ActualizarUsuario`, `sp_EliminarUsuario`, `sp_BuscarUsuarioPorNombre` |
| **Órdenes** | `sp_InsertarOrden`, `sp_ListarOrdenes`, `sp_ActualizarEstadoOrden`, `sp_ActualizarOrden`, `sp_EliminarOrden`, `sp_BuscarOrdenes`, `sp_ObtenerDetalleOrden`, `sp_ContarAlertasProximas` |
| **Reportes** | `sp_ReporteHistoricoPorEquipo`, `sp_ReporteResumenPorTecnico`, `sp_ReporteOrdenesPorEstado`, `sp_ReportePorTipoMantenimiento` |
| **Función diferencial** | `fn_TiempoPromedioEntreMantenimientos` |

---

## Instrucciones de Instalación

### Requisitos Previos
- Visual Studio 2019 o superior
- .NET Framework 4.7.2
- SQL Server LocalDB (incluido con Visual Studio)

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/Donsan20/wSistemaMantenimientoPreventivoCorrectivo.git
   cd wSistemaMantenimientoPreventivoCorrectivo
   ```

2. **Abrir el proyecto en Visual Studio**
   - Abrir `wSistemaMantenimientoPreventivoCorrectivo.sln`

3. **Restaurar paquetes NuGet**
   - Click derecho en la solución → "Restore NuGet Packages"
   - O desde Package Manager Console: `Update-Package -reinstall`

4. **Ejecutar la base de datos**
   - El archivo `MantenimientoDB.mdf` está incluido en el proyecto
   - La cadena de conexión usa `|DataDirectory|` para portabilidad
   - No se requiere configuración manual de SQL Server

5. **Compilar y ejecutar**
   - Presionar `F5` o click en "Iniciar"

---

## Credenciales de Prueba

| Usuario | Contraseña | Rol |
|---------|-----------|-----|
| `admin` | `123` | Administrador |
| `tecnico` | `123` | Técnico |
| `super` | `123` | Supervisor |

---

## Reglas de Negocio

| Código | Regla |
|--------|-------|
| RN01 | Solo el rol Admin puede crear nuevas órdenes de trabajo |
| RN02 | Un Técnico solo ve funcionalidades operativas (sin módulos administrativos) |
| RN03 | Toda orden nueva nace en estado "Pendiente". Tipos: "Preventivo" o "Correctivo" |
| RN04 | No se puede registrar una orden sin vincularla a un equipo activo |
| RN05 | No se pueden eliminar equipos con órdenes pendientes |
| RN06 | No se puede eliminar al último administrador del sistema |
| RN07 | Solo se pueden eliminar órdenes en estado Pendiente o Cancelada |

---

## Autores

| Nombre | Rol |
|--------|-----|
| Cristian Santos | Desarrollador |
| [Nombre del compañero] | Desarrollador |

---

## Licencia

Proyecto académico desarrollado para fines educativos.
