using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Interfaces
{
    public interface IJobDashboardSyncService
    {
        Task<List<JobDashboardEntity>> GetAllJobsAsync(CancellationToken cancellationToken = default);
    }
}
