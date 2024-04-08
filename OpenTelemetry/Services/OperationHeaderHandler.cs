using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OpenTelemetry.Services
{
    public class OperationHeaderHandler : DelegatingHandler
    {
        private TelemetryLogger _telemetryLogger;
        private IHttpContextAccessor _contextAccessor;
        public OperationHeaderHandler(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _telemetryLogger = _contextAccessor.HttpContext.RequestServices.GetRequiredService<TelemetryLogger>();
            request.Headers.Add("Operation-Id", _telemetryLogger.OperationId.ToString());
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
