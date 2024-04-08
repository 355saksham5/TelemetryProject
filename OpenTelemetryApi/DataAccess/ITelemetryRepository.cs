using OpenTelemetryApi.Models;

namespace OpenTelemetryApi.DataAccess
{
    public interface ITelemetryRepository
    {
        Task<IEnumerable<LogModel>> GetAllLogsByApiIdAsync(string apiId);
        Task<IEnumerable<LogModel>> GetLogsByOperationIdAsync(Guid operationId);
        Task AddLogAsync(LogModel log);
        Task AddListOfLogAsync(IList<LogModel> logList);
    }
}
