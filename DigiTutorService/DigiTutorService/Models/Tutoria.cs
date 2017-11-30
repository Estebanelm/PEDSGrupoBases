using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DigiTutorService.Models
{
    [KnownType(typeof(Publicacion))]
    [DataContract]
    public class Tutoria : Publicacion
    {
        [DataMember]
        public string Costo { get; set; }
        [DataMember]
        public string Lugar { get; set; }
        [DataMember]
        public bool EstoyRegitrado { get; set; }
        [DataMember]
        public DateTime FechaTutoria { get; set; }

    }
}