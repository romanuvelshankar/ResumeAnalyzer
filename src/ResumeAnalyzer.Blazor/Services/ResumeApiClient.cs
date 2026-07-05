namespace ResumeAnalyzer.Blazor.Services
{
    using ResumeAnalyzer.Shared.Entities;
    using ResumeAnalyzer.Shared.Models;
    using System.Net.Http.Json;
    using System.Text.Json;

    public class ResumeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseApiUrl;

        public ResumeApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var url = configuration["Microservices:ResumeApi"];
            if (url is null)
                throw new InvalidOperationException("Microservices:ResumeApi is missing in configuration.");
            _baseApiUrl = url;
        }

        public async Task<UploadResumeResponse?> UploadResumeAsync(MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(_baseApiUrl + "api/uploadresume", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UploadResumeResponse>();
        }

        public async Task<ResumeAnalysisResult?> AnalyzeResumeAsync(string resumeId)
        {
            var response =await _httpClient.PostAsJsonAsync(_baseApiUrl + "api/analyzeresume",
                    new AnalyzeResumeRequest
                    {
                        ResumeId = resumeId
                    });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ResumeAnalysisResult>();
        }

        public async Task<JobMatchResult?> MatchJobAsync(string resumeId, string jobDescription)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseApiUrl + "api/matchjob",
                    new JobMatchRequest
                    {
                        ResumeId = resumeId,
                        JobDescription = jobDescription
                    });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<JobMatchResult>();
        }
        public async Task<List<JobDashboardEntity>> JobDashboardAsync()
        {
            var response = await _httpClient.GetAsync(_baseApiUrl + "api/GetJobDashboard");

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<List<JobDashboardEntity>>(stream, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });

            return result ?? new List<JobDashboardEntity>();
        }
    }
}
