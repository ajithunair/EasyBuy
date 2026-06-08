using Confluent.Kafka;

namespace EasyBuy.OrderService.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);
}

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = "order-group"
        };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }
    public Task ProduceAsync(string topic, Message<string, string> message)
    {
        _producer.ProduceAsync(topic, message);
        return Task.CompletedTask;
    }
}