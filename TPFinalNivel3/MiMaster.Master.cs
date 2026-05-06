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

            // Solo si hay sesión activa, cargamos los datos
            if (Seguridad.sesionActiva(Session["usuario"]))
            {
                Usuario user = (Usuario)Session["usuario"];
                lblUser.Text = user.Email;

                // Validamos la imagen de perfil
                if (!string.IsNullOrEmpty(user.UrlImagenPerfil))
                {
                    // Si tiene foto, la cargamos
                    imgAvatar.ImageUrl = "~/Images/" + user.UrlImagenPerfil; // Asumiendo que las guardas en una carpeta /Images/
                }
                else
                {
                    // Si NO tiene foto, cargamos un placeholder por defecto
                    imgAvatar.ImageUrl = "https://www.pngkit.com/png/full/301-3012694_account-user-profile-avatar-comments-fa-user-circle.png";
                }

            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            // Limpiamos la sesión y mandamos al Login
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}