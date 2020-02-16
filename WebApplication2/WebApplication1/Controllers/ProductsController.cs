using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return products;
        //}

        //public IHttpActionResult GetProduct(int id)
        //{
        //    var product = products.FirstOrDefault((p) => p.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        public IEnumerable<Product> Get()
        {
            return products;
        }

        public IHttpActionResult Get(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        ////POST api/values
        public void Post([FromBody] string value)
        {
        }

        //[HttpPost]
        //public void PostAction([FromUri] string Name)
        //{
        //}

        // PUT api/values/5  
        public void Put(int id, [FromBody] string value)
        {
        }

        //[HttpDelete]
        //[Authorize]
        // DELETE api/values/5  
        public void Delete(int id)
        {
        }
    }
}
