using System;
using System.Data;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Ordenes
    {
        private CD_Ordenes objDato = new CD_Ordenes();

        /// <summary>
        /// Inserta una nueva orden con validaciones de negocio (existente del Sprint 1).
        /// </summary>
        public bool InsertarOrden(int idEquipo, int idTecnico, string tipoMantenimiento, DateTime fechaProgramada, string descripcionFalla)
        {
            // Regla de Negocio: La descripción es obligatoria
            if (string.IsNullOrWhiteSpace(descripcionFalla))
            {
                throw new Exception("La descripción de la falla no puede estar vacía.");
            }

            // Regla de Negocio: Un mantenimiento programado no debe ser en el pasado
            if (fechaProgramada.Date < DateTime.Today)
            {
                throw new Exception("La fecha programada no puede ser menor a la fecha actual.");
            }

            // Regla de Negocio: El tipo de mantenimiento debe ser válido
            if (tipoMantenimiento != "Preventivo" && tipoMantenimiento != "Correctivo")
            {
                throw new Exception("El tipo de mantenimiento debe ser 'Preventivo' o 'Correctivo'.");
            }

            return objDato.InsertarOrden(idEquipo, idTecnico, tipoMantenimiento, fechaProgramada, descripcionFalla);
        }

        /// <summary>
        /// Lista todas las órdenes de trabajo (existente del Sprint 1).
        /// </summary>
        public DataTable ListarOrdenes()
        {
            return objDato.ListarOrdenes();
        }

        /// <summary>
        /// Cuenta las alertas de mantenimientos próximos (existente del Sprint 1).
        /// </summary>
        public int ContarAlertasProximas()
        {
            return objDato.ContarAlertasProximas();
        }

        /// <summary>
        /// Actualiza el estado de una orden con validaciones de negocio.
        /// </summary>
        public bool ActualizarEstadoOrden(int idOrden, string nuevoEstado)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idOrden <= 0)
            {
                throw new Exception("El ID de la orden no es válido.");
            }

            // Regla de Negocio: El estado debe ser uno de los permitidos
            string[] estadosValidos = { "Pendiente", "En Proceso", "Completada", "Cancelada" };
            if (Array.IndexOf(estadosValidos, nuevoEstado) == -1)
            {
                throw new Exception("Estado no válido. Use: Pendiente, En Proceso, Completada o Cancelada.");
            }

            return objDato.ActualizarEstadoOrden(idOrden, nuevoEstado);
        }

        /// <summary>
        /// Actualiza todos los campos de una orden con validaciones de negocio.
        /// </summary>
        public bool ActualizarOrden(int idOrden, int idEquipo, int idTecnico, string tipoMantenimiento, DateTime fechaProgramada, string descripcionFalla, string estadoOrden)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idOrden <= 0)
            {
                throw new Exception("El ID de la orden no es válido.");
            }

            // Regla de Negocio: La descripción es obligatoria
            if (string.IsNullOrWhiteSpace(descripcionFalla))
            {
                throw new Exception("La descripción de la falla no puede estar vacía.");
            }

            // Regla de Negocio: El tipo de mantenimiento debe ser válido
            if (tipoMantenimiento != "Preventivo" && tipoMantenimiento != "Correctivo")
            {
                throw new Exception("El tipo de mantenimiento debe ser 'Preventivo' o 'Correctivo'.");
            }

            // Regla de Negocio: El estado debe ser válido
            string[] estadosValidos = { "Pendiente", "En Proceso", "Completada", "Cancelada" };
            if (Array.IndexOf(estadosValidos, estadoOrden) == -1)
            {
                throw new Exception("Estado no válido.");
            }

            return objDato.ActualizarOrden(idOrden, idEquipo, idTecnico, tipoMantenimiento, fechaProgramada, descripcionFalla, estadoOrden);
        }

        /// <summary>
        /// Elimina una orden con validaciones de negocio.
        /// </summary>
        public bool EliminarOrden(int idOrden)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idOrden <= 0)
            {
                throw new Exception("El ID de la orden no es válido.");
            }

            return objDato.EliminarOrden(idOrden);
        }

        /// <summary>
        /// Busca órdenes con filtros múltiples (los filtros son opcionales).
        /// </summary>
        public DataTable BuscarOrdenes(string equipo, string tipoMantenimiento, string estadoOrden, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            // Regla de Negocio: Si se proporciona fechaDesde y fechaHasta, desde debe ser <= hasta
            if (fechaDesde.HasValue && fechaHasta.HasValue && fechaDesde.Value > fechaHasta.Value)
            {
                throw new Exception("La fecha inicial no puede ser mayor a la fecha final.");
            }

            return objDato.BuscarOrdenes(equipo, tipoMantenimiento, estadoOrden, fechaDesde, fechaHasta);
        }

        /// <summary>
        /// Obtiene el detalle completo de una orden.
        /// </summary>
        public DataTable ObtenerDetalleOrden(int idOrden)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idOrden <= 0)
            {
                throw new Exception("El ID de la orden no es válido.");
            }

            return objDato.ObtenerDetalleOrden(idOrden);
        }
    }
}
