using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificamos si existe un mensaje de excepción almacenado en el estado de sesión
            if (Session["error"] != null)
            {
                // Inyectamos el mensaje de error en el Label animado del frontend
                lblMensaje.Text = Session["error"].ToString();

                // Buena práctica: Limpiamos la variable de sesión para liberar memoria 
                // y evitar que el mismo error se muestre si el usuario recarga la página a mano
                Session["error"] = null;
            }
            else
            {
                // Si acceden a Error.aspx directamente sin haber disparado un error real
                lblMensaje.Text = "Se ha producido un error inesperado en el sistema.";
            }
        }
    }
}