using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Services;

namespace ExternalApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly TelemetryLogger _logger;

        public WeatherForecastController(TelemetryLogger logger)
        {
            _logger = logger;
        }

        [HttpGet("ex2")]
        public string  Get()
        {
            _logger.Trace("ex2 hi");
            return "hello";
        }
    }
}
