using Confluent.Kafka;
using EasyBuy.Model;
using EasyBuy.ProductService.Data;
using Newtonsoft.Json;

namespace EasyBuy.ProductService.Kafka;

public class KafkaConsumer(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => { _ = ConsumeAsync("order-created", stoppingToken); });
    }

    public async Task ConsumeAsync(string topic, CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "order-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe(topic);

        while (!cancellationToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(cancellationToken);
            var order = JsonConvert.DeserializeObject<OrderModel>(consumeResult.Message.Value); 
            
            using var scope = serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            var product = await dbContext.Products.FindAsync(order.ProductId);
            if (product != null)
            {
                product.Quantity -= order.Quantity;
                await dbContext.SaveChangesAsync();
            }
        }
        consumer.Close();
    }
}