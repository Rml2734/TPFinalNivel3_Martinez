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
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Si no hay sesión, no hay favoritos que mostrar
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                // 2. Cargamos la lista
                cargarFavoritos();
            }
        }

        private void cargarFavoritos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            // 3. Obtenemos el ID del usuario logueado
            int idUser = ((Usuario)Session["usuario"]).Id;

            // 4. Llamamos a tu nuevo método y lo pegamos al Repetidor
            repFavoritos.DataSource = negocio.listarFavoritos(idUser);
            repFavoritos.DataBind();
        
        }

        protected void btnEliminarFav_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Capturamos el ID del artículo del botón que se clickeó
                int idArticulo = int.Parse(((LinkButton)sender).CommandArgument);

                // 2. Obtenemos el ID del usuario logueado
                int idUser = ((dominio.Usuario)Session["usuario"]).Id;

                // 3. Ejecutamos la eliminación en la DB
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.eliminarFavorito(idUser, idArticulo);

                // 4. Recargamos la lista para que el producto desaparezca de la vista inmediatamente
                cargarFavoritos();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}