namespace ResumeAnalyzer.AnalysisService.Services
{
    using ResumeAnalyzer.AnalysisService.Interfaces;
    using ResumeAnalyzer.Shared.Models;

    public class KeywordExtractionService : IKeywordExtractionService
    {
        private readonly List<string> _skills =
        [
            ".NET",
        "Azure",
        "SQL",
        "Docker",
        "Kubernetes",
        "Terraform",
        "React",
        "Angular",
        "Microservices"
        ];

        public Task<ResumeKeywords>
            ExtractAsync(
                string resumeText)
        {
            var foundSkills =
                _skills
                    .Where(skill =>
                        resumeText.Contains(
                            skill,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();

            return Task.FromResult(
                new ResumeKeywords
                {
                    FoundSkills = foundSkills
                });
        }
    }
}
