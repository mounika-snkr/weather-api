using AutoMapper;
using Moq;
using System;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Forecasts;
using WeatherApp.Application.Interfaces.Locations;
using WeatherApp.Application.Services;
using WeatherApp.Domain.Entities;
using Xunit;

namespace WeatherApp.Application.Tests
{
    public class ForecastServiceTests
    {
        private readonly Mock<IForecastRepository> _mockForecastRepository;
        private readonly ForecastService _forecastService;
        private readonly IMapper _mapper;

        public ForecastServiceTests()
        {
            _mockForecastRepository = new Mock<IForecastRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();

            _forecastService = new ForecastService(_mockForecastRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetLatestForecastByLocationIdAsync_ReturnsForecast()
        {
            // Arrange
            var forecast = new Forecast() { Id = 1, LocationId = 1, Temperature = 20.0, WindSpeed = 30.0, WeatherCode = 2, FetchedAt = DateTime.Now };
            _mockForecastRepository.Setup(repo => repo.GetLatestForecastByLocationIdAsync(1)).ReturnsAsync(forecast);
            var expectedItem = _mapper.Map<ForecastDto>(forecast);

            // Act
            var result = await _forecastService.GetLatestForecastByLocationIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedItem.Id, result.Id);
        }

        [Fact]
        public async Task AddForecastDataAsync_CallsRepositoryMethod()
        {
            // Arrange
            var forecastData = new Forecast() { Id = 2, LocationId = 2, Temperature = 20.0, WindSpeed = 30.0, WeatherCode = 2, FetchedAt = DateTime.Now };
            var newForecast = _mapper.Map<ForecastDto>(forecastData);

            // Act
            await _forecastService.AddForecastDataAsync(newForecast);

            // Assert
            _mockForecastRepository.Verify(
                repo => repo.AddForecastDataAsync(
                    It.Is<Forecast>(l => l.LocationId == forecastData.LocationId 
                    && l.Temperature == forecastData.Temperature 
                    && l.WindSpeed == forecastData.WindSpeed
                    && l.WeatherCode == forecastData.WeatherCode
                    && l.FetchedAt == forecastData.FetchedAt)
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task DeleteForecast_CallsRepositoryMethod()
        {
            //Arrange
            var forecastData = new Forecast() { Id = 2, LocationId = 2, Temperature = 20.0, WindSpeed = 30.0, WeatherCode = 2, FetchedAt = DateTime.Now };

            //Act
            await _forecastService.DeleteForecast(forecastData.Id);

            //Assert
            _mockForecastRepository.Verify(repo => repo.DeleteForecastAsync(forecastData), Times.Once);
        }
    }
}
