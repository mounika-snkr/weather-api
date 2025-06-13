using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.API.Controllers;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Forecasts;
using WeatherApp.Application.Interfaces.OpenMeteo;
using Xunit;

namespace WeatherApp.API.Tests
{
    public class ForecastControllerTests
    {
        private readonly Mock<IForecastService> _mockForecastService;
        private readonly Mock<IOpenMeteoService> _mockOpenMeteoService;
        private readonly ForecastController _controller;

        public ForecastControllerTests()
        {
            _mockForecastService = new Mock<IForecastService>();
            _mockOpenMeteoService = new Mock<IOpenMeteoService>();
            _controller = new ForecastController(_mockForecastService.Object, _mockOpenMeteoService.Object);
        }

        [Fact]
        public async Task GetForecast_ReturnsForecastDto_WhenSuccessful()
        {
            // Arrange
            int locationId = 1;
            double latitude = 10.0;
            double longitude = 20.0;
            var forecast = new ForecastDto { Id = 1, LocationId = locationId, Temperature = 25.5, WindSpeed = 5.2 };

            _mockOpenMeteoService.Setup(s => s.GetOpenMeteoWeatherForecast(latitude, longitude))
                .ReturnsAsync(forecast);
            _mockForecastService.Setup(s => s.AddForecastDataAsync(It.IsAny<ForecastDto>()))
                .ReturnsAsync(forecast.Id);

            // Act
            var result = await _controller.GetForecast(locationId, latitude, longitude);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ForecastDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<ForecastDto>(okResult.Value);
            Assert.Equal(locationId, returnValue.LocationId);
            Assert.Equal(forecast.Temperature, returnValue.Temperature);
            Assert.Equal(forecast.WindSpeed, returnValue.WindSpeed);
        }

        [Fact]
        public async Task GetForecast_ReturnsBadRequest_WhenOpenMeteoReturnsNull()
        {
            // Arrange
            int locationId = 1;
            double latitude = 10.0;
            double longitude = 20.0;

            _mockOpenMeteoService.Setup(s => s.GetOpenMeteoWeatherForecast(latitude, longitude))
                .ReturnsAsync((ForecastDto?)null);

            // Act
            var result = await _controller.GetForecast(locationId, latitude, longitude);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ForecastDto>>(result);
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetLatestForecast_ReturnsForecastDto_WhenExists()
        {
            // Arrange
            int locationId = 2;
            var forecast = new ForecastDto { Id = 2, LocationId = locationId, Temperature = 18.0, WindSpeed = 3.1 };

            _mockForecastService.Setup(s => s.GetLatestForecastByLocationIdAsync(locationId))
                .ReturnsAsync(forecast);

            // Act
            var result = await _controller.GetLatestForecast(locationId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ForecastDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<ForecastDto>(okResult.Value);
            Assert.Equal(locationId, returnValue.LocationId);
        }

        [Fact]
        public async Task GetLatestForecast_ReturnsNotFound_WhenNoForecast()
        {
            // Arrange
            int locationId = 3;
            _mockForecastService.Setup(s => s.GetLatestForecastByLocationIdAsync(locationId))
                .ReturnsAsync((ForecastDto?)null);

            // Act
            var result = await _controller.GetLatestForecast(locationId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ForecastDto>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteForecast_ReturnsNoContent_WhenSuccessful()
        {
            // Arrange
            int forecastId = 4;
            _mockForecastService.Setup(s => s.DeleteForecast(forecastId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteForecast(forecastId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
