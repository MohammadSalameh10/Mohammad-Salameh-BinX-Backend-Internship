using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Models;

namespace MyFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> products =
          new List<Product>
          {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 1200
                },
                new Product
                {
                    Id = 2,
                    Name = "Keyboard",
                    Price = 70
                },
                new Product
                {
                    Id = 3,
                    Name = "Mouse",
                    Price = 25
                }
          };

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            Product? product = products.FirstOrDefault(product => product.Id == id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} was not found.");
            }

            return Ok(product);
        }
    }
}
