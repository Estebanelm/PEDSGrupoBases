using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Comentario
    {
        public int Id_Comentario { get; set; }
        public string Id_Autor { get; set; }
        public string Nombre_Autor { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha_comentario { get; set; }
    }
}