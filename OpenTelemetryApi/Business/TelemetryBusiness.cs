using OpenTelemetryApi.DataAccess;
using OpenTelemetryApi.Models;

namespace OpenTelemetryApi.Business
{
    public class TelemetryBusiness : ITelemetryBusiness
    {
        private ITelemetryRepository _telemetryRepository;
        public TelemetryBusiness(ITelemetryRepository _telemetryRepository)
        {
            this._telemetryRepository = _telemetryRepository;
        }

        public async Task AddListOfLogAsync(IList<LogModel> logList)
        {
            await _telemetryRepository.AddListOfLogAsync(logList);
        }

        public async Task AddLogAsync(LogModel log)
        {
            await _telemetryRepository.AddLogAsync(log);
        }

        public async Task<IEnumerable<LogModel>> GetAllLogsByApiIdAsync(string apiId)
        {
            return await _telemetryRepository.GetAllLogsByApiIdAsync(apiId);
        }

        public async Task<IEnumerable<LogModel>> GetLogsByOperationIdAsync(Guid operationId)
        {
            return await _telemetryRepository.GetLogsByOperationIdAsync(operationId);
        }
    }
}
