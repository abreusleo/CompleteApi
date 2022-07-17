namespace CompleteApi.Services;

public class ConfigService
{
    public readonly RabbitMqConfig RabbitMqConfig;

    public ConfigService(IConfiguration config)
    {
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