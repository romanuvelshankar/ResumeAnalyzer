using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ResumeAnalyzer.Api.Interfaces;

namespace ResumeAnalyzer.Api.Functions;

public class JobDashboardFunction
{
    private readonly ILogger<JobDashboardFunction> _logger;
    private readonly IJobDashboardService _jobDashboardService;

    public JobDashboardFunction(ILogger<JobDashboardFunction> logger, IJobDashboardService jobDashboardService)
    {
        _logger = logger;
        _jobDashboardService = jobDashboardService;
    }

    [Function("JobDashboardFunction")]
    public async Task<IActionResult> GetJobs([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        var jobs = await _jobDashboardService.GetJobsAsync();
        return new OkObjectResult(jobs);
    }
}