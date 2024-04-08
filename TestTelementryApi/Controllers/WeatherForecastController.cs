using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using OpenTelemetry.Services;
using TestTelementryApi.Service;

namespace TestTelementryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly TelemetryLogger _telemetryLogger;
        private readonly IExternalApi1 api1;
        private readonly IExternalApi2 api2;

        public WeatherForecastController(TelemetryLogger telemetryLogger, IExternalApi2 api2, IExternalApi1 api1)
        {
            this._telemetryLogger = telemetryLogger;
            this.api1 = api1;
            this.api2 = api2;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            _telemetryLogger.Trace(1);
            _telemetryLogger.Information("it is to inform");
            Console.WriteLine(await api1.Get());
            Console.WriteLine(await api2.Get());
            _telemetryLogger.Error(new { error = "custom error"});

            return BadRequest();
        }
    }
}
