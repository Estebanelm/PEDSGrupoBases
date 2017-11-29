using DigiTutorService.Models;
using System.Web.Http;
using DigiTutorService.DataAccessLayer;

namespace DigiTutorService.Controllers
{
    public class UniversidadesController : ApiController
    {
        private  FachadaCatalogoDAL catalogo = new FachadaCatalogoDAL();
        //devuelve lista de Universidades
        [HttpGet]
        public IHttpActionResult GetUniversidades()
        {
          
            return Ok(catalogo.GetUniversidades());
        }
        
     
        [HttpPost]
        public IHttpActionResult PostUniversidad([FromBody] Universidad univ)
        {
            // agregar una Universidad
            if (univ.Nombre == null)
            {
                return BadRequest();
            }
            else
            {
                if (catalogo.AddUniversidad(univ))
                    return Ok();
                else return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult BorrarUniversidad(int id)
        {
            //borrar universidad
            //hay q revisar si esta siendo usada por algun estudiante
            if (catalogo.DeleteUniversidad(id))
                return Ok();
            else return BadRequest();
        }
    }
}
