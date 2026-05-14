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

            // Si no es admin, lo sacamos de aquí
            if (!(Seguridad.sesionActiva(Session["usuario"]) && ((dominio.Usuario)Session["usuario"]).Admin))
            {
                Session.Add("error", "No tienes permisos de administrador para acceder a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }


            // Por defecto, ocultamos el botón eliminar cada vez que carga la página
            btnEliminar.Visible = false;

            try
            {
                //1 Solo cargamos los desplegables la primera vez que entra a la página
                if (!IsPostBack)
                {
                    // CARGA DE DESPLEGABLES (Marcas y Categorías)
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

                    // 2. ¿ESTAMOS EDITANDO? (Detección de ID)
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                    if (id != "")
                    {
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        // Traemos la lista filtrada por ID
                        List<Articulo> lista = negocio.listar(id);

                        // VERIFICACIÓN DEL ID FANTASMA
                        if (lista.Count > 0)
                        {
                            btnEliminar.Visible = true;
                            Articulo seleccionado = lista[0];

                            // CARGA DE CAMPOS
                            txtCodigo.Text = seleccionado.Codigo;
                            txtNombre.Text = seleccionado.Nombre;
                            txtDescripcion.Text = seleccionado.Descripcion;
                            ID_txtImagenUrl.Text = seleccionado.ImagenUrl;

                            // Formato de precio para que el validador no salte
                            txtPrecio.Text = seleccionado.Precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                            imgArticulo.ImageUrl = seleccionado.ImagenUrl;

                            // Selección de desplegables
                            ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                            ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();

                            // Imagen por defecto si está vacío
                            if (string.IsNullOrEmpty(seleccionado.ImagenUrl))
                                imgArticulo.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
                        }
                        else
                        {
                            // SI EL ID NO EXISTE (Ej: 999999)
                            Session.Add("error", "El artículo con ID " + id + " no existe en el sistema.");
                            Response.Redirect("Error.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // PRIMERO: Validamos que los campos cumplan las reglas (Nombre, Precio, etc.)
            if (!Page.IsValid)
                return;

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

                // --- BLOQUE COMBINADO PARA EL PRECIO ---
                // A. Reemplazamos la coma por punto para normalizar el texto
                string precioTexto = txtPrecio.Text.Replace(",", ".");
                // B. Usamos InvariantCulture para que siempre entienda el punto como decimal
                nuevo.Precio = decimal.Parse(precioTexto, System.Globalization.CultureInfo.InvariantCulture);
                // ----------------------------------------

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
                // Si algo falla, el parpadeo rojo nos avisará qué pasó
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
                // Página llamada Error.aspx, 
                Session.Add("error", ex.ToString());
                // Response.Redirect("Error.aspx"); 
            }
        }
    }
}