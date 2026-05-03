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