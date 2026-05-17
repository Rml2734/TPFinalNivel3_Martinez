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
            // Método del ciclo de vida de la página.
            // Nota: Se mantiene vacío intencionalmente ya que el módulo de registro
            // no requiere hidratación ni cargas dinámicas en su petición inicial.
        }

        // =========================================================================
        // CONTROLADORES DE EVENTOS DE INTERFAZ (PROCESO DE ALTA DE CUENTA)
        // =========================================================================
        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            // Si el email no es válido, el código se detiene aquí
            if (!Page.IsValid)
                return;

            try
            {    
                Usuario user = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                // Hidratamos el objeto con las nuevas credenciales de identidad
                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;

                // Transacción: Inserción física en DB y recuperación del ID autogenerado (Scope Identity)
                user.Id = usuarioNegocio.insertarNuevo(user);

                // Autenticación Automática: Almacenamos el token de identidad en el estado de Sesión
                Session.Add("usuario", user);

                // Redirección segura hacia la portada principal del catálogo
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                // Aislamiento y enrutamiento centralizado de excepciones transaccionales
                Session.Add("error", "Error crítico al intentar registrar la nueva cuenta de usuario: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}