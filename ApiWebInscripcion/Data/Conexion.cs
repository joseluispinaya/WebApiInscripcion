using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ApiWebInscripcion.Data
{
    public class Conexion
    {
        public static string RutaConexion = ConfigurationManager.ConnectionStrings["ConexionCadenas"].ConnectionString;
    }
}