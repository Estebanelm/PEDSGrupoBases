using System.Web.Http;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer;
using System.Net.Http;
using System.Net;

namespace DigiTutorService.Controllers {
    public class PublicacionesController : ApiController {

        FachadaPublicacionDAL publicaciones = new FachadaPublicacionDAL();

        //retorna lista de publicaciones que puede ver un estudiante
        [Route("api/{userid}/publicaciones")]
        [HttpGet]
        public IHttpActionResult GetPublicacionesVisibles (string userid, int pag) {
            //return List<Publicacion>
            return Ok(publicaciones.GetPublicaciones(userid, pag));

        }

        //retorna lista de publicaciones de un estudiante específico
        [Route("api/{userid}/publicaciones/{id}")]
        [HttpGet]
        public IHttpActionResult GetPublicacionesEstudiante (string userid, string id, int pag) {
            //return List<Publicacion>
            return Ok(publicaciones.GetPublicaciones(userid,id, pag));

        }


        //===========================================================================================================
        [Route("api/tutorias")]
        [HttpPost]
        public IHttpActionResult PostTutoria ([FromBody] Tutoria tut) {
            // crear una tutoria

            if(publicaciones.CreateTutoria(tut))
                return Ok();
            else return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult PostPublicacion ([FromBody] Contenido pub) {
            // crear un Publicacion

            if (publicaciones.CreatePublicacion(pub))
                return Ok();
            else return BadRequest();
        }


        //=================================================================================================================     

            
        [Route("api/tutorias/{tutId}/registro/{estId}")]
        [HttpPut]
        public IHttpActionResult RegistrarTutoria(string estId, int tutId)
        {
            //por hacer!!!


            //publicaciones.
           // if (publicaciones.CreateTutoria(tut))
                return Ok();
           // else return BadRequest();
        }
        [Route("api/tutorias/{tutId}/registro/{estId}")]
        [HttpDelete]
        public IHttpActionResult DesregistrarTutoria(int tutId, string estId)
        {
            //por hacer!!!


            //publicaciones.
            // if (publicaciones.CreateTutoria(tut))
            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult BorrarPublicacion (int id) {
            //desactivar publicacion
            if (publicaciones.DeletePublicacion(id))
                return Ok();
            else return BadRequest();
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }
    }
}

