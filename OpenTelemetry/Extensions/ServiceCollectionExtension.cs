using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using OpenTelemetry.Services;

namespace OpenTelemetry.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddOpenTelementryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LoggingSettings>(configuration.GetSection(LoggingSettings.SectionName));
            services.AddTransient<OperationHeaderHandler>();
            services.AddHttpContextAccessor();
            services.ConfigureAll<HttpClientFactoryOptions>( options =>
            {
                options.HttpMessageHandlerBuilderActions.Add(b => b.AdditionalHandlers.Add(b.Services.GetRequiredService<OperationHeaderHandler>()));
            });
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IOpenTelemetryApi ,OpenTelemetryApi>();
            services.AddScoped<TelemetryLogger>();
        }
    }
}
