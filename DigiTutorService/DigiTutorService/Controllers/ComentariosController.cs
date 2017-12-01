using DigiTutorService.Models;
using System.Web.Http;
using DigiTutorService.DataAccessLayer;
using System.Net.Http;
using System.Net;

namespace DigiTutorService.Controllers
{
    public class ComentariosController : ApiController
    {
        FachadaPublicacionDAL publicaciones = new FachadaPublicacionDAL();
        //devuelve lista de Comentarios
        [HttpGet]
        public IHttpActionResult GetComentarios(int id, int pag)
        {
            //return List<Comentario>
             return Ok(publicaciones.GetComentarios(id, pag));
        }
        
     
        [HttpPost]
        public IHttpActionResult PostComentario(int id, [FromBody] Comentario comm)
        {
            // agregar un Comentario
            if (publicaciones.AddComentario(comm))
                return Ok();
            else return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult BorrarComentario(int id)
        {
            //borrar Comentario
            if (publicaciones.DeleteComentario(id))
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
