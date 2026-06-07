using EasyBuy.Model;
using EasyBuy.OrderService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy.OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(OrderDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<ActionResult<List<OrderModel>>> GetOrders()
    {
        var orders = await dbContext.Orders.ToListAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<OrderModel>> CreateOrder(OrderModel order)
    {
        order.OrderDate = DateTime.UtcNow;
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
        return Ok(order);
    }
}