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

                // El Admin no ve el botón eliminar...
                //if (Seguridad.esAdmin(user))
                //{
                  //  btnEliminar.Visible = false; // El Admin no ve el botón.
                //}
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. PRIMERO validamos: Si falta algo, el código se detiene aquí y no toca la DB
                if (!Page.IsValid)
                    return;

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario user = (Usuario)Session["usuario"];

                // 2. Actualizamos el objeto con los datos de los TextBox
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.UrlImagenPerfil = txtUrlImagen.Text;

                // 3. Guardamos en la Base de Datos
                negocio.actualizar(user);

                // 4. Actualizamos la Sesión para que la Master Page refleje los cambios
                Session["usuario"] = user;

                // 5. Redireccionamos al Home
                // Usamos 'false' en el Redirect para evitar una excepción interna de .NET
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["usuario"];

                // REGLA DE ORO: Un admin no puede auto-eliminarse
                if (Seguridad.esAdmin(user))
                {
                    // Lo mandamos a una página de error o mostrar un mensaje
                    Session.Add("error", "Por razones de seguridad, las cuentas de administrador no pueden ser eliminadas desde el perfil.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }


                UsuarioNegocio negocio = new UsuarioNegocio();              
                negocio.eliminar(user.Id);

                // Limpiamos la sesión porque el usuario ya no existe
                Session.Abandon();
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                // Solo redirigimos si NO es el error de hilo anulado (ThreadAbortException)
                if (!(ex is System.Threading.ThreadAbortException))
                {
                    Session.Add("error", "Ocurrió un error inesperado al intentar eliminar la cuenta.");
                    Response.Redirect("Error.aspx");
                }
            }
        }
    }
}