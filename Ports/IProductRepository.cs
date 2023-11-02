using microservice.Models;

namespace microservice.Ports;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();

    Task<Product?> GetById(int id);

    Task InsertProduct(Product product);

    Task DeleteProduct(int id);

    Task UpdateProduct(Product product);
}