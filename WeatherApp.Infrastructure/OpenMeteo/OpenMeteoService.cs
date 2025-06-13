using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.OpenMeteo;

namespace WeatherApp.Infrastructure.Services.OpenMeteo
{
    public class OpenMeteoService : IOpenMeteoService
    {
        private readonly string? _openMeteoBaseUrl;
        public HttpClient _httpClient { get; }
        public IMapper _mapper { get; }

        public OpenMeteoService(IConfiguration configuration, HttpClient httpClient, IMapper mapper)
        {
            _openMeteoBaseUrl = configuration["OpenMeteo:BaseUrl"];
            _httpClient = httpClient;
            _mapper = mapper;
        }


        public async Task<ForecastDto> GetOpenMeteoWeatherForecast(double latitude, double longitude)
        {
            var url = $"{_openMeteoBaseUrl}?latitude={latitude}&longitude={longitude}&current_weather=true&windspeed_unit=mph";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<WeatherResponse>(content);
                return _mapper.Map<ForecastDto>(weatherData?.current_weather);
            }
            else
            {
                throw new Exception($"Error fetching weather data: {response.ReasonPhrase}");
            }
        }
    }
}
