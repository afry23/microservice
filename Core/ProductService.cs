using microservice.Models;
using microservice.Ports;

namespace microservice.Core;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Product>> GetAllProducts() => _repository.GetProducts();
    public Task<Product?> GetById(int id) => _repository.GetById(id);
    public Task AddProduct(Product product) => _repository.InsertProduct(product);
    public Task UpdateProduct(Product product) => _repository.UpdateProduct(product);
    public Task DeleteProduct(int id) => _repository.DeleteProduct(id);
}