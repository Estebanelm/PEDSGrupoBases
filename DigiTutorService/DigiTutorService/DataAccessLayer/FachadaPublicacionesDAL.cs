using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer.Repository;

namespace DigiTutorService.DataAccessLayer
{
    public class FachadaPublicacionDAL
    {
        //este método busca las publicaciones que se van a mostar en la página principal
        //para el estudiante que se logueó
        public void AddDatosPublicacion<T>(PublicacionDAO publicacion, List<ComentarioDAO> comentarios, List<EvaluacionDAO> evaluaciones, List<UsuarioDAO> listaUsuarios, List<Tecnologia_x_publicacionDAO> listaTecnologiasxPublicacion, List<TecnologiaDAO> listaTecnologias, string userid, ref T publicacionAAgregar) where T : Publicacion
        {
            publicacionAAgregar.CantidadComentarios = comentarios.Where(x => x.id_publicacion == publicacion.id).Count();
            publicacionAAgregar.CantidadEvaluaciones = evaluaciones.Where(x => x.id_publicacion == publicacion.id).Count();
            publicacionAAgregar.Descripcion = publicacion.descripcion;
            publicacionAAgregar.FechaCreacion = publicacion.fecha_publicacion;
            publicacionAAgregar.Id = publicacion.id;
            publicacionAAgregar.Id_autor = publicacion.id_estudiante;
            EvaluacionDAO miEvaluacion = evaluaciones.Where(x => x.id_estudiante.Equals(userid) && x.id_publicacion == publicacion.id).FirstOrDefault();
            publicacionAAgregar.MiEvaluacion = miEvaluacion == null ? "null" : ((bool)miEvaluacion.positiva ? "pos" : "neg");
            UsuarioDAO usuario = listaUsuarios.Where(x => x.id.Equals(publicacion.id_estudiante)).FirstOrDefault();
            publicacionAAgregar.Nombre_autor = usuario.nombre + " " + usuario.apellido;
            List<Publicacion.Tecnologia> listaTecnologiasPub = new List<Publicacion.Tecnologia>();
            foreach (var tecnologia in listaTecnologiasxPublicacion.Where(x => publicacion.id == x.id_publicacion))
            {
                listaTecnologiasPub.Add(new Publicacion.Tecnologia
                {
                    Nombre = listaTecnologias.Where(x => x.id == tecnologia.id_tecnologia).FirstOrDefault().nombre
                });
            }
            publicacionAAgregar.Tecnologias = listaTecnologiasPub;
            publicacionAAgregar.Titulo = publicacion.titulo;
        }
        public List<Publicacion> GetPublicaciones(string userid, int pag)
        {
            List<Estudiante_sigue_EstudianteDAO> listSeguidos = RepositoryDAL.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguidor.Equals(userid));
            listSeguidos.Add(new Estudiante_sigue_EstudianteDAO { id_estudianteSeguido = userid });
            IEnumerable<string> listIdSeguidos = listSeguidos.Select(y => y.id_estudianteSeguido);
            List<PublicacionDAO> listaPublicacionesVisibles = RepositoryDAL.Read<PublicacionDAO, DateTime>(x => listIdSeguidos.Contains(x.id_estudiante) && x.activo, x => x.fecha_publicacion);
            List<PublicacionDAO> veintePublicaciones = listaPublicacionesVisibles.Skip(20 * (pag - 1)).Take(20).ToList();
            IEnumerable<int> listaIdPublicaciones = veintePublicaciones.Select(x => x.id);
            List<TutoriaDAO> listaTutorias = RepositoryDAL.Read<TutoriaDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIDTutorias = listaTutorias.Select(x => x.id);
            List<RegistroTutoriaDAO> listRegistros = RepositoryDAL.Read<RegistroTutoriaDAO>(x => listaIDTutorias.Contains(x.id_tutoria));
            List<Tecnologia_x_publicacionDAO> listaTecnologiasxPublicacion = RepositoryDAL.Read<Tecnologia_x_publicacionDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIdTecnologias = listaTecnologiasxPublicacion.Select(x => x.id_tecnologia);
            List<TecnologiaDAO> listaTecnologias = RepositoryDAL.Read<TecnologiaDAO>(x => listaIdTecnologias.Contains(x.id));
            List<ContenidoDAO> listaContenidos = RepositoryDAL.Read<ContenidoDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIdDocumentos = listaContenidos.Where(x => x.id_documento!= null).Select(x => (int)x.id_documento);
            List<DocumentoDAO> listaDocumentos = RepositoryDAL.Read<DocumentoDAO>(x => listaIdDocumentos.Contains(x.id));
            List<ComentarioDAO> comentarios = RepositoryDAL.Read<ComentarioDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            List<EvaluacionDAO> evaluaciones = RepositoryDAL.Read<EvaluacionDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<string> estudiantesIdPublicando = veintePublicaciones.Select(x => x.id_estudiante);
            List<UsuarioDAO> listaUsuarios = RepositoryDAL.Read<UsuarioDAO>(x => estudiantesIdPublicando.Contains(x.id));


