using Refit;

namespace ExternalApi1
{
    public interface IExternalApi2
    {
        [Get("/WeatherForecast/ex2")]
        Task<string> Get();
    }
}
