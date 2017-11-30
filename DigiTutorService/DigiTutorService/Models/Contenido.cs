using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigiTutorService.Models
{
    [KnownType(typeof(Publicacion))]
    [DataContract]
    public class Contenido : Publicacion
    {
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Documento { get; set; }
        [DataMember]
        public string Video { get; set; }
    }
}