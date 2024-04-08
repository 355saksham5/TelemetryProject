using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Services;

namespace ExternalApi1.Controllers
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
        private IExternalApi2 api;

        public WeatherForecastController(TelemetryLogger logger, IExternalApi2 api)
        {
            _logger = logger;
            this.api = api;
        }

        [HttpGet("ex1")]
        public async Task<string> Get()
        {
            _logger.Trace("externalApi1");
            var x = await api.Get();
            _logger.Trace(x+" was in ex1");
            return "hi";

        }
    }
}
