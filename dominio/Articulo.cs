using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; } // Objeto, no solo el ID
        public Categoria Categoria { get; set; } // Objeto
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; } // En SQL es money, acá decimal
    }
}
