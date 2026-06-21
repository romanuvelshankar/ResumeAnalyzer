namespace ResumeAnalyzer.Api.Interfaces
{
    public interface IPdfExtractionService
    {
        Task<string> ExtractTextAsync(Stream pdfStream);
    }
}
