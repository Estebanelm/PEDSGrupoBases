using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class TecnologiasController : ApiController
    {

        //devuelve lista de tecnologias
        [HttpGet]
        public IEnumerable<Tecnologia> GetTecnologias()
        {
            //return List<Tecnologias>
            return null;
        }
        
     
        [HttpPost]
        public IHttpActionResult PostTecnologia([FromBody] Tecnologia tec)
        {
            // agregar una tecnologia

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult ModificarTecnologia(int id, [FromBody] Tecnologia tec)
        {
            //modificar tecnologia

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult BorrarTecnologia(int id)
        {
            //borrar tecnologia
            //revisar si esta usada por algun estudiante
            
            return Ok();
        }


    }
}
