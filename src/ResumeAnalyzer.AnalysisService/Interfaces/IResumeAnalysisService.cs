namespace ResumeAnalyzer.AnalysisService.Interfaces
{
    using ResumeAnalyzer.Shared.Models;

    public interface IResumeAnalysisService
    {
        Task<ResumeAnalysisResult> AnalyzeAsync(string resumeText);
    }
}
