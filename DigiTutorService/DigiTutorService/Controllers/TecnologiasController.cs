using DigiTutorService.DataAccessLayer;
using DigiTutorService.Models;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class TecnologiasController : ApiController
    {
        private FachadaCatalogoDAL catalogo = new FachadaCatalogoDAL();
        //devuelve lista de tecnologias
        [HttpGet]
        public IHttpActionResult GetTecnologias()
        {
            //return List<Tecnologias>
            return Ok(catalogo.GetTecnologias());
        }
        
        [HttpPost]
        public IHttpActionResult PostTecnologia([FromBody] Tecnologia tec)
        {
            // agregar una tecnologia
            if (tec.Categoria != null && tec.Nombre != null)
            {
                catalogo.AddTecnologia(tec);
                return Ok();
            }
            else return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult BorrarTecnologia(int id)
        {
            //borrar tecnologia
            //revisar si esta usada por algun estudiante
            if (catalogo.DeleteTecnologia(id)) Ok();

            return BadRequest();
        }


    }
}
