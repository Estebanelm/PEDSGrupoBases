using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class LoginController : ApiController
    {
      
        
     
        [HttpPost]
        public IHttpActionResult PostLogin([FromBody] Login login)
        {
            // revisar si la informacion de login matchea
            // crear el token y devolver el token de autenticacion

            return Ok();
        }

    }
}
