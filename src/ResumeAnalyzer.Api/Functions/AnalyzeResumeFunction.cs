using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Shared.Models;
using System.Net;

namespace ResumeAnalyzer.Api.Functions;

public class AnalyzeResumeFunction
{
    private readonly IBlobStorageService _storage;
    private readonly IPdfExtractionService _pdf;
    private readonly IOpenAIService _ai;
    private readonly ITableStorageService _tableStorage;

    public AnalyzeResumeFunction(IBlobStorageService storage, IPdfExtractionService pdf, IOpenAIService ai, ITableStorageService tableStorage)
    {
        _storage = storage;
        _pdf = pdf;
        _ai = ai;
        _tableStorage = tableStorage;
    }

    [Function("AnalyzeResume")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var request = await req.ReadFromJsonAsync<AnalyzeResumeRequest>() ?? throw new InvalidOperationException("Request body is missing.");

        var pdfStream = await _storage.DownloadAsync(request.ResumeId);

        var resumeText = await _pdf.ExtractTextAsync(pdfStream);

        var analysis = await _ai.AnalyzeResumeAsync(resumeText);

        analysis.ResumeId = request.ResumeId;

        await _tableStorage.SaveAnalysisAsync(analysis);

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(analysis);

        return response;
    }
}