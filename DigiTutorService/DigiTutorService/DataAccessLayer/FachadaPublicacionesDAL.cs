using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;


namespace DigiTutorService.DataAccessLayer
{
    public class FachadaPublicacionDAL
    {
        //este método busca las publicaciones que se van a mostar en la página principal
        //para el estudiante que se logueó
        public IEnumerable<Publicacion> GetPublicaciones(string userid, int pag)
        {
            //usar skip y take(20) en LINQ
            return null;
        }
        //este método busca las publicaciones que se van a mostar en la página de otro
        //estudiante cuando se visita otro perfil.
        public IEnumerable<Publicacion> GetPublicaciones(string userid, string otherUserId, int pag)
        {
            //usar skip y take(20) en LINQ
            return null;
        }
        public T GetDocumento<T>(int DocId) where T : class
        {
            return null;
        }
        public IEnumerable<Comentario> GetComentarios(int pubId, int noPag)
        {
            return null;
        }
        public bool CreatePublicacion(Contenido contenido)
        {
            return false;
        }
		public bool CreateTutoria(Tutoria tutoria)
		{
			return false;
		}
        public bool AddDocumento<T>(T documento) where T : class
        {
            return false;
        }

        public bool AddComentario(Comentario comentario)
        {
            return false;
        }
        public bool AddOrModifyEvaluacion(Evaluacion evaluacion)
        {
            return false;
        }
        public bool DeletePublicacion(int pubId)
        {
            return false;
        }
        public bool DeleteComentario(int IdComentario)
        {
            return false;
        }
    }
}