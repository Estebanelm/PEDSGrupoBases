using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Apoyo
    {
        public string id_estudianteApoyado { get; set; }
        public string id_estudianteQueApoya { get; set; }
        public string Tecnologia { get; set; }
    }
}