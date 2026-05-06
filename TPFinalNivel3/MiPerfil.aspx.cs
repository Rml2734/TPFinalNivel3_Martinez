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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // OJO: Seguridad. Si no hay sesión, mandamos al Login
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            // Solo la primera vez que carga, rellenamos los campos
            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["usuario"];

                txtEmail.Text = user.Email;
                txtNombre.Text = user.Nombre;
                txtApellido.Text = user.Apellido;
                txtUrlImagen.Text = user.UrlImagenPerfil;

                // Si tiene imagen, la cargamos en la previsualización
                if (!string.IsNullOrEmpty(user.UrlImagenPerfil))
                {
                    // Asumimos que es una URL completa o una ruta relativa que funciona
                    imgNuevoPerfil.ImageUrl = user.UrlImagenPerfil;
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario user = (Usuario)Session["usuario"];

                // Actualizamos los datos del objeto en sesión con lo que escribió el usuario
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.UrlImagenPerfil = txtUrlImagen.Text;

                // Mandamos a actualizar la base de datos (ocupamos crear este método)
                negocio.actualizar(user);

                // IMPORTANTE: Al actualizar la base, también debemos actualizar la 'foto redonda' de la Master Page.
                // Para eso, necesitamos forzar una recarga o usar controles de imagen que se actualicen en tiempo real. 
                // Lo más sencillo por ahora es redireccionar a la misma página para que la Master se vuelva a cargar con los datos nuevos.
                Response.Redirect("MiPerfil.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }


    }
}