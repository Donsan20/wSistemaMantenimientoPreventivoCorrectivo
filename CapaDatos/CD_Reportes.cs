using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Reportes
    {
        /// <summary>
        /// Reporte: Histórico completo de mantenimientos de un equipo.
        /// </summary>
        public DataTable ReporteHistoricoPorEquipo(int idEquipo)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ReporteHistoricoPorEquipo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdEquipo", idEquipo);
                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al generar el histórico del equipo: " + ex.Message);
                }
            }
            return tabla;
        }

        /// <summary>
        /// Reporte: Resumen de órdenes por técnico (totales, completadas, pendientes, etc.).
        /// </summary>
        public DataTable ReporteResumenPorTecnico()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ReporteResumenPorTecnico", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al generar el resumen por técnico: " + ex.Message);
                }
            }
            return tabla;
        }

        /// <summary>
        /// Reporte: Cantidad de órdenes agrupadas por estado.
        /// </summary>
        public DataTable ReporteOrdenesPorEstado()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ReporteOrdenesPorEstado", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al generar el reporte por estado: " + ex.Message);
                }
            }
            return tabla;
        }

        /// <summary>
        /// Reporte: Cantidad y porcentaje de órdenes por tipo de mantenimiento.
        /// </summary>
        public DataTable ReportePorTipoMantenimiento()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ReportePorTipoMantenimiento", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(command);
                        adaptador.Fill(tabla);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al generar el reporte por tipo: " + ex.Message);
                }
            }
            return tabla;
        }

        /// <summary>
        /// Función diferencial: Calcula el tiempo promedio (en días) entre mantenimientos completados de un equipo.
        /// </summary>
        public double TiempoPromedioEntreMantenimientos(int idEquipo)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(
                        "SELECT dbo.fn_TiempoPromedioEntreMantenimientos(@IdEquipo)", connection))
                    {
                        command.Parameters.AddWithValue("@IdEquipo", idEquipo);
                        object resultado = command.ExecuteScalar();
                        return resultado != DBNull.Value ? Convert.ToDouble(resultado) : 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al calcular el tiempo promedio: " + ex.Message);
                }
            }
        }
    }
}
