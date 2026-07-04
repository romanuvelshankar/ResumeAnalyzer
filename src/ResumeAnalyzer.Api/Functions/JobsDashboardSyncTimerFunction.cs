using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ResumeAnalyzer.Api.Functions;

public class JobsDashboardSyncTimerFunction
{
    private readonly ILogger _logger;

    public JobsDashboardSyncTimerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<JobsDashboardSyncTimerFunction>();
    }

    [Function("JobsDashboardSyncTimerFunction")]
    public void Run([TimerTrigger("0 0 * * * *")] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}