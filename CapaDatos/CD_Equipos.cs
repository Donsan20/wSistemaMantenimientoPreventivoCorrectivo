using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Equipos
    {
        /// <summary>
        /// Lista solo los equipos activos (existente del Sprint 1).
        /// </summary>
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

        /// <summary>
        /// Lista TODOS los equipos (activos, inactivos y eliminados).
        /// </summary>
        public DataTable ListarTodosEquipos()
        {
            DataTable dtEquipos = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ListarTodosEquipos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtEquipos);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al listar todos los equipos: " + ex.Message);
                }
            }
            return dtEquipos;
        }

        /// <summary>
        /// Inserta un nuevo equipo en la base de datos.
        /// </summary>
        public bool InsertarEquipo(string nombreEquipo, string estado)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_InsertarEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NombreEquipo", nombreEquipo);
                        command.Parameters.AddWithValue("@Estado", estado);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar el equipo: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de un equipo existente.
        /// </summary>
        public bool ActualizarEquipo(int idEquipo, string nombreEquipo, string estado)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ActualizarEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdEquipo", idEquipo);
                        command.Parameters.AddWithValue("@NombreEquipo", nombreEquipo);
                        command.Parameters.AddWithValue("@Estado", estado);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar el equipo: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Elimina un equipo de forma lógica (cambia estado a 'Eliminado').
        /// </summary>
        public bool EliminarEquipo(int idEquipo)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_EliminarEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdEquipo", idEquipo);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar el equipo: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Busca equipos por nombre (búsqueda parcial con LIKE).
        /// </summary>
        public DataTable BuscarEquipoPorNombre(string busqueda)
        {
            DataTable dtEquipos = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_BuscarEquipoPorNombre", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Busqueda", busqueda);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtEquipos);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al buscar equipos: " + ex.Message);
                }
            }
            return dtEquipos;
        }
    }
}
