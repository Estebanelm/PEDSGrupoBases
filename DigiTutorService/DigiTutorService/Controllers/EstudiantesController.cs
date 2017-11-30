using System.Web.Http;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer;

namespace DigiTutorService.Controllers {
    public class EstudiantesController : ApiController {

        private FachadaUsuariosDAL usuarios = new FachadaUsuariosDAL();


        [Route("api/{userid:int}/estudiantes/{id}")]
        [HttpGet]
        public IHttpActionResult GetEstudiante (string userid, string id) {
            Estudiante est = null;
            if (userid != null && id != null)
            {
                if (userid == id)
                    est = usuarios.GetEstudiantePropio(id);
                else
                    est = usuarios.GetEstudianteAjeno(userid, id);

            }

            if (est != null)
            {
                return Ok(est);
            }
            else
                return BadRequest();
            
        }


        //busqueda de estudiantes
        [HttpGet]
        public IHttpActionResult GetEstudiantes (string nombre, int id_un, int id_pais, 
                                    int id_tec, int pag ){
            //retorna una lista de Estudiantes ordenada por reputacion
            return Ok(usuarios.GetEstudiantes(nombre, id_un, id_pais, id_tec, pag));

        }

        //busqueda de reclutamiento
        [HttpGet]
        public IHttpActionResult BusquedaAdministrativa ( int id_un, int id_pais, 
                         int tec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4, int pag )
        {
        //retorna una lista de Estudiantes ordenada por el
        //algoritmo calculado de reclutamiento
        return Ok(usuarios.GetReporteEstudiantes(id_un,id_pais,tec1, w1, tec2, w2, tec3, w3, tec4, w4, pag));
            

        }



        [HttpPost]
        public IHttpActionResult PostEstudiante (string pwd, [FromBody] Estudiante estudiante) {
            // crear un Estudiante
            if (estudiante.HasInfoCreacion())
            {
                if (usuarios.CrearEstudiante(pwd, estudiante))
                    return Ok();
                else return BadRequest();
            }
            else return BadRequest();
        }

        [HttpPut]
        public IHttpActionResult ModificarEstudiante (string id, [FromBody] Estudiante estudiante) {
            //modificar estudiante
                 if (estudiante.HasInfoCreacion())
            {
                if (usuarios.ModificarEstudiante(id, estudiante))
                    return Ok();
                else return BadRequest();
            }
            else return BadRequest();
        }

        [HttpDelete]
        public IHttpActionResult BorrarEstudiante (string id) {
            //borrar admin
            if (usuarios.DeleteEstudiante(id))
                return Ok();
            else return BadRequest();
        }

        
    }
}