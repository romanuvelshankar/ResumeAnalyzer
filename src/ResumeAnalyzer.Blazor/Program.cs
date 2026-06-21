//using ResumeAnalyzer.Blazor.Components;
//using ResumeAnalyzer.Blazor.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//var resumeApiUrl = builder.Configuration["Microservices:ResumeApi"];

//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri(resumeApiUrl)
//});

//builder.Services.AddScoped<HttpClient>();
//builder.Services.AddScoped<ResumeApiClient>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
//app.UseHttpsRedirection();

//app.UseAntiforgery();

//app.MapStaticAssets();
//app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();

//var config = builder.Configuration;

//app.Run();
using ResumeAnalyzer.Blazor.Components;
using ResumeAnalyzer.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Components (Blazor Web App .NET 8)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Typed HttpClient for ResumeApiClient (correct approach)
builder.Services.AddHttpClient<ResumeApiClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();

    var resumeApiUrl = config.GetValue<string>("Microservices:ResumeApi");

    if (string.IsNullOrWhiteSpace(resumeApiUrl))
        throw new InvalidOperationException("Microservices:ResumeApi is missing in configuration.");

    client.BaseAddress = new Uri(resumeApiUrl);
});


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