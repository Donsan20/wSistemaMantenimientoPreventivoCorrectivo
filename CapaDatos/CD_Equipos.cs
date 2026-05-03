using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Equipos
    {
        public DataTable ListarEquiposActivos()
        {
            DataTable dtEquipos = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ListarEquiposActivos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtEquipos);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al cargar los equipos desde la base de datos: " + ex.Message);
                }
            }
            return dtEquipos;
        }
    }
}
