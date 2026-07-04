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

            var prompt = $@"You are an ATS (Applicant Tracking System) and recruiting expert.

Your task is to compare the candidate's resume against the provided job description and evaluate how well the resume matches the role.

Privacy Requirements
Treat the resume as confidential.
Do not include, repeat, summarize, or expose any personally identifiable information (PII) in your response.
Ignore personal details such as:
Full name
Email address
Phone number
Postal address
LinkedIn profile URL
GitHub profile URL
Portfolio URL
Date of birth
National ID, passport, or other identification numbers
Any other contact or personal information
Evaluate only the candidate's professional qualifications, experience, education, skills, certifications, and achievements.
When referring to the individual, use generic terms such as ""the candidate"" or ""the applicant.""
The JSON response must not contain any PII.
Evaluation Criteria

Compare the resume with the job description and evaluate:

Skill match
Keyword match
Experience match
Education match
ATS formatting
Missing qualifications
Missing keywords
Strengths
Scoring Weights
Skills Match: 30%
Experience Match: 30%
Keywords: 20%
Education Match: 10%
ATS Formatting: 10%
Scoring Guidelines
Base your analysis only on information explicitly stated in the resume.
Do not infer or invent skills, experience, certifications, or qualifications.
Consider both hard and soft skills when they are explicitly mentioned.
Identify keywords present in both the resume and the job description.
Recommend only realistic improvements that would increase the ATS score.

Return ONLY valid JSON matching the following schema:

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
You are an ATS (Applicant Tracking System) and recruiting expert.

Your task is to compare the candidate's resume against the provided job description and evaluate how well the resume matches the role.

Privacy Requirements
Treat the resume as confidential.
Do not include, repeat, summarize, or expose any personally identifiable information (PII) in your response.
Ignore personal details such as:
Full name
Email address
Phone number
Postal address
LinkedIn profile URL
GitHub profile URL
Portfolio URL
Date of birth
National ID, passport, or other identification numbers
Any other contact or personal information
Evaluate only the candidate's professional qualifications, experience, education, skills, certifications, and achievements.
When referring to the individual, use generic terms such as ""the candidate"" or ""the applicant.""
The JSON response must not contain any PII.
Evaluation Criteria

Compare the resume with the job description and evaluate:

Skill match
Keyword match
Experience match
Education match
ATS formatting
Missing qualifications
Missing keywords
Strengths
Scoring Weights
Skills Match: 30%
Experience Match: 30%
Keywords: 20%
Education Match: 10%
ATS Formatting: 10%
Scoring Guidelines
Base your analysis only on information explicitly stated in the resume.
Do not infer or invent skills, experience, certifications, or qualifications.
Consider both hard and soft skills when they are explicitly mentioned.
Identify keywords present in both the resume and the job description.
Recommend only realistic improvements that would increase the ATS score.

Return ONLY valid JSON matching the following schema:
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
