namespace ResumeAnalyzer.Api.Services
{
    using ResumeAnalyzer.Api.Interfaces;
    using ResumeAnalyzer.Api.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OpenAIService : IOpenAIService
    {
        public async Task<ResumeAnalysisResult>AnalyzeResumeAsync(string resumeText)
        {
            return new ResumeAnalysisResult
            {
                AtsScore = 84,
                Summary =
                    "Strong Azure and .NET profile.",

                Strengths =
                [
                    ".NET",
                "Azure",
                "Microservices"
                ],

                MissingSkills =
                [
                    "Terraform",
                "AKS"
                ],

                Recommendations =
                [
                    "Add quantified achievements",
                "Mention CI/CD experience"
                ]
            };
        }

        public async Task<JobMatchResult> MatchJobAsync(string resumeText, string jobDescription)
        {
            return new JobMatchResult
            {
                MatchScore = 82,

                MatchedSkills =
                [
                    ".NET",
                "Azure"
                ],

                MissingSkills =
                [
                    "Terraform"
                ],

                MissingKeywords =
                [
                    "AKS"
                ],

                Recommendation =
                    "Add Terraform experience."
            };
        }
    }
}
