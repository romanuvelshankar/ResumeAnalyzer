using Azure.Core;
using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResumeAnalyzer.Api.Interfaces;
using ResumeAnalyzer.Api.Providers;
using ResumeAnalyzer.Api.Services;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
var keyVaultUrl = builder.Configuration["KeyVaultUrl"];

TokenCredential credential = builder.Environment.IsDevelopment() ? new AzureCliCredential() : new DefaultAzureCredential();

if (!string.IsNullOrWhiteSpace(keyVaultUrl))
{
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);
}

var serviceUri = new Uri("https://stresumeanalyzer.table.core.windows.net/");

var tableClient = new TableClient(serviceUri,"JobDashboard",new DefaultAzureCredential());

builder.Services.AddSingleton(_ =>
{
    var connectionString = builder.Configuration["ConnectionString"];
    return new TableClient(connectionString, "jobsdashboard");
});

// HttpClient per provider
builder.Services.AddHttpClient<RemotiveProvider>();
builder.Services.AddHttpClient<ArbeitnowProvider>();

// Register providers
builder.Services.AddScoped<IJobProvider, RemotiveProvider>();
builder.Services.AddScoped<IJobProvider, ArbeitnowProvider>();

builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<ITableStorageService, TableStorageService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<IPdfExtractionService, PdfExtractionService>();

builder.Services.AddScoped<IJobDashboardSyncService, JobDashboardSyncService>();
builder.Services.AddScoped<IJobDashboardService,JobDashboardService>();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var app = builder.Build();

app.Run();
