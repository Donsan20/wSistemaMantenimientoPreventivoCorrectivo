# Sistema de Mantenimiento Preventivo y Correctivo ⚙️

Este proyecto es una aplicación de escritorio desarrollada en **C# (Windows Forms)** que permite gestionar y hacer seguimiento a las órdenes de mantenimiento preventivo y correctivo de los equipos de una empresa. El software está respaldado por una base de datos relacional **SQL Server** y sigue una **Arquitectura de 3 Capas** para garantizar escalabilidad, seguridad y buenas prácticas de ingeniería de software.

## 🚀 Características Principales

*   **Autenticación y Roles:** Acceso seguro con validación de credenciales. Diferentes niveles de acceso:
    *   `Administrador`: Control total del sistema.
    *   `Técnico`: Puede crear órdenes pero no tiene acceso a ciertos módulos (equipos/reportes).
    *   `Supervisor`: Puede ver reportes y alertas, pero no puede crear órdenes.
*   **Gestión de Órdenes de Trabajo:** Creación ágil de mantenimientos clasificándolos en preventivos o correctivos.
*   **Alertas Tempranas:** Sistema inteligente de colores en el Dashboard que alerta visualmente si hay mantenimientos programados para los próximos 7 días.
*   **Filtros en Tiempo Real:** Búsqueda dinámica de órdenes por tipo (Preventivo/Correctivo) y estado (Pendiente/Completada).

## 🏗 Arquitectura del Software

El proyecto aplica los principios **SOLID** y está estructurado en 3 capas (N-Tier Architecture):
1.  **Capa de Presentación (UI):** Contiene los formularios visuales. No hay lógica de acceso a datos aquí.
2.  **Capa de Negocio (BLL):** Valida las reglas del negocio (fechas, permisos, campos obligatorios).
3.  **Capa de Datos (DAL):** Interactúa exclusivamente con la base de datos a través de **Procedimientos Almacenados (Stored Procedures)**, previniendo la inyección SQL. Implementa el **Patrón Singleton** para gestionar eficientemente las conexiones.

## 🛠 Requisitos del Sistema

*   **IDE:** Visual Studio 2022 (o superior).
*   **Framework:** .NET Framework 4.7.2.
*   **Base de Datos:** Microsoft SQL Server (LocalDB o Express).

## ⚙️ Instrucciones de Uso y Configuración

1.  **Clonar el repositorio:**
    ```bash
    git clone [URL_DEL_REPOSITORIO]
    ```
2.  **Abrir el Proyecto:**
    *   Abre el archivo `wSistemaMantenimientoPreventivoCorrectivo.sln` usando Visual Studio.
3.  **Configurar Base de Datos:**
    *   El proyecto utiliza un archivo `.mdf` local que se auto-adjunta. No se requiere configuración adicional de cadenas de conexión.
4.  **Ejecución:**
    *   Haz clic en "Iniciar" en Visual Studio.
    *   Puedes iniciar sesión con los siguientes usuarios de prueba:
        *   **Admin:** `admin` / `123`
        *   **Técnico:** `tecnico` / `123`
        *   **Supervisor:** `super` / `123`

---
*Desarrollado como proyecto universitario para la asignatura de Herramientas de Programación II.*