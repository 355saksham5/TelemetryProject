namespace OpenTelemetry.Model
{
    public class LogModel
    {
        public LogType LogType { get;  set; }
        public string ApiId { get; set; }
        public string Content { get; set; }
        public DateTime LoggedAt { get; set; }
        public Guid OperationId { get; set; }
        internal LogModel(object content, LogType logType, Guid operationId)
        {
            this.ApiId = Configurations.apiId;
            this.LogType = logType;
            this.Content = content.ToString();
            this.LoggedAt = DateTime.Now;
            this.OperationId = operationId;
        }
        
    }
}
