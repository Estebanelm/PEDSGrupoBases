using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Publicacion
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
        public string Descripcion { get; set; }
        public int CantidadComentarios { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string MiEvaluacion { get; set; }
        public int CantidadEvaluaciones { get; set; }


        public bool HasInfoCreacion()
        {
            if (Id_autor != null && Nombre_autor != null && Descripcion != null && Tecnologias.Count != 0)
                return true;
            else return false;
        }
    }
}