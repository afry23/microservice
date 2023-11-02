using microservice.Adapters.Infrastructure;
using microservice.Models;
using microservice.Ports;
using Microsoft.EntityFrameworkCore;

namespace microservice.Adapters.SQLiteAdapter;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _dbContext;

    public ProductRepository(ProductContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetById(int id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public async Task InsertProduct(Product product)
    {
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        var product = await GetById(id);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UpdateProduct(Product product)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync();
    }
}