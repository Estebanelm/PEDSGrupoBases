using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DigiTutorService.Models;

namespace DigiTutorService.Controllers {
    public class PublicacionesController : ApiController {

        //retorna lista de publicaciones que puede ver un estudiante
        [Route("api/{userid:int}/publicaciones")]
        [HttpGet]
        public IHttpActionResult GetPublicacionesVisibles (int userid, int pag) {
            //return List<Publicacion>
            return Ok("metido en pubs");

        }

        //retorna lista de publicaciones de un estudiante específico
        [Route("api/{userid:int}/publicaciones/{id}")]
        [HttpGet]
        public IHttpActionResult GetPublicacionesEstudiante (int userid, int id, int pag) {
            //return List<Publicacion>
            return Ok("metido en las publicaciones de estudiante especifico");

        }


        //===========================================================================================================
        [Route("api/tutorias/")]
        [HttpPost]
        public IHttpActionResult PostTutoria ([FromBody] Tutoria tut) {
            // crear una tutoria

            return Ok ("crear tutoria");
        }

        [HttpPost]
        public IHttpActionResult PostPublicacion ([FromBody] Contenido pub) {
            // crear un Publicacion

            return Ok ("post publicaciones");
        }


//=================================================================================================================       

        [HttpDelete]
        public IHttpActionResult BorrarPublicacion (int id) {
            //desactivar publicacion
            return NotFound();
        }
    }
}

