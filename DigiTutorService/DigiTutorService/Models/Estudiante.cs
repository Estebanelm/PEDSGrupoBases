using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Estudiante
    {
        public class TecnologiaPerfil
        {
            public string Nombre { get; set; }
            public int Apoyos { get; set; }
            public string MiApoyo { get; set; }
        }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Universidad { get; set; }
        public string Pais { get; set; }
        public List<TecnologiaPerfil> Tecnologias { get; set; }
        public int CantSeguidores { get; set; }
        public int Participacion { get; set; }
        public int Reputacion { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string Correo { get; set; }
        public string Correo2 { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Descripcion { get; set; }
    }
}