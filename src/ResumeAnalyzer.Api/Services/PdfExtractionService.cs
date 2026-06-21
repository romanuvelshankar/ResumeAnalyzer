namespace ResumeAnalyzer.Api.Services
{
    using ResumeAnalyzer.Api.Interfaces;
    using UglyToad.PdfPig;

    public class PdfExtractionService : IPdfExtractionService
    {
        public Task<string> ExtractTextAsync(Stream pdfStream)
        {
            using var pdf = PdfDocument.Open(pdfStream);

            var text = string.Join(Environment.NewLine, pdf.GetPages().Select(p => p.Text));

            return Task.FromResult(text);
        }
    }
}
