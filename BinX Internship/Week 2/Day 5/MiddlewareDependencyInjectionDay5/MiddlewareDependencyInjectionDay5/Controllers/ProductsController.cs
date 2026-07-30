using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddlewareDependencyInjectionDay5.Models;
using MiddlewareDependencyInjectionDay5.Services;

namespace MiddlewareDependencyInjectionDay5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            List<Product> products = _productService.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            Product? product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} was not found.");
            }

            return Ok(product);
        }

    }
}
