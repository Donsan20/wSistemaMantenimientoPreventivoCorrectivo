using System.Data;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Equipos
    {
        private CD_Equipos objDato = new CD_Equipos();

        public DataTable ListarEquiposActivos()
        {
            // Aquí podríamos agregar más reglas si fuera necesario (ej. solo equipos de cierta categoría)
            return objDato.ListarEquiposActivos();
        }
    }
}
