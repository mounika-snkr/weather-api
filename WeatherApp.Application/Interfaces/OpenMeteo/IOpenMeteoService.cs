using WeatherApp.Application.DTOs;

namespace WeatherApp.Application.Interfaces.OpenMeteo
{
    public interface IOpenMeteoService
    {
        Task<ForecastDto> GetOpenMeteoWeatherForecast(double latitude, double longitude);
    }
}
