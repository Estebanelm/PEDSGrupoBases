using DigiTutorService.Models;
using System.Web.Http;
using DigiTutorService.DataAccessLayer;
using System.Net.Http;
using System.Net;

namespace DigiTutorService.Controllers
{
    public class SeguimientoController : ApiController
    {
        private FachadaUsuariosDAL usuarios = new FachadaUsuariosDAL();
        
        [Route("api/{userid}/seguimiento/{id}")]
        [HttpPost]
        public IHttpActionResult SeguirEstudiante(string userid, string id)
        {
            if (userid != null & id != null)
            {
                //agregar seguimiento a la tabla de seguimientos
                Seguimiento seg = new Seguimiento { id_estudianteSigue = userid, id_estudianteSeguido = id };
                if(usuarios.AddSeguimiento(seg))
                return Ok("se agrego seguimiento");
                else return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("api/{userid}/seguimiento/{id}")]
        [HttpDelete]
        public IHttpActionResult DejarSeguirEstudiante(string userid, string id)
        {
            //dejar de seguir 
            //borrar seguimiento de la tabla
            if (userid != null & id != null)
            {
                //agregar seguimiento a la tabla de seguimientos
                Seguimiento seg = new Seguimiento { id_estudianteSigue = userid, id_estudianteSeguido = id };
                if (usuarios.DeleteSeguimiento(seg))
                    return Ok("se borro seguimiento");
                else return BadRequest();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }
    }
}
