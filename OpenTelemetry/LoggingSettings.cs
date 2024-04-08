using OpenTelemetry.Model;

namespace OpenTelemetry
{
    public class LoggingSettings
    {
        internal const string SectionName = "ApplicationInSight";
        public Dictionary<string, LogType> LogLevel { get; set; }
        public string ApiId { get; set; }
        public bool PerLogRequest { get; set; } = true;
        public bool LogRequest { get; set; } = true;
        public bool LogResponse { get; set; } = true;

    }
}
