using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            // OJO: Verifica que el nombre de tu instancia de SQL sea el correcto (.)
            // CONEXIÓN LOCAL (La que usas ahorita para desarrollar)
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true");

            // CONEXIÓN RAILWAY (La desbloquearemos el domingo)
            // El string de conexión te lo dará Railway algo así: 
            // "Server=sqlserver.proxy.rlwy.net,12345;Database=CATALOGO_WEB_DB;User Id=sa;Password=tu_password_de_railway;"
            // conexion = new SqlConnection("ACÁ_IRÁ_EL_STRING_DE_RAILWAY");

            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Este método es el que nos salva de la Inyección SQL que vimos en el examen
        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }
}
