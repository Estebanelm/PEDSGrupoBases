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
        static string SUCCESS = "success";

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
           
                //se pone el apyo como "fijo" para que no salga el botoncito de "+" puesto que yo no me puedo apoyar a mi mismo
                tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "fijo", Nombre = tec.Tecnologia.nombre});
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
                ApoyosDisponibles = estudiante.apoyos_disponibles,
                Foto = estudiante.foto
                
                
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
                Foto= estudiante.foto
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
            List<UsuarioDAO> res = RepositoryDAL1.Read<UsuarioDAO>(x => (nombreEstudiante != null ? (x.nombre.Contains(nombreEstudiante) || x.apellido.Contains(nombreEstudiante)) : true) &&
            (id_universidad > 0 ? x.Estudiante.id_universidad == id_universidad : true) &&
            (id_pais > 0 ? x.Estudiante.id_pais == id_pais : true) &&
            (id_tecnologia > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(id_tecnologia) : true) &&
            (x.activo));

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
                    Tecnologias = tecApoyo,
                    Foto= user.Estudiante.foto
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
            (tec4 > 0 ? x.Estudiante.Tecnologia_x_Estudiante.Select(tec => tec.id_tecnologia).Contains(tec4) : true) &&
            !x.is_admin && x.activo);

            List<Estudiante> resultado = new List<Estudiante>();
          
            foreach (UsuarioDAO user in usuarios)
            {
                //creamos la lista de tecnolgias con sus respectivos apoyos
                List<Estudiante.TecnologiaPerfil> tecApoyo = new List<Estudiante.TecnologiaPerfil>();
                foreach (Tecnologia_x_EstudianteDAO tec in user.Estudiante.Tecnologia_x_Estudiante)
                {
                    //se pone el apyo como "fijo" para que no salga el botoncito de "+" puesto que son resultados de busqueda
                    tecApoyo.Add(new Estudiante.TecnologiaPerfil { Apoyos = tec.cantidadApoyos, MiApoyo = "fijo", Nombre = tec.Tecnologia.nombre });
                }

                
                resultado.Add( new Estudiante
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
                    Tecnologias = tecApoyo,
                    Foto = user.Estudiante.foto,
                    PuntuacionAlgoritmo = CalcularAlgoritmoReclutamiento(user, tec1, w1, tec2, w2, tec3, w3, tec4, w4)
                });
                               
            }
            //ordenar por puntuacion de algoritmo
            resultado = resultado.OrderByDescending(x => x.PuntuacionAlgoritmo).ToList();

            //sacar los 20 que necesitamos
            var res = resultado.Skip(20 * (pag - 1)).Take(20).ToList();
            return res;

        }

        //===============================================================================================================================================================
        public string CrearEstudiante(string password, Estudiante estudiante)
        {
            //primero buscamos a ver si existe ese estudiante
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(estudiante.Id)).FirstOrDefault();
            EstudianteDAO estud = RepositoryDAL1.Read<EstudianteDAO>(x => x.Usuario.id.Equals(estudiante.Id)).FirstOrDefault();
            //si no existe el estudiante
            if (user == null && estud == null)
            {
                PaisDAO pais = RepositoryDAL1.Read<PaisDAO>(x => x.nombre.Equals(estudiante.Pais)).FirstOrDefault();
                UniversidadDAO univ = RepositoryDAL1.Read<UniversidadDAO>(x => x.nombre.Equals(estudiante.Universidad)).FirstOrDefault();


                //fallo al crear el pais y universidad
                if (pais == null || univ == null) return "la universidad o pais dados no existen";


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
                estud = new EstudianteDAO
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
                    if (RepositoryDAL1.Create(estud))
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
                            })) return "Hubo un error al agregar las tecnologias del estudiante";//error al crear la tecnologia x estudiante
                        }
                        return SUCCESS;
                    }
                }

                //fallo al ingresar a la base de datos

            }
            return "Ya existe un estudiante con ese Carne";
        }

        //===============================================================================================================================================================
        public string CrearAdministrador(string password, Administrador administrador)
        {
            //primero buscamos a ver si existe esl administrador
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(administrador.NombreUsuario)).FirstOrDefault();
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
                if (RepositoryDAL1.Create(user)) return SUCCESS; //se creo administrador

                //no se logro crear fallo en BD

            }
            return "Ya existe un Administrador con esa identificacion";//ya existe un administrador asi
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
                    return RepositoryDAL1.Update(estSeguido); //se creo el seguimiento correctamente y se sumo los seguidores al seguido

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
                if (RepositoryDAL1.Update<UsuarioDAO>(user))
                {
                    if (RepositoryDAL1.Update(existente))
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

                return RepositoryDAL1.Update(user);//se actualizo administrador
            }

            //error al entrar en la base de datos
        }
        //===============================================================================================================================================================
        public bool DeleteEstudiante(string estudianteId)
        {
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(estudianteId)).FirstOrDefault();
            if (user == null) return false; //no existe ese estudiante

            user.activo = false;
            return RepositoryDAL1.Update(user); //Se desactivo el usuario

            //fallo al entrar a la base de datos
        }
        //===============================================================================================================================================================
        public bool DeleteAdministrador(string adminId)
        {
            UsuarioDAO user = RepositoryDAL1.Read<UsuarioDAO>(x => x.id.Equals(adminId)).FirstOrDefault();
            if (user == null) return false; //no existe ese usuario

            user.activo = false;
            return RepositoryDAL1.Update(user); //Se desactivo el usuario

            //fallo al entrar a la base de datos
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
                return RepositoryDAL1.Update(estSeguido);//se actualizo y dejo de seguir estudiante

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
                    ApoyoDAO apoyoAAgregar = new ApoyoDAO
                    {
                        fecha = DateTime.Now,
                        id_estudianteApoyado = apoyo.id_estudianteApoyado,
                        id_estudianteDaApoyo = apoyo.id_estudianteQueApoya,
                        id_tecnologia = tecnologia.id
                    };
                    RepositoryDAL1.Create(apoyoAAgregar);

                    //restamos la cantidad de apoyos disponibles y actualizamos
                    estudianteApoya.apoyos_disponibles -= 1;
                    RepositoryDAL1.Update(estudianteApoya);

                    //se suma la cantidad de apoyos a la tabla de tecnologiasXestudiante
                    Tecnologia_x_EstudianteDAO tecEst = RepositoryDAL1.Read<Tecnologia_x_EstudianteDAO>(x => x.id_estudiante.Equals(estudianteApoyado.id_usuario) &&
                            x.id_tecnologia.Equals(tecnologia.id)).FirstOrDefault();

                    tecEst.cantidadApoyos += 1;
                    RepositoryDAL1.Update(tecEst);

                    string nombreCompletoEstudianteQueApoya = estudianteApoya.Usuario.nombre + " " + estudianteApoya.Usuario.apellido;
                    string nombreCompletoEstudianteApoyado = estudianteApoyado.Usuario.nombre + " " + estudianteApoyado.Usuario.apellido;

                    Twitter.TwitterConnection.sendTweet(nombreCompletoEstudianteQueApoya, nombreCompletoEstudianteApoyado, tecnologia.nombre);

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


        private int CalcularAlgoritmoReclutamiento(UsuarioDAO user, int tec1, int w1, int tec2, int w2, int tec3, int w3, int tec4, int w4)
        {
            
            int puntajeParticipacion = 0; //15%
            int puntajeSeguidores = 0;//20%
            int puntajeApoyos = 0;//40%
            int puntajeReputacion = (user.Estudiante.reputacion * 25) / 100; //la reputacion es de 0 a 100 y representa un 25%

            //puntaje de apoyos
            int puntajetec1 = 0;
            int puntajetec2 = 0;
            int puntajetec3 = 0;
            int puntajetec4 = 0;


            //calculo participacion
            int part = user.Estudiante.participacion;
            if(1<part&&part<4) puntajeParticipacion = 2;
            if (3 < part && part < 7) puntajeParticipacion = 5;
            if (6 < part && part < 15) puntajeParticipacion = 10;
            if (14 < part) puntajeParticipacion = 15;

            //calculo seguidores
            int cant = user.Estudiante.numero_seguidores;
            if (cant > 0 && cant < 4) puntajeSeguidores = 3;
            if (cant > 3 && cant < 6) puntajeSeguidores = 5;
            if (cant > 5 && cant < 8) puntajeSeguidores = 7;
            if (cant > 7 && cant < 11) puntajeSeguidores = 10;
            if (cant > 10 && cant < 16) puntajeSeguidores = 15;
            if (cant > 15 ) puntajeSeguidores = 20;

            //calculo de puntaje de apoyos
            if (tec1 > 0 && w1>0)
            {
                int tecApoyo = user.Estudiante.Tecnologia_x_Estudiante.Where(x => x.id_tecnologia == tec1).FirstOrDefault().cantidadApoyos;

                if (tecApoyo == 1) puntajetec1 = 5;
                if (tecApoyo == 2) puntajetec1 = 10;
                if (tecApoyo == 3) puntajetec1 = 15;
                if (tecApoyo == 4) puntajetec1 = 20;
                if (tecApoyo == 5) puntajetec1 = 25;
                if (tecApoyo == 6) puntajetec1 = 35;
                if (tecApoyo == 7) puntajetec1 = 45;
                if (tecApoyo == 8) puntajetec1 = 50;
                if (tecApoyo == 9) puntajetec1 = 60;
                if (tecApoyo == 10) puntajetec1 = 70;
                if (tecApoyo == 11) puntajetec1 = 80;
                if (tecApoyo >= 12) puntajetec1 = 100;

                puntajetec1 = (puntajetec1 * w1) / 100;
            }

            if (tec2 > 0 && w2 > 0)
            {
                int tecApoyo = user.Estudiante.Tecnologia_x_Estudiante.Where(x => x.id_tecnologia == tec2).FirstOrDefault().cantidadApoyos;

                if (tecApoyo == 1) puntajetec2 = 5;
                if (tecApoyo == 2) puntajetec2 = 10;
                if (tecApoyo == 3) puntajetec2 = 15;
                if (tecApoyo == 4) puntajetec2 = 20;
                if (tecApoyo == 5) puntajetec2 = 25;
                if (tecApoyo == 6) puntajetec2 = 35;
                if (tecApoyo == 7) puntajetec2 = 45;
                if (tecApoyo == 8) puntajetec2 = 50;
                if (tecApoyo == 9) puntajetec2 = 60;
                if (tecApoyo == 10) puntajetec2 = 70;
                if (tecApoyo == 11) puntajetec2 = 80;
                if (tecApoyo >= 12) puntajetec2 = 100;

                puntajetec2 = (puntajetec2 * w2) / 100;
            }

            if (tec3 > 0 && w3 > 0)
            {
                int tecApoyo = user.Estudiante.Tecnologia_x_Estudiante.Where(x => x.id_tecnologia == tec3).FirstOrDefault().cantidadApoyos;

                if (tecApoyo == 1) puntajetec3 = 5;
                if (tecApoyo == 2) puntajetec3 = 10;
                if (tecApoyo == 3) puntajetec3 = 15;
                if (tecApoyo == 4) puntajetec3 = 20;
                if (tecApoyo == 5) puntajetec3 = 25;
                if (tecApoyo == 6) puntajetec3 = 35;
                if (tecApoyo == 7) puntajetec3 = 45;
                if (tecApoyo == 8) puntajetec3 = 50;
                if (tecApoyo == 9) puntajetec3 = 60;
                if (tecApoyo == 10) puntajetec3 = 70;
                if (tecApoyo == 11) puntajetec3 = 80;
                if (tecApoyo >= 12) puntajetec3 = 100;

                puntajetec3 = (puntajetec3 * w3) / 100;
            }

            if (tec4 > 0 && w4 > 0)
            {
                int tecApoyo = user.Estudiante.Tecnologia_x_Estudiante.Where(x => x.id_tecnologia == tec4).FirstOrDefault().cantidadApoyos;

                if (tecApoyo == 1) puntajetec4 = 5;
                if (tecApoyo == 2) puntajetec4 = 10;
                if (tecApoyo == 3) puntajetec4 = 15;
                if (tecApoyo == 4) puntajetec4 = 20;
                if (tecApoyo == 5) puntajetec4 = 25;
                if (tecApoyo == 6) puntajetec4 = 35;
                if (tecApoyo == 7) puntajetec4 = 45;
                if (tecApoyo == 8) puntajetec4 = 50;
                if (tecApoyo == 9) puntajetec4 = 60;
                if (tecApoyo == 10) puntajetec4 = 70;
                if (tecApoyo == 11) puntajetec4 = 80;
                if (tecApoyo >= 12) puntajetec4 = 100;

                puntajetec4 = (puntajetec4 * w4) / 100;
            }

            puntajeApoyos = puntajetec1 + puntajetec2 + puntajetec3 + puntajetec4;

            return puntajeSeguidores + puntajeReputacion + puntajeParticipacion + puntajeApoyos;

        }
    }
}