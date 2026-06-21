namespace ResumeAnalyzer.Api.Models
{
    public class JobMatchResult
    {
        public string ResumeId { get; set; } = string.Empty;

        public int MatchScore { get; set; }

        public List<string> MatchedSkills { get; set; } = [];

        public List<string> MissingSkills { get; set; } = [];

        public List<string> MissingKeywords { get; set; } = [];

        public string Recommendation { get; set; } = string.Empty;

        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }
}
