//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigiTutorService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evaluacion
    {
        public int id { get; set; }
        public int id_publicacion { get; set; }
        public string id_estudiante { get; set; }
        public Nullable<bool> positiva { get; set; }
    
        public virtual Estudiante Estudiante { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }
}
