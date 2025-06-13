using Microsoft.AspNetCore.Mvc;
using WeatherApp.Application.DTOs;
using WeatherApp.Application.Interfaces.Forecasts;
using WeatherApp.Application.Interfaces.OpenMeteo;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastService _forecastService;
        private readonly IOpenMeteoService _openMeteoService;

        public ForecastController(IForecastService forecastService, IOpenMeteoService openMeteoService)
        {
            _forecastService = forecastService;
            _openMeteoService = openMeteoService;
        }

        /// <summary>
        /// Retrieves the weather forecast for the specified location by calling the Open-Meteo API,
        /// assigns the location ID, saves the forecast data, and returns the result.
        /// </summary>
        /// <param name="locationId">The ID of the location.</param>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <returns>The weather forecast data for the specified location.</returns>
        [HttpGet("{locationId}/{latitude}/{longitude}")]
        public async Task<ActionResult<ForecastDto>> GetForecast([FromRoute] int locationId, [FromRoute] double latitude, [FromRoute] double longitude)
        {
            //call open-meteo API to get forecast data
            var forecast = await _openMeteoService.GetOpenMeteoWeatherForecast(latitude, longitude);
            forecast.LocationId = locationId;
            forecast.Id = await _forecastService.AddForecastDataAsync(forecast);
            return forecast == null ? NotFound() : Ok(forecast);
        }



        /// <summary>
        /// Retrieves the latest weather forecast for the specified location ID.
        /// </summary>
        /// <param name="locationId">The ID of the location.</param>
        /// <returns>The latest weather forecast data for the location.</returns>
        [HttpGet]
        public async Task<ActionResult<ForecastDto>> GetLatestForecast(int locationId)
        {
            var forecastData = await _forecastService.GetLatestForecastByLocationIdAsync(locationId);
            return forecastData == null ? NotFound() : Ok(forecastData);
        }

        /// <summary>
        /// Deletes the weather forecast with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the forecast to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteForecast(int id)
        {
            await _forecastService.DeleteForecast(id);
            return NoContent();
        }
    }
}
