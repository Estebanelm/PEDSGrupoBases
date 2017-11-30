using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer.Repository;

namespace DigiTutorService.DataAccessLayer
{
    
    public class FachadaUsuariosDAL
    {
        static int WEEK= 7;
        static int APOYOS_SEMANA = 5;



        //metodos
        //===============================================================================================================================================================
       
            //Obtiene los datos del estudiante que está logueado
        public Estudiante GetEstudiantePropio(string EstudianteId)
        {
            //obtner el la informacion de usuario y de estudiante de la base de datos
            EstudianteDAO estudiante = RepositoryDAL.Read<EstudianteDAO>(x => x.id_usuario.Equals(EstudianteId)).FirstOrDefault();
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(EstudianteId)).FirstOrDefault();
            if(estudiante==null && user ==null)            
            return null;

            //obtener la informacion de tecnologias del estudiante
            List<Tecnologia_x_EstudianteDAO> tecEstudiante = RepositoryDAL.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(EstudianteId));

            //creamos la lista de tecnolgias con sus respectivos apoyos
            List<Estudiante.TecnologiaPerfil> tecApoyo = new List<Estudiante.TecnologiaPerfil>();
            foreach(Tecnologia_x_EstudianteDAO tec in tecEstudiante)
            {
                //se pone el apyo como "fijo" para que no salga el botoncito de "+" puesto que yo no me uedo apoyar a mi mismo
                tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "fijo", Nombre = tec.Tecnologia.nombre });                
            }

            //crear el estudiante
            Estudiante result = new Estudiante { Id = user.id, Apellido = user.apellido, CantSeguidores = estudiante.numero_seguidores, Correo = user.correo_principal, Correo2 = estudiante.correo_secundario,
                Descripcion = estudiante.descripcion, FechaInscripcion = user.fecha_creacion, Nombre = user.nombre, Pais = estudiante.Pai.nombre, Participacion = estudiante.participacion,
                Reputacion = estudiante.reputacion, Telefono = estudiante.telefono_celular, Telefono2 = estudiante.telefono_fijo, Universidad = estudiante.Universidad.nombre, Tecnologias= tecApoyo };

            return result;
        }


//===============================================================================================================================================================

        public bool CheckLogin(Login login)
        {
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(login.id_estudiante)).FirstOrDefault();
            if (user == null)
            {
                //no existe ese nombre de usuario
                return false;
            }
            //si la contraseña es correcta
            if (user.contrasena.Equals(login.contrasena))
            {
                //generar token de seguridad y asignarlo a login
                login.tokenSeguridad = "aqui va el token de seguridad";
                return true;                

            }
            return false;
        }


