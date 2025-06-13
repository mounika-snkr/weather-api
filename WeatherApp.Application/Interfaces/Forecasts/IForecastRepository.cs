using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Interfaces.Forecasts
{
    public interface IForecastRepository
    {
        Task<Forecast?> GetForecastByIdAsync(int id);
        Task<Forecast?> GetLatestForecastByLocationIdAsync(int locationId);
        Task AddForecastDataAsync(Forecast forecast);
        Task DeleteForecastAsync(Forecast forecast);
    }
}
