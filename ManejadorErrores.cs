using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    /// <summary>
    /// Clase utilitaria para manejo estructurado de errores.
    /// Centraliza la captura, registro y presentación de excepciones.
    /// Aplica el principio SRP separando la lógica de error del código de negocio.
    /// </summary>
    public static class ManejadorErrores
    {
        private static readonly string _rutaLog = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "errores.log"
        );

        /// <summary>
        /// Registra una excepción en el archivo de log y en la consola de depuración.
        /// </summary>
        public static void Registrar(Exception ex, string contexto = "")
        {
            try
            {
                string entrada = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                                 $"Contexto: {contexto} | " +
                                 $"Tipo: {ex.GetType().Name} | " +
                                 $"Mensaje: {ex.Message} | " +
                                 $"StackTrace: {ex.StackTrace}";

                File.AppendAllText(_rutaLog, entrada + Environment.NewLine + Environment.NewLine);
                System.Diagnostics.Debug.WriteLine(entrada);
            }
            catch
            {
                // Si falla el log, no interrumpir el flujo principal
            }
        }

        /// <summary>
        /// Muestra un mensaje amigable al usuario según el tipo de excepción.
        /// </summary>
        public static void MostrarMensajeAmigable(Exception ex, string titulo = "Error")
        {
            string mensaje;

            if (EsErrorDeBaseDeDatos(ex))
            {
                mensaje = "Ocurrió un problema con la base de datos. Por favor, intente nuevamente más tarde.";
            }
            else if (EsErrorDeValidacion(ex))
            {
                mensaje = ex.Message; // Mostrar el mensaje de validación directamente
            }
            else if (esErrorDeConexion(ex))
            {
                mensaje = "No se pudo conectar con la base de datos. Verifique que el archivo de base de datos esté presente.";
            }
            else
            {
                mensaje = "Ocurrió un error inesperado. Por favor, contacte al administrador del sistema.";
            }

            Registrar(ex, titulo);

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Determina si la excepción es un error de base de datos SQL.
        /// </summary>
        public static bool EsErrorDeBaseDeDatos(Exception ex)
        {
            return ex is SqlException ||
                   (ex.InnerException != null && ex.InnerException is SqlException);
        }

        /// <summary>
        /// Determina si la excepción es un error de validación de datos.
        /// </summary>
        public static bool EsErrorDeValidacion(Exception ex)
        {
            return ex is ArgumentException ||
                   ex is ArgumentNullException ||
                   ex is InvalidOperationException;
        }

        /// <summary>
        /// Determina si la excepción es un error de conexión.
        /// </summary>
        private static bool esErrorDeConexion(Exception ex)
        {
            return ex.Message.Contains("connection") ||
                   ex.Message.Contains("conectar") ||
                   ex.Message.Contains("database") ||
                   ex.Message.Contains("base de datos");
        }
    }
}
