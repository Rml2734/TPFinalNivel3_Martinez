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
            // 1. Por defecto, estas opciones están ocultas para todos
            liFavoritos.Visible = false;
            liListaArticulos.Visible = false;

            // 2. Si hay una sesión activa, revisamos qué permisos tiene
            if (Seguridad.sesionActiva(Session["usuario"]))
            {
                Usuario user = (Usuario)Session["usuario"];

                // El usuario logueado (sea quien sea) puede ver sus favoritos
                liFavoritos.Visible = true;

                // 3. SOLO si el usuario es Admin, le mostramos la gestión de artículos
                if (user.Admin)
                {
                    liListaArticulos.Visible = true;
                }

                // Cargamos los datos de perfil (Email y Foto) que ya teníamos...
                lblUser.Text = user.Email;
                if (!string.IsNullOrEmpty(user.UrlImagenPerfil))
                {
                    // Si tiene foto, la cargamos
                    //imgAvatar.ImageUrl = "~/Images/" + user.UrlImagenPerfil; // Asumiendo que las guardas en una carpeta /Images/
                    imgAvatar.ImageUrl = user.UrlImagenPerfil;
                }
                else
                {
                    // Si NO tiene foto, cargamos un placeholder por defecto
                    imgAvatar.ImageUrl = "https://www.pngkit.com/png/full/301-3012694_account-user-profile-avatar-comments-fa-user-circle.png";
                }

            }

            // Si existe una sesión de usuario (esto lo definiremos al programar el login)
            //if (Session["usuario"] != null)
            //{
              //  liFavoritos.Visible = true;
                // Aquí también podrías ocultar los botones de Login/Registro
            //}
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            // Limpiamos la sesión y mandamos al Login
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}