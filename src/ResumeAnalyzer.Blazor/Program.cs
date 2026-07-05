using MudBlazor.Services;
using ResumeAnalyzer.Blazor.Components;
using ResumeAnalyzer.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Components (Blazor Web App .NET 8)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });

// ✅ Typed HttpClient for ResumeApiClient (correct approach)
builder.Services.AddHttpClient<ResumeApiClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var resumeApiUrl = config.GetValue<string>("Microservices:ResumeApi");

    if (string.IsNullOrWhiteSpace(resumeApiUrl))
        throw new InvalidOperationException("Microservices:ResumeApi is missing in configuration.");

    client.BaseAddress = new Uri(resumeApiUrl);
});
builder.Services.AddMudServices();

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();