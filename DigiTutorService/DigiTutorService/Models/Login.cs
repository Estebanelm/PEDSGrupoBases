using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Login
    {
            public string id_estudiante { get; set; }
            public string contrasena { get; set; }
            public string tokenSeguridad { get; set; }
    }
}