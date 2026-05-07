using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            if (!IsPostBack && id != null)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                // Usamos el listar(id) que ya creamos antes
                Articulo seleccionado = (negocio.listar(id))[0];

                lblNombre.InnerText = seleccionado.Nombre;
                lblCodigo.Text = seleccionado.Codigo;
                lblDescripcion.Text = seleccionado.Descripcion;
                lblMarca.Text = seleccionado.Marca.Descripcion;
                lblCategoria.Text = seleccionado.Categoria.Descripcion;
                lblPrecio.Text = seleccionado.Precio.ToString("C");
                imgArticulo.ImageUrl = seleccionado.ImagenUrl;
            }
        }
    }
}