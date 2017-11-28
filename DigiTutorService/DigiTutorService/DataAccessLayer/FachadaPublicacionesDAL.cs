using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;


namespace DigiTutorService.DataAccessLayer
{
    public class FachadaPublicacionDAL
    {
        public IEnumerable<Publicacion> GetPublicaciones(string userid, int pag)
        {
            return null;
        }
        public IEnumerable<Publicacion> GetPublicaciones(string userid, string otherUserId, int pag)
        {
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
    }
}