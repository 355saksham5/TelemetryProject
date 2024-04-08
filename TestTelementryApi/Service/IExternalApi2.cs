using Refit;

namespace TestTelementryApi.Service
{
    public interface IExternalApi2
    {
        [Get("/WeatherForecast/ex2")]
        Task<string> Get();
    }
}
