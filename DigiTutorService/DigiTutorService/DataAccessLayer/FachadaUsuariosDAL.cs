using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;


namespace DigiTutorService.DataAccessLayer
{
    public class FachadaUsuariosDAL
    {
        //Obtiene los datos del estudiante que está logueado
        public Estudiante GetEstudiantePropio(string EstudianteId)
        {
            
            return null;
        }

        public Estudiante GetEstudianteAjeno(string EstudianteId)
        {
            return null;
        }

        public Administrador GetAdministrador(string AdminId)
        {
            return null;
        }
        public IEnumerable<Estudiante> GetEstudiantes(string nombreEstudiante, int id_universidad, int id_pais,int id_tecnologia, int pag)
        {
            return null;
        }
    }
}