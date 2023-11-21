using System.Text;
using System.Text.Json;
using Calculator.Dtos;
using RabbitMQ.Client;

namespace Calculator.Services;

public interface IMessageService
{ 
    bool Enqueue(CalculatorResponseDto calculatorResponse);
}

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly IModel _channel;
    private readonly ConfigService _config;
    private const string Context = "MessageService";

    public MessageService(ILogger<MessageService> logger, ConfigService config)
    {
        _logger = logger;
        _config = config;
        _logger.LogInformation("{Context} - Initializing MessageService...", Context);
        
        ConnectionFactory factory = new ConnectionFactory
        {
            HostName = config.RabbitMqConfig.HostName, 
            Port = config.RabbitMqConfig.Port,
            UserName = config.RabbitMqConfig.Credentials,
            Password = config.RabbitMqConfig.Credentials
        };

        IConnection connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        
        _channel.QueueDeclare(queue: _config.QueuesConfig.CalculateResponse, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
        _logger.LogInformation("{Context}:Constructor - MessageService is initialized.", Context);
}

    public bool Enqueue(CalculatorResponseDto calculatorResponse)
    {
        try
        {
            _logger.LogTrace("{Context}:Enqueue - Publishing response...", Context);
            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(calculatorResponse));
            _channel.BasicPublish(exchange:"", routingKey:_config.QueuesConfig.CalculateResponse,basicProperties:null,body:body);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Context}:Enqueue - Something went wrong while Enqueuing message.", Context);
            return false;
        }
    }
}