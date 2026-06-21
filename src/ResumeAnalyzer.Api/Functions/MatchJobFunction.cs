using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Shared.Models;
using System.Net;

namespace ResumeAnalyzer.Api.Functions;

public class MatchJobFunction
{
    private readonly IBlobStorageService _storage;
    private readonly IPdfExtractionService _pdf;
    private readonly IOpenAIService _ai;

    public MatchJobFunction( IBlobStorageService storage, IPdfExtractionService pdf, IOpenAIService ai)
    {
        _storage = storage;
        _pdf = pdf;
        _ai = ai;
    }

    [Function("MatchJob")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var request = await req.ReadFromJsonAsync<JobMatchRequest>() ?? throw new InvalidOperationException("Request body is missing.");

        var pdfStream = await _storage.DownloadAsync(request.ResumeId);

        var resumeText = await _pdf.ExtractTextAsync(pdfStream);

        var result = await _ai.MatchJobAsync(resumeText, request.JobDescription);

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(result);

        return response;
    }
}