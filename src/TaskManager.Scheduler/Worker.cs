using TaskManager.Core.Models;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Scheduler;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private DatabaseContext _context;
    private readonly IConfiguration _config;

    public Worker(ILogger<Worker> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string connectionString = string.Format("Data Source={0};", _config["DatabasePath"]);
        _context = new DatabaseContext(connectionString);

        while (!stoppingToken.IsCancellationRequested)
        {
            var task = new Task(async () =>
            {
                await ManageCronTaskExecution();
            });
            task.Start();

            await Task.Delay(10000, stoppingToken);
        }
    }

    private async Task ManageCronTaskExecution()
    {
        var tasks = await _context.CronTaskRepository.GetTasks();

        foreach (var task in tasks)
        {
            if (ShouldExecuteTaskNow(task))
            {
                var scheduledTask = new Task(() =>
                {
                    string taskLoggingTemplate = "taskId: {taskId}, name: {taskName}, userId: {userId}, apiId: {apiId}";
                    _logger.LogInformation(taskLoggingTemplate, task.Id, task.Name, task.UserId, task.ApiId);
                });
                scheduledTask.Start();
            }    
        }
    }

    private bool ShouldExecuteTaskNow(CronTask task)
    {
        CronDate taskCronDate = (CronDate)task;

        return taskCronDate.Matches(DateTime.Now);
    }
}
