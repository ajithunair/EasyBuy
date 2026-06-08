using EasyBuy.Model;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy.ProductService.Data;

public static class ProductSeedData
{
    private static readonly DateTime SeededAt = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private static readonly ProductModel[] SeedProducts =
    [
        CreateProduct(1, "Shirt", "Classic cotton shirt", 25, 40),
        CreateProduct(2, "Pant", "Regular fit pants", 75, 100),
        CreateProduct(3, "T-shirt", "Soft crew neck t-shirt", 30, 80),
        CreateProduct(4, "Jacket", "Lightweight casual jacket", 120, 35),
        CreateProduct(5, "Cap", "Adjustable baseball cap", 10, 25),
        CreateProduct(6, "School bag", "Durable school backpack", 100, 90),
        CreateProduct(7, "Shoes", "Everyday running shoes", 30, 80),
        CreateProduct(8, "Socks", "Pack of cotton socks", 8, 150),
        CreateProduct(9, "Belt", "Leather formal belt", 18, 60),
        CreateProduct(10, "Wallet", "Slim leather wallet", 22, 45),
        CreateProduct(11, "Watch", "Analog wrist watch", 85, 30),
        CreateProduct(12, "Sunglasses", "UV protection sunglasses", 40, 55),
        CreateProduct(13, "Hoodie", "Fleece pullover hoodie", 65, 70),
        CreateProduct(14, "Shorts", "Casual cotton shorts", 28, 85),
        CreateProduct(15, "Kurta", "Traditional cotton kurta", 55, 40),
        CreateProduct(16, "Dress", "Printed summer dress", 95, 25),
        CreateProduct(17, "Skirt", "A-line casual skirt", 45, 30),
        CreateProduct(18, "Sweater", "Knitted winter sweater", 72, 35),
        CreateProduct(19, "Scarf", "Soft wool scarf", 20, 65),
        CreateProduct(20, "Gloves", "Warm winter gloves", 16, 75),
        CreateProduct(21, "Laptop sleeve", "Padded laptop sleeve", 32, 50),
        CreateProduct(22, "Travel bag", "Weekend travel duffel", 140, 20),
        CreateProduct(23, "Water bottle", "Insulated steel bottle", 24, 120),
        CreateProduct(24, "Notebook", "Hardbound ruled notebook", 6, 200),
        CreateProduct(25, "Pen set", "Pack of gel pens", 12, 180)
    ];

    public static async Task EnsureSeededAsync(ProductDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();

        var existingProductIds = await dbContext.Products
            .Select(product => product.Id)
            .ToListAsync();
        var existingProductIdSet = existingProductIds.ToHashSet();

        var missingProducts = SeedProducts
            .Where(product => !existingProductIdSet.Contains(product.Id))
            .ToList();

        if (missingProducts.Count == 0)
        {
            return;
        }

        dbContext.Products.AddRange(missingProducts);
        await dbContext.SaveChangesAsync();
    }

    private static ProductModel CreateProduct(int id, string name, string description, decimal price, int quantity)
    {
        return new ProductModel
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            Quantity = quantity,
            CreatedAt = SeededAt,
            IsAvailable = quantity > 0
        };
    }
}
