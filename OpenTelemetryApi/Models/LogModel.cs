namespace OpenTelemetryApi.Models
{
    public class LogModel
    {
        public Guid Id { get; set; } 
        public LogType LogType { get; set; }
        public string ApiId { get; set; }
        public string Content { get; set; }
        public DateTime LoggedAt { get; set; }
        public Guid OperationId { get; set; }
    }
}
