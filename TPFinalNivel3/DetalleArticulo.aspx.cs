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
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Capturamos el identificador del artículo enviado a través de la URL
            string id = Request.QueryString["id"];

            // Validamos que sea la carga inicial y que el ID no sea nulo para evitar excepciones
            if (!IsPostBack && id != null)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                try
                {
                    // Recuperamos el artículo específico invocando la sobrecarga del método listar(id)
                    // Accedemos directamente al índice [0] ya que el ID es una clave primaria única
                    Articulo seleccionado = (negocio.listar(id))[0];

                    // Mapeo de datos del objeto hacia los controles de la interfaz de usuario
                    lblNombre.InnerText = seleccionado.Nombre;
                    lblCodigo.Text = seleccionado.Codigo;
                    lblDescripcion.Text = seleccionado.Descripcion;
                    lblMarca.Text = seleccionado.Marca.Descripcion;
                    lblCategoria.Text = seleccionado.Categoria.Descripcion;

                    // Formateo de moneda basado en la cultura del sistema
                    lblPrecio.Text = seleccionado.Precio.ToString("C");

                    imgArticulo.ImageUrl = seleccionado.ImagenUrl;
                }
                catch (Exception ex)
                {
                    Session.Add("error", "No se pudo cargar el detalle del artículo.");
                    Response.Redirect("Error.aspx");
                }
            }
            else if (id == null)
            {
                // Redirección preventiva si se intenta acceder a la página sin un ID válido
                Response.Redirect("Default.aspx");
            }
        }
    }
}