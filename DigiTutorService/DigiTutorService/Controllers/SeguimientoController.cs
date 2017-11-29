using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class SeguimientoController : ApiController
    {

        
        [Route("api/{userid}/seguimiento/{id}")]
        [HttpPost]
        public IHttpActionResult SeguirEstudiante(int userid, int id)
        {
            //seguir
            //agregar seguimiento a la tabla de seguimientos
            
            return Ok("se agrego seguimiento");
        }

        [Route("api/{userid}/seguimiento/{id}")]
        [HttpDelete]
        public IHttpActionResult DejarSeguirEstudiante(int userid, int id)
        {
            //dejar de seguir 
            //borrar seguimiento de la tabla
            
            return Ok();
        }
    }
}
