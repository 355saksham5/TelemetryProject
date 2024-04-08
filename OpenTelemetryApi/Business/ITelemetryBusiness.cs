using OpenTelemetryApi.Models;

namespace OpenTelemetryApi.Business
{
    public interface ITelemetryBusiness
    {
        Task<IEnumerable<LogModel>> GetAllLogsByApiIdAsync(string apiId);
        Task<IEnumerable<LogModel>> GetLogsByOperationIdAsync(Guid operationId);
        Task AddLogAsync(LogModel log);
        Task AddListOfLogAsync(IList<LogModel> logList);
    }
}
