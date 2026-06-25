using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeAnalyzer.Api.Models
{
    public class AzureOpenAIOptions
    {
        public string Endpoint { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;

        public string DeploymentName { get; set; } = string.Empty;
    }
}
