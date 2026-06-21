namespace ResumeAnalyzer.AnalysisService.Interfaces
{
    using ResumeAnalyzer.Shared.Models;

    public interface IAtsScoringService
    {
        Task<AtsScoreBreakdown> CalculateAsync( string resumeText);
    }
}
