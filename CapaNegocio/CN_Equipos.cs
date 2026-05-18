using System;
using System.Data;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Equipos
    {
        private CD_Equipos objDato = new CD_Equipos();

        /// <summary>
        /// Lista solo los equipos activos (existente del Sprint 1).
        /// </summary>
        public DataTable ListarEquiposActivos()
        {
            return objDato.ListarEquiposActivos();
        }

        /// <summary>
        /// Lista TODOS los equipos (activos, inactivos y eliminados).
        /// </summary>
        public DataTable ListarTodosEquipos()
        {
            return objDato.ListarTodosEquipos();
        }

        /// <summary>
        /// Inserta un nuevo equipo con validaciones de negocio.
        /// </summary>
        public bool InsertarEquipo(string nombreEquipo, string estado)
        {
            // Regla de Negocio: El nombre es obligatorio
            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                throw new Exception("El nombre del equipo no puede estar vacío.");
            }

            // Regla de Negocio: El nombre no debe exceder 100 caracteres
            if (nombreEquipo.Trim().Length > 100)
            {
                throw new Exception("El nombre del equipo no puede exceder los 100 caracteres.");
            }

            // Regla de Negocio: El estado debe ser válido
            if (string.IsNullOrWhiteSpace(estado) || (estado != "Activo" && estado != "Inactivo"))
            {
                throw new Exception("El estado debe ser 'Activo' o 'Inactivo'.");
            }

            return objDato.InsertarEquipo(nombreEquipo.Trim(), estado);
        }

        /// <summary>
        /// Actualiza un equipo existente con validaciones de negocio.
        /// </summary>
        public bool ActualizarEquipo(int idEquipo, string nombreEquipo, string estado)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idEquipo <= 0)
            {
                throw new Exception("El ID del equipo no es válido.");
            }

            // Regla de Negocio: El nombre es obligatorio
            if (string.IsNullOrWhiteSpace(nombreEquipo))
            {
                throw new Exception("El nombre del equipo no puede estar vacío.");
            }

            // Regla de Negocio: El nombre no debe exceder 100 caracteres
            if (nombreEquipo.Trim().Length > 100)
            {
                throw new Exception("El nombre del equipo no puede exceder los 100 caracteres.");
            }

            // Regla de Negocio: El estado debe ser válido
            if (string.IsNullOrWhiteSpace(estado) || (estado != "Activo" && estado != "Inactivo" && estado != "Eliminado"))
            {
                throw new Exception("El estado debe ser 'Activo', 'Inactivo' o 'Eliminado'.");
            }

            return objDato.ActualizarEquipo(idEquipo, nombreEquipo.Trim(), estado);
        }

        /// <summary>
        /// Elimina un equipo de forma lógica con validaciones de negocio.
        /// </summary>
        public bool EliminarEquipo(int idEquipo)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idEquipo <= 0)
            {
                throw new Exception("El ID del equipo no es válido.");
            }

            return objDato.EliminarEquipo(idEquipo);
        }

        /// <summary>
        /// Busca equipos por nombre con validación de negocio.
        /// </summary>
        public DataTable BuscarEquipoPorNombre(string busqueda)
        {
            // Regla de Negocio: La búsqueda no puede estar vacía
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                throw new Exception("Ingrese un término de búsqueda.");
            }

            return objDato.BuscarEquipoPorNombre(busqueda.Trim());
        }
    }
}
