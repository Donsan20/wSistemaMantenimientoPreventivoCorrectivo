# Manual de Usuario - Sistema de Mantenimiento Preventivo y Correctivo

## 1. Introducción

El **Sistema de Mantenimiento Preventivo y Correctivo** es una aplicación de escritorio diseñada para gestionar y controlar las actividades de mantenimiento de equipos industriales. Permite crear órdenes de trabajo, asignarlas a técnicos, dar seguimiento al estado de cada mantenimiento y generar reportes estadísticos.

---

## 2. Requisitos del Sistema

- **Sistema operativo**: Windows 7 o superior
- **.NET Framework**: Versión 4.7.2 (se instala automáticamente si no está presente)
- **SQL Server LocalDB**: Incluido con Visual Studio (no requiere instalación adicional)
- **Espacio en disco**: Mínimo 50 MB libres

---

## 3. Inicio de Sesión

1. Abra la aplicación haciendo doble clic en el archivo ejecutable.
2. Se mostrará la pantalla de **Login**.
3. Ingrese su **Usuario** y **Contraseña**.
4. Haga clic en el botón **INGRESAR**.

### Credenciales por defecto

| Usuario | Contraseña | Rol |
|---------|-----------|-----|
| `admin` | `123` | Administrador |
| `tecnico` | `123` | Técnico |
| `super` | `123` | Supervisor |

> **Nota**: Si las credenciales son incorrectas, aparecerá un mensaje de error. Verifique que los datos estén bien escritos.

---

## 4. Dashboard (Pantalla Principal)

Al ingresar correctamente, se muestra el **Dashboard de Control** con:

- **Panel lateral izquierdo**: Menú de navegación con los módulos disponibles según su rol.
- **Panel de alertas**: Muestra en rojo si hay mantenimientos programados para los próximos 7 días, o en verde si todo está al día.
- **Tabla de órdenes**: Lista todas las órdenes de trabajo con su código, equipo, tipo, fecha y estado.
- **Filtro**: ComboBox en la esquina superior derecha para filtrar órdenes por tipo (Preventivo/Correctivo) o estado (Pendiente/Completada).

### Menú lateral

| Botón | Función | Visible para |
|-------|---------|-------------|
| **DASHBOARD** | Vuelve a la pantalla principal | Todos |
| **REPORTES** | Abre el módulo de reportes | Admin, Supervisor |
| **EQUIPOS** | Abre la gestión de equipos | Admin, Supervisor |
| **USUARIOS** | Abre la gestión de usuarios | Solo Admin |
| **NUEVA ORDEN** | Abre el formulario de creación de órdenes | Solo Admin |
| **CERRAR SESIÓN** | Cierra sesión y vuelve al Login | Todos |

---

## 5. Gestión de Órdenes de Trabajo

### 5.1 Crear una nueva orden

1. Haga clic en **NUEVA ORDEN** en el menú lateral.
2. Seleccione un **Equipo** del menú desplegable.
3. Seleccione el **Tipo de Mantenimiento** (Preventivo o Correctivo).
4. Seleccione la **Fecha Programada** usando el calendario.
5. Escriba la **Descripción de la Falla** en el campo de texto.
6. Haga clic en **Guardar**.

> **Importante**: La fecha no puede ser anterior al día actual. La descripción es obligatoria.

### 5.2 Editar una orden

1. Abra el módulo de **NUEVA ORDEN**.
2. Haga clic sobre la orden que desea editar en la tabla inferior.
3. Los campos se llenarán automáticamente con los datos de la orden.
4. Modifique los campos que necesite cambiar.
5. Haga clic en **Actualizar** (el botón cambia de texto automáticamente).

### 5.3 Cambiar el estado de una orden

1. Seleccione una orden en la tabla.
2. Seleccione el nuevo estado en el combo **Estado** (Pendiente, En Proceso, Completada, Cancelada).
3. Haga clic en **Cambiar Estado**.

### 5.4 Eliminar una orden

1. Seleccione una orden en la tabla.
2. Haga clic en **Eliminar Orden**.
3. Confirme la eliminación en el cuadro de diálogo.

> **Nota**: Solo se pueden eliminar órdenes en estado **Pendiente** o **Cancelada**.

### 5.5 Buscar órdenes

En la sección **Búsqueda y Filtros**:

- **Equipo**: Escriba el nombre del equipo (búsqueda parcial).
- **Tipo**: Seleccione Preventivo, Correctivo o Todos.
- **Estado**: Seleccione Pendiente, En Proceso, Completada, Cancelada o Todos.
- Haga clic en **Buscar** para aplicar los filtros.
- Haga clic en **Limpiar** para quitar todos los filtros.

