using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Locations;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Gets all locations.
        /// </summary>
        /// <returns>A list of all locations.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
            return Ok(await _locationService.GetAllLocationsAsync());
        }

        /// <summary>
        /// Gets a location by its identifier.
        /// </summary>
        /// <param name="id">The location identifier.</param>
        /// <returns>The location with the specified identifier, or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocation([FromRoute] int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            return location == null ? NotFound() : Ok(location);
        }

        /// <summary>
        /// Adds a new location.
        /// </summary>
        /// <param name="locationDto">The location data transfer object.</param>
        /// <returns>The created location.</returns>
        [HttpPost]
        public async Task<ActionResult<LocationDto>> AddLocation([FromBody] CreateLocationDto locationDto)
        {
            int newId = await _locationService.AddLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocation), new { id = newId }, new LocationCreatedResponse { Id = newId });
        }
    }
}
