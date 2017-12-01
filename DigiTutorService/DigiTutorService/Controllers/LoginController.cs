using DigiTutorService.DataAccessLayer;
using DigiTutorService.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class LoginController : ApiController
    {
        List<Login> loginTable = new List<Login>();
        private FachadaUsuariosDAL usuarios = new FachadaUsuariosDAL();

        [HttpPost]
        public IHttpActionResult PostLogin([FromBody] Login login)
        {
            //desencriptar la informacion de login

            // revisar si la informacion de login matchea
            // crear el token y devolver el token de autenticacion
            if (usuarios.CheckLogin(login))
                //devolver token
                return Ok();
            else
                return BadRequest();
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }

    }
}
