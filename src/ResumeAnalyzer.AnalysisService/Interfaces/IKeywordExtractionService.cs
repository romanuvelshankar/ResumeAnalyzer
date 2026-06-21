namespace ResumeAnalyzer.AnalysisService.Interfaces
{
    using ResumeAnalyzer.Shared.Models;

    public interface IKeywordExtractionService
    {
        Task<ResumeKeywords> ExtractAsync(string resumeText);
    }
}
