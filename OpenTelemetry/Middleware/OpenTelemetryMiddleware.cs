using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using OpenTelemetry.Services;
using System.Net;

namespace OpenTelemetry.Middleware
{
    public class OpenTelemetryMiddleware
    {
        private TelemetryLogger _telemetryLogger { get; set; }
        private IOpenTelemetryApi _openTelemetryApi { get; set; }
        private RequestDelegate _next { get; set; }
        public OpenTelemetryMiddleware(RequestDelegate next, IOpenTelemetryApi openTelemetryApi, IOptions<LoggingSettings> loggingSettings)
        {
            this._next = next;
            this._openTelemetryApi = openTelemetryApi;
            Configurations.apiId = loggingSettings.Value.ApiId;
            Configurations.perLogRequest = loggingSettings.Value.PerLogRequest;
        }

        public async Task Invoke(HttpContext context, TelemetryLogger telemetryLogger)
        {
            _telemetryLogger = telemetryLogger;
            var requestUrl = context.Request.GetDisplayUrl();         
            HttpResponse response = context.Response;
            var originalResponseBody = response.Body;
            using var newResponseBody = new MemoryStream();
            response.Body = newResponseBody;

            _telemetryLogger.Request(requestUrl);

            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _telemetryLogger.Exception(ex);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var statusCode = context.Response.StatusCode;
            var responseBody = "";
            if (IsFailureStatusCode(statusCode))
            {
                newResponseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText =
                    await new StreamReader(response.Body).ReadToEndAsync();
                newResponseBody.Seek(0, SeekOrigin.Begin);
                await newResponseBody.CopyToAsync(originalResponseBody);
                responseBody = responseBodyText;
            }

            _telemetryLogger.Response(new
            {
                statusCode = statusCode,
                responseBody = responseBody
            });

            if (!Configurations.perLogRequest)
            {
                this.PostAllLogs();
            }

        }

        private static bool IsFailureStatusCode(int statusCode)
        {
            return (statusCode >= 400) && (statusCode <= 599);
        }

        private void PostAllLogs()
        {
            _openTelemetryApi.PostLogList(_telemetryLogger.Logs);
        }
        
    }
}
