using CompleteApi.Interface;

namespace CompleteApi.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger;
    private const string LogContext = "WeatherForecastService";

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }
    
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecast[] GetWeatherForecast()
    {
        _logger.LogTrace("{Context} - GetWeatherForecast method.", LogContext);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

}