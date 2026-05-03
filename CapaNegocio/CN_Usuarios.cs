using System;
using wSistemaMantenimientoPreventivoCorrectivo.CapaDatos;

namespace wSistemaMantenimientoPreventivoCorrectivo.CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objDato = new CD_Usuarios();

        public string ValidarLogin(string usuario, string password)
        {
            // Validaciones de negocio:
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("El usuario y la contraseña no pueden estar vacíos.");
            }

            // Llamamos a la capa de datos
            return objDato.ValidarLogin(usuario, password);
        }
    }
}
