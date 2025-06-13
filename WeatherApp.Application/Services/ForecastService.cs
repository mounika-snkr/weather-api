using AutoMapper;
using WeatherApp.Domain.Entities;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Forecasts;

namespace WeatherApp.Application.Services
{
    public class ForecastService : IForecastService
    {
        public IForecastRepository _forecastRepository { get; }
        public IMapper _mapper { get; }

        public ForecastService(IForecastRepository forecastRepository, IMapper mapper)
        {
            _forecastRepository = forecastRepository;
            _mapper = mapper;
        }

        public async Task<ForecastDto?> GetLatestForecastByLocationIdAsync(int locationId)
        {
            Forecast? forecast = await _forecastRepository.GetLatestForecastByLocationIdAsync(locationId);
            return forecast == null ? null : _mapper.Map<ForecastDto>(forecast);
        }

        public async Task<int> AddForecastDataAsync(ForecastDto forecastDto)
        {
            Forecast forecast = _mapper.Map<Forecast>(forecastDto);
            forecast.Humidity = 0.0;
            forecast.FetchedAt = DateTime.Now;
            forecast.ForecastTime = DateTime.Now;

            await _forecastRepository.AddForecastDataAsync(forecast);
            return forecast.Id;
        }

        public async Task DeleteForecast(int forecastId)
        {
            var forecast = await _forecastRepository.GetForecastByIdAsync(forecastId);
            if (forecast == null) throw new KeyNotFoundException($"Forecast with id {forecastId} not found");
            await _forecastRepository.DeleteForecastAsync(forecast);
        }
    }
}
