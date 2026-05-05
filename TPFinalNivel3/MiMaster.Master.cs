using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Por defecto ocultamos favoritos
            liFavoritos.Visible = false;

            // Si existe una sesión de usuario (esto lo definiremos al programar el login)
            if (Session["usuario"] != null)
            {
                liFavoritos.Visible = true;
                // Aquí también podrías ocultar los botones de Login/Registro
            }
        }
    }
}