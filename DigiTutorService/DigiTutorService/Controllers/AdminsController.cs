using System.Web.Http;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer;
using System.Net.Http;
using System.Net;

namespace DigiTutorService.Controllers {
    public class AdminsController : ApiController {

        private FachadaUsuariosDAL usuarios = new FachadaUsuariosDAL();

        [HttpGet]
        public IHttpActionResult GetAdministrador (string id) {
            //return List<Tecnologias>

            return Ok(usuarios.GetAdministrador(id));
        }

        [HttpPost]
        public IHttpActionResult PostAdministrador (string pwd, [FromBody] Administrador admin) {
            if(admin == null) return BadRequest();

            // crear un administrador
            if (admin.HasInfoCreacion())
            {
                //desencriptar o encriptar o si viene encriptada solo guardarla talvez o no se
                var res = usuarios.CrearAdministrador(pwd, admin);
                if (res=="success")
                    return Ok();
                else return BadRequest(res);
            }
            else return BadRequest();
            
        }

        [HttpPut]
        public IHttpActionResult ModificarAdministrador (string id, [FromBody] Administrador admin) {
            //modificar admin

            if (admin==null) return BadRequest();

            if (admin.HasInfoCreacion())
            {
                if (usuarios.ModificarAdministador(id, admin))
                    return Ok();
                else return BadRequest();
            }
            else return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult BorrarAdministrador (string id) {
            //borrar admin
            if (usuarios.DeleteAdministrador(id)) 
            return Ok ();
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