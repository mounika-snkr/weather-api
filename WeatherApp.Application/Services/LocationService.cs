using AutoMapper;
using WeatherApp.Domain.Entities;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Locations;

namespace WeatherApp.Application.Services
{
    public class LocationService : ILocationService
    {
        public ILocationRepository _weatherForecastRepository { get; }
        public IMapper _mapper { get; }

        public LocationService(ILocationRepository weatherForecastRepository, IMapper mapper)
        {
            _weatherForecastRepository = weatherForecastRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _weatherForecastRepository.GetAllLocationsAsync();
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto?> GetLocationByIdAsync(int id)
        {
            var location = await _weatherForecastRepository.GetLocationByIdAsync(id);
            return location == null ? null : _mapper.Map<LocationDto>(location);
        }

        public async Task<int> AddLocationAsync(CreateLocationDto locationDto)
        {
            Location location = _mapper.Map<Location>(locationDto);
            return await _weatherForecastRepository.AddLocationAsync(location);
        }
    }
}