            List<Publicacion> listaAEnviar = new List<Publicacion>();
            foreach(PublicacionDAO publicacion in listaPublicacionesVisibles)
            {
                if (publicacion.isTutoria)
                {
                    Tutoria publicacionAAgregar = new Tutoria();
                    AddDatosPublicacion(publicacion, comentarios, evaluaciones, listaUsuarios, listaTecnologiasxPublicacion, listaTecnologias, userid, ref publicacionAAgregar);
                    TutoriaDAO tutoria = listaTutorias.Where(x => x.id_publicacion == publicacion.id).FirstOrDefault();
                    publicacionAAgregar.Costo = tutoria.costo;
                    IEnumerable<string> listaIdEstudiantesRegistro = listRegistros.Where(x => x.id_tutoria == tutoria.id).Select(x => x.id_estudiante);
                    publicacionAAgregar.EstoyRegitrado = listaIdEstudiantesRegistro.Contains(userid) ? true : false;
                    publicacionAAgregar.FechaTutoria = tutoria.fecha_tutoria;
                    publicacionAAgregar.Lugar = tutoria.lugar;
                    listaAEnviar.Add(publicacionAAgregar);
                }
                else
                {
                    Contenido publicacionAAgregar = new Contenido();
                    AddDatosPublicacion(publicacion, comentarios, evaluaciones, listaUsuarios, listaTecnologiasxPublicacion, listaTecnologias, userid, ref publicacionAAgregar);
                    ContenidoDAO contenido = listaContenidos.Where(x => x.id_publicacion == publicacion.id).FirstOrDefault();
                    publicacionAAgregar.Documento = listaDocumentos.Where(x => x.id == contenido.id_documento).FirstOrDefault().contenido;
                    publicacionAAgregar.Link = contenido.enlace_extra;
                    publicacionAAgregar.Video = contenido.enlace_video;
                    listaAEnviar.Add(publicacionAAgregar);
                }
                
            }
            return listaAEnviar;
        }
        //este método busca las publicaciones que se van a mostar en la página de otro
        //estudiante cuando se visita otro perfil.
        public IEnumerable<Publicacion> GetPublicaciones(string userid, string otherUserId, int pag)
        {
            
            return null;
        }
        public T GetDocumento<T>(int DocId) where T : class
        {
            return null;
        }
        public IEnumerable<Comentario> GetComentarios(int pubId, int noPag)
        {
            return null;
        }
        public bool CreatePublicacion(Contenido contenido)
        {
            return false;
        }
		public bool CreateTutoria(Tutoria tutoria)
		{
			return false;
		}
        public bool AddDocumento<T>(T documento) where T : class
        {
            return false;
        }

        public bool AddComentario(Comentario comentario)
        {
            return false;
        }
        public bool AddOrModifyEvaluacion(Evaluacion evaluacion)
        {
            return false;
        }
        public bool DeletePublicacion(int pubId)
        {
            return false;
        }
        public bool DeleteComentario(int IdComentario)
        {
            return false;
        }
    }
}