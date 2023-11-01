using System.Transactions;
using microservice.Models;
using microservice.Repository;
using Microsoft.AspNetCore.Mvc;

namespace microservice.Controllers;

[Route("api/{controller}")]
[ApiController]
public class ProductController : Controller
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository productRepository)
    {
        _repository = productRepository;
    }
    // GET
    [HttpGet]
    public IActionResult Index()
    {
        var products = _repository.GetProducts();
        return new OkObjectResult(products);
    }
    
    [HttpGet("{id}",  Name = "Get")]
    public IActionResult Get(int id)
    {
        var product = _repository.GetById(id);
        return new OkObjectResult(product);
    } 
    
    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        using var scope = new TransactionScope();
        _repository.InsertProduct(product);
        scope.Complete();
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    } 
    
    [HttpPut]
    public IActionResult Put([FromBody] Product product)
    {
        if (product != null)
        {
            using var scope = new TransactionScope();
            _repository.UpdateProduct(product);
            scope.Complete();
            return new OkResult();
        }
        return new NoContentResult();
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repository.DeleteProduct(id);
        return new OkResult();
    }
}