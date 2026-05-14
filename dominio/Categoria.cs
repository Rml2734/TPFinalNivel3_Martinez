using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    // Representa la clasificación lógica de los artículos en el sistema
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Sobreescritura del método ToString para facilitar la visualización en controles de UI
        // Permite que componentes como DropDownList muestren la descripción automáticamente
        public override string ToString() 
        { 
            return Descripcion;
        }
    }
}
