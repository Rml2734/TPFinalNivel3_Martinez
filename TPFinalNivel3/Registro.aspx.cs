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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                // Si el email no es válido, el código se detiene aquí
                if (!Page.IsValid)
                    return;

                Usuario user = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;

                // Guardamos en la DB y obtenemos el ID
                user.Id = usuarioNegocio.insertarNuevo(user);

                // Dejamos al usuario logueado automáticamente guardándolo en la Sesión
                Session.Add("usuario", user);

                // Lo mandamos al inicio
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                // Aquí podrías usar una sesión de error si el profe lo enseñó
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}