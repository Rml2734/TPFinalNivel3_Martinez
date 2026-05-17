using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPFinalNivel3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Método del ciclo de vida de la página. 
            // Nota: Se mantiene vacío intencionalmente ya que el módulo de autenticación
            // no requiere inicialización de datos estáticos ni dinámicos en la carga inicial.
        }

        // =========================================================================
        // CONTROLADORES DE EVENTOS DE INTERFAZ (PROCESO DE AUTENTICACIÓN)
        // =========================================================================
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                // Hidratamos el objeto con las credenciales capturadas de la UI
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;

                // Transacción: Consultamos a la capa de negocio el estado de las credenciales
                if (negocio.Loguear(usuario))
                {
                    // Registramos la entidad identidad completa en el estado de Sesión
                    Session.Add("usuario", usuario);

                    // Redirección segura hacia la raíz principal del catálogo
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    // Manejo de contingencia ante credenciales inválidas o inexistentes
                    lblError.Text = "Usuario o contraseña incorrectos. Por favor, reintente.";
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Aislamiento y captura de excepciones en el flujo de autenticación de datos
                Session.Add("error", "Error crítico al intentar validar las credenciales de acceso: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}