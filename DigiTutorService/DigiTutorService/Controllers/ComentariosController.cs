using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class ComentariosController : ApiController
    {

        //devuelve lista de Comentarios
        [HttpGet]
<<<<<<< HEAD
        public IHttpActionResult GetComentarios()
=======
        public IEnumerable<Comentario> GetComentarios(int pubId, int noPag)
>>>>>>> d18fbcafdf0944b53f93db55dd5ce6e7b081a608
        {
            //return List<Comentario>
             return null;
        }
        
     
        [HttpPost]
        public IHttpActionResult PostComentario([FromBody] Comentario comm)
        {
            // agregar un Comentario

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult BorrarComentario(int id)
        {
            //borrar Comentario
            
            return Ok();
        }


    }
}
