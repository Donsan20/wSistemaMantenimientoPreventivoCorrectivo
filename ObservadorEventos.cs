using System;
using System.Collections.Generic;

namespace wSistemaMantenimientoPreventivoCorrectivo
{
    /// <summary>
    /// Patrón Observer (GoF) para notificar cambios entre formularios.
    /// 
    /// Problema que resuelve:
    /// Cuando se crea/edita/elimina una orden en un formulario, el Dashboard
    /// necesita refrescarse automáticamente. Sin este patrón, tendríamos que
    /// pasar referencias entre formularios o usar eventos acoplados.
    /// 
    /// Solución:
    /// - El Dashboard se suscribe al GestorEventos
    /// - Cuando OrdenDeTrabajo guarda algo, llama a NotificarCambioOrdenes()
    /// - El Dashboard recibe la notificación y refresca su tabla
    /// </summary>

    /// <summary>
    /// Interfaz que deben implementar los observadores (formularios que quieren recibir notificaciones).
    /// </summary>
    public interface ISuscriptor
    {
        /// <summary>
        /// Método que se ejecuta cuando ocurre un evento notificado.
        /// </summary>
        /// <param name="tipoEvento">El tipo de evento que ocurrió.</param>
        /// <param name="datos">Datos opcionales asociados al evento.</param>
        void Actualizar(string tipoEvento, object datos = null);
    }

    /// <summary>
    /// Tipos de eventos que se pueden notificar en el sistema.
    /// </summary>
    public enum TipoEvento
    {
        OrdenCreada,
        OrdenActualizada,
        OrdenEliminada,
        EquipoModificado,
        UsuarioModificado
    }

    /// <summary>
    /// Clase central del patrón Observer (Subject).
    /// Mantiene la lista de suscriptores y notifica los cambios.
    /// </summary>
    public static class GestorEventos
    {
        private static readonly List<ISuscriptor> _suscriptores = new List<ISuscriptor>();
        private static readonly object _candado = new object();

        /// <summary>
        /// Registra un suscriptor para recibir notificaciones.
        /// </summary>
        public static void Suscribirse(ISuscriptor suscriptor)
        {
            lock (_candado)
            {
                if (!_suscriptores.Contains(suscriptor))
                {
                    _suscriptores.Add(suscriptor);
                }
            }
        }

        /// <summary>
        /// Elimina un suscriptor de la lista de notificaciones.
        /// </summary>
        public static void Desuscribirse(ISuscriptor suscriptor)
        {
            lock (_candado)
            {
                _suscriptores.Remove(suscriptor);
            }
        }

        /// <summary>
        /// Notifica a todos los suscriptores sobre un evento.
        /// </summary>
        public static void Notificar(TipoEvento evento, object datos = null)
        {
            lock (_candado)
            {
                foreach (var suscriptor in _suscriptores)
                {
                    try
                    {
                        suscriptor.Actualizar(evento.ToString(), datos);
                    }
                    catch (Exception ex)
                    {
                        // Si un suscriptor falla, no interrumpir a los demás
                        ManejadorErrores.Registrar(ex, $"GestorEventos.Notificar - {evento}");
                    }
                }
            }
        }

        // ==================== Métodos de conveniencia ====================

        /// <summary>
        /// Notifica que se creó una nueva orden de trabajo.
        /// </summary>
        public static void NotificarNuevaOrden(object datos = null)
        {
            Notificar(TipoEvento.OrdenCreada, datos);
        }

        /// <summary>
        /// Notifica que se actualizó una orden de trabajo.
        /// </summary>
        public static void NotificarOrdenActualizada(object datos = null)
        {
            Notificar(TipoEvento.OrdenActualizada, datos);
        }

        /// <summary>
        /// Notifica que se eliminó una orden de trabajo.
        /// </summary>
        public static void NotificarOrdenEliminada(object datos = null)
        {
            Notificar(TipoEvento.OrdenEliminada, datos);
        }

        /// <summary>
        /// Notifica que se modificó un equipo.
        /// </summary>
        public static void NotificarEquipoModificado(object datos = null)
        {
            Notificar(TipoEvento.EquipoModificado, datos);
        }

        /// <summary>
        /// Notifica que se modificó un usuario.
        /// </summary>
        public static void NotificarUsuarioModificado(object datos = null)
        {
            Notificar(TipoEvento.UsuarioModificado, datos);
        }
    }
}
