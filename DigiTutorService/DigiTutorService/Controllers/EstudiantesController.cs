using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DigiTutorService.Models;

namespace DigiTutorService.Controllers {
    public class EstudiantesController : ApiController {

        [Route("api/{userid:int}/estudiantes/{id}")]
        [HttpGet]
        public IHttpActionResult GetEstudiante (int userid, int id) {
            //retorna un entidad estudiante
            return Ok();
        }

        //busqueda de estudiantes
        [HttpGet]
        public IHttpActionResult GetEstudiantes (string nombre, int id_un, int id_pais, 
                                    int id_tec, int pag ){
            //retorna una lista de Estudiantes ordenada por reputacion
            return Ok();

        }

        //busqueda de reclutamiento
        [HttpGet]
        public IHttpActionResult BusquedaAdministrativa ( int id_un, int id_pais, 
                         int tec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4, int pag )
        {
        //retorna una lista de Estudiantes ordenada por el
        //algoritmo calculado de reclutamiento
        return Ok();
            

        }



        [HttpPost]
        public IHttpActionResult PostEstudiante (string pwd, [FromBody] Estudiante tec) {
            // crear un Estudiante

            return Ok ();
        }

        [HttpPut]
        public IHttpActionResult ModificarEstudiante (string id, [FromBody] Estudiante admin) {
            //modificar admin

            return Ok ();
        }

        [HttpDelete]
        public IHttpActionResult BorrarEstudiante (string id) {
            //borrar admin

            return Ok ();
        }
    }
}