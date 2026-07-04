using ResumeAnalyzer.Api.Models;
using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Interfaces
{
    public interface ITableStorageService
    {
        Task SaveAnalysisAsync(ResumeAnalysisResult result);

        Task<ResumeAnalysisResult?> GetAnalysisAsync(string resumeId);

        Task SaveJobMatchAsync(string resumeId, JobMatchResult result);

        Task<JobMatchResult?> GetJobMatchAsync(string resumeId);

        Task SaveJobAsync(JobDashboardEntity job);

        Task SaveJobsAsync(List<JobDashboardEntity> jobs);

        Task<List<JobDashboardEntity>> GetJobsDashboardInfoAsync();
    }
}
