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
    
    public partial class ContenidoDAO
    {
        public int id_publicacion { get; set; }
        public string enlace_video { get; set; }
        public string enlace_extra { get; set; }
        public Nullable<int> id_documento { get; set; }
        public int id { get; set; }
    
        public virtual DocumentoDAO Documento { get; set; }
        public virtual PublicacionDAO Publicacion { get; set; }
    }
}
