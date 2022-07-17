namespace Api.Services;

public class ConfigService
{
    public readonly RabbitMqConfig RabbitMqConfig;
    public readonly QueuesConfig QueuesConfig;

    public ConfigService(IConfiguration config)
    {
        QueuesConfig = new QueuesConfig(config.GetSection("QueuesConfig"));
        RabbitMqConfig = new RabbitMqConfig(config.GetSection("RabbitMqConfig"));
    }
}
public class RabbitMqConfig
{
    public int Port { get; }
    public string Credentials { get; }
    public string HostName { get; }

    public RabbitMqConfig(IConfiguration config)
    {
        Port = config.GetValue<int>("Port");
        Credentials = config.GetValue<string>("Credentials");
        HostName = config.GetValue<string>("HostName");
    }
}

public class QueuesConfig
{
    public string CalculateRequest { get; }
    public string CalculateResponse { get; } 

    public QueuesConfig(IConfiguration config)
    {
        CalculateRequest = config.GetValue<string>("Api_CalculateRequest_Calculator");
        CalculateResponse = config.GetValue<string>("Calculator_CalculateResponse_Api");
    }
}