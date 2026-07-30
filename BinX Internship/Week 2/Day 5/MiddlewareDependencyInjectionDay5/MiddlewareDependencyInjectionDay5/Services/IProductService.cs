using MiddlewareDependencyInjectionDay5.Models;

namespace MiddlewareDependencyInjectionDay5.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();

        Product? GetProductById(int id);
    }
}
