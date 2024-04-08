using OpenTelemetry.Model;
using System.Net.Http.Json;

namespace OpenTelemetry.Services
{
    public class OpenTelemetryApi : IOpenTelemetryApi
    {
        private HttpClient _httpClient;
        private string _requestUrlPostLog;
        private string _requestUrlPostLogList;
        public OpenTelemetryApi(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._requestUrlPostLog = Configurations.baseUrlTelementryApi+Configurations.postUrl;
            this._requestUrlPostLogList = Configurations.baseUrlTelementryApi + Configurations.postListUrl;
        }
        public void PostLog(LogModel log)
        { 
            _httpClient.PostAsJsonAsync(_requestUrlPostLog, log);
        }
        public void PostLogList(IEnumerable<LogModel> logList)
        {
            _httpClient.PostAsJsonAsync(_requestUrlPostLogList, logList);
        }
    }
}
