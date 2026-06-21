namespace ResumeAnalyzer.AnalysisService.Services
{
    using ResumeAnalyzer.AnalysisService.Interfaces;
    using ResumeAnalyzer.Shared.Models;

    public class JobMatchingService : IJobMatchingService
    {
        private readonly IKeywordExtractionService _keywords;

        public JobMatchingService(IKeywordExtractionService keywords)
        {
            _keywords = keywords;
        }

        public async Task<JobMatchResult> MatchAsync(string resumeText, string jobDescription)
        {
            var resumeSkills = await _keywords.ExtractAsync(resumeText);

            var jdSkills = await _keywords.ExtractAsync(jobDescription);

            var matched = resumeSkills.FoundSkills
                                      .Intersect(jdSkills.FoundSkills)
                                      .ToList();

            var missing = jdSkills.FoundSkills
                                  .Except(resumeSkills.FoundSkills)
                                  .ToList();

            var score = jdSkills.FoundSkills.Count == 0
                                                    ? 0
                                                    : matched.Count * 100 /
                                                      jdSkills.FoundSkills.Count;

            return new JobMatchResult
            {
                MatchScore = score,
                MatchedSkills = matched,
                MissingSkills = missing,
                MissingKeywords = missing,
                Recommendation =
                    $"Add {string.Join(", ", missing)}"
            };
        }
    }
}
