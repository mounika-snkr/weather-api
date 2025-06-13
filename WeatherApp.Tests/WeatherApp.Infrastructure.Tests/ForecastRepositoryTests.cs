using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.Entities;
using Xunit;

namespace WeatherApp.Infrastructure.Tests
{
    public class ForecastRepositoryTests
    {
        private readonly WeatherAppContext _context;
        private readonly ForecastRepository _repository;

        public ForecastRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<WeatherAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new WeatherAppContext(options);
            _repository = new ForecastRepository(_context);
        }

        [Fact]
        public async Task AddForecastDataAsync_AddsForecastToDatabase()
        {
            var location = new Location { Latitude = 10.0, Longitude = 20.0 };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var forecast = new Forecast
            {
                LocationId = location.Id,
                Temperature = 25.5,
                WindSpeed = 5.2,
                WeatherCode = 100,
                FetchedAt = DateTime.UtcNow,
                Location = location
            };

            await _repository.AddForecastDataAsync(forecast);

            var dbForecast = await _context.Forecasts.FirstOrDefaultAsync(f => f.Id == forecast.Id);
            Assert.NotNull(dbForecast);
            Assert.Equal(25.5, dbForecast.Temperature);
        }

        [Fact]
        public async Task GetForecastByIdAsync_ReturnsCorrectForecast()
        {
            var location = new Location { Latitude = 11.0, Longitude = 21.0 };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var forecast = new Forecast
            {
                LocationId = location.Id,
                Temperature = 15.0,
                WindSpeed = 3.0,
                WeatherCode = 200,
                FetchedAt = DateTime.UtcNow,
                Location = location
            };
            _context.Forecasts.Add(forecast);
            await _context.SaveChangesAsync();

            var result = await _repository.GetForecastByIdAsync(forecast.Id);
            Assert.NotNull(result);
            Assert.Equal(15.0, result.Temperature);
        }

        [Fact]
        public async Task GetLatestForecastByLocationIdAsync_ReturnsLatestForecast()
        {
            var location = new Location { Latitude = 12.0, Longitude = 22.0 };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var oldForecast = new Forecast
            {
                LocationId = location.Id,
                Temperature = 10.0,
                WindSpeed = 2.0,
                WeatherCode = 300,
                FetchedAt = DateTime.UtcNow.AddHours(-2),
                Location = location
            };
            var newForecast = new Forecast
            {
                LocationId = location.Id,
                Temperature = 20.0,
                WindSpeed = 4.0,
                WeatherCode = 400,
                FetchedAt = DateTime.UtcNow,
                Location = location
            };
            _context.Forecasts.AddRange(oldForecast, newForecast);
            await _context.SaveChangesAsync();

            var result = await _repository.GetLatestForecastByLocationIdAsync(location.Id);
            Assert.NotNull(result);
            Assert.Equal(20.0, result.Temperature);
        }

        [Fact]
        public async Task DeleteForecastAsync_RemovesForecastFromDatabase()
        {
            var location = new Location { Latitude = 13.0, Longitude = 23.0 };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var forecast = new Forecast
            {
                LocationId = location.Id,
                Temperature = 30.0,
                WindSpeed = 6.0,
                WeatherCode = 500,
                FetchedAt = DateTime.UtcNow,
                Location = location
            };
            _context.Forecasts.Add(forecast);
            await _context.SaveChangesAsync();

            await _repository.DeleteForecastAsync(forecast);

            var dbForecast = await _context.Forecasts.FirstOrDefaultAsync(f => f.Id == forecast.Id);
            Assert.Null(dbForecast);
        }
    }
}