//===============================================================================================================================================================

        public Estudiante GetEstudianteAjeno(string estudiante1, string EstudianteaBuscar)
        {
            //obtner el la informacion de usuario y de estudiante de la base de datos
            EstudianteDAO estudiante = RepositoryDAL.Read<EstudianteDAO>(x => x.id_usuario.Equals(EstudianteaBuscar)).FirstOrDefault();
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(EstudianteaBuscar)).FirstOrDefault();
            if (estudiante == null && user == null)
                return null;

            //obtener la informacion de tecnologias del estudiante
            List<Tecnologia_x_EstudianteDAO> tecEstudiante = RepositoryDAL.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(EstudianteaBuscar));

            //creamos la lista de tecnolgias con sus respectivos apoyos
            List<Estudiante.TecnologiaPerfil> tecApoyo = new List<Estudiante.TecnologiaPerfil>();
            foreach (Tecnologia_x_EstudianteDAO tec in tecEstudiante)
            {
                //buscamos si la persona ha sido apoyada por el estudiante que lo busca
                ApoyoDAO apoyo = RepositoryDAL.Read<ApoyoDAO>(x => x.id_estudianteDaApoyo.Equals(estudiante1) && x.id_estudianteApoyado.Equals(EstudianteaBuscar) &&
                        x.id_tecnologia.Equals(tec.id_tecnologia)).FirstOrDefault();
                if (apoyo != null)
                {
                    //si el apoyo fue dado en la ultima semana
                    if (apoyo.fecha < DateTime.Now.AddDays(-WEEK))
                    {
                        tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "fijo", Nombre = tec.Tecnologia.nombre });
                    }
                    //apoyo dado hace mas de una semana
                    else
                    tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "transitorio", Nombre = tec.Tecnologia.nombre });
                }
                //no ha sido apoyado en esta tecnologia
                else
                {
                    tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "tnull", Nombre = tec.Tecnologia.nombre });
                }
            }

            //crear el estudiante
            Estudiante result = new Estudiante
            {
                Id = user.id,
                Apellido = user.apellido,
                CantSeguidores = estudiante.numero_seguidores,
                Correo = user.correo_principal,
                Correo2 = estudiante.correo_secundario,
                Descripcion = estudiante.descripcion,
                FechaInscripcion = user.fecha_creacion,
                Nombre = user.nombre,
                Pais = estudiante.Pai.nombre,
                Participacion = estudiante.participacion,
                Reputacion = estudiante.reputacion,
                Telefono = estudiante.telefono_celular,
                Telefono2 = estudiante.telefono_fijo,
                Universidad = estudiante.Universidad.nombre,
                Tecnologias = tecApoyo,
                ApoyosDisponibles= estudiante.apoyos_disponibles
            };

            return result;
        }


        //===============================================================================================================================================================
        public Administrador GetAdministrador(string AdminId)
        {
            //buscamos al usuario en la tabla
            UsuarioDAO admin = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(AdminId)).FirstOrDefault();
            if (admin != null)
            {
                return new Administrador { Apellido = admin.apellido, Correo = admin.correo_principal,
                    FechaInscripcion = admin.fecha_creacion, Id = admin.id_generado, NombreUsuario = admin.id, Nombre = admin.nombre }; 
            }
            return null;
        }

        //===============================================================================================================================================================
        public IEnumerable<Estudiante> GetEstudiantes(string nombreEstudiante, int id_universidad, int id_pais,int id_tecnologia, int pag)
        {
            return null;
        }

        //===============================================================================================================================================================

        public IEnumerable<Estudiante> GetReporteEstudiantes(int id_un, int id_pais,
                         int tec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4, int pag)
        {
            return null;
        }

        //===============================================================================================================================================================
        public bool CrearEstudiante(string password, Estudiante estudiante)
        {
            //primero buscamos a ver si existe ese estudiante
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(estudiante.Id)).FirstOrDefault();
            EstudianteDAO existente = RepositoryDAL.Read<EstudianteDAO>(x => x.Usuario.id.Equals(estudiante.Id)).FirstOrDefault();
            //si no existe el estudiante
            if (user == null && existente == null)
            {
                PaisDAO pais = RepositoryDAL.Read<PaisDAO>(x => x.nombre.Equals(estudiante.Pais)).FirstOrDefault();
                UniversidadDAO univ = RepositoryDAL.Read<UniversidadDAO>(x => x.nombre.Equals(estudiante.Universidad)).FirstOrDefault();
                //fallo al crear el pais y universidad
                if (pais == null || univ ==null) return false;
                user = new UsuarioDAO
                {
                    id = estudiante.Id,
                    activo = true,
                    apellido = estudiante.Apellido,
                    contrasena = password,
                    correo_principal = estudiante.Correo,
                    fecha_creacion = DateTime.Now,
                    is_admin = false,
                    nombre = estudiante.Nombre
                };
                existente = new EstudianteDAO
                {
                    id_usuario = estudiante.Id,
                    apoyos_disponibles = APOYOS_SEMANA,
                    descripcion = estudiante.Descripcion,
                    correo_secundario = estudiante.Correo2,
                    id_pais = pais.id,
                    telefono_celular = estudiante.Telefono,
                    telefono_fijo = estudiante.Telefono2,
                    foto = estudiante.Foto,
                    id_universidad = univ.id,
                    numero_seguidores = 0,
                    participacion = 0,
                    reputacion = 0
                };

                //agregar los DAO a la base de Datos
                if (RepositoryDAL.Create(user))
                {
                    if (RepositoryDAL.Create(existente))
                    {
                        //estudiante creado
                        return true;
                    }
                }
                //fallo al ingresar a la base de datos

            }
            return false;
        }

        //===============================================================================================================================================================
        public bool CrearAdministrador(string password, Administrador administrador)
        {
            //primero buscamos a ver si existe esl administrador
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(administrador.Id)).FirstOrDefault();
            if (user==null)
            {
                user = new UsuarioDAO
                {
                    activo = true,
                    fecha_creacion = DateTime.Now,
                    apellido = administrador.Apellido,
                    contrasena = password,
                    correo_principal = administrador.Correo,
                    id = administrador.NombreUsuario,
                    is_admin = true,
                    nombre = administrador.Nombre
                };
                if (RepositoryDAL.Create(user)) return true; //se creo administrador

                //no se logro crear fallo en BD

            }
            return false;//ya existe un administrador asi
        }

        //===============================================================================================================================================================
        public bool AddSeguimiento(Seguimiento seguimiento)
        {
            //buscamos si ya existe un seguimiento (es decir si el estudiante "seguidor" sigue al estudiante "seguido")
            Estudiante_sigue_EstudianteDAO seg = RepositoryDAL.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguido.Equals(seguimiento.id_estudianteSeguido) && x.id_estudianteSeguidor.Equals(seguimiento.id_estudianteSigue)).FirstOrDefault();
            //si no existe
            if (seg == null)
            {
                seg = new Estudiante_sigue_EstudianteDAO { id_estudianteSeguidor = seguimiento.id_estudianteSigue, id_estudianteSeguido = seguimiento.id_estudianteSeguido };
                if (RepositoryDAL.Create(seg))
                {
                    EstudianteDAO estSeguido = RepositoryDAL.Read<EstudianteDAO>(x => x.id_usuario.Equals(seguimiento.id_estudianteSeguido)).FirstOrDefault();
                    if (estSeguido == null) return false; //error, no existe el estudiante q usted quiere seguir
                    //sumamos la cantidad de seguidores 
                    estSeguido.numero_seguidores += 1;
                    if(RepositoryDAL.Update(estSeguido)==1) return true; //se creo el seguimiento correctamente y se sumo los seguidores al seguido

                }

                //fallo al entra a la base de datos
            }

            //ya existia el seguimiento, no se hizo nada
            return false;
        }

        //===============================================================================================================================================================
        public bool ModificarEstudiante(string estudianteId, Estudiante estudiante)
        {
            //primero intentamos obtener el estudiante correcto
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(estudianteId)).FirstOrDefault();
            EstudianteDAO existente = RepositoryDAL.Read<EstudianteDAO>(x => x.Usuario.id.Equals(estudianteId)).FirstOrDefault();
            //si no existe el estudiante
            if (user == null && existente == null) return false; //error, no existe el estudiante, no deberia suceder
            else 
            {
                PaisDAO pais = RepositoryDAL.Read<PaisDAO>(x => x.nombre.Equals(estudiante.Pais)).FirstOrDefault();
                UniversidadDAO univ = RepositoryDAL.Read<UniversidadDAO>(x => x.nombre.Equals(estudiante.Universidad)).FirstOrDefault();
                //fallo al crear el pais y universidad
                if (pais == null || univ == null) return false;


                //modificar el usuario
                user.apellido = estudiante.Apellido;
                user.correo_principal = estudiante.Correo;
                user.nombre = estudiante.Nombre;

                //modificar estudiante
                existente.id_usuario = estudiante.Id;
                existente.apoyos_disponibles = APOYOS_SEMANA;
                existente.descripcion = estudiante.Descripcion;
                existente.correo_secundario = estudiante.Correo2;
                existente.id_pais = pais.id;
                existente.telefono_celular = estudiante.Telefono;
                existente.telefono_fijo = estudiante.Telefono2;
                existente.foto = estudiante.Foto;
                //actualizar informacion
                if (RepositoryDAL.Update<UsuarioDAO>(user) == 1)
                {
                    if (RepositoryDAL.Update(existente) == 1) return true;
                }
                        
                   
                //fallo al ingresar a la base de datos

            }
            return false;
        }
        //===============================================================================================================================================================
        public bool ModificarAdministador(string adminId, Administrador administrador)
        {
            //primero intentamos obtener el admnistrador
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(adminId)).FirstOrDefault();
            //si no existe el administrador
            if (user == null) return false; //error, no existe el administrador, no deberia suceder
            else
            {



                //modificar el usuario
                user.apellido = administrador.Apellido;
                user.correo_principal = administrador.Correo;
                user.nombre = administrador.Nombre;

                if(RepositoryDAL.Update(user)==1)  return true;//se actualizo administrador
            }

            return false; //error al entrar en la base de datos
        }
        //===============================================================================================================================================================
        public bool DeleteEstudiante(string estudianteId)
        {
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(estudianteId)).FirstOrDefault();
            if (user == null) return false; //no existe ese estudiante

            user.activo = false;
            if (RepositoryDAL.Update(user) == 1) return true; //Se desactivo el usuario

            //fallo al entrar a la base de datos
            return false;
        }
        //===============================================================================================================================================================
        public bool DeleteAdministrador(string adminId)
        {
            UsuarioDAO user = RepositoryDAL.Read<UsuarioDAO>(x => x.id.Equals(adminId)).FirstOrDefault();
            if (user == null) return false; //no existe ese usuario

            user.activo = false;
            if (RepositoryDAL.Update(user) == 1) return true; //Se desactivo el usuario

            //fallo al entrar a la base de datos
            return false;
        }

        //===============================================================================================================================================================
        public bool DeleteSeguimiento(Seguimiento seguimiento)
        {
            Estudiante_sigue_EstudianteDAO seg = RepositoryDAL.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguido.Equals(seguimiento.id_estudianteSeguido) && x.id_estudianteSeguidor.Equals(seguimiento.id_estudianteSigue)).FirstOrDefault();
            if (seg == null) return false;//no existe el seguimiento
            if (RepositoryDAL.Delete(seg))
            {

                EstudianteDAO estSeguido = RepositoryDAL.Read<EstudianteDAO>(x => x.id_usuario.Equals(seguimiento.id_estudianteSeguido)).FirstOrDefault();
                if (estSeguido == null) return false; //error, no existe el estudiante q usted quiere dejar seguir
                                                      
                estSeguido.numero_seguidores-= 1;//restamos cantidad de seguidores
                if (RepositoryDAL.Update(estSeguido) == 1) return true;//se actualizo y dejo de seguir estudiante
                
                //problema actualizando el estudiante
            }
            //problema borrando
            return false;
        }
        //===============================================================================================================================================================
        public bool AddApoyo(Apoyo apoyo)
        {
            TecnologiaDAO tecnologia = RepositoryDAL.Read<TecnologiaDAO>
            ApoyoDAO apy = RepositoryDAL.Read<ApoyoDAO>(x => x.id_estudianteApoyado.Equals(apoyo.id_estudianteApoyado) && x.id_estudianteDaApoyo.Equals(apoyo.id_estudianteQueApoya)).FirstOrDefault();
            return false;
        }

        //===============================================================================================================================================================
        public bool DeleteApoyo(Apoyo apoyo)
        {
            return false;
        }

        //===============================================================================================================================================================
    }
}