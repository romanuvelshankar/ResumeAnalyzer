using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Shared.Models;

namespace ResumeAnalyzer.Api.Functions;

public class UploadResumeFunction
{
    private readonly IBlobStorageService _storage;

    public UploadResumeFunction(IBlobStorageService storage)
    {
        _storage = storage;
    }

    [Function("UploadResume")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        var form = await req.ReadFormAsync();

        var file = form.Files["resume"] ?? throw new InvalidOperationException("Request body is missing.");

        var blobName = await _storage.UploadAsync(file.FileName, file.OpenReadStream());

        return new OkObjectResult(new UploadResumeResponse
        {
            ResumeId = blobName,
            FileName = file.FileName
        });
    }
}