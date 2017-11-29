using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigiTutorService.DataAccessLayer;
using System.Collections.Generic;
using DigiTutorService.Models;

namespace DigiTutorTest
{
    [TestClass]
    public class FachadaCatalogoDalTests
    {
        [TestMethod]
        public void TestGetTecnologias()
        {
            FachadaCatalogoDAL testFachada = new FachadaCatalogoDAL();
            List<Tecnologia> expected = testFachada.GetTecnologias();

            Assert.AreNotEqual(null, expected);
        }
    }
}
