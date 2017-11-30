using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiTutorService.Models
{
    public class Administrador
    {
        public string NombreUsuario { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public bool HasInfoCreacion()
        {
            if (NombreUsuario != null && Nombre != null && Apellido != null && Correo != null)
                return true;
            else return false;
        }
    }
}