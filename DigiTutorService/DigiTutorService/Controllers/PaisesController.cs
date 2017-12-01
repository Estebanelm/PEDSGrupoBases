using DigiTutorService.DataAccessLayer;
using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class PaisesController : ApiController
    {
        private FachadaCatalogoDAL catalogo = new FachadaCatalogoDAL();
        List<Pais> paises = new Pais[]{
            new Pais { Nombre = "Argentina" },
            new Pais { Nombre = "Australia" },
            new Pais { Nombre = "Austria" },
            new Pais { Nombre ="Costa Rica" }}.ToList();

        //devuelve lista de paises
        [HttpGet]
        public IHttpActionResult GetPaises()
        {
            return Ok(catalogo.GetPaises());
        }
    }
}
