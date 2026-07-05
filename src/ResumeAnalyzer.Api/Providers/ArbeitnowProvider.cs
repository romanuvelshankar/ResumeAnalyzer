using Newtonsoft.Json;
using ResumeAnalyzer.Api.Models;
using ResumeAnalyzer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeAnalyzer.Api.Providers
{
    public class ArbeitnowProvider : IJobProvider
    {
        private readonly HttpClient _httpClient;

        public ArbeitnowProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://www.arbeitnow.com/");
        }

        public async Task<List<JobDashboardEntity>> GetJobsAsync(CancellationToken cancellationToken = default)
        {
            var json = await _httpClient.GetStringAsync("api/job-board-api", cancellationToken);
            var result = JsonConvert.DeserializeObject<ArbeitNowApiResponse>(json);

            return result?.Data.Select(j => new JobDashboardEntity
            {
                Title = j.Title,
                Company = j.CompanyName,
                Location = j.Location,
                Description = j.Description,
                TagsSerialized = string.Join(';', j.Tags),
                IsRemote = j.Remote,
                Source = "Arbeitnow"
            }).ToList() ?? [];
        }
    }
}
