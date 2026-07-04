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

        //private async Task<List<ArbeitNowJob>> GetJobsAsync(CancellationToken cancellationToken = default)
        //{
        //    var response = await _httpClient.GetAsync("https://arbeitnow.com/api/job-board-api", cancellationToken);

        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync(cancellationToken);

        //    var result = JsonConvert.DeserializeObject<ArbeitNowApiResponse>(json);

        //    return result?.Data ?? [];
        //}

        //public async Task<List<Jobs>> GetJobsAsync(CancellationToken cancellationToken = default)
        //{
        //    var response = await _httpClient.GetAsync("remote-jobs", cancellationToken);

        //    response.EnsureSuccessStatusCode();

        //    var json = await response.Content.ReadAsStringAsync(cancellationToken);

        //    var result = JsonConvert.DeserializeObject<Root>(json);

        //    return result?.Jobs ?? new List<Jobs>();
        //}

        //public async Task SyncJobsAsync()
        //{

        //    // TODO:
        //    // 1. Call Arbeitnow API
        //    // 2. Call RemoteOK API
        //    // 3. Merge results
        //    // 4. Extract skills
        //    // 5. Save to database



        //    await Task.Delay(1000);
        //}

        public async Task<List<JobDashboardEntity>> GetAllJobsAsync(CancellationToken cancellationToken = default)
        {
            var tasks = _providers.Select(p => p.GetJobsAsync(cancellationToken));

            var results = await Task.WhenAll(tasks);
            var listOfJobs = results.SelectMany(x => x).ToList();

            if (results.Any())
            {
                await _tableStorageService.SaveJobsAsync(listOfJobs);
            }

            return results.SelectMany(x => x).ToList();
        }
    }
}
