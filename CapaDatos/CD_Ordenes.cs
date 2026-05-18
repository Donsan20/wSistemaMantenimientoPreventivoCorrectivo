using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Ordenes
    {
        /// <summary>
        /// Inserta una nueva orden de trabajo (existente del Sprint 1).
        /// </summary>
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

        /// <summary>
        /// Lista todas las órdenes de trabajo (existente del Sprint 1).
        /// </summary>
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

        /// <summary>
        /// Cuenta las alertas de mantenimientos próximos (existente del Sprint 1).
        /// </summary>
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

        /// <summary>
        /// Actualiza únicamente el estado de una orden.
        /// </summary>
        public bool ActualizarEstadoOrden(int idOrden, string nuevoEstado)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ActualizarEstadoOrden", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdOrden", idOrden);
                        command.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar el estado de la orden: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Actualiza todos los campos de una orden de trabajo.
        /// </summary>
        public bool ActualizarOrden(int idOrden, int idEquipo, int idTecnico, string tipoMantenimiento, DateTime fechaProgramada, string descripcionFalla, string estadoOrden)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ActualizarOrden", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdOrden", idOrden);
                        command.Parameters.AddWithValue("@IdEquipo", idEquipo);
                        command.Parameters.AddWithValue("@IdTecnico", idTecnico);
                        command.Parameters.AddWithValue("@TipoMantenimiento", tipoMantenimiento);
                        command.Parameters.AddWithValue("@FechaProgramada", fechaProgramada);
                        command.Parameters.AddWithValue("@DescripcionFalla", descripcionFalla);
                        command.Parameters.AddWithValue("@EstadoOrden", estadoOrden);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar la orden: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Elimina una orden (solo si está Pendiente o Cancelada).
        /// </summary>
        public bool EliminarOrden(int idOrden)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_EliminarOrden", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdOrden", idOrden);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar la orden: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Busca órdenes con filtros múltiples (equipo, tipo, estado, fechas).
        /// </summary>
        public DataTable BuscarOrdenes(string equipo, string tipoMantenimiento, string estadoOrden, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_BuscarOrdenes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Equipo", string.IsNullOrEmpty(equipo) ? (object)DBNull.Value : equipo);
                        command.Parameters.AddWithValue("@TipoMantenimiento", string.IsNullOrEmpty(tipoMantenimiento) ? (object)DBNull.Value : tipoMantenimiento);
                        command.Parameters.AddWithValue("@EstadoOrden", string.IsNullOrEmpty(estadoOrden) ? (object)DBNull.Value : estadoOrden);
                        command.Parameters.AddWithValue("@FechaDesde", fechaDesde.HasValue ? (object)fechaDesde.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@FechaHasta", fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value);

                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al buscar órdenes: " + ex.Message);
                }
            }
            return tabla;
        }

        /// <summary>
        /// Obtiene el detalle completo de una orden específica.
        /// </summary>
        public DataTable ObtenerDetalleOrden(int idOrden)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ObtenerDetalleOrden", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdOrden", idOrden);
                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener el detalle de la orden: " + ex.Message);
                }
            }
            return tabla;
        }
    }
}
