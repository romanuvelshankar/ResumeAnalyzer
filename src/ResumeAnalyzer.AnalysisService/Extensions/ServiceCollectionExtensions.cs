namespace ResumeAnalyzer.AnalysisService.Extensions
{
    using global::ResumeAnalyzer.AnalysisService.Interfaces;
    using global::ResumeAnalyzer.AnalysisService.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAnalysisServices(this IServiceCollection services)
        {
            services.AddScoped<IResumeAnalysisService, ResumeAnalysisService>();

            services.AddScoped<IAtsScoringService, AtsScoringService>();

            services.AddScoped<IKeywordExtractionService, KeywordExtractionService>();

            services.AddScoped<IJobMatchingService, JobMatchingService>();

            return services;
        }
    }
}
