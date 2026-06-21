using Azure;
using Azure.Data.Tables;

namespace ResumeAnalyzer.Shared.Entities
{
    public class JobMatchEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "JobMatch";

        public string RowKey { get; set; } = string.Empty;

        public int MatchScore { get; set; }

        public string MatchedSkills { get; set; } = string.Empty;

        public string MissingSkills { get; set; } = string.Empty;

        public string MissingKeywords { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}
