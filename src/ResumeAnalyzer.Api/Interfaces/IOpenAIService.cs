using ResumeAnalyzer.Api.Models;

namespace ResumeAnalyzer.Api.Interfaces
{
    public interface IOpenAIService
    {
        Task<ResumeAnalysisResult> AnalyzeResumeAsync(string resumeText);

        Task<JobMatchResult> MatchJobAsync(string resumeText, string jobDescription);
    }
}
