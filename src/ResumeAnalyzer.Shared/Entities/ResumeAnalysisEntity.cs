using Azure;
using Azure.Data.Tables;

namespace ResumeAnalyzer.Shared.Entities
{
    public class ResumeAnalysisEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "Resume";

        public string RowKey { get; set; } = string.Empty;

        public int AtsScore { get; set; }

        public string Summary { get; set; } = string.Empty;

        public string Strengths { get; set; } = string.Empty;

        public string MissingSkills { get; set; } = string.Empty;

        public string Recommendations { get; set; } = string.Empty;

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
