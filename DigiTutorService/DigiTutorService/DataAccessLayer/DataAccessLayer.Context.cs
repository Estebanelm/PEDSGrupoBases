﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DigiTutorDBEntities : DbContext
    {
        public DigiTutorDBEntities()
            : base("name=DigiTutorDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CategoriaDAO> CategoriaDAOs { get; set; }
        public virtual DbSet<DocumentoDAO> DocumentoDAOs { get; set; }
        public virtual DbSet<EstudianteDAO> EstudianteDAOs { get; set; }
        public virtual DbSet<Estudiante_sigue_EstudianteDAO> Estudiante_sigue_EstudianteDAO { get; set; }
        public virtual DbSet<EvaluacionDAO> EvaluacionDAOs { get; set; }
        public virtual DbSet<PaiDAO> PaiDAOs { get; set; }
        public virtual DbSet<PublicacionDAO> PublicacionDAOs { get; set; }
        public virtual DbSet<RegistroTutoriaDAO> RegistroTutoriaDAOs { get; set; }
        public virtual DbSet<TecnologiaDAO> TecnologiaDAOs { get; set; }
        public virtual DbSet<Tecnologia_x_EstudianteDAO> Tecnologia_x_EstudianteDAO { get; set; }
        public virtual DbSet<TutoriaDAO> TutoriaDAOs { get; set; }
        public virtual DbSet<UniversidadDAO> UniversidadDAOs { get; set; }
        public virtual DbSet<UsuarioDAO> UsuarioDAOs { get; set; }
        public virtual DbSet<DAO> DAOs { get; set; }
        public virtual DbSet<ComentarioDAO> ComentarioDAOs { get; set; }
        public virtual DbSet<ContenidoDAO> ContenidoDAOs { get; set; }
    }
}
