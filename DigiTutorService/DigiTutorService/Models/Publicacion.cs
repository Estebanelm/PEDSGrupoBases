using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace DigiTutorService.Models
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public class Publicacion
    { 
        private static List<Type> KnownTypes { get; set; }

        public static List<Type> GetKnownTypes()
        {
            return KnownTypes;
        }

        static Publicacion()
        {
            KnownTypes = new List<Type>();
            try
            {
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (!type.IsAbstract && type.IsSubclassOf(typeof(Publicacion)))
                    {
                        KnownTypes.Add(type);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error!");
            }
        }
        public class Tecnologia
        {
            public string Nombre { get; set; }
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Titulo { get; set; }
        [DataMember]
        public string Id_autor { get; set; }
        [DataMember]
        public string Nombre_autor { get; set; }
        [DataMember]
        public List<Tecnologia> Tecnologias { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public int CantidadComentarios { get; set; }
        [DataMember]
        public DateTime FechaCreacion { get; set; }
        [DataMember]
        public string MiEvaluacion { get; set; }
        [DataMember]
        public int CantidadEvaluaciones { get; set; }


        public bool HasInfoCreacion()
        {
            if (Id_autor != null && Nombre_autor != null && Descripcion != null && Tecnologias.Count != 0)
                return true;
            else return false;
        }
    }
}