using Azure.Data.Tables;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Services
{
    public class JobDashboardService : IJobDashboardService
    {
        private readonly TableClient _tableClient;

        public JobDashboardService(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task<List<JobDashboardEntity>> GetJobsAsync()
        {
            var jobs = new List<JobDashboardEntity>();

            await foreach (var job in _tableClient.QueryAsync<JobDashboardEntity>(x => x.PartitionKey == "JobDashboard"))
            {
                jobs.Add(job);
            }

            return jobs;
        }
    }
}
