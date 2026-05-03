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
    }
}