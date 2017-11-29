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
        public List<T> GetAll<T, TDAO>() where T : new() where TDAO : class
        {
            List<TDAO> listObjetos = RepositoryDAL.Read<TDAO>();
            List<T> returnList = new List<T>();
            foreach (TDAO item in listObjetos)
            {
                T switchedObject = Switch<T>(item);
                returnList.Add(switchedObject);
            }
            return returnList;
        }
        public TConvert Switch<TConvert>(object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }
        public List<Tecnologia> GetTecnologias()
        {
            return GetAll<Tecnologia, TecnologiaDAO>();
        }
        public IEnumerable<Universidad> GetUniversidades()
        {
            return GetAll<Universidad, UniversidadDAO>();
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