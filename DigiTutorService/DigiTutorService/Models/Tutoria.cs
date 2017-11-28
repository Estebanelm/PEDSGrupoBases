using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Tutoria : Publicacion
    {
        public string Costo { get; set; }
        public string Lugar { get; set; }
        public bool EstoyRegitrado { get; set; }
        public DateTime FechaTutoria { get; set; }

    }
}