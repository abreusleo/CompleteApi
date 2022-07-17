using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Api.Dtos;

namespace Api.Services;

public interface IMessageService
{ 
    bool Enqueue(CalculatorRequestDto calculatorRequest);
}

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly IModel _channel;
    private readonly ConfigService _configService;
    private const string Context = "MessageService";

    public MessageService(ILogger<MessageService> logger, ConfigService config, ConfigService configService)
    {
        _logger = logger;
        _configService = configService;
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
        
        _channel.QueueDeclare(queue: _configService.QueuesConfig.CalculateRequest, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
        _logger.LogInformation("{Context}:Constructor - MessageService is initialized.", Context);
}

    public bool Enqueue(CalculatorRequestDto calculatorRequest)
    {
        try
        {
            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(calculatorRequest));
            _channel.BasicPublish(exchange:"", routingKey:_configService.QueuesConfig.CalculateRequest,basicProperties:null,body:body);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Context}:Enqueue - Something went wrong while Enqueuing message.", Context);
            return false;
        }
    }
}