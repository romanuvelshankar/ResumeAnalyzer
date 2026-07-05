using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using ResumeAnalyzer.Api.Interfaces;

namespace ResumeAnalyzer.Api.Functions;

public class JobsDashboardSyncTimerFunction
{
    private readonly ILogger _logger;
    private readonly IJobDashboardSyncService _jobDashboardSyncService;

    public JobsDashboardSyncTimerFunction(ILoggerFactory loggerFactory, IJobDashboardSyncService jobDashboardSyncService)
    {
        _logger = loggerFactory.CreateLogger<JobsDashboardSyncTimerFunction>();
        _jobDashboardSyncService = jobDashboardSyncService;
    }

    [Function("JobsDashboardSyncTimerFunction")]
    public async Task Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }

        await _jobDashboardSyncService.GetAllJobsAsync();
    }
}