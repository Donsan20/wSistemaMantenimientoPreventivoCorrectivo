# Documentación Sprint 1 - Sistema de Mantenimiento Preventivo y Correctivo

Este documento detalla el cumplimiento exacto de los entregables requeridos para el **Sprint 1**, estructurado de manera formal y directa para facilitar su evaluación.

---

## 1. Problema y Objetivos del Sistema
* **Problema:** En el sector industrial, el control de mantenimiento de la maquinaria (motores, tornos, etc.) suele realizarse de forma manual o mediante planillas de cálculo. Esta desorganización genera pérdida de trazabilidad técnica, imposibilidad de anticipar fallas y paradas de producción imprevistas que aumentan los costos operativos.
* **Objetivo del Sistema:** Desarrollar un sistema de escritorio centralizado que permita digitalizar la gestión de mantenimiento, agendar órdenes de trabajo (preventivas y correctivas), controlar el historial de cada equipo y generar alertas tempranas de manera automatizada.

## 2. Requisitos Funcionales y No Funcionales

### Requisitos Funcionales (RF)
*   **RF01 - Autenticación:** Acceso restringido mediante credenciales validadas en base de datos.
*   **RF02 - Gestión de Roles:** Funcionalidades e interfaces adaptables dinámicamente según el rol del usuario (Administrador, Técnico, Supervisor).
*   **RF03 - Dashboard General:** Visualización en tiempo real de órdenes activas, responsables y estado actual.
*   **RF04 - Gestión de Órdenes:** Capacidad para crear y consultar órdenes de trabajo asignadas a equipos específicos.
*   **RF05 - Alertas Tempranas:** Notificación visual de mantenimientos críticos programados para los próximos 7 días.
*   **RF06 - Filtrado Optimizado:** Búsqueda y filtrado de órdenes de trabajo gestionado en memoria (DataView) para no saturar el servidor SQL.

### Requisitos No Funcionales (RNF)
*   **RNF01 - Plataforma:** Aplicación de escritorio desarrollada en C# utilizando Windows Forms.
*   **RNF02 - Motor de Base de Datos:** Persistencia de datos en SQL Server (LocalDB con `DataDirectory`) para garantizar portabilidad sin configuraciones complejas de servidor.
*   **RNF03 - Seguridad:** Uso estricto de Procedimientos Almacenados (Stored Procedures) para la interacción con la base de datos, previniendo ataques de inyección SQL.

## 3. Reglas de Negocio
*   **RN01 - Restricción de Creación:** Exclusivamente el rol "Admin" posee los privilegios para crear nuevas órdenes de trabajo en el sistema.
*   **RN02 - Aislamiento Técnico:** Un rol "Técnico" solo tiene visibilidad e interacción con las funcionalidades operativas (se le ocultan módulos administrativos).
*   **RN03 - Ciclo de Vida de la Orden:** Toda orden nueva nace por defecto en estado "Pendiente". Los tipos de mantenimiento son estrictamente "Preventivo" o "Correctivo".
*   **RN04 - Integridad Relacional:** No es posible registrar una orden sin vincularla a un equipo activo previamente registrado.

## 4. Arquitectura Propuesta
El sistema implementa una **Arquitectura en 3 Capas (N-Tier Architecture)**, separando lógicamente las responsabilidades para garantizar la mantenibilidad y escalabilidad del código:
*   **Capa de Presentación (UI):** Formularios de Windows Forms. Se encargan exclusivamente de la interacción con el usuario y la captura de datos.
*   **Capa de Negocio (BLL):** Orquesta las validaciones y aplica las reglas de negocio antes de permitir la comunicación con la base de datos.
*   **Capa de Datos (DAL):** Única capa con acceso a la librería ADO.NET. Implementa el **Patrón de Diseño GoF Singleton** en la clase de conexión para garantizar una única instancia activa, optimizando el consumo de recursos.

## 5. Diagrama de Clases Preliminar
> 📄 **[Ver Diagrama de Clases (Formato PDF)](./pdf/diagrama_clases.pdf)**

## 6. Modelo Entidad-Relación y BD Normalizada
El diseño relacional se encuentra normalizado hasta la Tercera Forma Normal (3FN), eliminando redundancias y garantizando la integridad referencial.
> 📄 **[Ver Modelo Entidad-Relación (Formato PDF)](./pdf/diagrama_er.pdf)**

## 7. Script Inicial de BD
El script fundacional entregado (`MantenimientoDB.sql`) incluye la creación de la base de datos, la estructura de las tablas, restricciones de llaves foráneas y la inserción de datos semilla para pruebas funcionales. Adicionalmente, incluye el esquema de Procedimientos Almacenados que utiliza el backend.

## 8. Prototipo del FrontEnd en Windows Forms
El prototipo excede el requisito de "solo diseño visual", logrando las siguientes integraciones operativas:
*   Pantalla de Login con validación real de credenciales.
*   Dashboard dinámico que renderiza controles y botones de acción dependiendo de los permisos del rol autenticado.
*   Modal de creación de órdenes conectado al motor relacional de la BD.

## 9. Repositorio GitHub con Commits y README Inicial
El proyecto completo, incluyendo código fuente, scripts y esta documentación, se encuentra versionado en un repositorio de GitHub. El mismo cuenta con un archivo `.gitignore` estandarizado para C# y un `README.md` con las instrucciones necesarias para desplegar la base de datos LocalDB y ejecutar el sistema.
