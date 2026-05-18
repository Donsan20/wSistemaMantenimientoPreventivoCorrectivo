using System;
using System.Data;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objDato = new CD_Usuarios();

        /// <summary>
        /// Valida credenciales de acceso (existente del Sprint 1).
        /// </summary>
        public string ValidarLogin(string usuario, string password)
        {
            // Validaciones de negocio:
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("El usuario y la contraseña no pueden estar vacíos.");
            }

            return objDato.ValidarLogin(usuario, password);
        }

        /// <summary>
        /// Lista todos los usuarios del sistema.
        /// </summary>
        public DataTable ListarUsuarios()
        {
            return objDato.ListarUsuarios();
        }

        /// <summary>
        /// Inserta un nuevo usuario con validaciones de negocio.
        /// </summary>
        public bool InsertarUsuario(string username, string password, bool estado, int idRol)
        {
            // Regla de Negocio: El username es obligatorio
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("El nombre de usuario no puede estar vacío.");
            }

            // Regla de Negocio: El username no debe exceder 50 caracteres
            if (username.Trim().Length > 50)
            {
                throw new Exception("El nombre de usuario no puede exceder los 50 caracteres.");
            }

            // Regla de Negocio: La contraseña es obligatoria
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("La contraseña no puede estar vacía.");
            }

            // Regla de Negocio: La contraseña no debe exceder 50 caracteres
            if (password.Length > 50)
            {
                throw new Exception("La contraseña no puede exceder los 50 caracteres.");
            }

            // Regla de Negocio: El rol debe ser válido (1=Admin, 2=Tecnico, 3=Super)
            if (idRol < 1 || idRol > 3)
            {
                throw new Exception("El rol seleccionado no es válido.");
            }

            return objDato.InsertarUsuario(username.Trim(), password, estado, idRol);
        }

        /// <summary>
        /// Actualiza un usuario existente con validaciones de negocio.
        /// </summary>
        public bool ActualizarUsuario(int idUsuario, string username, string password, bool estado, int idRol)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idUsuario <= 0)
            {
                throw new Exception("El ID del usuario no es válido.");
            }

            // Regla de Negocio: El username es obligatorio
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("El nombre de usuario no puede estar vacío.");
            }

            // Regla de Negocio: El username no debe exceder 50 caracteres
            if (username.Trim().Length > 50)
            {
                throw new Exception("El nombre de usuario no puede exceder los 50 caracteres.");
            }

            // Regla de Negocio: La contraseña es obligatoria
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("La contraseña no puede estar vacía.");
            }

            // Regla de Negocio: La contraseña no debe exceder 50 caracteres
            if (password.Length > 50)
            {
                throw new Exception("La contraseña no puede exceder los 50 caracteres.");
            }

            // Regla de Negocio: El rol debe ser válido
            if (idRol < 1 || idRol > 3)
            {
                throw new Exception("El rol seleccionado no es válido.");
            }

            return objDato.ActualizarUsuario(idUsuario, username.Trim(), password, estado, idRol);
        }

        /// <summary>
        /// Elimina un usuario de forma lógica con validaciones de negocio.
        /// </summary>
        public bool EliminarUsuario(int idUsuario)
        {
            // Regla de Negocio: El ID debe ser válido
            if (idUsuario <= 0)
            {
                throw new Exception("El ID del usuario no es válido.");
            }

            return objDato.EliminarUsuario(idUsuario);
        }

        /// <summary>
        /// Busca usuarios por nombre con validación de negocio.
        /// </summary>
        public DataTable BuscarUsuarioPorNombre(string busqueda)
        {
            // Regla de Negocio: La búsqueda no puede estar vacía
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                throw new Exception("Ingrese un término de búsqueda.");
            }

            return objDato.BuscarUsuarioPorNombre(busqueda.Trim());
        }
    }
}
