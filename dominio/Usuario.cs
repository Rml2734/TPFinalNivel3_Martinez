using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    // Representa la identidad de un usuario dentro del sistema
    public class Usuario
    {
        public int Id { get; set; }

        // Credenciales de acceso
        public string Email { get; set; }
        public string Pass { get; set; }

        // Información complementaria del perfil
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UrlImagenPerfil { get; set; }

        // Define el nivel de autorización: True para administradores, False para clientes
        public bool Admin { get; set; }
    }
}
