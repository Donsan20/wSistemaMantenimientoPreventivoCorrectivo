using System;
using System.Data;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    public class CD_Usuarios
    {
        /// <summary>
        /// Valida credenciales de acceso (existente del Sprint 1).
        /// </summary>
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

        /// <summary>
        /// Lista todos los usuarios con su rol asociado.
        /// </summary>
        public DataTable ListarUsuarios()
        {
            DataTable dtUsuarios = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_ListarUsuarios", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtUsuarios);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al listar los usuarios: " + ex.Message);
                }
            }
            return dtUsuarios;
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        public bool InsertarUsuario(string username, string password, bool estado, int idRol)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_InsertarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Estado", estado);
                        command.Parameters.AddWithValue("@IdRol", idRol);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al insertar el usuario: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        public bool ActualizarUsuario(int idUsuario, string username, string password, bool estado, int idRol)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ActualizarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Estado", estado);
                        command.Parameters.AddWithValue("@IdRol", idRol);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar el usuario: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Elimina un usuario de forma lógica (cambia estado a 0).
        /// </summary>
        public bool EliminarUsuario(int idUsuario)
        {
            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_EliminarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        int filasAfectadas = command.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al eliminar el usuario: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Busca usuarios por nombre de usuario (búsqueda parcial con LIKE).
        /// </summary>
        public DataTable BuscarUsuarioPorNombre(string busqueda)
        {
            DataTable dtUsuarios = new DataTable();

            using (SqlConnection connection = CD_Conexion.Instancia.ObtenerConexion())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("sp_BuscarUsuarioPorNombre", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Busqueda", busqueda);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dtUsuarios);
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al buscar usuarios: " + ex.Message);
                }
            }
            return dtUsuarios;
        }
    }
}
