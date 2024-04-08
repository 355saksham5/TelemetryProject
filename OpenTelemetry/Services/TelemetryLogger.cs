using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenTelemetry.Model;

namespace OpenTelemetry.Services
{
    public class TelemetryLogger
    {
        private IOpenTelemetryApi OpenTelemetryApi { get; set; }
        internal Guid OperationId { get; set; }
        private IHttpContextAccessor Context { get; set; }
        private LoggingSettings LoggingSettings { get; set; }

        internal List<LogModel> Logs;

        public TelemetryLogger(IOpenTelemetryApi openTelemetryApi, IOptions<LoggingSettings> loggingSettings, IHttpContextAccessor Context)
        {
            this.OpenTelemetryApi = openTelemetryApi;
            this.LoggingSettings = loggingSettings.Value;
            this.Logs = new List<LogModel>();
            this.Context = Context;
            this.OperationId = this.GetOperationId();
        }

        public void Request(Object Content)
        {
            if (ShouldLog(LogType.Request))
            {
                var log = new LogModel(Content, LogType.Request, OperationId);
                this.AddLogs(log);
            };
        }
        public void Response(Object Content)
        {
            if (ShouldLog(LogType.Response))
            {
                var log = new LogModel(Content, LogType.Response, OperationId);
                this.AddLogs(log);
            }
        }
        public void Information(Object Content)
        {
            if(ShouldLog(LogType.Information))
            {
                var log = new LogModel(Content, LogType.Information, OperationId);
                this.AddLogs(log);
            }
        }
        public void Error(Object Content)
        {
            if (ShouldLog(LogType.Error))
            {
                var log = new LogModel(Content, LogType.Error, OperationId);
                this.AddLogs(log);
            }
        }
        public void Trace(Object Content)
        {
            var log = new LogModel(Content, LogType.Trace, OperationId);
            if (ShouldLog(LogType.Trace))
            {
                this.AddLogs(log);
            }
        }
        public void Event(Object Content)
        {
            if (ShouldLog(LogType.Event))
            {
                var log = new LogModel(Content, LogType.Event, OperationId);
                this.AddLogs(log);
            }
        }
        public void Exception(Object Content)
        {
            if (ShouldLog(LogType.Exception))
            {
                var log = new LogModel(Content, LogType.Exception, OperationId);
                this.AddLogs(log);
            }
        }
        private void AddLogs(LogModel log)
        {
            if(Configurations.perLogRequest)
            {
                OpenTelemetryApi.PostLog(log);
            }
            else
            {
                Logs.Add(log);
            }
            
        }

        private Guid GetOperationId()
        {
            var operationId = Context.HttpContext.Request.Headers["Operation-Id"].ToString();
            if (!String.IsNullOrEmpty(operationId))
            {
                return new Guid(operationId);
            }
            else
            {
                return Guid.NewGuid();
            }
        }
        private bool ShouldLog(LogType logType)
        {
            if (logType == LogType.Request )
            {
                return LoggingSettings.LogRequest;
            }
            if (logType == LogType.Response)
            {
                return LoggingSettings.LogResponse;
            }
            var callingNamespace = (new System.Diagnostics.StackTrace()).GetFrame(2).GetMethod().ReflectedType.Namespace;
            var currentLogTypeLevel = logType;
            LogType accessedLogLevel;
            
            if(!this.LoggingSettings.LogLevel.TryGetValue(callingNamespace, out accessedLogLevel) ||
                    currentLogTypeLevel >= accessedLogLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
