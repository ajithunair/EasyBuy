using EasyBuy.Model;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy.ProductService.Data;

public class ProductDbContext: DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<ProductModel> Products { get; set; }
}
