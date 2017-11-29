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
        public bool CheckLogin(Login login)
        {
            return false;
        }
        public Estudiante GetEstudianteAjeno(string estudiante1, string EstudianteaBuscar)
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
        public IEnumerable<Estudiante> GetReporteEstudiantes(int id_un, int id_pais,
                         int ec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4, int pag)
        {
            return null;
        }
        public bool CrearEstudiante(string password, Estudiante estudiante)
        {
            return false;
        }
        public bool CrearAdministrador(string password, Administrador administrador)
        {
            return false;
        }
        public bool AddSeguimiento(Seguimiento seguimiento)
        {
            return false;
        }
        public bool ModificarEstudiante(string estudianteId, Estudiante estudiante)
        {
            return false;
        }
        public bool ModificarAdministador(string adminId, Administrador administrador)
        {
            return false;
        }
        public bool DeleteEstudiante(string estudianteId)
        {
            return false;
        }
        public bool DeleteAdministrador(string adminId)
        {
            return false;
        }
        public bool DeleteSeguimiento(int estudianteQueSigue, int estudianteSeguido)
        {
            return false;
        }
		public bool AddApoyo(Apoyo apoyo)
        {
            return false;
        }
		public bool DeleteApoyo(Apoyo apoyo)
        {
            return false;
        }
    }
}