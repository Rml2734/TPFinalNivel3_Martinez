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
            // Por defecto, ocultamos el botón eliminar cada vez que carga la página
            btnEliminar.Visible = false;

            try
            {
                //1 Solo cargamos los desplegables la primera vez que entra a la página
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

                    // 2. ¿Estamos editando? (Detectamos el ID de la URL)
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                    if (id != "")
                    {
                        // Si entramos aquí, es porque SÍ hay un ID, o sea, estamos EDITANDO
                        btnEliminar.Visible = true; // ¡Aquí lo hacemos aparecer!

                        // Buscamos el artículo por ID para cargar los campos
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        // Necesitaremos un método para buscar solo UNO, ya te lo paso.
                        Articulo seleccionado = (negocio.listar(id))[0];

                        // Llenamos los TextBox
                        txtCodigo.Text = seleccionado.Codigo;
                        txtNombre.Text = seleccionado.Nombre;
                        txtDescripcion.Text = seleccionado.Descripcion;
                        ID_txtImagenUrl.Text = seleccionado.ImagenUrl;
                        txtPrecio.Text = seleccionado.Precio.ToString();
                        imgArticulo.ImageUrl = seleccionado.ImagenUrl;

                        // Seleccionamos los desplegables
                        ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();

                        // Dentro del if (id != "") después de cargar los datos:
                        if (string.IsNullOrEmpty(seleccionado.ImagenUrl))
                            imgArticulo.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";

                    }


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

                // Si hay un ID en la URL, se lo asignamos al objeto y MODIFICAMOS
                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(nuevo);
                }
                else
                {
                    // Si no hay ID, es un artículo nuevo y AGREGAMOS
                    negocio.agregar(nuevo);
                }

                // 4. Llamamos al método de negocio para que lo mande a la DB
                //negocio.agregar(nuevo);

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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                // Validamos que el ID venga en la URL antes de intentar borrar
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    negocio.eliminar(id);
                    Response.Redirect("ListaArticulos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                // Si no tienes una página llamada Error.aspx creada, 
                // mejor comenta la redirección para ver el error real en Visual Studio
                Session.Add("error", ex.ToString());
                // Response.Redirect("Error.aspx"); 
            }
        }
    }
}