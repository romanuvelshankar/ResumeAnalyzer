using Newtonsoft.Json;
using ResumeAnalyzer.Api.Models;
using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Providers
{
    public class RemotiveProvider : IJobProvider
    {
        private readonly HttpClient _httpClient;

        public RemotiveProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://remotive.com/api/");
        }

        public async Task<List<JobDashboardEntity>> GetJobsAsync(CancellationToken cancellationToken = default)
        {
            var json = await _httpClient.GetStringAsync("remote-jobs", cancellationToken);


            Console.WriteLine(json);
            var result = JsonConvert.DeserializeObject<RemotiveApiResponse>(json);

            return result?.Jobs.Select(j => new JobDashboardEntity
            {
                Title = j.Title,
                Company = j.CompanyName,
                Location = j.CandidateRequiredLocation,
                Description = j.Description,
                TagsSerialized = string.Join(';', j.Tags),
                IsRemote = true,
                Source = "Remotive"
            }).ToList() ?? [];
        }
    }
}
