using microservice.Core;
using microservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace microservice.Adapters.APIAdapter;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var products = await _service.GetAllProducts();
            return new OkObjectResult(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); 
        }
    }
    
    [HttpGet("{id}",  Name = "Get")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var product = await _service.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return new OkObjectResult(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); 
        }
    } 
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product? product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.AddProduct(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    } 
    
    [HttpPut]
    public async Task<IActionResult> Put(int id, [FromBody] Product? product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await _service.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            await _service.UpdateProduct(product);
            return NoContent();
        }
        catch (Exception ex)
        {
            // Log exception (not shown here)
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var product = await _service.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _service.DeleteProduct(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            // Log exception (not shown here)
            return StatusCode(500, "Internal server error");
        }
    }
}