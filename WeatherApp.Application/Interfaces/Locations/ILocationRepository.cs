using WeatherApp.Domain.Entities;

namespace WeatherApp.Application.Interfaces.Locations
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<Location?> GetLocationByIdAsync(int id);
        Task<int> AddLocationAsync(Location location);
    }
}
