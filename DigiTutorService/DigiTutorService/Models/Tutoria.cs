using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Tutoria
    {
        public class Tecnologia
        {
            public string Nombre { get; set; }
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Id_autor { get; set; }
        public string Nombre_autor { get; set; }
        public List<Tecnologia> Tecnologias { get; set; }
        public string Descripción { get; set; }
        public int CantidadComentarios { get; set; }
        public DateTime FechaCreacion { get; set; } 
	    public bool Evaluado_por_mi { get; set; }
        public int CantidadEvaluaciones { get; set; }
        public string Costo { get; set; }
        public string Lugar { get; set; }
        public bool estoyRegitrado { get; set; }
        public DateTime FechaTutoria { get; set; }

    }
}