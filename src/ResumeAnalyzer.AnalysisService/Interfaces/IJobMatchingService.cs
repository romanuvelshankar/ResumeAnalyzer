namespace ResumeAnalyzer.AnalysisService.Interfaces
{
    using ResumeAnalyzer.Shared.Models;

    public interface IJobMatchingService
    {
        Task<JobMatchResult> MatchAsync(string resumeText, string jobDescription);
    }
}
