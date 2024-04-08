using OpenTelemetryApi.Data;
using OpenTelemetryApi.Models;

namespace OpenTelemetryApi.DataAccess
{
    public class TelemetryRepository : ITelemetryRepository
    {
        public AppDbContext _context { get; set; }
        public TelemetryRepository(AppDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IEnumerable<LogModel>> GetAllLogsByApiIdAsync(string apiId)
        {
            return _context.Logs.Where(log => log.ApiId == apiId);
        }

        public async Task<IEnumerable<LogModel>> GetLogsByOperationIdAsync(Guid operationId)
        {
            return _context.Logs.Where(log => log.OperationId == operationId);
        }

        public async Task AddLogAsync(LogModel log)
        {
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task AddListOfLogAsync(IList<LogModel> logList)
        {
            await _context.Logs.AddRangeAsync(logList);
            await _context.SaveChangesAsync();
        }
    }

}

