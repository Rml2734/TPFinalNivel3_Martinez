using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPFinalNivel3
{
    public partial class ListaArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo cargamos si no es un Postback para no perder eficiencia
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulos.DataSource = negocio.listar();
                dgvArticulos.DataBind();
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Usamos el DataKeyNames="Id" que pusimos en el ASPX
            string id = dgvArticulos.SelectedDataKey.Value.ToString();

            // Redirigimos al formulario pasando el ID por la URL (QueryString)
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            // Recuperamos la lista de la sesión
            List<dominio.Articulo> listaOriginal = (List<dominio.Articulo>)Session["listaArticulos"];

            // SEGURIDAD: Si por alguna razón la sesión es null, recargamos la lista desde el negocio
            if (listaOriginal == null)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaOriginal = negocio.listar();
                Session.Add("listaArticulos", listaOriginal);
            }

            // Filtramos
            List<dominio.Articulo> listaFiltrada = listaOriginal.FindAll(x =>
                x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            // Actualizamos el Grid
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }
    }
}