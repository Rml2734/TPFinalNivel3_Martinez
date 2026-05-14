using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    // Entidad que representa la marca del fabricante del artículo
    public class Marca
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }


        // Sobreescritura del método ToString para optimizar el enlace de datos (Data Binding)
        // en controles de selección de la interfaz de usuario (UI)
        public override string ToString() 
        { 
            return Descripcion;
        }
    }
}
