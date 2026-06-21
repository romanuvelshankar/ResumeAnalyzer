using ResumeAnalyzer.Api.Models;

namespace ResumeAnalyzer.Api.Interfaces
{
    public interface ITableStorageService
    {
        Task SaveAnalysisAsync(ResumeAnalysisResult result);

        Task<ResumeAnalysisResult?> GetAnalysisAsync(string resumeId);

        Task SaveJobMatchAsync(string resumeId, JobMatchResult result);

        Task<JobMatchResult?> GetJobMatchAsync(string resumeId);
    }
}
