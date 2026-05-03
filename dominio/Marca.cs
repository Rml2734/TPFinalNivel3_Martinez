using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        // Tip: Sobreescribir el ToString ayuda mucho para los DropDownList
        public override string ToString() { return Descripcion; }
    }
}
