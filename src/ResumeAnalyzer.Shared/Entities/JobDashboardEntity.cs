using Azure;
using Azure.Data.Tables;

namespace ResumeAnalyzer.Shared.Entities
{
    public class JobDashboardEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "JobDashboard";
        public string RowKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Description { get; set; }
        public List<string> Tags { get; set; } = [];
        public bool IsRemote { get; set; }
        public string Source { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
