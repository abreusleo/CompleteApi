using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CompleteApi.Services.Background;

public class MessageConsumer : BackgroundService
{
    private readonly ILogger<MessageConsumer> _logger;
    private readonly IModel _channel;
    private const string Context = "MessageConsumer";

    public MessageConsumer(ILogger<MessageConsumer> logger, ConfigService config)
    {
        _logger = logger;

        ConnectionFactory factory = new()
        {
            HostName = config.RabbitMqConfig.HostName, 
            Port = config.RabbitMqConfig.Port,
            UserName = config.RabbitMqConfig.Credentials,
            Password = config.RabbitMqConfig.Credentials
        };

        IConnection connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "Api2_Message_Api1", durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Context}:ExecuteAsync - Trying to consume new message", Context);
        Consume();
        
        return Task.CompletedTask;
    }
    private void Consume()
    {
        EventingBasicConsumer consumer = new(_channel);
        consumer.Received += (sender, args) =>
        {
            string message = Encoding.UTF8.GetString(args.Body.ToArray());
            _logger.LogInformation("{Context}:ExecuteAsync - New message: {Message}", Context, message);
        };
        _channel.BasicConsume(queue: "Api2_Message_Api1",
            autoAck: true,
            consumer: consumer);
    }
}