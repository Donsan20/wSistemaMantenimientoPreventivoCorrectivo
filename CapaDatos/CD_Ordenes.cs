using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Ordenes
    {
        public bool InsertarOrden(int idEquipo, int idTecnico, string tipoMantenimiento, DateTime fechaProgramada, string descripcionFalla)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_InsertarOrden", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdEquipo", idEquipo);
                        command.Parameters.AddWithValue("@IdTecnico", idTecnico);
                        command.Parameters.AddWithValue("@TipoMantenimiento", tipoMantenimiento);
                        command.Parameters.AddWithValue("@FechaProgramada", fechaProgramada);
                        command.Parameters.AddWithValue("@DescripcionFalla", descripcionFalla);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al guardar en la base de datos: " + ex.Message);
                }
            }
        }

        public DataTable ListarOrdenes()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ListarOrdenes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener las órdenes: " + ex.Message);
                }
            }
            return tabla;
        }

        public int ContarAlertasProximas()
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ContarAlertasProximas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al contar las alertas: " + ex.Message);
                }
            }
        }
    }
}
