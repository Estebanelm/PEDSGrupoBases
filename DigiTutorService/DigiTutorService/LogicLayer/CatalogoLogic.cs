using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer.Repository;
using System.ComponentModel;
using DigiTutorService.DataAccessLayer;

namespace DigiTutorService.LogicLayer
{
    public class CatalogoLogic
    {

        public RepositoryDAL RepositoryDAL1;

        public CatalogoLogic()
        {
            RepositoryDAL1 = new RepositoryDAL();
        }

        public List<T> GetAll<T, TDAO>() where T : new() where TDAO : class
        {
            List<TDAO> listObjetos = RepositoryDAL1.Read<TDAO>();
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
            List<TecnologiaDAO> listaTecnologias = RepositoryDAL1.Read<TecnologiaDAO>();
            List<CategoriaDAO> listaCategorias = RepositoryDAL1.Read<CategoriaDAO>();
            List<Tecnologia> listaARetornar = new List<Tecnologia>();
            foreach (var tecnologia in listaTecnologias)
            {
                listaARetornar.Add(new Tecnologia
                {
                    Nombre = tecnologia.nombre,
                    Id = tecnologia.id,
                    Categoria = listaCategorias.Where(x => x.id == tecnologia.id_categoria).Select(x => x.nombre).FirstOrDefault()
                });
            }
            return listaARetornar;
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
            CategoriaDAO categoriacategoriaExistente = RepositoryDAL1.Read<CategoriaDAO>(x => x.nombre.Equals(tecnologia.Categoria)).FirstOrDefault();
            if (categoriacategoriaExistente == null)
            {
                CategoriaDAO nuevaCat = new CategoriaDAO { nombre = tecnologia.Categoria };
                RepositoryDAL1.Create(nuevaCat);
            }
            CategoriaDAO categoriaActual = RepositoryDAL1.Read<CategoriaDAO>(x => x.nombre.Equals(tecnologia.Categoria)).FirstOrDefault();
            TecnologiaDAO nuevaTec = new TecnologiaDAO { nombre = tecnologia.Nombre, id_categoria = categoriaActual.id};
            return RepositoryDAL1.Create(nuevaTec);
        }
        public bool AddUniversidad(Universidad universidad)
        {
            return RepositoryDAL1.Create(Switch<UniversidadDAO>(universidad));
        }
		public bool DeleteUniversidad(int UniversidadId)
		{
            UniversidadDAO universidadABorrar = new UniversidadDAO { id = UniversidadId };
            return RepositoryDAL1.Delete(universidadABorrar);
		}
		public bool DeleteTecnologia(int TecnologiaId)
		{
            TecnologiaDAO tecnologiaABorrar = new TecnologiaDAO { id = TecnologiaId };
            return RepositoryDAL1.Delete(tecnologiaABorrar);
        }
    }
}