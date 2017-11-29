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
        public IHttpActionResult GetComentarios(int id, int pag)
        {
            //return List<Comentario>
             return null;
        }
        
     
        [HttpPost]
        public IHttpActionResult PostComentario(int id, [FromBody] Comentario comm)
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
