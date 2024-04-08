using OpenTelemetry.Model;

namespace OpenTelemetry.Services
{
    public interface IOpenTelemetryApi
    {
       internal void PostLog(LogModel log);
       internal void PostLogList(IEnumerable<LogModel> logList);
    }
}
