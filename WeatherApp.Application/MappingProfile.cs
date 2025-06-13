using AutoMapper;
using WeatherApp.Application.DTOs;
using WeatherApp.Domain.Entities;

namespace WeatherApp.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<CreateLocationDto, Location>().ReverseMap();
            CreateMap<Forecast, ForecastDto>().ReverseMap();
            CreateMap<CurrentWeatherDto, ForecastDto>();
        }
    }
}
