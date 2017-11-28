using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Seguimiento
    {
        public string id_estudianteSigue { get; set; }
        public string id_estudianteSeguido { get; set; }
    }
}