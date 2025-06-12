using Microsoft.AspNetCore.Mvc;
using WeatherApp.Domain.Entities;

namespace WeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherAppContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherAppContext weatherAppContext)
        {
            _logger = logger;
            _dbContext = weatherAppContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var data = _dbContext.Forecasts.ToList();
            return Ok(data);
        }
    }
}
