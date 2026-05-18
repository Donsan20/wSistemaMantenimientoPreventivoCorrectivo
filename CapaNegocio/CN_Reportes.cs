using System;
using System.Data;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Reportes
    {
        private CD_Reportes objDato = new CD_Reportes();

        /// <summary>
        /// Genera el histórico de mantenimientos de un equipo.
        /// </summary>
        public DataTable ReporteHistoricoPorEquipo(int idEquipo)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idEquipo <= 0)
            {
                throw new Exception("El ID del equipo no es válido.");
            }

            return objDato.ReporteHistoricoPorEquipo(idEquipo);
        }

        /// <summary>
        /// Genera el resumen de órdenes por técnico.
        /// </summary>
        public DataTable ReporteResumenPorTecnico()
        {
            return objDato.ReporteResumenPorTecnico();
        }

        /// <summary>
        /// Genera el conteo de órdenes agrupadas por estado.
        /// </summary>
        public DataTable ReporteOrdenesPorEstado()
        {
            return objDato.ReporteOrdenesPorEstado();
        }

        /// <summary>
        /// Genera el reporte de órdenes por tipo de mantenimiento con porcentajes.
        /// </summary>
        public DataTable ReportePorTipoMantenimiento()
        {
            return objDato.ReportePorTipoMantenimiento();
        }

        /// <summary>
        /// Calcula el tiempo promedio (en días) entre mantenimientos completados de un equipo.
        /// </summary>
        public double TiempoPromedioEntreMantenimientos(int idEquipo)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idEquipo <= 0)
            {
                throw new Exception("El ID del equipo no es válido.");
            }

            return objDato.TiempoPromedioEntreMantenimientos(idEquipo);
        }
    }
}
