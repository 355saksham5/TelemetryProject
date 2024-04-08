using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenTelemetryApi.Business;
using OpenTelemetryApi.Models;

namespace OpenTelemetryApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LogsController : ControllerBase
    {
        private ITelemetryBusiness _telemetryBusiness;


        public LogsController(ITelemetryBusiness _telemetryBusiness)
        {
            this._telemetryBusiness = _telemetryBusiness;
        }

        [HttpGet]
        [Route("AllLogs")]
        public async Task<IEnumerable<LogModel>> GetAllLogsByApiId([FromQuery] string apiId)
        {
            return await _telemetryBusiness.GetAllLogsByApiIdAsync(apiId);
        }

        [HttpGet]
        [Route("Logs")]
        public async Task<IEnumerable<LogModel>> GetLogsByOperationId([FromQuery] Guid operationId)
        {
            return await _telemetryBusiness.GetLogsByOperationIdAsync(operationId);
        }

        [HttpPost]
        [Route("Logs")]
        public async Task AddLogs([FromBody] Object log)
        {
            await _telemetryBusiness.AddLogAsync(JsonConvert.DeserializeObject<LogModel>(log.ToString()));
        }
        [HttpPost]
        [Route("LogsList")]
        public async Task AddListOfLogs([FromBody] Object log)
        {
            await _telemetryBusiness.AddListOfLogAsync(JsonConvert.DeserializeObject<IList<LogModel>>(log.ToString()));
        }
    }
}
