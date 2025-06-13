using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.API.Controllers;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Locations;
using Xunit;

namespace WeatherApp.API.Tests
{
    public class LocationsControllerTests
    {
        private readonly Mock<ILocationService> _mockLocationService;
        private readonly LocationsController _locationController;

        public LocationsControllerTests()
        {
            _mockLocationService = new Mock<ILocationService>();
            _locationController = new LocationsController(_mockLocationService.Object);
        }

        [Fact]
        public async Task GetLocations_ReturnsAllLocations()
        {
            // Arrange
            var locations = new List<LocationDto>
                {
                    new LocationDto { Id = 1, Latitude = 10.0, Longitude = 20.0 },
                    new LocationDto { Id = 2, Latitude = 30.0, Longitude = 40.0 }
                };
            _mockLocationService.Setup(s => s.GetAllLocationsAsync())
                .ReturnsAsync(locations);

            // Act
            var result = await _locationController.GetLocations();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<LocationDto>>>(result);
            var value = Assert.IsType<OkObjectResult>(okResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<LocationDto>>(value.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetLocation_ReturnsLocation_WhenFound()
        {
            // Arrange
            var location = new LocationDto { Id = 1, Latitude = 10.0, Longitude = 20.0 };
            _mockLocationService.Setup(s => s.GetLocationByIdAsync(1))
                .ReturnsAsync(location);

            // Act
            var result = await _locationController.GetLocation(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<LocationDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<LocationDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetLocation_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockLocationService.Setup(s => s.GetLocationByIdAsync(99))
                .ReturnsAsync((LocationDto?)null);

            // Act
            var result = await _locationController.GetLocation(99);

            // Assert
            var actionResult = Assert.IsType<ActionResult<LocationDto>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddLocation_ReturnsCreatedLocation()
        {
            // Arrange
            var createDto = new CreateLocationDto { Latitude = 50.0, Longitude = 60.0 };
            var newId = 3;
            _mockLocationService.Setup(s => s.AddLocationAsync(createDto))
                .ReturnsAsync(newId);
            _mockLocationService.Setup(s => s.GetLocationByIdAsync(newId))
                .ReturnsAsync(new LocationDto { Id = newId, Latitude = 50.0, Longitude = 60.0 });

            // Act
            var result = await _locationController.AddLocation(createDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<LocationDto>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<LocationDto>(createdAtActionResult.Value);
            Assert.Equal(newId, returnValue.Id);
            Assert.Equal(50.0, returnValue.Latitude);
            Assert.Equal(60.0, returnValue.Longitude);
        }
    }
}
