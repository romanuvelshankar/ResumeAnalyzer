
namespace ResumeAnalyzer.AnalysisService.Services
{
    using ResumeAnalyzer.AnalysisService.Interfaces;
    using ResumeAnalyzer.Shared.Models;

    public class ResumeAnalysisService : IResumeAnalysisService
    {
        private readonly IAtsScoringService _ats;
        private readonly IKeywordExtractionService _keywords;

        public ResumeAnalysisService(IAtsScoringService ats,IKeywordExtractionService keywords)
        {
            _ats = ats;
            _keywords = keywords;
        }

        public async Task<ResumeAnalysisResult> AnalyzeAsync(string resumeText)
        {
            var atsScore =
                await _ats.CalculateAsync(
                    resumeText);

            var keywords =
                await _keywords.ExtractAsync(
                    resumeText);

            return new ResumeAnalysisResult
            {
                AtsScore = atsScore.TotalScore,
                Strengths = keywords.FoundSkills,
                MissingSkills = atsScore.MissingSkills,
                Recommendations = atsScore.Recommendations,
                Summary =
                    $"Resume contains {keywords.FoundSkills.Count} technical skills."
            };
        }
    }
}
