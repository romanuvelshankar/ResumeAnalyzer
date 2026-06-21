namespace ResumeAnalyzer.Shared.Models
{
    public class AtsScoreBreakdown
    {
        public int TotalScore { get; set; }

        public List<string> MissingSkills { get; set; } = [];

        public List<string> Recommendations { get; set; } = [];
    }
}
