namespace Api.Services.Interfaces;

public interface IWeatherForecastService
{
    public IEnumerable<WeatherForecast> GetWeatherForecast();
}