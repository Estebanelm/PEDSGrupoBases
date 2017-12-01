using DigiTutorService.DataAccessLayer;
using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class PaisesController : ApiController
    {
        private FachadaCatalogoDAL catalogo = new FachadaCatalogoDAL();
        List<Pais> paises = new Pais[]{
            new Pais { Nombre = "Argentina" },
            new Pais { Nombre = "Australia" },
            new Pais { Nombre = "Austria" },
            new Pais { Nombre ="Costa Rica" }}.ToList();

        //devuelve lista de paises
        [HttpGet]
        public IHttpActionResult GetPaises()
        {
            /*
            FachadaPublicacionDAL publicaciones = new FachadaPublicacionDAL();
            Contenido pub = new Contenido
            {
                Descripcion = "Unos documentos con las respuestas del examen que me robé",
                Documento = "drive.google.com/documento2.pdf",
                FechaCreacion = new DateTime(2017, 11, 30),
                Id_autor = "201270170",
                Link = "wikipedia.com/calculo",
                Nombre_autor = "Esteban Calvo",
                Titulo = "Respuestas Calculo",
                Tecnologias = new Contenido.Tecnologia[]
                {
                    new Publicacion.Tecnologia{ Nombre = "Algebra"}
                }.ToList()
            };
            publicaciones.CreatePublicacion(pub);
            return Ok();
            */
            return Ok(catalogo.GetPaises());
        }
    }
}
