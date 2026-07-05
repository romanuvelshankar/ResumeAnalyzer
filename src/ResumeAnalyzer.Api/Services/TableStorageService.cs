using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Api.Models;
using ResumeAnalyzer.Shared.Entities;

namespace ResumeAnalyzer.Api.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly TableClient _tableClient;
        private readonly TableClient _tableJobDashboardClient;

        public TableStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];

            _tableClient = new TableClient(connectionString, "resumeanalysis");
            _tableJobDashboardClient = new TableClient(connectionString, "jobsdashboard");

            _tableClient.CreateIfNotExists();
            _tableJobDashboardClient.CreateIfNotExists();
        }

        private static List<string> SplitList(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return [];
            }

            return value.Split('|', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .ToList();
        }

        private async Task DeleteAllJobsAsync()
        {
            await foreach (var entity in _tableJobDashboardClient.QueryAsync<TableEntity>(
                e => e.PartitionKey == "JobDashboard"))
            {
                await _tableJobDashboardClient.DeleteEntityAsync(
                    entity.PartitionKey,
                    entity.RowKey);
            }
        }

        private JobDashboardEntity CharacterLimitJobDashboard(JobDashboardEntity job)
        {
            foreach (var prop in typeof(JobDashboardEntity).GetProperties())
            {
                if (prop.PropertyType != typeof(string) || !prop.CanWrite)
                    continue;

                var value = (string?)prop.GetValue(job);

                if (value?.Length > 32000)
                {
                    prop.SetValue(job, value[..32000]);
                }
            }
            return job;
        }

        private static string GenerateJobRowKey(JobDashboardEntity job)
        {
            var raw = $"{job.Title}-{job.Company}-{job.Source}-{job.Location}";
            return raw.Replace("/", "-")
                      .Replace("\\", "-")
                      .Replace("#", "-")
                      .Replace("?", "-");
        }

        public async Task SaveAnalysisAsync(ResumeAnalysisResult result)
        {
            var entity = new ResumeAnalysisEntity
            {
                PartitionKey = "Resume",
                RowKey = result.ResumeId,

                AtsScore = result.AtsScore,

                Summary = result.Summary,

                Strengths = string.Join("|", result.Strengths),

                MissingSkills = string.Join("|", result.MissingSkills),

                Recommendations = string.Join("|", result.Recommendations)
            };

            await _tableClient.UpsertEntityAsync(entity);
        }

        public async Task<ResumeAnalysisResult?> GetAnalysisAsync(string resumeId)
        {
            try
            {
                var response = await _tableClient.GetEntityAsync<ResumeAnalysisEntity>("Resume", resumeId);

                var entity = response.Value;

                return new ResumeAnalysisResult
                {
                    ResumeId = resumeId,

                    AtsScore = entity.AtsScore,

                    Summary = entity.Summary,

                    Strengths = SplitList(entity.Strengths),

                    MissingSkills = SplitList(entity.MissingSkills),

                    Recommendations = SplitList(entity.Recommendations),

                    AnalyzedAt = entity.Timestamp?.UtcDateTime ?? DateTime.UtcNow
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task SaveJobMatchAsync(string resumeId, JobMatchResult result)
        {
            var entity = new JobMatchEntity
            {
                PartitionKey = "JobMatch",

                RowKey = resumeId,

                MatchScore = result.MatchScore,

                MatchedSkills = string.Join("|", result.MatchedSkills),

                MissingSkills = string.Join("|", result.MissingSkills),

                MissingKeywords = string.Join("|", result.MissingKeywords),

                Recommendation = result.Recommendation
            };

            await _tableClient.UpsertEntityAsync(entity);
        }

        public async Task<JobMatchResult?> GetJobMatchAsync(string resumeId)
        {
            try
            {
                var response = await _tableClient.GetEntityAsync<JobMatchEntity>("JobMatch", resumeId);

                var entity = response.Value;

                return new JobMatchResult
                {
                    ResumeId = resumeId,

                    MatchScore = entity.MatchScore,

                    MatchedSkills = SplitList(entity.MatchedSkills),

                    MissingSkills = SplitList(entity.MissingSkills),

                    MissingKeywords = SplitList(entity.MissingKeywords),

                    Recommendation = entity.Recommendation,

                    AnalyzedAt = entity.Timestamp?.UtcDateTime ?? DateTime.UtcNow
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task SaveJobAsync(JobDashboardEntity job)
        {
            job.PartitionKey = "JobDashboard";

            // IMPORTANT: avoid duplicates
            job.RowKey = GenerateJobRowKey(job);

            await _tableJobDashboardClient.UpsertEntityAsync(job);
        }

        public async Task SaveJobsAsync(List<JobDashboardEntity> jobs)
        {
            await DeleteAllJobsAsync();

            foreach (var job in jobs)
            {
                job.PartitionKey = "JobDashboard";
                job.RowKey = GenerateJobRowKey(job);

                CharacterLimitJobDashboard(job);

                await _tableJobDashboardClient.UpsertEntityAsync(job);
            }
        }

        public async Task<List<JobDashboardEntity>> GetJobsDashboardInfoAsync()
        {
            var jobs = new List<JobDashboardEntity>();

            await foreach (var job in _tableJobDashboardClient.QueryAsync<JobDashboardEntity>(x => x.PartitionKey == "JobDashboard"))
                            {
                                jobs.Add(job);
                            }

            return jobs;
        }
    }
}
