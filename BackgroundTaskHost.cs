using System.Collections.Concurrent;

public class BackgroundTaskHost : IHostedService
{
    private readonly ILogger _logger;
    private ConcurrentDictionary<int, Task> _backgroundTasks = new();

    public BackgroundTaskHost(ILogger<BackgroundTaskHost> logger)
    {
        _logger = logger;
    }
    
    public void RegisterTask(Task t)
    {
        if (_backgroundTasks.TryAdd(t.Id, t))
        {
            t.ContinueWith(ContinuationAction);
            t.Start();
            _logger.LogInformation($"Added new Task {t.Id}");
        }
        else
        {
            // todo
            _logger.LogWarning($"Problem adding Task {t.Id} !!!");
        }
    }

    public Dictionary<int, string> BackgroundTasksStatuses() =>
        _backgroundTasks.ToDictionary(x => x.Key, x => x.Value.Status.ToString());

    private void ContinuationAction(Task arg1)
    {
        _logger.LogInformation($"Task {arg1.Id} completed with state {arg1.Status}");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BackgroundTaskHost started");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"BackgroundTaskHost stopping...");

        var tasks = _backgroundTasks.Select(x => x.Value).ToArray();
        var runningTasks = tasks.Where(x => x.Status <= TaskStatus.Running).ToList();

        while (runningTasks.Any() && !cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Waiting for tasks to complete: {string.Join(',', runningTasks.Select(x=>x.Id.ToString()))}");
            cancellationToken.WaitHandle.WaitOne(5000);
            runningTasks = tasks.Where(x => x.Status <= TaskStatus.Running).ToList();
        }
        
        _logger.LogInformation($"BackgroundTaskHost stopped");
        return Task.CompletedTask;
    }
}