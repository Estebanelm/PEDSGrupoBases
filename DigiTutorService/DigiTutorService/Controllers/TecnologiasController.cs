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
        public IHttpActionResult GetTecnologias()
        {
            //return List<Tecnologias>
            return Ok("hola");
        }
        
        [HttpPost]
        public IHttpActionResult PostTecnologia([FromBody] Tecnologia tec)
        {
            // agregar una tecnologia

            return Ok($"creo la tecnologia con nombre {tec.Nombre}");
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
