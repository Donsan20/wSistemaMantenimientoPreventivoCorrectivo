using System;
using System.Windows.Forms;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    /// <summary>
    /// Patrón Factory Method (GoF) para la creación centralizada de formularios.
    /// 
    /// Beneficios:
    /// - Centraliza la lógica de creación de formularios.
    /// - Permite controlar permisos de acceso por rol en un solo lugar.
    /// - Facilita la extensión: agregar un nuevo formulario solo requiere
    ///   modificar esta clase, no todos los lugares donde se instancia.
    /// </summary>
    public static class FabricaFormularios
    {
        /// <summary>
        /// Tipos de formularios disponibles en el sistema.
        /// </summary>
        public enum TipoFormulario
        {
            OrdenDeTrabajo,
            GestionEquipos,
            GestionUsuarios,
            Reportes
        }

        /// <summary>
        /// Crea una instancia del formulario solicitado.
        /// </summary>
        /// <param name="tipo">El tipo de formulario a crear.</param>
        /// <param name="rolUsuario">El rol del usuario actual para validar permisos.</param>
        /// <returns>La instancia del formulario si el usuario tiene permisos, null en caso contrario.</returns>
        public static Form CrearFormulario(TipoFormulario tipo, string rolUsuario)
        {
            if (!TienePermiso(tipo, rolUsuario))
            {
                ManejadorErrores.MostrarMensajeAmigable(
                    new UnauthorizedAccessException("No tiene permisos para acceder a esta función."),
                    "Acceso Denegado"
                );
                return null;
            }

            switch (tipo)
            {
                case TipoFormulario.OrdenDeTrabajo:
                    return new OrdenDeTrabajo();

                case TipoFormulario.GestionEquipos:
                    return new GestionEquipos();

                case TipoFormulario.GestionUsuarios:
                    return new GestionUsuarios();

                case TipoFormulario.Reportes:
                    return new Reportes();

                default:
                    throw new ArgumentException($"Tipo de formulario desconocido: {tipo}");
            }
        }

        /// <summary>
        /// Valida si un rol tiene permiso para acceder a un tipo de formulario.
        /// </summary>
        private static bool TienePermiso(TipoFormulario tipo, string rolUsuario)
        {
            // Normalizar rol
            string rol = rolUsuario?.ToLower() ?? "";

            switch (tipo)
            {
                case TipoFormulario.OrdenDeTrabajo:
                    // Solo Admin puede crear órdenes
                    return rol == "admin";

                case TipoFormulario.GestionEquipos:
                    // Admin y Super pueden gestionar equipos
                    return rol == "admin" || rol == "super";

                case TipoFormulario.GestionUsuarios:
                    // Solo Admin puede gestionar usuarios
                    return rol == "admin";

                case TipoFormulario.Reportes:
                    // Admin y Super pueden ver reportes
                    return rol == "admin" || rol == "super";

                default:
                    return false;
            }
        }
    }
}
