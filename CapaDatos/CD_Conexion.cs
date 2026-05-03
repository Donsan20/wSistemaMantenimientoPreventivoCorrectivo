using System;
using System.Data.SqlClient;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaDatos
{
    // Patrón GoF: Singleton para asegurar una única instancia de conexión
    public class CD_Conexion
    {
        private static CD_Conexion _instancia;
        private readonly string cadenaConexion;

        // Constructor privado (parte del patrón Singleton)
        private CD_Conexion()
        {
            cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MantenimientoDB.mdf;Integrated Security=True;";
        }

        // Método estático para obtener la única instancia
        public static CD_Conexion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Conexion();
                }
                return _instancia;
            }
        }

        // Método para obtener una nueva conexión SQL
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
