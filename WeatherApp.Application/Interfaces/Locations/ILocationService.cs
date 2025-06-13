using WeatherApp.Application.DTOs;

namespace WeatherApp.Application.Interfaces.Locations
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync();
        Task<LocationDto?> GetLocationByIdAsync(int id);
        Task<int> AddLocationAsync(CreateLocationDto locationDto);
    }
}
