using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Api.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<ITableStorageService, TableStorageService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<IPdfExtractionService, PdfExtractionService>();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
