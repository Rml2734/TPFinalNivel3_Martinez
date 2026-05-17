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
            // Validación preventiva en el servidor del estado de las reglas de entrada
            if (!Page.IsValid)
                return;

            try
            {
                Usuario user = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                // Normalización de la cadena para mitigar ingresos con espacios en blanco fortuitos
                string emailIngresado = txtEmail.Text.Trim();

                // =========================================================================
                // CONTROL ANTIDUPLICADOS: VALIDACIÓN DE DISPONIBILIDAD DE CREDENCIALES
                // =========================================================================
                if (usuarioNegocio.verificarCorreoExistente(emailIngresado))
                {
                    // Inyección asíncrona de notificación nativa en el cliente para alertar conflicto de duplicidad
                    string script = "alert('El correo electrónico ya se encuentra registrado por otro usuario. Intenta con uno diferente.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                    return; // Interrupción preventiva: Detiene la persistencia antes de afectar la DB
                }

                // Si las credenciales están disponibles, procedemos con la hidratación
                user.Email = emailIngresado;
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