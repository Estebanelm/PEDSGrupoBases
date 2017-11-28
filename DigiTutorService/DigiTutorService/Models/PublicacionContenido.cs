﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class PublicacionContenido
    {
        public class Tecnologia
        {
            string Nombre { get; set; }
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Id_autor { get; set; }
        public string Nombre_autor { get; set; }
        public List<Tecnologia> Tecnologias { get; set; }
        public string Descripcion { get; set; }
        public int CantidadComentarios { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Link { get; set; }
        public string Documento { get; set; }
        public string Video { get; set; }
        public string miEvaluacion { get; set; }
        public int CantidadEvaluaciones { get; set; }


    }
}