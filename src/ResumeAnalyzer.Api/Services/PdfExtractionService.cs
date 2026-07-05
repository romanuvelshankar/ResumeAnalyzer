namespace ResumeAnalyzer.Api.Services
{
    using DocumentFormat.OpenXml.Packaging;
    using ResumeAnalyzer.Api.Interfaces;
    using UglyToad.PdfPig;

    public class PdfExtractionService : IPdfExtractionService
    {
        private static string DetectFileType(Stream stream)
        {
            var buffer = new byte[8];

            stream.Read(buffer, 0, buffer.Length);

            // PDF: %PDF
            if (buffer[0] == 0x25 && buffer[1] == 0x50 &&
                buffer[2] == 0x44 && buffer[3] == 0x46)
                return "pdf";

            // DOCX: ZIP container (PK..)
            if (buffer[0] == 0x50 && buffer[1] == 0x4B)
                return "docx";

            return "unknown";
        }

        private string ExtractPdf(Stream pdfStream)
        {
            using var pdf = PdfDocument.Open(pdfStream);

            return string.Join(Environment.NewLine,
                pdf.GetPages().Select(p => p.Text));
        }

        private string ExtractDocx(Stream stream)
        {
            using var doc = WordprocessingDocument.Open(stream, false);

            var body = doc.MainDocumentPart?.Document?.Body;

            if (body == null)
                return string.Empty;

            return body.InnerText;
        }

        public async Task<string> ExtractTextAsync(Stream input)
        {
            using var memory = new MemoryStream();
            await input.CopyToAsync(memory);

            memory.Position = 0;

            var type = DetectFileType(memory);

            memory.Position = 0;

            return type switch
            {
                "pdf" => ExtractPdf(memory),
                "docx" => ExtractDocx(memory),
                _ => throw new NotSupportedException("Unsupported file type")
            };
        }
    }
}
