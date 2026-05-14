using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class Seguridad
    {
        // Verifica si existe un usuario autenticado en la sesión actual
        public static bool sesionActiva(object user)
        {
            // Casteo preventivo: validamos que el objeto no sea nulo antes de tratarlo como Usuario
            dominio.Usuario usuario = user != null ? (dominio.Usuario)user : null;

            // Un ID distinto de 0 indica que el usuario proviene de una persistencia válida en DB
            if (usuario != null && usuario.Id != 0)
                return true;
            else
                return false;
        }

        // Determina si el usuario en sesión posee privilegios de administrador
        public static bool esAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            // Retorna true solo si el usuario existe y su propiedad Admin es true (1 en la DB)
            return usuario != null && usuario.Admin;
        }

    }
}
