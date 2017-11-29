using System;
using System.Collections.Generic;
using System.Linq;
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
            return GetAll<Pais, PaisDAO>();
        }
        public bool AddTecnologia(Tecnologia tecnologia)
        {
            CategoriaDAO categoriacategoriaExistente = RepositoryDAL.Read<CategoriaDAO>(x => x.nombre.Equals(tecnologia.Nombre)).FirstOrDefault();
            if (categoriacategoriaExistente == null)
            {
                CategoriaDAO nuevaCat = new CategoriaDAO { nombre = tecnologia.Nombre };
                RepositoryDAL.Create(nuevaCat);
            }
            CategoriaDAO categoriaActual = RepositoryDAL.Read<CategoriaDAO>(x => x.nombre.Equals(tecnologia.Nombre)).FirstOrDefault();
            TecnologiaDAO nuevaTec = new TecnologiaDAO { nombre = tecnologia.Nombre, id_categoria = categoriaActual.id};
            return RepositoryDAL.Create(nuevaTec);
        }
        public bool AddUniversidad(Universidad universidad)
        {
            return RepositoryDAL.Create(Switch<UniversidadDAO>(universidad));
        }
		public bool DeleteUniversidad(int UniversidadId)
		{
            UniversidadDAO universidadABorrar = new UniversidadDAO { id = UniversidadId };
            return RepositoryDAL.Delete(universidadABorrar);
		}
		public bool DeleteTecnologia(int TecnologiaId)
		{
            TecnologiaDAO tecnologiaABorrar = new TecnologiaDAO { id = TecnologiaId };
            return RepositoryDAL.Delete(tecnologiaABorrar);
        }
    }
}