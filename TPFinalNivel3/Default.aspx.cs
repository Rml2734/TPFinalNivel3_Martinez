using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace TPFinalNivel3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio(); //
            try
            {
                // 1. Cargamos la lista desde la DB
                ListaArticulo = negocio.listar(); //

                // 2. Le decimos al repetidor de dónde sacar los datos
                repRepetidor.DataSource = ListaArticulo;

                // 3. ¡IMPORTANTE! Esto vincula los datos con el HTML
                repRepetidor.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                // Por ahora lo mandamos al error si algo falla
            }
        }

        protected void btnFavorito_Click(object sender, EventArgs e)
        {
            // 1. Validar si hay usuario logueado. Si no, lo mandamos a loguearse.
            if (!negocio.Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            try
            {
                // 2. Obtenemos el ID del artículo desde el CommandArgument del botón que se clickeó
                int idArticulo = int.Parse(((LinkButton)sender).CommandArgument);

                // 3. Obtenemos el ID del usuario desde la sesión
                int idUser = ((dominio.Usuario)Session["usuario"]).Id;

                // 4. Guardamos en la base de datos
                negocio.ArticuloNegocio negocioArt = new negocio.ArticuloNegocio();
                negocioArt.insertarFavorito(idUser, idArticulo);

                // TIP: Para saber que funcionó, mandémoslo a la página de favoritos
                Response.Redirect("Favoritos.aspx", false);

                // Opcional: Podrías poner un mensaje de "Agregado con éxito"
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            // 1. Intentamos traer la lista de la sesión
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];

            // 2. Si es null, la cargamos de nuevo (por si expiró la sesión)
            if (lista == null)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                lista = negocio.listar();
                Session["listaArticulos"] = lista;
            }

            // 3. Ahora sí filtramos sin miedo al error
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            repRepetidor.DataSource = listaFiltrada;
            repRepetidor.DataBind();

            // --- LA MAGIA ESTÁ AQUÍ ---
            // Si la lista filtrada no tiene elementos, mostramos el mensaje
            if (listaFiltrada.Count == 0)
            {
                lblSinResultados.Visible = true;
            }
            else
            {
                lblSinResultados.Visible = false;
            }
        }




    }
}