---

## 6. Gestión de Equipos

### 6.1 Crear un equipo

1. Haga clic en **EQUIPOS** en el menú lateral.
2. Escriba el **Nombre** del equipo.
3. Seleccione el **Estado** (Activo o Inactivo).
4. Haga clic en **Guardar**.

### 6.2 Editar un equipo

1. Haga clic sobre el equipo en la tabla.
2. Modifique el nombre o el estado.
3. Haga clic en **Actualizar**.

### 6.3 Eliminar un equipo

1. Seleccione un equipo en la tabla.
2. Haga clic en **Eliminar**.
3. Confirme la eliminación.

> **Nota**: No se pueden eliminar equipos que tengan órdenes de trabajo pendientes.

### 6.4 Buscar equipos

Escriba en el campo **Buscar** y los resultados se filtrarán automáticamente mientras escribe.

---

## 7. Gestión de Usuarios

### 7.1 Crear un usuario

1. Haga clic en **USUARIOS** en el menú lateral.
2. Escriba el **Usuario** (nombre de acceso).
3. Escriba la **Contraseña**.
4. Seleccione el **Rol** (Admin, Tecnico o Super).
5. Seleccione el **Estado** (Activo o Inactivo).
6. Haga clic en **Guardar**.

### 7.2 Editar un usuario

1. Haga clic sobre el usuario en la tabla.
2. Modifique los campos necesarios.
3. Haga clic en **Actualizar**.

> **Nota**: Por seguridad, la contraseña no se muestra al seleccionar un usuario. Debe escribirla nuevamente si desea cambiarla.

### 7.3 Eliminar un usuario

1. Seleccione un usuario en la tabla.
2. Haga clic en **Eliminar**.
3. Confirme la eliminación.

> **Nota**: No se puede eliminar al último administrador del sistema.

### 7.4 Buscar usuarios

Escriba en el campo **Buscar** y los resultados se filtrarán automáticamente.

---

## 8. Reportes

Acceda desde el botón **REPORTES** en el menú lateral. El módulo contiene 4 pestañas:

### 8.1 Histórico por Equipo

1. Seleccione un equipo del menú desplegable.
2. Haga clic en **Generar Reporte**.
3. Se mostrará el historial completo de mantenimientos de ese equipo.
4. En la parte inferior se muestra el **tiempo promedio entre mantenimientos** (en días).

### 8.2 Resumen por Técnico

1. Haga clic en **Generar Reporte**.
2. Se muestra una tabla con las estadísticas de cada técnico:
   - Total de órdenes asignadas
   - Completadas, Pendientes, En Proceso, Canceladas

### 8.3 Órdenes por Estado

1. Haga clic en **Generar Reporte**.
2. Se muestra la cantidad de órdenes agrupadas por estado.

### 8.4 Por Tipo de Mantenimiento

1. Haga clic en **Generar Reporte**.
2. Se muestra la cantidad y el porcentaje de órdenes por tipo (Preventivo/Correctivo).

---

## 9. Cerrar Sesión

1. Haga clic en **CERRAR SESIÓN** en el menú lateral.
2. Confirme en el cuadro de diálogo.
3. El sistema volverá a la pantalla de Login.

---

## 10. Preguntas Frecuentes

### ¿Qué hago si no puedo iniciar sesión?
- Verifique que el usuario y contraseña estén correctamente escritos.
- Si es la primera vez, use las credenciales por defecto: `admin` / `123`.

### ¿Por qué no puedo eliminar un equipo?
- El equipo tiene órdenes de trabajo pendientes. Complete o cancele esas órdenes primero.

### ¿Por qué no puedo eliminar un usuario?
- Si es el último administrador, el sistema no lo permite para evitar quedarse sin acceso administrativo.

### ¿Por qué no puedo crear una orden con fecha pasada?
- El sistema solo permite programar mantenimientos para hoy o fechas futuras.

### ¿Qué significa la alerta roja en el Dashboard?
- Hay uno o más mantenimientos programados para los próximos 7 días que requieren atención.

### ¿El sistema guarda un registro de errores?
- Sí. Los errores se registran automáticamente en un archivo llamado `errores.log` en la carpeta de la aplicación.

---

## 11. Soporte

Para reportar problemas o solicitar asistencia, contacte al administrador del sistema o a los desarrolladores del proyecto.
