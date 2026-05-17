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
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // =========================================================================
            // 1. FILTRO DE SEGURIDAD INTERNA (PROTECCIÓN DE RUTA)
            // =========================================================================
            // Validamos preventivamente que exista un token de sesión válido antes de renderizar
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                // Redirección preventiva con endResponse en false para mitigar interrupciones de hilo
                Response.Redirect("Login.aspx", false);
                return;
            }

            // =========================================================================
            // 2. CICLO DE VIDA: CARGA INICIAL DE LA VISTA
            // =========================================================================
            if (!IsPostBack)
            {
                // Invocamos la carga del origen de datos únicamente en la primera petición
                cargarFavoritos();
            }
        }

        // =========================================================================
        // 2. MÉTODOS DE CARGA DE DATOS (BACKEND LÓGICA)
        // =========================================================================
        private void cargarFavoritos()
        {
            try
            {
                // Instanciamos la capa de negocio para acceder a los datos relacionales
                ArticuloNegocio negocio = new ArticuloNegocio();

                // Desencapsulamos el ID único de la entidad Usuario almacenada en la Sesión
                int idUser = ((Usuario)Session["usuario"]).Id;

                // Mapeo directo de la colección de negocio al control de repetición de datos (Repeater)
                repFavoritos.DataSource = negocio.listarFavoritos(idUser);
                repFavoritos.DataBind();
            }
            catch (Exception ex)
            {
                // Enrutamiento centralizado de excepciones hacia la UI de contingencia
                Session.Add("error", "Error crítico al intentar mapear la lista de favoritos.");
                Response.Redirect("Error.aspx", false);
            }

        }

        // =========================================================================
        // 3. CONTROLADORES DE EVENTOS DE INTERFAZ (EVENT HANDLERS)
        // =========================================================================
        protected void btnEliminarFav_Click(object sender, EventArgs e)
        {
            try
            {
                // Rompemos el encapsulamiento del control remitente (sender) para extraer su argumento de comando
                int idArticulo = int.Parse(((LinkButton)sender).CommandArgument);

                // Recuperamos las credenciales de identidad del usuario en sesión
                int idUser = ((dominio.Usuario)Session["usuario"]).Id;

                // Ejecutamos la eliminación en la DB
                ArticuloNegocio negocio = new ArticuloNegocio();

                // Transacción: Remoción física del registro relacional en la tabla de favoritos (DB)
                negocio.eliminarFavorito(idUser, idArticulo);

                // Sincronización visual inmediata: Re-enlazamos los datos para actualizar la UI sin recargar la página completa
                cargarFavoritos();
            }
            catch (Exception ex)
            {
                // Captura y aislamiento de fallas en transacciones de datos relacionales
                Session.Add("error", "No se pudo remover el artículo de tu lista de favoritos.");
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}