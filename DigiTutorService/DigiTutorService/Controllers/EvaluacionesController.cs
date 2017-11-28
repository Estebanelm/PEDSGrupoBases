using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class EvaluacionesController : ApiController
    {        

        [HttpPut]
        public IHttpActionResult DarEvaluacion([FromBody] Evaluacion eval)
        {
            //agregar o modificar evaluacion
            
            return Ok();
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
    }
}
