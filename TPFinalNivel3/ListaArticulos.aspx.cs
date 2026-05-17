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
    public partial class ListaArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // =========================================================================
            // 1. FILTRO DE SEGURIDAD INTERNA (AUTORIZACIÓN RESTRINGIDA)
            // =========================================================================
            // Validamos preventivamente que exista sesión activa y que posea el rol de Administrador
            if (!(Seguridad.sesionActiva(Session["usuario"]) && ((dominio.Usuario)Session["usuario"]).Admin))
            {
                Session.Add("error", "No tienes credenciales de administrador para acceder a este módulo de gestión.");
                Response.Redirect("Error.aspx", false);
                return;
            }

            // =========================================================================
            // 2. CICLO DE VIDA: CARGA INICIAL DE LA GRILLA ADMINISTRATIVA
            // =========================================================================
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> listaOriginal = negocio.listar();

                // Mapeo inicial hacia el origen de datos de la interfaz gráfica (UI)
                dgvArticulos.DataSource = listaOriginal;
                dgvArticulos.DataBind();

                // Almacenamos la colección en Sesión para optimizar el rendimiento del filtrado en memoria
                Session.Add("listaArticulos", listaOriginal);
            }
        }

        // =========================================================================
        // 3. CONTROLADORES DE EVENTOS DE INTERFAZ (ACCIONES DE GRILLA Y FILTROS)
        // =========================================================================
        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Rompemos el encapsulamiento de la fila seleccionada usando la directiva DataKeyNames de la UI
            string id = dgvArticulos.SelectedDataKey.Value.ToString();

            // Redirección controlada al formulario de edición inyectando el ID por QueryString
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        // =========================================================================
        // Controlador de eventos que ejecuta el filtrado predictivo de artículos en memoria interna.
        // =========================================================================
        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            // Desencapsulamos la lista original almacenada de forma segura en el estado de sesión
            List<dominio.Articulo> listaOriginal = (List<dominio.Articulo>)Session["listaArticulos"];

            // Mecanismo de Contingencia: Si la sesión expiró o es nula, rehidratamos los datos desde la DB
            if (listaOriginal == null)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaOriginal = negocio.listar();
                Session.Add("listaArticulos", listaOriginal);
            }

            // Aplicación de Filtro Reactivo mediante Expresiones Lambda (Normalización a mayúsculas para evitar Case-Sensitivity)
            List<dominio.Articulo> listaFiltrada = listaOriginal.FindAll(x =>
                x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            // Sincronización visual inmediata de la grilla basada en los registros coincidentes
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }
    }
}