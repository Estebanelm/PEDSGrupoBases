using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class ApoyosController : ApiController
    {        

        [HttpPut]
        public IHttpActionResult DarApoyo([FromBody] Apoyo apoyo)
        {
            //agregar apoyo a la lista de apoyos
            
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult QuitarApoyo([FromBody] Apoyo apoyo)
        {
            //dejar de dar apoyo
            
            return Ok();
        }
    }
}
