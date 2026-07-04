using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Interfaces
{
    public interface IJobDashboardService
    {
        Task<List<JobDashboardEntity>> GetJobsAsync();
    }
}
