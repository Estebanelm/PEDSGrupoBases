//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigiTutorService.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tecnologia_x_EstudianteDAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tecnologia_x_EstudianteDAO()
        {
            this.cantidadApoyos = 0;
        }
    
        public int id { get; set; }
        public int id_tecnologia { get; set; }
        public string id_estudiante { get; set; }
        public int cantidadApoyos { get; set; }
    
        public virtual EstudianteDAO Estudiante { get; set; }
        public virtual TecnologiaDAO Tecnologia { get; set; }
    }
}
