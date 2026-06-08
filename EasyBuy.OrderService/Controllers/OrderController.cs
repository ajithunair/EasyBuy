using Confluent.Kafka;
using EasyBuy.Model;
using EasyBuy.OrderService.Data;
using EasyBuy.OrderService.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EasyBuy.OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(OrderDbContext dbContext, IKafkaProducer kafkaProducer) : Controller
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
        
        //Produce Kafka message
        await kafkaProducer.ProduceAsync("order-created", new Message<string, string>
        {
            Key = order.Id.ToString(),
            Value = JsonConvert.SerializeObject(order)
        });
        return Ok(order);
    }
}