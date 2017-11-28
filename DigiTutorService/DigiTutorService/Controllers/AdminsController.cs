using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DigiTutorService.Models;

namespace DigiTutorService.Controllers {
    public class AdminsController : ApiController {

        [HttpGet]
        public Administrador GetAdministrador (string id) {
            //return List<Tecnologias>
            return null;
        }

        [HttpPost]
        public IHttpActionResult PostAdministrador (string pwd, [FromBody] Administrador tec) {
            // crear un administrador

            return Ok ();
        }

        [HttpPut]
        public IHttpActionResult ModificarAdministrador (string id, [FromBody] Administrador admin) {
            //modificar admin

            return Ok ();
        }

        [HttpDelete]
        public IHttpActionResult BorrarAdministrador (string id) {
            //borrar admin

            return Ok ();
        }
    }
}