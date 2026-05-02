using System;
using System.Data;
using System.Data.SqlClient; // El diccionario para SQL Server
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    public static class ConexionBD
    {
        // 1. CENTRALIZAMOS LA CADENA DE CONEXIÓN AQUÍ (Solo se cambia una vez)
        // Usamos LocalDB y DataDirectory para que la base de datos sea un archivo local (.mdf) y no dependa del PC
        private static string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MantenimientoDB.mdf;Integrated Security=True;";

        // Método público para que cualquier formulario pueda obtener la conexión si la necesita
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        // 2. MÉTODO UNIVERSAL PARA CARGAR TABLAS (DataGridView)
        // Recibe la consulta SQL y devuelve un DataTable listo para usar
        public static DataTable CargarDatos(string consultaSql)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = ObtenerConexion())
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(consultaSql, conexion);
                    adaptador.Fill(tabla);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error de base de datos: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return tabla; // Devolvemos la tabla llena (o vacía si hubo error)
        }
    }
}