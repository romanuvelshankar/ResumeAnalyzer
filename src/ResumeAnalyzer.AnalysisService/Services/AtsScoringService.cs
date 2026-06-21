namespace ResumeAnalyzer.AnalysisService.Services
{
    using ResumeAnalyzer.AnalysisService.Interfaces;
    using ResumeAnalyzer.Shared.Models;

    public class AtsScoringService : IAtsScoringService
    {
        private readonly List<string> _importantSkills =
        [
            ".NET",
            "Azure",
            "Docker",
            "Kubernetes",
            "Terraform",
            "Microservices",
            "CI/CD",
            "GitHub Actions"
        ];

        public Task<AtsScoreBreakdown> CalculateAsync(string resumeText)
        {
            var score = 0;

            var foundSkills = new List<string>();
            var missingSkills = new List<string>();

            foreach (var skill in _importantSkills)
            {
                if (resumeText.Contains(
                    skill,
                    StringComparison.OrdinalIgnoreCase))
                {
                    score += 10;
                    foundSkills.Add(skill);
                }
                else
                {
                    missingSkills.Add(skill);
                }
            }

            return Task.FromResult(
                new AtsScoreBreakdown
                {
                    TotalScore = score,
                    MissingSkills = missingSkills,
                    Recommendations =
                    [
                        "Add quantified achievements",
                    "Highlight cloud projects"
                    ]
                });
        }
    }
}
