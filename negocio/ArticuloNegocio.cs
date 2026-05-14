using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        // Obtiene una lista de artículos. Permite filtrar por un ID específico.
        public List<Articulo> listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta con LEFT JOIN para asegurar la obtención de datos aunque no posean Marca o Categoría
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, " +
                  "ISNULL(M.Descripcion, 'Sin Marca') Marca, " +
                  "ISNULL(C.Descripcion, 'Sin Categoría') Categoria, " +
                  "A.IdMarca, A.IdCategoria " +
                  "From ARTICULOS A " +
                  "LEFT JOIN MARCAS M ON A.IdMarca = M.Id " +
                  "LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                  "Where 1=1 ";


                // Si mandamos un ID por parámetro, filtramos la consulta SQL
                if (id != "")
                {
                    consulta += " and A.Id = " + id;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // Validación de nulos para la URL de imagen
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    if (!(datos.Lector["IdMarca"] is DBNull))
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Persiste un nuevo artículo utilizando parámetros para evitar Inyección SQL
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @img, @precio)");

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@img", nuevo.ImagenUrl);
                datos.setearParametro("@precio", nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Actualiza los datos de un artículo existente
        public void modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @idCat, ImagenUrl = @img, Precio = @precio Where Id = @id");

                datos.setearParametro("@codigo", art.Codigo);
                datos.setearParametro("@nombre", art.Nombre);
                datos.setearParametro("@desc", art.Descripcion);
                datos.setearParametro("@idMarca", art.Marca.Id);
                datos.setearParametro("@idCat", art.Categoria.Id);
                datos.setearParametro("@img", art.ImagenUrl);
                datos.setearParametro("@precio", art.Precio);
                datos.setearParametro("@id", art.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Elimina un registro de la tabla ARTICULOS por ID
        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // --- Gestión de Favoritos ---
        public void insertarFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into FAVORITOS (IdUser, IdArticulo) values (@idUser, @idArticulo)");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // Obtiene artículos marcados como favoritos por un usuario específico mediante JOIN
        public List<Articulo> listarFavoritos(int idUser)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Hacemos un JOIN para traer los datos del artículo que están en la tabla FAVORITOS
                datos.setearConsulta("Select A.Id, A.Nombre, A.ImagenUrl from ARTICULOS A Inner Join FAVORITOS F ON A.Id = F.IdArticulo Where F.IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // Elimina la relación de favorito entre un usuario y un artículo específico
        public void eliminarFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Delete from FAVORITOS where IdUser = @idUser and IdArticulo = @idArt");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArt", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // Verifica la existencia de un artículo en la lista de favoritos del usuario
        public bool yaEsFavorito(int idUser, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consultamos si existe el registro
                datos.setearConsulta("Select Id from FAVORITOS Where IdUser = @idUser AND IdArticulo = @idArticulo");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();

                // Si el lector tiene filas, es porque ya existe
                return datos.Lector.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}