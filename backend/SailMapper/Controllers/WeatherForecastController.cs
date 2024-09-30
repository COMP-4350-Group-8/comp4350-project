using Microsoft.AspNetCore.Mvc;
using SailMapper.Data;
using SailMapper.Classes;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SailDBContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SailDBContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            _dbContext.WeatherForecasts.AddRange(forecasts);
            _dbContext.SaveChanges();
            return forecasts;
        }
    }
}
