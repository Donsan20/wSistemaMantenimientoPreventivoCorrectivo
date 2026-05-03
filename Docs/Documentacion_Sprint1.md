# Documentación Sprint 1 - Sistema de Mantenimiento

Este documento consolida los entregables requeridos para el **Sprint 1** del proyecto de Herramientas de Programación II.

---

## a) Definición del problema empresarial

**Contexto:** La empresa manufacturera "Industrias Técnicas S.A." depende en gran medida del correcto funcionamiento de su maquinaria de producción (motores eléctricos, bandas transportadoras, tornos, etc.). 
**Problema:** Actualmente, el registro y control de los mantenimientos de los equipos se lleva a cabo mediante hojas de cálculo en Excel y papeles impresos. Esto ocasiona:
1. Pérdida de información histórica sobre las fallas de los equipos.
2. Imposibilidad de generar alertas tempranas para mantenimientos preventivos.
3. Descoordinación entre los supervisores y los técnicos.
**Solución:** Se requiere desarrollar un "Sistema de Mantenimiento Preventivo y Correctivo" que digitalice y centralice el proceso de creación y seguimiento de órdenes de trabajo, permitiendo a diferentes roles interactuar con la plataforma de acuerdo a sus permisos.

---

## b) Levantamiento y redacción de requisitos funcionales y no funcionales

### Requisitos Funcionales (RF)
*   **RF01 - Autenticación:** El sistema debe permitir a los usuarios iniciar sesión mediante un nombre de usuario y contraseña.
*   **RF02 - Control de Roles:** El sistema debe restringir las funcionalidades dependiendo de si el usuario es Administrador, Técnico o Supervisor.
*   **RF03 - Dashboard Principal:** El sistema debe mostrar un listado general con todas las órdenes de trabajo activas y su estado actual.
*   **RF04 - Filtrado de Órdenes:** El sistema debe permitir filtrar las órdenes en el Dashboard por su estado (Pendiente, Completada) o tipo (Preventivo, Correctivo).
*   **RF05 - Creación de Órdenes:** El sistema debe permitir a los Técnicos y Administradores crear nuevas órdenes seleccionando el equipo, fecha, tipo de mantenimiento y la descripción de la falla.
*   **RF06 - Alertas Visuales:** El sistema debe alertar en el Dashboard cuántos mantenimientos están programados para los próximos 7 días.

### Requisitos No Funcionales (RNF)
*   **RNF01 - Arquitectura:** El sistema debe estar construido utilizando el patrón de Arquitectura de 3 Capas (Presentación, Negocio, Datos).
*   **RNF02 - Patrones de Diseño:** La conexión a la base de datos debe implementar el Patrón Singleton.
*   **RNF03 - Seguridad:** El sistema no debe contener consultas SQL embebidas en el código fuente, debe utilizar Procedimientos Almacenados (Stored Procedures) para prevenir inyección SQL.

---

## c) Identificación y documentación de reglas de negocio

*   **RN01 - Restricción de Fechas:** No se puede programar una orden de mantenimiento preventivo para una fecha que ya haya pasado (menor al día actual).
*   **RN02 - Obligatoriedad de Campos:** Al crear una orden, es estrictamente obligatorio seleccionar un equipo y proveer una descripción textual de la falla.
*   **RN03 - Permisos de Supervisor:** Un usuario con el rol de "Supervisor" no tiene permisos para crear nuevas órdenes de mantenimiento, solo puede monitorearlas.
*   **RN04 - Permisos de Técnico:** Un usuario con el rol de "Técnico" no tiene acceso al panel de gestión de equipos ni a la generación de reportes gerenciales.
*   **RN05 - Estado de Equipos:** Solo se pueden registrar órdenes de mantenimiento sobre equipos cuyo estado en el sistema sea "Activo".

---

## d) Diseño preliminar de arquitectura en capas

El proyecto adopta un diseño **N-Tier Architecture (3 Capas)** aplicando los principios SOLID (específicamente SRP - Principio de Responsabilidad Única):

1.  **Capa de Presentación (UI):** Formularios en Windows Forms. Su única responsabilidad es capturar la interacción del usuario y mostrar datos. No tienen comunicación directa con SQL.
2.  **Capa de Negocio (BLL):** Clases intermedias (`CN_Usuarios`, `CN_Ordenes`, `CN_Equipos`). Reciben las peticiones de la UI, ejecutan validaciones condicionales (Reglas de Negocio) y si todo es correcto, llaman a la Capa de Datos.
3.  **Capa de Datos (DAL):** Clases (`CD_Conexion`, `CD_Usuarios`, `CD_Ordenes`). Su única responsabilidad es abrir la conexión SQL (usando Patrón Singleton) y ejecutar los Procedimientos Almacenados.

---

## e) Diagrama de clases preliminar

```mermaid
classDiagram
    class Form1_Login {
        +btnIngresar_Click()
    }
    class Admin_Dashboard {
        +CargarTablaOrdenes()
        +CargarAlertas()
    }
    class OrdenDeTrabajo {
        +btnguardar_Click()
    }
    
    class CN_Usuarios {
        +ValidarLogin(usuario, password) string
    }
    class CN_Ordenes {
        +InsertarOrden(...) bool
        +ListarOrdenes() DataTable
        +ContarAlertasProximas() int
    }
    class CN_Equipos {
        +ListarEquiposActivos() DataTable
    }
    
    class CD_Usuarios {
        +ValidarLogin(usuario, password) string
    }
    class CD_Ordenes {
        +InsertarOrden(...) bool
        +ListarOrdenes() DataTable
        +ContarAlertasProximas() int
    }
    class CD_Equipos {
        +ListarEquiposActivos() DataTable
    }
    class CD_Conexion {
        -cadenaConexion: string
        -_instancia: CD_Conexion
        +Instancia: CD_Conexion
        +ObtenerConexion() SqlConnection
    }

    Form1_Login --> CN_Usuarios : usa
    Admin_Dashboard --> CN_Ordenes : usa
    OrdenDeTrabajo --> CN_Equipos : usa
    OrdenDeTrabajo --> CN_Ordenes : usa

    CN_Usuarios --> CD_Usuarios : valida
    CN_Ordenes --> CD_Ordenes : invoca
    CN_Equipos --> CD_Equipos : invoca

    CD_Usuarios --> CD_Conexion : usa
    CD_Ordenes --> CD_Conexion : usa
    CD_Equipos --> CD_Conexion : usa
```

---

## f) Modelo entidad-relación

```mermaid
erDiagram
    Roles ||--o{ Usuarios : tiene
    Usuarios ||--o{ OrdenesTrabajo : asignado_a
    Equipos ||--o{ OrdenesTrabajo : sufre

    Roles {
        int IdRol PK
        varchar NombreRol
    }
    Usuarios {
        int IdUsuario PK
        varchar Username
        varchar Password
        bit Estado
        int IdRol FK
    }
    Equipos {
        int IdEquipo PK
        varchar NombreEquipo
        varchar Estado
    }
    OrdenesTrabajo {
        int IdOrden PK
        int IdEquipo FK
        int IdTecnico FK
        varchar TipoMantenimiento
        datetime FechaProgramada
        varchar DescripcionFalla
        varchar EstadoOrden
    }
```
