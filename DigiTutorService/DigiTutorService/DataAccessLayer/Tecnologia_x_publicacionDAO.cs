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
    
    public partial class Tecnologia_x_publicacionDAO
    {
        public int Id { get; set; }
        public int id_tecnologia { get; set; }
        public int id_publicacion { get; set; }
    
        public virtual PublicacionDAO Publicacion { get; set; }
        public virtual TecnologiaDAO Tecnologia { get; set; }
    }
}
