using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Providers
{
    public interface IJobProvider
    {
        Task<List<JobDashboardEntity>> GetJobsAsync(CancellationToken cancellationToken = default);
    }
}
