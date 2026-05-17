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

            // =========================================================================
            // 1. FILTRO DE SEGURIDAD INTERNA (PROTECCIÓN DE RUTA)
            // =========================================================================
            // Validamos que exista una sesión activa antes de permitir la edición de datos
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            // =========================================================================
            // 2. CICLO DE VIDA: REHIDRATACIÓN INICIAL DE CAMPOS DE INTERFAZ
            // =========================================================================
            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["usuario"];

                // Mapeamos los datos de la entidad en sesión hacia los controles de la UI
                txtEmail.Text = user.Email;
                txtNombre.Text = user.Nombre;
                txtApellido.Text = user.Apellido;
                txtUrlImagen.Text = user.UrlImagenPerfil;

                // Sincronización inicial del componente multimedia si posee un recurso válido
                if (!string.IsNullOrEmpty(user.UrlImagenPerfil))
                {
                    // Asumimos que es una URL completa o una ruta relativa que funciona
                    imgNuevoPerfil.ImageUrl = user.UrlImagenPerfil;
                }             
            }

        }

        // =========================================================================
        // 3. CONTROLADORES DE EVENTOS DE INTERFAZ (ACCIONES TRANSACCIONALES)
        // =========================================================================
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación preventiva en el servidor antes de comprometer la persistencia
            if (!Page.IsValid)
                return;

            try
            {             
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario user = (Usuario)Session["usuario"];

                // Actualizamos las propiedades del objeto con las modificaciones del cliente
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                user.UrlImagenPerfil = txtUrlImagen.Text;

                // Transacción: Modificación física del registro relacional en la DB
                negocio.actualizar(user);

                // Sincronización de Estado: Refrescamos la memoria RAM del servidor para la Master Page
                Session["usuario"] = user;

                // // Redirección segura hacia la portada principal
                // Usamos 'false' en el Redirect para evitar una excepción interna de .NET
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error transaccional al intentar actualizar los datos de perfil: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        // =========================================================================
        // CONTROLADORES DE EVENTOS DE INTERFAZ (ACCIONES DESTRUCTIVAS DE CUENTA)
        // =========================================================================
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["usuario"];

                // Regla de Negocio: Restricción absoluta de auto-eliminación para roles elevados ADMIN
                if (Seguridad.esAdmin(user))
                {
                    // Lo mandamos a una página de error o mostrar un mensaje
                    Session.Add("error", "Por motivos de consistencia de seguridad, un Administrador no puede remover su propia cuenta.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();

                // Transacción: Remoción física de la entidad de la base de datos relacional
                negocio.eliminar(user.Id);

                // Destrucción total de cookies y estado de sesión en el servidor
                Session.Abandon();
                Session.Clear();

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                // Mecanismo de control ante interrupciones forzadas de hilos del framework (.NET)
                if (!(ex is System.Threading.ThreadAbortException))
                {
                    Session.Add("error", "Ocurrió un error inesperado al intentar eliminar la cuenta.");
                    Response.Redirect("Error.aspx");
                }
            }
        }
    }
}