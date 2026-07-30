using MiddlewareDependencyInjectionDay5.Models;

namespace MiddlewareDependencyInjectionDay5.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> products =
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
        public List<Product> GetProducts()
        {
            return products;
        }
        public Product? GetProductById(int id)
        {
            return products.FirstOrDefault(products =>  products.Id == id);
        }
    }
}
