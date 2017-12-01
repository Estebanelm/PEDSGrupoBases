using System;
using System.Collections.Generic;
using System.Linq;
using DigiTutorService.Models;
using DigiTutorService.DataAccessLayer.Repository;

namespace DigiTutorService.DataAccessLayer
{

    public class FachadaUsuariosDAL
    {
        static int WEEK = 7;
        static int APOYOS_SEMANA = 5;

        public RepositoryDAL RepositoryDAL1;

        public FachadaUsuariosDAL()
        {
            RepositoryDAL1 = new RepositoryDAL();
        }

        //metodos
        //===============================================================================================================================================================

        //Obtiene los datos del estudiante que está logueado
        public Estudiante GetEstudiantePropio(string EstudianteId)
        {
            //obtner la informacion de usuario y de estudiante de la base de datos
            EstudianteDAO estudiante = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(EstudianteId)).FirstOrDefault();
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(EstudianteId)).FirstOrDefault();
            if (estudiante == null && user == null)   //si no existen         
                return null;

            //obtener la informacion de tecnologias del estudiante
            List<Tecnologia_x_EstudianteDAO> tecEstudiante = RepositoryDAL1.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(EstudianteId));

            //creamos la lista de tecnolgias con sus respectivos apoyos
            List<Estudiante.TecnologiaPerfil> tecApoyo = new List<Estudiante.TecnologiaPerfil>();
            IEnumerable<int> listaIdTecnologias = tecEstudiante.Select(x => x.id);

            List<TecnologiaDAO> listaTecnologias = RepositoryDAL1.Read<TecnologiaDAO>(x => listaIdTecnologias.Contains(x.id));
            foreach (Tecnologia_x_EstudianteDAO tec in tecEstudiante)
            {
                string nombreTecnologia = listaTecnologias.Where(x => x.id.Equals(tec.id_tecnologia)).Select(x => x.nombre).FirstOrDefault();
                //se pone el apyo como "fijo" para que no salga el botoncito de "+" puesto que yo no me puedo apoyar a mi mismo
                tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "fijo", Nombre = nombreTecnologia});
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
                Tecnologias = tecApoyo
            };

            return result;
        }


        //===============================================================================================================================================================

        public bool CheckLogin(Login login)
        {
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(login.id_estudiante)).FirstOrDefault();
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
            EstudianteDAO estudiante = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(EstudianteaBuscar)).FirstOrDefault();
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(EstudianteaBuscar)).FirstOrDefault();
            if (estudiante == null && user == null)
                return null;

            //obtener la informacion de tecnologias del estudiante
            List<Tecnologia_x_EstudianteDAO> tecEstudiante = RepositoryDAL1.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(EstudianteaBuscar));

            //creamos la lista de tecnolgias con sus respectivos apoyos
            List<Estudiante.TecnologiaPerfil> tecApoyo = new List<Estudiante.TecnologiaPerfil>();
            foreach (Tecnologia_x_EstudianteDAO tec in tecEstudiante)
            {
                //buscamos si la persona ha sido apoyada por el estudiante que lo busca
                ApoyoDAO apoyo = RepositoryDAL1.Read<ApoyoDAO>(x => x.id_estudianteDaApoyo.Equals(estudiante1) && x.id_estudianteApoyado.Equals(EstudianteaBuscar) &&
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
                    tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "null", Nombre = tec.Tecnologia.nombre });
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
                ApoyosDisponibles = estudiante.apoyos_disponibles
            };

            return result;
        }


        //===============================================================================================================================================================
        public Administrador GetAdministrador(string AdminId)
        {
            //buscamos al usuario en la tabla
            UsuarioDAO admin = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(AdminId)).FirstOrDefault();
            if (admin != null)
            {
                return new Administrador
                {
                    Apellido = admin.apellido,
                    Correo = admin.correo_principal,
                    FechaInscripcion = admin.fecha_creacion,
                    Id = admin.id_generado,
                    NombreUsuario = admin.id,
                    Nombre = admin.nombre
                };
            }
            return null;
        }

        //===============================================================================================================================================================

        //busqueda de estudiantes
        public IEnumerable<Estudiante> GetEstudiantes(string nombreEstudiante, int id_universidad, int id_pais, int id_tecnologia, int pag)
        {
            //seleccionamos a los usuarios que cumplen con los  filtros 
            var res = RepositoryDAL1.Read<UsuarioDAO>(x => (nombreEstudiante != "" ? x.nombre.Equals(nombreEstudiante) : true) &&
            (id_universidad > 0 ? x.Estudiante.id_universidad == id_universidad : true) &&
            (id_pais > 0 ? x.Estudiante.id_pais == id_pais : true) &&
            (id_tecnologia > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(id_tecnologia) : true));

            //ordenamos los resultados por reputacion
            res.OrderByDescending(x => x.Estudiante.reputacion);

            //elegimos 20 segun el número de página
            var resultado = res.Skip(20 * (pag - 1)).Take(20).ToList();

            //llenamos la lista de resultado
            List<Estudiante> result = new List<Estudiante>();
            foreach (UsuarioDAO user in resultado)
            {
                //creamos la lista de tecnolgias con sus respectivos apoyos
                List<Estudiante.TecnologiaPerfil> tecApoyo = new List<Estudiante.TecnologiaPerfil>();
                foreach (Tecnologia_x_EstudianteDAO tec in user.Estudiante.Tecnologia_x_Estudiante)
                {
                    //se pone el apyo como "fijo" para que no salga el botoncito de "+" puesto que son resultados de busqueda
                    tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "fijo", Nombre = tec.Tecnologia.nombre });
                }
                //creamos el estudiante y lo agregamos al resultado
                result.Add(new Estudiante
                {
                    Id = user.id,
                    Apellido = user.apellido,
                    CantSeguidores = user.Estudiante.numero_seguidores,
                    Correo = user.correo_principal,
                    Correo2 = user.Estudiante.correo_secundario,
                    Descripcion = user.Estudiante.descripcion,
                    FechaInscripcion = user.fecha_creacion,
                    Nombre = user.nombre,
                    Pais = user.Estudiante.Pai.nombre,
                    Participacion = user.Estudiante.participacion,
                    Reputacion = user.Estudiante.reputacion,
                    Telefono = user.Estudiante.telefono_celular,
                    Telefono2 = user.Estudiante.telefono_fijo,
                    Universidad = user.Estudiante.Universidad.nombre,
                    Tecnologias = tecApoyo
                });                          
                
             }

            return result;

        }

        //===============================================================================================================================================================
        //busqueda de reclutamiento
        public IEnumerable<Estudiante> GetReporteEstudiantes(int id_un, int id_pais,
                         int tec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4, int pag)
        {
            //seleccionamos a los usuarios que cumplen con los  filtros 
            var usuarios = RepositoryDAL1.Read<UsuarioDAO>(x =>
            (id_un > 0 ? x.Estudiante.id_universidad == id_un : true) &&
            (id_pais > 0 ? x.Estudiante.id_pais == id_pais : true) &&
            (tec1 > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(tec1) : true) &&
            (tec2 > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(tec2) : true) &&
            (tec3 > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(tec3) : true) &&
            (tec4 > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(tec4) : true));




            return null;
        }

        //===============================================================================================================================================================
        public bool CrearEstudiante(string password, Estudiante estudiante)
        {
            //primero buscamos a ver si existe ese estudiante
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(estudiante.Id)).FirstOrDefault();
            EstudianteDAO existente = RepositoryDAL1.Read<EstudianteDAO>(x => x.Usuario.id.Equals(estudiante.Id)).FirstOrDefault();
            //si no existe el estudiante
            if (user == null && existente == null)
            {
                PaisDAO pais = RepositoryDAL1.Read<PaisDAO>(x => x.nombre.Equals(estudiante.Pais)).FirstOrDefault();
                UniversidadDAO univ = RepositoryDAL1.Read<UniversidadDAO>(x => x.nombre.Equals(estudiante.Universidad)).FirstOrDefault();


                //fallo al crear el pais y universidad
                if (pais == null || univ == null) return false;


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
                if (RepositoryDAL1.Create(user))
                {
                    if (RepositoryDAL1.Create(existente))
                    {
                        //una vez creados el usuario y estudiante, agregamos las tecnologias
                        //se obtiene la lista de tecnologias que el estudiante selecciono
                        IEnumerable<string> listaNombreTecnologias = estudiante.Tecnologias.Select(t => t.Nombre);
                        List<TecnologiaDAO> tecnologiasEstudiante = RepositoryDAL1.Read<TecnologiaDAO>(x => listaNombreTecnologias.Contains(x.nombre));
                        //por cada una de las tecnologías se agrega la nueva tecnologia
                        foreach (TecnologiaDAO tec in tecnologiasEstudiante)
                        {
                            if (!RepositoryDAL1.Create(new Tecnologia_x_EstudianteDAO
                            {
                                id_estudiante = estudiante.Id,
                                id_tecnologia = tec.id,
                                cantidadApoyos = 0
                            })) return false;//error al crear la tecnologia x estudiante
                        }
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
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(administrador.Id)).FirstOrDefault();
            if (user == null)
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
                if (RepositoryDAL1.Create(user)) return true; //se creo administrador

                //no se logro crear fallo en BD

            }
            return false;//ya existe un administrador asi
        }

        //===============================================================================================================================================================
        public bool AddSeguimiento(Seguimiento seguimiento)
        {
            //buscamos si ya existe un seguimiento (es decir si el estudiante "seguidor" sigue al estudiante "seguido")
            Estudiante_sigue_EstudianteDAO seg = RepositoryDAL1.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguido.Equals(seguimiento.id_estudianteSeguido) && x.id_estudianteSeguidor.Equals(seguimiento.id_estudianteSigue)).FirstOrDefault();
            //si no existe
            if (seg == null)
            {
                seg = new Estudiante_sigue_EstudianteDAO { id_estudianteSeguidor = seguimiento.id_estudianteSigue, id_estudianteSeguido = seguimiento.id_estudianteSeguido };
                if (RepositoryDAL1.Create(seg))
                {
                    EstudianteDAO estSeguido = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(seguimiento.id_estudianteSeguido)).FirstOrDefault();
                    if (estSeguido == null) return false; //error, no existe el estudiante q usted quiere seguir
                    //sumamos la cantidad de seguidores 
                    estSeguido.numero_seguidores += 1;
                    if (RepositoryDAL1.Update(estSeguido) == 1) return true; //se creo el seguimiento correctamente y se sumo los seguidores al seguido

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
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(estudianteId)).FirstOrDefault();
            EstudianteDAO existente = RepositoryDAL1.Read<EstudianteDAO>(x => x.Usuario.id.Equals(estudianteId)).FirstOrDefault();
            //si no existe el estudiante
            if (user == null && existente == null) return false; //error, no existe el estudiante, no deberia suceder
            else
            {
                PaisDAO pais = RepositoryDAL1.Read<PaisDAO>(x => x.nombre.Equals(estudiante.Pais)).FirstOrDefault();
                UniversidadDAO univ = RepositoryDAL1.Read<UniversidadDAO>(x => x.nombre.Equals(estudiante.Universidad)).FirstOrDefault();
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
                if (RepositoryDAL1.Update<UsuarioDAO>(user) == 1)
                {
                    if (RepositoryDAL1.Update(existente) == 1)
                    {
                        //actualizar las tecnolgias----------------------------------------------'por hacer



                    }
                    return true;
                }


                //fallo al ingresar a la base de datos

            }
            return false;
        }
        //===============================================================================================================================================================
        public bool ModificarAdministador(string adminId, Administrador administrador)
        {
            //primero intentamos obtener el admnistrador
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(adminId)).FirstOrDefault();
            //si no existe el administrador
            if (user == null) return false; //error, no existe el administrador, no deberia suceder
            else
            {

                //modificar el usuario
                user.apellido = administrador.Apellido;
                user.correo_principal = administrador.Correo;
                user.nombre = administrador.Nombre;

                if (RepositoryDAL1.Update(user) == 1) return true;//se actualizo administrador
            }

            return false; //error al entrar en la base de datos
        }
        //===============================================================================================================================================================
        public bool DeleteEstudiante(string estudianteId)
        {
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(estudianteId)).FirstOrDefault();
            if (user == null) return false; //no existe ese estudiante

            user.activo = false;
            if (RepositoryDAL1.Update(user) == 1) return true; //Se desactivo el usuario

            //fallo al entrar a la base de datos
            return false;
        }
        //===============================================================================================================================================================
        public bool DeleteAdministrador(string adminId)
        {
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(adminId)).FirstOrDefault();
            if (user == null) return false; //no existe ese usuario

            user.activo = false;
            if (RepositoryDAL1.Update(user) == 1) return true; //Se desactivo el usuario

            //fallo al entrar a la base de datos
            return false;
        }

        //===============================================================================================================================================================
        public bool DeleteSeguimiento(Seguimiento seguimiento)
        {
            Estudiante_sigue_EstudianteDAO seg = RepositoryDAL1.Read<Estudiante_sigue_EstudianteDAO>(x => x.id_estudianteSeguido.Equals(seguimiento.id_estudianteSeguido) && x.id_estudianteSeguidor.Equals(seguimiento.id_estudianteSigue)).FirstOrDefault();
            if (seg == null) return false;//no existe el seguimiento
            if (RepositoryDAL1.Delete(seg))
            {

                EstudianteDAO estSeguido = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(seguimiento.id_estudianteSeguido)).FirstOrDefault();
                if (estSeguido == null) return false; //error, no existe el estudiante q usted quiere dejar seguir

                estSeguido.numero_seguidores -= 1;//restamos cantidad de seguidores
                if (RepositoryDAL1.Update(estSeguido) == 1) return true;//se actualizo y dejo de seguir estudiante

                //problema actualizando el estudiante
            }
            //problema borrando
            return false;
        }
        //===============================================================================================================================================================
        public bool AddApoyo(Apoyo apoyo)
        {
            EstudianteDAO estudianteApoyado = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(apoyo.id_estudianteApoyado)).FirstOrDefault();
            EstudianteDAO estudianteApoya = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(apoyo.id_estudianteQueApoya)).FirstOrDefault(); ;
            TecnologiaDAO tecnologia = RepositoryDAL1.Read<TecnologiaDAO>(x => x.nombre.Equals(apoyo.Tecnologia)).FirstOrDefault();
            ApoyoDAO apy = RepositoryDAL1.Read<ApoyoDAO>(x => x.id_estudianteApoyado.Equals(apoyo.id_estudianteApoyado) && x.id_estudianteDaApoyo.Equals(apoyo.id_estudianteQueApoya) && x.id_tecnologia.Equals(tecnologia.id)).FirstOrDefault();
            //si no existe el apoyo
            if (apy == null)
            {
                //si el estudiante que apoya tiene apoyos disponibles
                if (estudianteApoya.apoyos_disponibles > 0)
                {
                    RepositoryDAL1.Create(new ApoyoDAO
                    {
                        fecha = DateTime.Now,
                        id_estudianteApoyado = apoyo.id_estudianteApoyado,
                        id_estudianteDaApoyo = apoyo.id_estudianteQueApoya,
                        id_tecnologia = tecnologia.id
                    });

                    //restamos la cantidad de apoyos disponibles y actualizamos
                    estudianteApoya.apoyos_disponibles -= 1;
                    RepositoryDAL1.Update(estudianteApoya);

                    //se suma la cantidad de apoyos a la tabla de tecnologiasXestudiante
                    Tecnologia_x_EstudianteDAO tecEst = RepositoryDAL1.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(estudianteApoyado.id_usuario) &&
                            x.id_tecnologia.Equals(tecnologia.id)).FirstOrDefault();

                    tecEst.cantidadApoyos += 1;
                    RepositoryDAL1.Update(tecEst);

                    return true; //creo el apoyo


                }
                //no tiene suficientes apoyos disponibles
            }

            //el apoyo ya existe
            return false;
        }

        //===============================================================================================================================================================
        public bool DeleteApoyo(Apoyo apoyo)
        {
            EstudianteDAO estudianteApoyado = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(apoyo.id_estudianteApoyado)).FirstOrDefault();
            EstudianteDAO estudianteApoya = RepositoryDAL1.Read<EstudianteDAO>(x => x.id_usuario.Equals(apoyo.id_estudianteQueApoya)).FirstOrDefault(); ;
            TecnologiaDAO tecnologia = RepositoryDAL1.Read<TecnologiaDAO>(x => x.nombre.Equals(apoyo.Tecnologia)).FirstOrDefault();
            ApoyoDAO apy = RepositoryDAL1.Read<ApoyoDAO>(x => x.id_estudianteApoyado.Equals(apoyo.id_estudianteApoyado) && x.id_estudianteDaApoyo.Equals(apoyo.id_estudianteQueApoya) && x.id_tecnologia.Equals(tecnologia.id)).FirstOrDefault();
            //si no existe el apoyo
            if (apy == null) return false; //el apoyo no existe
            else
            {
                if (RepositoryDAL1.Delete(apy))
                {

                    //sumamos la cantidad de apoyos disponibles y actualizamos
                    estudianteApoya.apoyos_disponibles += 1;
                    RepositoryDAL1.Update(estudianteApoya);

                    //se resta la cantidad de apoyos a la tabla de tecnologiasXestudiante
                    Tecnologia_x_EstudianteDAO tecEst = RepositoryDAL1.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(estudianteApoyado.id_usuario) &&
                            x.id_tecnologia.Equals(tecnologia.id)).FirstOrDefault();

                    tecEst.cantidadApoyos -= 1;
                    RepositoryDAL1.Update(tecEst);

                    return true; //borró el apoyo

                }


            }

            //error al crear el apoyo
            return false;
        }

        //===============================================================================================================================================================


        private int calcularAlgoritmoReclutamiento(UsuarioDAO user, int tec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4)
        {
            int puntajeReputacion = user.Estudiante.reputacion;

            return 0;
        }
    }
}