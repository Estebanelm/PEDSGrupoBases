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
        List<Pais> paises = new {"Argentina", "Australia", "Austria", "Costa Rica"};

        //devuelve lista de paises
        [HttpGet]
        public IEnumerable<Tecnologia> GetPaises()
        {
            //return List<Paises>

      
    }
}
