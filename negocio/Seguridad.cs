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
        public static bool sesionActiva(object user)
        {
            // Si el objeto en sesión no es nulo y tiene un ID válido, está activo
            dominio.Usuario usuario = user != null ? (dominio.Usuario)user : null;
            if (usuario != null && usuario.Id != 0)
                return true;
            else
                return false;
        }


        public static bool esAdmin(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            // Retorna true solo si el usuario existe y su propiedad Admin es true (1 en la DB)
            return usuario != null && usuario.Admin;
        }

    }
}
