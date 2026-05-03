using System;
using System.Data;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Ordenes
    {
        private CD_Ordenes objDato = new CD_Ordenes();

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

            return objDato.InsertarOrden(idEquipo, idTecnico, tipoMantenimiento, fechaProgramada, descripcionFalla);
        }

        public DataTable ListarOrdenes()
        {
            return objDato.ListarOrdenes();
        }

        public int ContarAlertasProximas()
        {
            return objDato.ContarAlertasProximas();
        }
    }
}
