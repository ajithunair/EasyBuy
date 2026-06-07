using EasyBuy.Model;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy.ProductService.Data;

public class ProductDbContext: DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel{Id = 1, CreatedAt = DateTime.UtcNow,Description =string.Empty,IsAvailable =  true,Name = "Shirt",Price = 25, Quantity = 40});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel{Id = 2, CreatedAt = DateTime.UtcNow,Description =string.Empty,IsAvailable =  true,Name = "Pant",Price = 75, Quantity = 100});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel{Id = 3, CreatedAt = DateTime.UtcNow,Description =string.Empty,IsAvailable =  true,Name = "T-shirt",Price = 30, Quantity = 80});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel{Id = 5, CreatedAt = DateTime.UtcNow,Description =string.Empty,IsAvailable =  true,Name = "Cap",Price = 10, Quantity = 25});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel{Id = 6, CreatedAt = DateTime.UtcNow,Description =string.Empty,IsAvailable =  true,Name = "School bag",Price = 100, Quantity = 90});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel{Id = 7, CreatedAt = DateTime.UtcNow,Description =string.Empty,IsAvailable =  true,Name = "Shoes",Price = 30, Quantity = 80});

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<ProductModel> Products { get; set; }
}