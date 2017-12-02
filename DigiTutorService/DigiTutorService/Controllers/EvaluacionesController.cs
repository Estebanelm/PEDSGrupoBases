using DigiTutorService.DataAccessLayer;
using DigiTutorService.LogicLayer;
using DigiTutorService.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class EvaluacionesController : ApiController
    {
        PublicacionesLogic publicaciones = new PublicacionesLogic();

        [HttpPut]
        public IHttpActionResult DarEvaluacion(int id, [FromBody] Evaluacion eval)
        {
            //agregar o modificar evaluacion
            if (publicaciones.AddOrModifyEvaluacion(eval))
                return Ok();
            else return BadRequest();
            
        }


        //no lo tenemos definido, pero puede ser importante para evitar que 
        //la base de datos se llene de apoyos que fueron removidos        
        /* 
       [HttpDelete]
       public IHttpActionResult RemoverEvaluacion([FromBody] Apoyo apoyo)
       {
           //dejar de dar apoyo

           return Ok();
       }

       */

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }
    }
}
