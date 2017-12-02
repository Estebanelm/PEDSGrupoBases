using DigiTutorService.DataAccessLayer;
using DigiTutorService.LogicLayer;
using DigiTutorService.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class TecnologiasController : ApiController
    {
        private CatalogoLogic catalogo = new CatalogoLogic();
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
                if (catalogo.AddTecnologia(tec))
                    return Ok();
                else return BadRequest();
            }
            else return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult BorrarTecnologia(int id)
        {
            //borrar tecnologia
            //revisar si esta usada por algun estudiante
            if (catalogo.DeleteTecnologia(id))
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
