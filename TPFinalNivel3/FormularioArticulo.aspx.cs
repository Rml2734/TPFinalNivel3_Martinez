using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace TPFinalNivel3
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Solo cargamos los desplegables la primera vez que entra a la página
                if (!IsPostBack)
                {
                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                    // Cargamos Marcas
                    ddlMarca.DataSource = marcaNegocio.listar();
                    ddlMarca.DataValueField = "Id";       // Lo que se guarda (el ID)
                    ddlMarca.DataTextField = "Descripcion"; // Lo que el usuario ve
                    ddlMarca.DataBind();

                    // Cargamos Categorías
                    ddlCategoria.DataSource = categoriaNegocio.listar();
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                // Aquí podrías redireccionar a una página de error
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Creamos el objeto (el molde vacío)
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                // 2. Le pasamos los datos de los TextBox
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenUrl = ID_txtImagenUrl.Text;

                // El precio es decimal, así que lo convertimos
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                // 3. Capturamos lo seleccionado en los desplegables
                // Creamos una marca y categoría nuevas solo para guardar el ID seleccionado
                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                // 4. Llamamos al método de negocio para que lo mande a la DB
                negocio.agregar(nuevo);

                // 5. Si todo sale bien, volvemos a la lista
                Response.Redirect("ListaArticulos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void ID_txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            // Actualizamos el control Image con lo que el usuario escribió
            imgArticulo.ImageUrl = ID_txtImagenUrl.Text;
        }
    }
}