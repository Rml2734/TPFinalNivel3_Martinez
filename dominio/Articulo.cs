using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dominio
{
    // Clase que representa la entidad principal de negocio: el Producto/Artículo
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relaciones de Composición: Representan la asociación con otras entidades
        public Marca Marca { get; set; } // Objeto, no solo el ID
        public Categoria Categoria { get; set; } // Objeto


        public string ImagenUrl { get; set; }

        // El tipo decimal asegura la precisión necesaria para valores monetarios
        public decimal Precio { get; set; } // En SQL es money, acá decimal
    }
}
