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
<<<<<<< HEAD
        public IHttpActionResult GetPublicacionesVisibles (int userid, int pag) {
=======
        public List<Publicacion> GetPublicacionesVisibles (string userid, int pag) {
>>>>>>> d18fbcafdf0944b53f93db55dd5ce6e7b081a608
            //return List<Publicacion>
            return null;
        }

        //retorna lista de publicaciones de un estudiante específico
        [HttpGet]
<<<<<<< HEAD
        public IHttpActionResult GetPublicacionesEstudiante (int userid, int id, int pag) {
=======
        public List<Publicacion> GetPublicacionesEstudiante (string userid, string id, int pag) {
>>>>>>> d18fbcafdf0944b53f93db55dd5ce6e7b081a608
            //return List<Publicacion>
            return null;

        }


//===========================================================================================================

        [HttpPost]
        public IHttpActionResult PostTutoria ([FromBody] Tutoria tut) {
            // crear una tutoria

            return Ok ();
        }

        [HttpPost]
        public IHttpActionResult PostPublicacion ([FromBody] Contenido pub) {
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

