using WeatherApp.Application.DTOs;

namespace WeatherApp.Application.Interfaces.Forecasts
{
    public interface IForecastService
    {
        Task<ForecastDto?> GetLatestForecastByLocationIdAsync(int locationId);
        Task<int> AddForecastDataAsync(ForecastDto forecastDto);
        Task DeleteForecast(int id);
    }
}
