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
            // =========================================================================
            // 1. CONTROL DE RECURSOS ESTÁTICOS SEGÚN ENTORNO (AUTOMATIZACIÓN FAVICON)
            // =========================================================================
            string url = Request.Url.ToString();

            if (url.Contains("localhost"))
            {
                // Entorno Local: Inyección directa para mitigar bloqueos de Brave Shields en HTTP
                favicon.Attributes["href"] = "Images/carro-de-la-compra.png";
            }
            else
            {
                // Entorno Producción (Railway): Resolución relativa a la raíz bajo HTTPS
                favicon.Attributes["href"] = ResolveUrl("~/Images/carro-de-la-compra.png");
            }

            // =========================================================================
            // 2. CONTROL DE ACCESOS Y VISIBILIDAD DE INTERFAZ SEGÚN ROLES (SEGURIDAD)
            // =========================================================================
            
            // Por defecto, ocultamos las opciones restringidas para usuarios anónimos
            liFavoritos.Visible = false;
            liListaArticulos.Visible = false;

            // Evaluamos si el cliente posee una sesión de usuario válida y activa
            if (Seguridad.sesionActiva(Session["usuario"]))
            {
                Usuario user = (Usuario)Session["usuario"];

                // Autorización Base: Cualquier usuario autenticado tiene acceso a sus favoritos
                liFavoritos.Visible = true;

                // AUTORIZACION ELEVADA: Modificación restringida estrictamente al rol de Administrador
                if (user.Admin)
                {
                    liListaArticulos.Visible = true;
                }

                // Mapeo de identidad del perfil autenticado hacia la interfaz gráfica (UI)
                lblUser.Text = user.Email;

                if (!string.IsNullOrEmpty(user.UrlImagenPerfil))
                {
                    // Si tiene foto, la cargamos               
                    imgAvatar.ImageUrl = user.UrlImagenPerfil;
                }
                else
                {
                    // Fallback visual: Si el perfil carece de avatar, inyectamos un placeholder estandarizado
                    imgAvatar.ImageUrl = "https://www.pngkit.com/png/full/301-3012694_account-user-profile-avatar-comments-fa-user-circle.png";
                }

            }        
        }

        // =========================================================================
        // 3. CONTROLADORES DE EVENTOS DE INTERFAZ (GESTIÓN DE SESIÓN DE USUARIO)
        // =========================================================================
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            // Destrucción del estado de sesión en el servidor y vaciado completo de variables en memoria
            Session.Abandon();
            Session.Clear();

            // Redirección segura al formulario de autenticación mitigando sobrecarga de hilos
            Response.Redirect("Login.aspx");
        }
    }
}