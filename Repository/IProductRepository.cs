using microservice.Models;

namespace microservice.Repository;

public interface IProductRepository
{
    IEnumerable<Product> GetProducts();

    Product GetById(int id);

    void InsertProduct(Product product);

    void DeleteProduct(int id);

    void UpdateProduct(Product product);

    void Save();
}