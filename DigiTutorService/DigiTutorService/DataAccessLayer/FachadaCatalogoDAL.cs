using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer.Repository;
using System.ComponentModel;

namespace DigiTutorService.DataAccessLayer
{
    public class FachadaCatalogoDAL
    {
        public TConvert Switch<TConvert>(object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }
        public List<Tecnologia> GetTecnologias()
        {
            List<TecnologiaDAO> listTecnologias = RepositoryDAL.Read<TecnologiaDAO>(tec => tec.id > 0);
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
            List<UniversidadDAO> listUniversidades = RepositoryDAL.Read<UniversidadDAO>(univ => univ.id > 0);
            List<Universidad> returnList = new List<Universidad>();
            foreach (UniversidadDAO univ in listUniversidades)
            {
                Universidad asd = Switch<Universidad>(univ);
                returnList.Add(new Universidad
                {
                    Nombre = univ.nombre
                });
            }
            return returnList;
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