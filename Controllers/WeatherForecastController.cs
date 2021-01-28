using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloDotnet5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherClient WeatherClient;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            WeatherClient weatherClient,
            ILogger<WeatherForecastController> logger)
        {
            WeatherClient = weatherClient;
            _logger = logger;
        }

        [HttpGet]
        [Route("{city}")]
        public async Task<WeatherForecast> Get(string city)
        {
            var forcast = await WeatherClient.GetCurrentWeatherAsync(city);
            return new WeatherForecast
            {
                Summary = forcast.weather.FirstOrDefault()?.description,
                TemperatureC = (int)forcast.main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forcast.dt).Date
            };
        }
    }
}
