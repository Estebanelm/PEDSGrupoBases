using DigiTutorService.DataAccessLayer;
using DigiTutorService.Models;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class ApoyosController : ApiController
    {

        private FachadaUsuariosDAL usuarios = new FachadaUsuariosDAL();

        [HttpPut]
        public IHttpActionResult DarApoyo([FromBody] Apoyo apoyo)
        {
            //agregar apoyo a la lista de apoyos
            if(apoyo.id_estudianteApoyado !=null && apoyo.id_estudianteQueApoya != null && apoyo.Tecnologia != null)
            {
                if (usuarios.AddApoyo(apoyo)) return Ok();
                else return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult QuitarApoyo([FromBody] Apoyo apoyo)
        {
            //dejar de dar apoyo
            if (apoyo.id_estudianteApoyado != null && apoyo.id_estudianteQueApoya != null && apoyo.Tecnologia != null)
            {
                if (usuarios.DeleteApoyo(apoyo)) return Ok();
                else return BadRequest();
            }
            else
                return BadRequest();
        }
    }
}
