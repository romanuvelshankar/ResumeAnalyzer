namespace ResumeAnalyzer.Api.Services
{
    using Azure;
    using Azure.AI.OpenAI;
    using Microsoft.Extensions.Configuration;
    using OpenAI.Chat;
    using ResumeAnalyzer.Api.Interfaces;
    using ResumeAnalyzer.Api.Models;
    using System.Text.Json;

    public class OpenAIService : IOpenAIService
    {
        private readonly IConfiguration _configuration;

        public OpenAIService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private AzureOpenAIClient CreateClient()
        {
            var endpoint = _configuration["AzureOpenAIEndpoint"];

            var apiKey = _configuration["AzureOpenAIAPIKey"];

            return new AzureOpenAIClient(new Uri(endpoint!), new AzureKeyCredential(apiKey!));
        }

        public async Task<ResumeAnalysisResult> AnalyzeResumeAsync(string resumeText)
        {
            var deploymentName = _configuration["AzureOpenAIDeploymentName"];
            var client = CreateClient();


            ChatClient chatClient = client.GetChatClient(deploymentName!);

            var prompt = $@"Analyze the following resume.

Return ONLY valid JSON.

{{
  ""atsScore"": 0,
  ""summary"": """",
  ""strengths"": [],
  ""missingSkills"": [],
  ""recommendations"": []
}}

Resume:

{resumeText}
";

            var completion = await chatClient.CompleteChatAsync(
                [
                    new SystemChatMessage(
                    "You are an ATS resume analyzer."),
                new UserChatMessage(prompt)
                ]);

            var json = completion.Value.Content[0].Text;

            json = CleanJson(json);

            return JsonSerializer.Deserialize<ResumeAnalysisResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new ResumeAnalysisResult();
        }

        public async Task<JobMatchResult> MatchJobAsync(string resumeText, string jobDescription)
        {
            var deploymentName = _configuration["AzureOpenAIDeploymentName"];
            var client = CreateClient();

            ChatClient chatClient = client.GetChatClient(deploymentName!);

            var prompt = $@"
Analyze the following resume.

Return ONLY valid JSON.

{{
  ""atsScore"": 0,
  ""summary"": """",
  ""strengths"": [],
  ""missingSkills"": [],
  ""recommendations"": []
}}

Resume:

{resumeText}
";

            var completion = await chatClient.CompleteChatAsync(
                [
                    new SystemChatMessage(
                    "You are a recruitment expert."),
                new UserChatMessage(prompt)
                ]);

            var json =
                completion.Value.Content[0].Text;

            json = CleanJson(json);

            return JsonSerializer.Deserialize<JobMatchResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new JobMatchResult();
        }

        private static string CleanJson(string json)
        {
            return json
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();
        }
    }
}
