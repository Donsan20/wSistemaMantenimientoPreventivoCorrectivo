using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Usuarios
    {
        // Usa el procedimiento almacenado para validar credenciales
        public string ValidarLogin(string usuario, string password)
        {
            string rolAsignado = string.Empty;

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ValidarLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Usuario", usuario);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rolAsignado = reader["NombreRol"].ToString();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al conectar con la base de datos: " + ex.Message);
                }
            }

            return rolAsignado;
        }
    }
}
