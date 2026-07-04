
namespace ResumeAnalyzer.Api.Models
{
    using Newtonsoft.Json;

    public class ArbeitNowApiResponse
    {
        [JsonProperty("data")]
        public List<ArbeitNowJob> Data { get; set; } = [];

        [JsonProperty("links")]
        public Links? Links { get; set; }

        [JsonProperty("meta")]
        public Meta? Meta { get; set; }
    }

    public class ArbeitNowJob
    {
        [JsonProperty("slug")]
        public string? Slug { get; set; }

        [JsonProperty("company_name")]
        public string? CompanyName { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("remote")]
        public bool Remote { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; } = [];

        [JsonProperty("job_types")]
        public List<string> JobTypes { get; set; } = [];

        [JsonProperty("location")]
        public string? Location { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }
    }

    public class Links
    {
        [JsonProperty("first")]
        public string? First { get; set; }

        [JsonProperty("last")]
        public string? Last { get; set; }

        [JsonProperty("prev")]
        public string? Prev { get; set; }

        [JsonProperty("next")]
        public string? Next { get; set; }
    }

    public class Meta
    {
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("current_page_url")]
        public string? CurrentPageUrl { get; set; }

        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }

        [JsonProperty("terms")]
        public string? Terms { get; set; }

        [JsonProperty("info")]
        public string? Info { get; set; }
    }
}
