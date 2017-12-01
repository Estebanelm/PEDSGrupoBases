using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer.Repository;

namespace DigiTutorService.DataAccessLayer
{
    public class FachadaPublicacionDAL
    {
        public RepositoryDAL RepositoryDAL1;

        public FachadaPublicacionDAL()
        {
            RepositoryDAL1 = new RepositoryDAL();
        }
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
        public IEnumerable<Publicacion> GetPublicaciones(string userid, int pag)
        {
            List<Estudiante_sigue_EstudianteDAO> listSeguidos = RepositoryDAL1.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguidor.Equals(userid));
            listSeguidos.Add(new Estudiante_sigue_EstudianteDAO { id_estudianteSeguido = userid });
            IEnumerable<string> listIdSeguidos = listSeguidos.Select(y => y.id_estudianteSeguido);
            List<PublicacionDAO> listaPublicacionesVisibles = RepositoryDAL1.Read<PublicacionDAO, DateTime>(x => listIdSeguidos.Contains(x.id_estudiante) && x.activo, x => x.fecha_publicacion);
            List<PublicacionDAO> veintePublicaciones = listaPublicacionesVisibles.Skip(20 * (pag - 1)).Take(20).ToList();
            IEnumerable<int> listaIdPublicaciones = veintePublicaciones.Select(x => x.id);
            List<TutoriaDAO> listaTutorias = RepositoryDAL1.Read<TutoriaDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIDTutorias = listaTutorias.Select(x => x.id);
            List<RegistroTutoriaDAO> listRegistros = RepositoryDAL1.Read<RegistroTutoriaDAO>(x => listaIDTutorias.Contains(x.id_tutoria));
            List<Tecnologia_x_publicacionDAO> listaTecnologiasxPublicacion = RepositoryDAL1.Read<Tecnologia_x_publicacionDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIdTecnologias = listaTecnologiasxPublicacion.Select(x => x.id_tecnologia);
            List<TecnologiaDAO> listaTecnologias = RepositoryDAL1.Read<TecnologiaDAO>(x => listaIdTecnologias.Contains(x.id));
            List<ContenidoDAO> listaContenidos = RepositoryDAL1.Read<ContenidoDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIdDocumentos = listaContenidos.Where(x => x.id_documento!= null).Select(x => (int)x.id_documento);
            List<DocumentoDAO> listaDocumentos = RepositoryDAL1.Read<DocumentoDAO>(x => listaIdDocumentos.Contains(x.id));
            List<ComentarioDAO> comentarios = RepositoryDAL1.Read<ComentarioDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            List<EvaluacionDAO> evaluaciones = RepositoryDAL1.Read<EvaluacionDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<string> estudiantesIdPublicando = veintePublicaciones.Select(x => x.id_estudiante);
            List<UsuarioDAO> listaUsuarios = RepositoryDAL1.Read<UsuarioDAO>(x => estudiantesIdPublicando.Contains(x.id));


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
                    publicacionAAgregar.EstoyRegistrado = listaIdEstudiantesRegistro.Contains(userid) ? true : false;
                    publicacionAAgregar.FechaTutoria = tutoria.fecha_tutoria;
                    publicacionAAgregar.Lugar = tutoria.lugar;
                    listaAEnviar.Add(publicacionAAgregar);
                }
                else
                {
                    Contenido publicacionAAgregar = new Contenido();
                    AddDatosPublicacion(publicacion, comentarios, evaluaciones, listaUsuarios, listaTecnologiasxPublicacion, listaTecnologias, userid, ref publicacionAAgregar);
                    ContenidoDAO contenido = listaContenidos.Where(x => x.id_publicacion == publicacion.id).FirstOrDefault();
                    DocumentoDAO documento = listaDocumentos.Where(x => x.id == contenido.id_documento).FirstOrDefault();
                    publicacionAAgregar.Documento = documento == null ? "" : documento.contenido;
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
            Estudiante_sigue_EstudianteDAO estudianteSeguido = RepositoryDAL1.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguidor.Equals(userid) && x.id_estudianteSeguido.Equals(otherUserId)).FirstOrDefault();
            if (estudianteSeguido == null)
            {
                return null;
            }
            string IdEstudianteSeguido = estudianteSeguido.id_estudianteSeguido;
            List<PublicacionDAO> listaPublicacionesVisibles = RepositoryDAL1.Read<PublicacionDAO, DateTime>(x => x.id_estudiante.Equals(IdEstudianteSeguido) && x.activo, x => x.fecha_publicacion);
            List<PublicacionDAO> veintePublicaciones = listaPublicacionesVisibles.Skip(20 * (pag - 1)).Take(20).ToList();
            IEnumerable<int> listaIdPublicaciones = veintePublicaciones.Select(x => x.id);
            List<TutoriaDAO> listaTutorias = RepositoryDAL1.Read<TutoriaDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIDTutorias = listaTutorias.Select(x => x.id);
            List<RegistroTutoriaDAO> listRegistros = RepositoryDAL1.Read<RegistroTutoriaDAO>(x => listaIDTutorias.Contains(x.id_tutoria));
            List<Tecnologia_x_publicacionDAO> listaTecnologiasxPublicacion = RepositoryDAL1.Read<Tecnologia_x_publicacionDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIdTecnologias = listaTecnologiasxPublicacion.Select(x => x.id_tecnologia);
            List<TecnologiaDAO> listaTecnologias = RepositoryDAL1.Read<TecnologiaDAO>(x => listaIdTecnologias.Contains(x.id));
            List<ContenidoDAO> listaContenidos = RepositoryDAL1.Read<ContenidoDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<int> listaIdDocumentos = listaContenidos.Where(x => x.id_documento != null).Select(x => (int)x.id_documento);
            List<DocumentoDAO> listaDocumentos = RepositoryDAL1.Read<DocumentoDAO>(x => listaIdDocumentos.Contains(x.id));
            List<ComentarioDAO> comentarios = RepositoryDAL1.Read<ComentarioDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            List<EvaluacionDAO> evaluaciones = RepositoryDAL1.Read<EvaluacionDAO>(x => listaIdPublicaciones.Contains(x.id_publicacion));
            IEnumerable<string> estudiantesIdPublicando = veintePublicaciones.Select(x => x.id_estudiante);
            List<UsuarioDAO> listaUsuarios = RepositoryDAL1.Read<UsuarioDAO>(x => estudiantesIdPublicando.Contains(x.id));


