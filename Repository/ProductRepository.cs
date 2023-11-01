using microservice.DbContexts;
using microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace microservice.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _dbContext;

    public ProductRepository(ProductContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<Product> GetProducts()
    {
        return _dbContext.Products.ToList();
    }

    public Product GetById(int id)
    {
        return _dbContext.Products.Find(id);
    }

    public void InsertProduct(Product product)
    {
        _dbContext.Add(product);
        Save();
    }

    public void DeleteProduct(int id)
    {
        var product = _dbContext.Products.Find(id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            Save();
        }
    }

    public void UpdateProduct(Product product)
    {
        _dbContext.Entry(product).State = EntityState.Modified;
        Save();
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}