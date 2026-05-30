using ApiWebInscripcion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiWebInscripcion.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    // Definimos la ruta base para este controlador
    [RoutePrefix("api/partidos")]
    public class PartidosController : ApiController
    {
        [HttpGet]
        [Route("resumenseries")]
        public IHttpActionResult ResumenSeries([FromUri] int idTorneo, [FromUri] int idCategoria)
        {
            // Llamamos a tu capa de datos pasando los parámetros recibidos
            var respuesta = PartidoData.ResumenSeries(idTorneo, idCategoria);

            // Devolvemos HTTP 200 OK junto con el objeto respuesta limpio
            return Ok(respuesta);
        }
    }
}