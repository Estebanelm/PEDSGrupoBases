using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Contenido : Publicacion
    {
        public string Link { get; set; }
        public string Documento { get; set; }
        public string Video { get; set; }
    }
}