            List<Publicacion> listaAEnviar = new List<Publicacion>();
            foreach (PublicacionDAO publicacion in listaPublicacionesVisibles)
            {
                if (publicacion.isTutoria)
                {
                    Tutoria publicacionAAgregar = new Tutoria();
                    AddDatosPublicacion(publicacion, comentarios, evaluaciones, listaUsuarios, listaTecnologiasxPublicacion, listaTecnologias, userid, ref publicacionAAgregar);
                    TutoriaDAO tutoria = listaTutorias.Where(x => x.id_publicacion == publicacion.id).FirstOrDefault();
                    publicacionAAgregar.Costo = tutoria.costo;
                    IEnumerable<string> listaIdEstudiantesRegistro = listRegistros.Where(x => x.id_tutoria == tutoria.id).Select(x => x.id_estudiante);
                    publicacionAAgregar.EstoyRegistrado = listaIdEstudiantesRegistro.Contains(userid) ? true : false;
                    publicacionAAgregar.FechaTutoria = tutoria.fecha_tutoria;
                    publicacionAAgregar.Lugar = tutoria.lugar;
                    listaAEnviar.Add(publicacionAAgregar);
                }
                else
                {
                    Contenido publicacionAAgregar = new Contenido();
                    AddDatosPublicacion(publicacion, comentarios, evaluaciones, listaUsuarios, listaTecnologiasxPublicacion, listaTecnologias, userid, ref publicacionAAgregar);
                    ContenidoDAO contenido = listaContenidos.Where(x => x.id_publicacion == publicacion.id).FirstOrDefault();
                    DocumentoDAO documento = listaDocumentos.Where(x => x.id == contenido.id_documento).FirstOrDefault();
                    publicacionAAgregar.Documento = documento == null ? "" : documento.contenido;
                    publicacionAAgregar.Link = contenido.enlace_extra;
                    publicacionAAgregar.Video = contenido.enlace_video;
                    listaAEnviar.Add(publicacionAAgregar);
                }

            }
            return listaAEnviar;
        }
        public T GetDocumento<T>(int DocId) where T : class
        {
            return null;
        }
        public IEnumerable<Comentario> GetComentarios(int pubId, int noPag)
        {
            List<ComentarioDAO> listaComentarios = RepositoryDAL1.Read<ComentarioDAO, DateTime>(x => x.id_publicacion == pubId && x.activo, x => x.fecha_creacion).Skip(20 * (noPag - 1)).Take(20).ToList();
            IEnumerable<string> listaestudianteIdComentarios = listaComentarios.Select(x => x.id_estudiante);
            List<UsuarioDAO> listaComentadores = RepositoryDAL1.Read<UsuarioDAO>(x => listaestudianteIdComentarios.Contains(x.id));
            List<Comentario> listaComentariosRetorno = new List<Comentario>();
            foreach (ComentarioDAO comentario in listaComentarios)
            {
                UsuarioDAO usuario = listaComentadores.Where(x => x.id.Equals(comentario.id_estudiante)).FirstOrDefault();
                Comentario nuevoComentario = new Comentario
                {
                    Id_Autor = comentario.id_estudiante,
                    Id_Comentario = comentario.id,
                    Contenido = comentario.contenido,
                    Fecha_comentario = comentario.fecha_creacion,
                    Nombre_Autor = usuario.nombre + " " + usuario.apellido,
                    id_publicacion = comentario.id_publicacion
                };
                listaComentariosRetorno.Add(nuevoComentario);
            }
            return listaComentariosRetorno;
        }
        public bool CreatePublicacion(Contenido contenido)
        {
            IEnumerable<string> nombresTecnologias = contenido.Tecnologias.Select(x => x.Nombre);
            List<TecnologiaDAO> listaTecnologias = RepositoryDAL1.Read<TecnologiaDAO>(x => nombresTecnologias.Contains(x.nombre));
            List<Tecnologia_x_publicacionDAO> listaTecnologiasPublicacion = new List<Tecnologia_x_publicacionDAO>();
            if (RepositoryDAL1.Create<PublicacionDAO>(new PublicacionDAO
            {
                descripcion = contenido.Descripcion,
                fecha_publicacion = DateTime.Now,
                id_estudiante = contenido.Id_autor,
                isTutoria = false,
                titulo = contenido.Titulo,
                Tecnologia_x_publicacion = listaTecnologiasPublicacion,
                activo = true
            }))
            {
                int id_publicacionCreada = RepositoryDAL1.Read<PublicacionDAO, int>(x => x.id > 0, x => x.id).FirstOrDefault().id;
                if (contenido.Documento != null)
                {
                    RepositoryDAL1.Create<DocumentoDAO>(new DocumentoDAO { contenido = contenido.Documento, tipo = "", tamano = 0 });
                }
                int id_contenidoCreado = RepositoryDAL1.Read<DocumentoDAO, int>(x => x.id > 0, x => x.id).FirstOrDefault().id;
                ContenidoDAO nuevoContenido = new ContenidoDAO
                {
                    enlace_extra = contenido.Link,
                    enlace_video = contenido.Video,
                    id_publicacion = id_publicacionCreada,
                    id_documento = contenido.Documento != null ? (int?)id_contenidoCreado : null
                };
                if (RepositoryDAL1.Create<ContenidoDAO>(nuevoContenido))
                    {                    
                    foreach (var tecnologia in listaTecnologias)
                    {
                        listaTecnologiasPublicacion.Add(new Tecnologia_x_publicacionDAO { id_tecnologia = tecnologia.id, id_publicacion = id_publicacionCreada });
                    }
                    EstudianteDAO estudianteAModificar = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(contenido.Id_autor)).FirstOrDefault();
                    estudianteAModificar.participacion++;
                    RepositoryDAL1.Update(estudianteAModificar);
                    return RepositoryDAL1.Create<Tecnologia_x_publicacionDAO>(listaTecnologiasPublicacion);
                }
            }
            return false;
            
        }
		public bool CreateTutoria(Tutoria tutoria)
		{
            IEnumerable<string> nombresTecnologias = tutoria.Tecnologias.Select(x => x.Nombre);
            List<TecnologiaDAO> listaTecnologias = RepositoryDAL1.Read<TecnologiaDAO>(x => nombresTecnologias.Contains(x.nombre));
            List<Tecnologia_x_publicacionDAO> listaTecnologiasPublicacion = new List<Tecnologia_x_publicacionDAO>();
            if (RepositoryDAL1.Create<PublicacionDAO>(new PublicacionDAO
            {
                descripcion = tutoria.Descripcion,
                fecha_publicacion = DateTime.Now,
                id_estudiante = tutoria.Id_autor,
                isTutoria = true,
                titulo = tutoria.Titulo,
                Tecnologia_x_publicacion = listaTecnologiasPublicacion,
                activo = true
            }))
            {
                int id_publicacionCreada = RepositoryDAL1.Read<PublicacionDAO, int>(x => x.id > 0, x => x.id).FirstOrDefault().id;
                TutoriaDAO nuevaTutoria = new TutoriaDAO
                {
                    costo = tutoria.Costo,
                    id_publicacion = id_publicacionCreada,
                    lugar = tutoria.Lugar,
                    fecha_tutoria = tutoria.FechaTutoria
                };
                if (RepositoryDAL1.Create<TutoriaDAO>(nuevaTutoria))
                {
                    foreach (var tecnologia in listaTecnologias)
                    {
                        listaTecnologiasPublicacion.Add(new Tecnologia_x_publicacionDAO { id_tecnologia = tecnologia.id, id_publicacion = id_publicacionCreada });
                    }
                    EstudianteDAO estudianteAModificar = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(tutoria.Id_autor)).FirstOrDefault();
                    estudianteAModificar.participacion++;
                    RepositoryDAL1.Update(estudianteAModificar);
                    return RepositoryDAL1.Create<Tecnologia_x_publicacionDAO>(listaTecnologiasPublicacion);
                }
            }
            return false;
        }
        public bool AddDocumento<T>(T documento) where T : class
        {
            return false;
        }

        public bool AddComentario(Comentario comentario)
        {
            PublicacionDAO publicacionDeComentario = RepositoryDAL1.Read<PublicacionDAO>(x => x.id == comentario.id_publicacion).FirstOrDefault();
            bool puedeComentar = true;
            if (publicacionDeComentario.isTutoria)
            {
                TutoriaDAO tutoria = publicacionDeComentario.Tutorias.FirstOrDefault();
                List<RegistroTutoriaDAO> registroTutoria = tutoria.RegistroTutorias.ToList();
                IEnumerable<string> estudiantesRegistrados = registroTutoria.Select(x => x.id_estudiante);
                puedeComentar = estudiantesRegistrados.Contains(comentario.Id_Autor);
            }
            ComentarioDAO nuevoComentario = new ComentarioDAO
            {
                contenido = comentario.Contenido,
                fecha_creacion = DateTime.Now,
                id_estudiante = comentario.Id_Autor,
                id_publicacion = comentario.id_publicacion,
                activo = true
            };
            return RepositoryDAL1.Create(nuevoComentario);
        }
        public bool AddOrModifyEvaluacion(Evaluacion evaluacion)
        {
            PublicacionDAO publicacionAEvaluar = RepositoryDAL1.Read<PublicacionDAO>(x => x.id == evaluacion.id_publicacion).FirstOrDefault();
            EstudianteDAO estudianteEvaluado = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(publicacionAEvaluar.id_estudiante)).FirstOrDefault();
            List<PublicacionDAO> publicacionesDelUsuario = RepositoryDAL1.Read<PublicacionDAO>(x => x.id_estudiante.Equals(estudianteEvaluado.id_usuario));
            int totalEvaluacionesNegativas = publicacionesDelUsuario.Select(x => x.evaluaciones_negativas).Sum();
            int totalEvaluacionesPositivas = publicacionesDelUsuario.Select(x => x.evaluaciones_positivas).Sum();
            int reputacion;
            double porcentaje0a1;
            EvaluacionDAO evaluacionExistente = RepositoryDAL1.Read<EvaluacionDAO>(x =>
                                                x.id_estudiante.Equals(evaluacion.Id_estudiante) &&
                                                x.id_publicacion == evaluacion.id_publicacion)
                                                .FirstOrDefault();
            if (evaluacion.Tipo_evaluacion.Equals("null") && evaluacionExistente != null)
            {
                if ((bool)evaluacionExistente.positiva)
                {
                    publicacionAEvaluar.evaluaciones_positivas--;
                    totalEvaluacionesPositivas--;
                }
                else
                {
                    publicacionAEvaluar.evaluaciones_negativas--;
                    totalEvaluacionesNegativas--;
                }
                if ((totalEvaluacionesPositivas + totalEvaluacionesNegativas) == 0)
                {
                    reputacion = 0;
                }
                else
                {
                    porcentaje0a1 = ((double)totalEvaluacionesPositivas / ((double)totalEvaluacionesPositivas + (double)totalEvaluacionesNegativas));
                    reputacion = (int)((porcentaje0a1) * 100);
                }
                estudianteEvaluado.reputacion = reputacion;
                RepositoryDAL1.Update(estudianteEvaluado);
                RepositoryDAL1.Update(publicacionAEvaluar);
                return RepositoryDAL1.Delete(evaluacionExistente);
            }
            if (publicacionAEvaluar.isTutoria)
            {
                EstudianteDAO estudianteQueEvalua = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(evaluacion.Id_estudiante)).FirstOrDefault();
                TutoriaDAO tutoriaEvaluada = publicacionAEvaluar.Tutorias.Where(x => x.id_publicacion == evaluacion.id_publicacion).FirstOrDefault();
                IEnumerable<int> listaIdTutoriasRegistradas = estudianteQueEvalua.RegistroTutorias.Select(x => x.id_tutoria);
                if (!listaIdTutoriasRegistradas.Contains(tutoriaEvaluada.id) || tutoriaEvaluada.fecha_tutoria > DateTime.Now)
                {
                    return false; //no se puede evaluar porque no está registrado o la tutoría no ha terminado
                }
            }
            EvaluacionDAO evaluacionAAgregar = new EvaluacionDAO
            {
                id_publicacion = evaluacion.id_publicacion,
                positiva = evaluacion.Tipo_evaluacion.Equals("positiva") ? true : false,
                id_estudiante = evaluacion.Id_estudiante
            };
            if (evaluacion.Tipo_evaluacion.Equals("positiva") && evaluacionExistente != null)
            {
                if (!(bool)evaluacionExistente.positiva) //es negativa
                {
                    publicacionAEvaluar.evaluaciones_positivas++;
                    totalEvaluacionesPositivas++;
                    publicacionAEvaluar.evaluaciones_negativas--;
                    totalEvaluacionesNegativas--;
                }
                else
                {
                    return true;
                }
                RepositoryDAL1.Delete(evaluacionExistente);
            }
            if (evaluacion.Tipo_evaluacion.Equals("positiva") && evaluacionExistente == null)
            {
                publicacionAEvaluar.evaluaciones_positivas++;
                totalEvaluacionesPositivas++;
            }
            if (evaluacion.Tipo_evaluacion.Equals("negativa") && evaluacionExistente != null)
            {
                if ((bool)evaluacionExistente.positiva) //es negativa
                {
                    publicacionAEvaluar.evaluaciones_positivas--;
                    totalEvaluacionesPositivas--;
                    publicacionAEvaluar.evaluaciones_negativas++;
                    totalEvaluacionesNegativas++;
                }
                else
                {
                    return true;
                }
                RepositoryDAL1.Delete(evaluacionExistente);
            }
            if (evaluacion.Tipo_evaluacion.Equals("negativa") && evaluacionExistente == null)
            {
                publicacionAEvaluar.evaluaciones_negativas++;
                totalEvaluacionesNegativas++;
            }
            porcentaje0a1 = ((double)totalEvaluacionesPositivas / ((double)totalEvaluacionesPositivas + (double)totalEvaluacionesNegativas));
            reputacion = (int)((porcentaje0a1) * 100);
            estudianteEvaluado.reputacion = reputacion;
            RepositoryDAL1.Update(estudianteEvaluado);
            RepositoryDAL1.Update(publicacionAEvaluar);
            return RepositoryDAL1.Create(evaluacionAAgregar);
        }
        public bool DeletePublicacion(int pubId)
        {
            PublicacionDAO publicacionAModificar = RepositoryDAL1.Read<PublicacionDAO>(x => x.id == pubId).FirstOrDefault();
            publicacionAModificar.activo = false;
            EstudianteDAO estudianteAModificar = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(publicacionAModificar.id_estudiante)).FirstOrDefault();
            estudianteAModificar.participacion--;
            RepositoryDAL1.Update(estudianteAModificar);
            return RepositoryDAL1.Update<PublicacionDAO>(publicacionAModificar);
        }
        public bool DeleteComentario(int IdComentario)
        {
            ComentarioDAO comentarioAModificar = RepositoryDAL1.Read<ComentarioDAO>(x => x.id == IdComentario).FirstOrDefault();
            comentarioAModificar.activo = false;
            return RepositoryDAL1.Update<ComentarioDAO>(comentarioAModificar);
        }
    }
}