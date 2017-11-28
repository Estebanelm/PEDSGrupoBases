using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigiTutorService.DataAccessLayer;
using DigiTutorService.Models;
using System.Linq;

namespace DigiTutorTest
{
    [TestClass]
    public class FachadaUsuariosDalTests
    {
        [TestMethod]
        public void TestCrearEstudiante()
        {
            FachadaUsuariosDAL testObject = new FachadaUsuariosDAL();
            string password = "123asdlk";
            Estudiante estudiante = new Estudiante
            {
                Nombre = "Esteban",
                Apellido = "Calvo",
                Universidad = "ITCR",
                Pais = "Costa Rica",
                Tecnologias = new Estudiante.TecnologiaPerfil[]
                    {
                        new Estudiante.TecnologiaPerfil{Nombre = "Calculo", Apoyos = 0, MiApoyo = "nunca"},
                        new Estudiante.TecnologiaPerfil{Nombre = "Calculo", Apoyos = 0, MiApoyo = "nunca"}
                    }.ToList(),
                Telefono = "8888-8888",
                Correo = "estebancalvo@hotmail.com",
                FechaInscripcion = DateTime.Now,
                Descripcion = "muchacho trabajador"
            };
            bool expected = true;
            bool actual = testObject.CrearEstudiante(password, estudiante);
            Assert.AreEqual(expected, actual);
        }
    }
}
