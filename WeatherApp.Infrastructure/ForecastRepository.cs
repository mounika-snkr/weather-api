using Microsoft.EntityFrameworkCore;
using WeatherApp.Application.Interfaces.Forecasts;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Infrastructure
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly WeatherAppContext _dbContext;

        public ForecastRepository(WeatherAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Forecast?> GetForecastByIdAsync(int id)
        {
            return await _dbContext.Forecasts.FindAsync(id);
        }

        public async Task<Forecast?> GetLatestForecastByLocationIdAsync(int locationId)
        {
            return await _dbContext.Forecasts.Where(f => f.LocationId == locationId).OrderByDescending(f => f.ForecastTime).FirstOrDefaultAsync();
        }

        public async Task AddForecastDataAsync(Forecast forecast)
        {
            _dbContext.Forecasts.Add(forecast);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteForecastAsync(Forecast forecast)
        {
            _dbContext.Forecasts.Remove(forecast);
            await _dbContext.SaveChangesAsync();
        }
    }
}
