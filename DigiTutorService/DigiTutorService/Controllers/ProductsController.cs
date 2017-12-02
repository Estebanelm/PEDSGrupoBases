using DigiTutorService.DataAccessLayer;
using DigiTutorService.LogicLayer;
using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DigiTutorService.Controllers
{
    public class ProductsController : ApiController
    {
        List<Product> products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        }.ToList();

        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
            CatalogoLogic asd = new CatalogoLogic();
            return Ok(asd.GetUniversidades());
            //return products;
        }
        
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IHttpActionResult PostProduct([FromBody] Product model)
        {
            products.Add(model);
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult ModifyProduct([FromBody] Product model)
        {
            var productoAModificar = products.Where(prod => prod.Id.Equals(model.Id)).FirstOrDefault();
            if (productoAModificar != null)
            {
                products.Remove(productoAModificar);
                products.Add(model);
            }
            return Ok();
        }
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            return resp;
        }
    }
}
