using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        // Valida las credenciales del usuario y mapea sus datos de perfil si son correctos
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, email, pass, nombre, apellido, urlImagenPerfil, admin From USERS Where email = @email And pass = @pass");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@pass", usuario.Pass);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // Asignación de propiedades básicas
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Admin = (bool)datos.Lector["admin"];

                    // Validaciones contra DBNull para evitar excepciones de casteo
                    if (!(datos.Lector["nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["nombre"];

                    // Apellido 
                    if (!(datos.Lector["apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["apellido"];

                    // Imagen
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil = (string)datos.Lector["urlImagenPerfil"];

                    return true;
                }
                return false;
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

        // Registra un nuevo usuario y retorna el ID autogenerado por la base de datos
        public int insertarNuevo(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Uso de OUTPUT INSERTED.Id para capturar la clave primaria inmediatamente
                datos.setearConsulta("Insert into USERS (email, pass, admin) OUTPUT INSERTED.Id values (@email, @pass, 0)");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);

                
                return datos.ejecutarAccionScalar();
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

        // Actualiza la información de perfil del usuario (Nombre, Apellido, Imagen)
        public void actualizar(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Actualizamos por ID, sin tocar el email
                datos.setearConsulta("Update USERS set nombre = @nombre, apellido = @apellido, urlImagenPerfil = @url Where Id = @id");
                datos.setearParametro("@nombre", user.Nombre);
                datos.setearParametro("@apellido", user.Apellido);

                // Manejo de operador null-coalescing para persistir nulos en la DB
                datos.setearParametro("@url", (object)user.UrlImagenPerfil ?? DBNull.Value);
                datos.setearParametro("@id", user.Id);

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

        // Elimina el usuario y sus registros relacionados para mantener la integridad de la base de datos
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Ejecución en cascada manual para limpiar dependencias antes de borrar el registro padre
                datos.setearConsulta("Delete From FAVORITOS Where IdUser = @id; Delete From USERS Where Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }


    }
}
