using Microsoft.Extensions.Options;

namespace OpenTelemetry
{
    internal static class Configurations
    {
        internal static bool perLogRequest = false;
        internal static string apiId = "";
        internal static string baseUrlTelementryApi = "https://localhost:7087";
        internal static string postUrl = "/api/Logs";
        internal static string postListUrl = "/api/LogsList";
    }
}
