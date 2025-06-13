using Microsoft.EntityFrameworkCore;
using WeatherApp.Application.Interfaces.Locations;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        private readonly WeatherAppContext _dbContext;

        public LocationRepository(WeatherAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _dbContext.Locations.OrderBy(l => l.Id).ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            return await _dbContext.Locations.FindAsync(id);
        }

        public async Task<int> AddLocationAsync(Location location)
        {
            _dbContext.Locations.Add(location);
            await _dbContext.SaveChangesAsync();
            return location.Id;
        }
    }
}
