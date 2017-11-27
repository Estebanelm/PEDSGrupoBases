using DigiTutorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi;
using Tweetinvi.Models;

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
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
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

            // Generate credentials that we want to use
            var creds = new TwitterCredentials("G7aCvHb5Hufk3YcjjHKawNub0", "oKiHAA76OmcNeaGKtR22pp5TrObWUXNLI49qj7RUdgT2Cv3rET", "931973066627993600-Ke3cXcyJuq5FoWh7R8djNbdfesXtxYB", "VO0fswBj43tDZ2AFwooRB6hEC3SSSaHj1bARmukPWpfZF"); ;

            // Use the ExecuteOperationWithCredentials method to invoke an operation with a specific set of credentials
            var tweet = Auth.ExecuteOperationWithCredentials(creds, () =>
            {
                return Tweet.PublishTweet("Primer tweet de DigiTutorBases");
            });

            return Ok();
        }
    }
}
