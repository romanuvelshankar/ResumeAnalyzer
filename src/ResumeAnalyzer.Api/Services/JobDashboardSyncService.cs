using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Api.Providers;
using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Services
{
    public class JobDashboardSyncService : IJobDashboardSyncService
    {

        private readonly IEnumerable<IJobProvider> _providers;
        private readonly ITableStorageService _tableStorageService;

        public JobDashboardSyncService(IEnumerable<IJobProvider> providers, ITableStorageService tableStorage)
        {
            _providers = providers;
            _tableStorageService = tableStorage;
        }


        public async Task<List<JobDashboardEntity>> GetAllJobsAsync(CancellationToken cancellationToken = default)
        {
            var tasks = _providers.Select(p => p.GetJobsAsync(cancellationToken)).ToList();

            var results = await Task.WhenAll(tasks);
            var listOfJobs = results.SelectMany(x => x).ToList();

            if (listOfJobs.Any())
            {
                await _tableStorageService.SaveJobsAsync(listOfJobs);
            }

            return listOfJobs;
        }
    }
}
