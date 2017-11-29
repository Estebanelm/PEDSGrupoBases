using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class UniversidadesController : ApiController
    {

        //devuelve lista de Universidades
        [HttpGet]
        public IHttpActionResult GetUniversidades()
        {
            //return List<Universidades>
            return null;
        }
        
     
        [HttpPost]
        public IHttpActionResult PostUniversidad([FromBody] Universidad univ)
        {
            // agregar una Universidad

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult BorrarUniversidad(int id)
        {
            //borrar universidad
            //hay q revisar si esta siendo usada por algun estudiante
            
            return Ok();
        }
    }
}
