using Microsoft.AspNetCore.Builder;
using OpenTelemetry.Middleware;

namespace OpenTelemetry.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseOpenTelementry(this IApplicationBuilder app)
        {
            app.UseMiddleware<OpenTelemetryMiddleware>();
        }
    }
}
