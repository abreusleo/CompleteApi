using RabbitMQ.Client;
using System.Text;
namespace CompleteApi.Services;

public interface IMessageService
{ 
    bool Enqueue(string message);
}

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly IModel _channel;
    private const string Context = "MessageService";

    public MessageService(ILogger<MessageService> logger, ConfigService config)
    {
        _logger = logger;
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
        
        _channel.QueueDeclare(queue: "FirstQueue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);
        _logger.LogInformation("{Context}:Constructor - MessageService is initialized.", Context);
}

    public bool Enqueue(string message)
    {
        try
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange:"", routingKey:"FirstQueue",basicProperties:null,body:body);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Context}:Enqueue - Something went wrong while Enqueuing message.", Context);
            return false;
        }
    }
}