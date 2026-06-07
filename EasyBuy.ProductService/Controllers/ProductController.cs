using EasyBuy.Model;
using EasyBuy.ProductService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy.ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<ActionResult<List<ProductModel>>> GetProducts()
    {
        var products = await dbContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductModel>> GetProduct(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}