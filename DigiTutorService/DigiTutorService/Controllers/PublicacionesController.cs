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
        [HttpGet]
        public List<Publicacion> GetPublicacionesVisibles (int userid, int pag) {
            //return List<Publicacion>

        }

        //retorna lista de publicaciones de un estudiante específico
        [HttpGet]
        public List<Publicacion> GetPublicacionesEstudiante (int userid, int id, int pag) {
            //return List<Publicacion>

        }


//===========================================================================================================

        [HttpPost]
        public IHttpActionResult PostTutoria ([FromBody] Tutoria tut) {
            // crear una tutoria

            return Ok ();
        }

        [HttpPost]
        public IHttpActionResult PostPublicacionContenido ([FromBody] PublicacionContenido pub) {
            // crear un Publicacion

            return Ok ();
        }


//=================================================================================================================
       

        [HttpDelete]
        public IHttpActionResult BorrarPublicacion (int id) {
            //desactivar publicacion
            return Ok ();
        }
    }
}

