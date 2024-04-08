using Refit;

namespace TestTelementryApi.Service
{
    public interface IExternalApi1
    {
        [Get("/WeatherForecast/ex1")]
        Task<string> Get();
    }
}
