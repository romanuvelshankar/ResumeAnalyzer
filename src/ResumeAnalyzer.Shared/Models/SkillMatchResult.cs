namespace ResumeAnalyzer.Shared.Models
{
    public class SkillMatchResult
    {
        public int MatchPercentage { get; set; }

        public List<string> MatchedSkills { get; set; } = [];

        public List<string> MissingSkills { get; set; } = [];
    }
}
