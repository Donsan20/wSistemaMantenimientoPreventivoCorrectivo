using System;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    /// <summary>
    /// Clase utilitaria para validaciones comunes.
    /// Aplica el principio SRP (Single Responsibility Principle) centralizando
    /// toda la lógica de validación en un solo lugar.
    /// </summary>
    public static class Validador
    {
        /// <summary>
        /// Valida que un texto no esté vacío ni sea solo espacios en blanco.
        /// </summary>
        public static bool EsTextoValido(string texto, int longitudMax = 0)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return false;
            }

            if (longitudMax > 0 && texto.Trim().Length > longitudMax)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un número entero sea positivo (mayor a cero).
        /// </summary>
        public static bool EsNumeroPositivo(int valor)
        {
            return valor > 0;
        }

        /// <summary>
        /// Valida que una fecha sea lógica.
        /// </summary>
        /// <param name="fecha">La fecha a validar.</param>
        /// <param name="permitirPasado">Si es false, rechaza fechas anteriores a hoy.</param>
        public static bool EsFechaValida(DateTime fecha, bool permitirPasado = false)
        {
            if (!permitirPasado && fecha.Date < DateTime.Today)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un campo obligatorio no esté vacío.
        /// Lanza una excepción con un mensaje personalizado si falla.
        /// </summary>
        public static void ValidarCampoObligatorio(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException($"El campo '{nombreCampo}' es obligatorio y no puede estar vacío.");
            }
        }

        /// <summary>
        /// Valida que un texto no exceda una longitud máxima.
        /// Lanza una excepción si falla.
        /// </summary>
        public static void ValidarLongitudMaxima(string valor, string nombreCampo, int longitudMax)
        {
            if (valor != null && valor.Length > longitudMax)
            {
                throw new ArgumentException($"El campo '{nombreCampo}' no puede exceder los {longitudMax} caracteres.");
            }
        }
    }
}
