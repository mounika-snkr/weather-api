using Moq;
using Xunit;
using AutoMapper;
using WeatherApp.Domain.Entities;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Services;
using WeatherApp.Application.Interfaces.Locations;

namespace WeatherApp.Application.Tests
{
    public class LocationServiceTests
    {
        private readonly Mock<ILocationRepository> _mockLocationRepository;
        private readonly LocationService _locationService;
        private readonly IMapper _mapper;

        public LocationServiceTests()
        {
            _mockLocationRepository = new Mock<ILocationRepository>();

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            _mapper = mapperConfiguration.CreateMapper();

            _locationService = new LocationService(_mockLocationRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetAllLocationsAsync_ReturnsLocations()
        {
            // Arrange
            var locationsData = new List<Location>()
            {
                new Location() { Id = 1, Latitude = 10.0, Longitude = 20.0 },
                new Location() { Id = 2, Latitude = 20.0, Longitude = 30.0 }
            };
            _mockLocationRepository.Setup(repo => repo.GetAllLocationsAsync()).ReturnsAsync(locationsData);
            var expectedItems = _mapper.Map<IEnumerable<LocationDto>>(locationsData);

            // Act
            var result = await _locationService.GetAllLocationsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetLocationByIdAsync_ReturnsLocation()
        {
            // Arrange
            var location = new Location() { Id = 1, Latitude = 10.0, Longitude = 20.0 };
            _mockLocationRepository.Setup(repo => repo.GetLocationByIdAsync(1)).ReturnsAsync(location);
            var expectedItem = _mapper.Map<LocationDto>(location);

            // Act
            var result = await _locationService.GetLocationByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedItem.Id, result.Id);
        }

        [Fact]
        public async Task AddLocationAsync_CallsRepositoryMethod()
        {
            // Arrange
            var locationData = new Location() { Id = 3, Latitude = 30.0, Longitude = 40.0 };
            var newLocation = _mapper.Map<CreateLocationDto>(locationData);

            // Act
            await _locationService.AddLocationAsync(newLocation);

            // Assert
            _mockLocationRepository.Verify(
                repo => repo.AddLocationAsync(
                    It.Is<Location>(l => l.Latitude == locationData.Latitude && l.Longitude == locationData.Longitude)
                ),
                Times.Once
            );
        }
    }
}
