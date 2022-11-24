using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Data;
using TaskManager.Scheduler.ApiHandlers;
using TaskManager.Scheduler.ApiRetrievers;

namespace TaskManager.Scheduler;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private DatabaseContext _context;
    private readonly IConfiguration _config;
    private Dictionary<string, Type> _apiHandlerTypes = new Dictionary<string, Type>();

    public Worker(ILogger<Worker> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetupEmailDefaultSender();
        _apiHandlerTypes = CreateApiHandlerTypesDictionary();

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

    private void SetupEmailDefaultSender()
    {
        var sender = new SmtpSender(() => new SmtpClient("localhost")
        {
            EnableSsl = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Port = 25
        });

        Email.DefaultSender = sender;
    }

    private Dictionary<string, Type> CreateApiHandlerTypesDictionary()
    {
        Type[] apiHandlerRelativeTypes = GetApiHandlerAttrChildTypes();
        Dictionary<string, Type> dict = new();
        foreach (var type in apiHandlerRelativeTypes)
        {
            string apiHandlerTypeName = GetApiHandlerTypeName();
            string typeName = type.Name;
            if (typeName.Contains(apiHandlerTypeName))
            {
                typeName = new string(typeName.SkipLast(apiHandlerTypeName.Length).ToArray());
            }

            dict.Add(typeName, type);
        }
        return dict;
    }

    private Type[] GetApiHandlerAttrChildTypes()
    {
        var assembly = typeof(Worker).Module.Assembly;
        return assembly.GetTypes()
            .Where(t =>
            {
                var apiHandlerAttr = t.GetTypeInfo()
                    .GetCustomAttributes<ApiHandlerAttribute>(inherit: true).FirstOrDefault();
                bool isApiHandlerChildType = t.IsClass
                    && t != typeof(ApiHandler<>)
                    && apiHandlerAttr is not null;
                if (isApiHandlerChildType) return true;
                return false;
            })
            .ToArray();
    }

    private string GetApiHandlerTypeName()
    {
        Regex regex = new(@"\w*");
        return regex.Match(typeof(ApiHandler<>).Name).Value;
    }

    private async Task ManageCronTaskExecution()
    {
        var tasks = await _context.CronTaskRepository.GetFullTasks();

        foreach (var task in tasks)
        {
            if (ShouldExecuteTaskNow(task))
            {
                var scheduledTask = new Task(async () =>
                {
                    await ExecuteCronTask(task);
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

    private async Task ExecuteCronTask(CronTask task)
    {
        Type? apiHandlerType; 
        _apiHandlerTypes.TryGetValue(task.Api.Name, out apiHandlerType);

        if (apiHandlerType is null) return;

        var apiHandler = Activator.CreateInstance(apiHandlerType, task) as IApiHandlerInvoker;
        try
        {
            await apiHandler!.InvokeAsync();
        }
        catch (HttpRequestException ex) { }
    }
}
