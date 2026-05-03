<<<<<<< HEAD
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
=======
# Sistema de Mantenimiento Preventivo y Correctivo

Bienvenido al repositorio del **Sistema de Mantenimiento**, un proyecto desarrollado para gestionar de forma eficiente el mantenimiento de equipos industriales y tecnológicos.

Este proyecto corresponde a los entregables del **Sprint 1**, donde se ha construido la arquitectura base de la aplicación, el modelado de la base de datos y la interfaz gráfica de usuario en Windows Forms.

## 🛠️ Tecnologías Utilizadas

- **Lenguaje:** C# (.NET Framework)
- **Interfaz Gráfica:** Windows Forms (WinForms)
- **Base de Datos:** Microsoft SQL Server (LocalDB)
- **Arquitectura:** Patrón en Capas (Capa de Presentación y Capa de Acceso a Datos)
- **Control de Versiones:** Git & GitHub

## 📂 Contenido del Sprint 1

En la rama principal (`master`), encontrarás:
- El código fuente de la aplicación C# (Formularios y lógica).
- El archivo de creación y población de la base de datos (`MantenimientoDB.sql`).
- El archivo de la base de datos pre-configurada (`MantenimientoDB.mdf` y su log).
- El documento teórico completo en el archivo `Documentacion_Sprint1.md`.

## 🚀 Requisitos para Ejecutar el Proyecto

Para garantizar el correcto funcionamiento del proyecto en un entorno local al clonar el repositorio, es necesario cumplir con el siguiente requisito indispensable de infraestructura:

> [!IMPORTANT]
> **Componente SQL Server Express LocalDB**  
> El sistema se conecta dinámicamente a la instancia `(localdb)\MSSQLLocalDB`. Este componente viene preinstalado con la carga de trabajo de *.NET Desktop Development* o *Data storage and processing* de Visual Studio.  
> **Si al iniciar la aplicación recibe un "Error 52: No se puede ubicar la instalación de Local Database Runtime"**, abra el *Visual Studio Installer*, vaya a *Componentes individuales*, busque **SQL Server Express LocalDB**, instálelo y vuelva a ejecutar el proyecto.

## ⚙️ Instrucciones de Instalación

1. Clona el repositorio en tu máquina local:
   ```bash
   git clone https://github.com/Donsan20/wSistemaMantenimientoPreventivoCorrectivo.git
   ```
2. Abre la solución `.sln` usando **Visual Studio 2019 o 2022**.
3. Asegúrate de tener seleccionado el proyecto principal como Proyecto de Inicio.
4. Presiona el botón **Iniciar** (o F5) para compilar y ejecutar el sistema.
5. Usa las credenciales de prueba configuradas en la base de datos para ingresar (Ejemplo: Rol Admin).

## 📄 Documentación Teórica

Para leer el Análisis de Requisitos, las Reglas de Negocio, el Diagrama de Clases y el Modelo Entidad-Relación (ER), por favor consulte el archivo [Documentacion_Sprint1.md](./Documentacion_Sprint1.md).

---
*Desarrollado como entrega universitaria para la cátedra de Programación/Ingeniería de Software.*
>>>>>>> a89a288b2fe6b3c80d9c93ae1ce4a8dbe904b1ff
