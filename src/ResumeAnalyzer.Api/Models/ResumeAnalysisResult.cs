namespace ResumeAnalyzer.Api.Models
{
    public class ResumeAnalysisResult
    {
        public string ResumeId { get; set; } = string.Empty;

        public int AtsScore { get; set; }

        public string Summary { get; set; } = string.Empty;

        public List<string> Strengths { get; set; } = [];

        public List<string> MissingSkills { get; set; } = [];

        public List<string> Recommendations { get; set; } = [];

        public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;
    }
}
