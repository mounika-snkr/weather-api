using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.Entities;
using Xunit;

namespace WeatherApp.Infrastructure.Tests
{
    public class LocationRepositoryTests
    {
        private readonly WeatherAppContext _context;
        private readonly LocationRepository _repository;

        public LocationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<WeatherAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new WeatherAppContext(options);
            _repository = new LocationRepository(_context);
        }

        [Fact]
        public async Task AddLocationAsync_ShouldAddLocation()
        {
            var location = new Location { Latitude = 10.0, Longitude = 20.0 };
            var id = await _repository.AddLocationAsync(location);

            var added = await _context.Locations.FindAsync(id);
            Assert.NotNull(added);
            Assert.Equal(10.0, added.Latitude);
            Assert.Equal(20.0, added.Longitude);
        }

        [Fact]
        public async Task GetAllLocationsAsync_ShouldReturnAllLocations()
        {
            _context.Locations.Add(new Location { Latitude = 1, Longitude = 2 });
            _context.Locations.Add(new Location { Latitude = 3, Longitude = 4 });
            await _context.SaveChangesAsync();

            var locations = await _repository.GetAllLocationsAsync();
            Assert.Equal(2, locations.Count());
        }

        [Fact]
        public async Task GetLocationByIdAsync_ShouldReturnCorrectLocation()
        {
            var location = new Location { Latitude = 5, Longitude = 6 };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            var result = await _repository.GetLocationByIdAsync(location.Id);
            Assert.NotNull(result);
            Assert.Equal(5, result.Latitude);
            Assert.Equal(6, result.Longitude);
        }

        [Fact]
        public async Task GetLocationByIdAsync_ShouldReturnNullIfNotFound()
        {
            var result = await _repository.GetLocationByIdAsync(999);
            Assert.Null(result);
        }
    }
}
