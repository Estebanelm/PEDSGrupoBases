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
    
    public partial class PublicacionDAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PublicacionDAO()
        {
            this.Evaluacions = new HashSet<EvaluacionDAO>();
            this.Comentarios = new HashSet<ComentarioDAO>();
            this.Tutorias = new HashSet<TutoriaDAO>();
        }
    
        public int id { get; set; }
        public string id_estudiante { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int evaluaciones_negativas { get; set; }
        public int evaluaciones_positivas { get; set; }
        public System.DateTime fecha_publicacion { get; set; }
        public bool activo { get; set; }
    
        public virtual EstudianteDAO Estudiante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluacionDAO> Evaluacions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComentarioDAO> Comentarios { get; set; }
        public virtual ContenidoDAO Contenido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TutoriaDAO> Tutorias { get; set; }
    }
}
