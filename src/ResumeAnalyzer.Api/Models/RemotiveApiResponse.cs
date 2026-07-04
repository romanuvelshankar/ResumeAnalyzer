using Newtonsoft.Json;
using System.Collections.Generic;

namespace ResumeAnalyzer.Api.Models
{
    public class RemotiveApiResponse
    {

        [JsonProperty("00-warning")]
        public string Warning { get; set; }

        [JsonProperty("0-legal-notice")]
        public string LegalNotice { get; set; }

        [JsonProperty("job-count")]
        public int JobCount { get; set; }

        [JsonProperty("total-job-count")]
        public int TotalJobCount { get; set; }

        [JsonProperty("jobs")]
        public List<Jobs> Jobs { get; set; }
    }

    public class Jobs
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("company_logo")]
        public string CompanyLogo { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("job_type")]
        public string JobType { get; set; }

        [JsonProperty("publication_date")]
        public string PublicationDate { get; set; }

        [JsonProperty("candidate_required_location")]
        public string CandidateRequiredLocation { get; set; }

        [JsonProperty("salary")]
        public string Salary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("company_logo_url")]
        public string CompanyLogoUrl { get; set; }
    }
}
