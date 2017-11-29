using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigiTutorService.Models;

namespace DigiTutorService.DataAccessLayer
{
    public class FachadaCatalogoDAL
    {
        public List<Tecnologia> GetTecnologias()
        {
            List<TecnologiaDAO> listTecnologias = TecnologiaDAO.GetTecnologias();
            List<Tecnologia> returnList = new List<Tecnologia>();
            foreach (TecnologiaDAO tec in listTecnologias)
            {
                returnList.Add(new Tecnologia
                {
                    Nombre = tec.nombre,
                    Categoria = tec.Categoria.nombre
                });
            }
            return returnList;
        }
        public IEnumerable<Universidad> GetUniversidades()
        {
            return null;
        }
        public IEnumerable<Pais> GetPaises()
        {
            return null;
        }
        public bool AddTecnologia(Tecnologia tecnologia)
        {
            return false;
        }
        public bool AddUniversidad(Universidad universidad)
        {
            return false;
        }
		public bool DeleteUniversidad(int UniversidadId)
		{
			return false;
		}
		public bool DeleteTecnologia(int TecnologiaId)
		{
			return false;
		}
    }
}