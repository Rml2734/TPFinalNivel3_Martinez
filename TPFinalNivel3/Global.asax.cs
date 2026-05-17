using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TPFinalNivel3
{
    public class Global : System.Web.HttpApplication
    {
        // =========================================================================
        // EVENTOS GLOBALES DEL CICLO DE VIDA DE LA APLICACIÓN
        // =========================================================================
        protected void Application_Start(object sender, EventArgs e)
        {
            // Método de inicialización global del servidor IIS.
            // Nota: Se mantiene vacío temporalmente a la espera de inyección 
            // de configuraciones de rutas o variables de entorno globales.
        }
    }
}