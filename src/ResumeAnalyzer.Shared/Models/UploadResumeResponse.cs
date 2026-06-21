namespace ResumeAnalyzer.Shared.Models
{
    public class UploadResumeResponse
    {
        public string ResumeId { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string BlobUrl { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; } =
            DateTime.UtcNow;
    }
}
