using System.Text;
using System.Text.Json;
using Calculator.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Calculator.Services.Background;

public class MessageConsumer : BackgroundService
{
    private readonly ILogger<MessageConsumer> _logger;
    private readonly IModel _channel;
    private readonly ConfigService _config;
    private readonly CalculateService _calculateService;
    private readonly IMessageService _messageService;
    private const string Context = "MessageConsumer";

    public MessageConsumer(ILogger<MessageConsumer> logger, ConfigService config, CalculateService calculateService, IMessageService messageService)
    {
        _logger = logger;
        _config = config;
        _calculateService = calculateService;
        _messageService = messageService;

        ConnectionFactory factory = new()
        {
            HostName = config.RabbitMqConfig.HostName, 
            Port = config.RabbitMqConfig.Port,
            UserName = config.RabbitMqConfig.Credentials,
            Password = config.RabbitMqConfig.Credentials
        };

        IConnection connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: _config.QueuesConfig.CalculateRequest, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Context}:ExecuteAsync - Trying to consume new message on calculator", Context);
        Consume();
        
        return Task.CompletedTask;
    }
    private void Consume()
    {
        EventingBasicConsumer consumer = new(_channel);
        consumer.Received += (sender, args) =>
        {
            double response = _calculateService.Calculate(JsonSerializer.Deserialize<CalculatorRequestDto>(Encoding.UTF8.GetString(args.Body.ToArray()))!);
            _messageService.Enqueue(new CalculatorResponseDto{Response = response});
        };
        _channel.BasicConsume(queue: _config.QueuesConfig.CalculateRequest,
            autoAck: true,
            consumer: consumer);
    }
}