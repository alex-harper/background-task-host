using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<HostOptions>(options =>
{
    options.ShutdownTimeout = TimeSpan.FromSeconds(90);
});
builder.Logging.ClearProviders();
builder.Logging.AddSystemdConsole(options =>
{
    options.IncludeScopes = false;
    options.TimestampFormat = "HH:mm:ss ";
});

builder.Services.AddSingleton<BackgroundTaskHost>();
builder.Services.AddHostedService<BackgroundTaskHost>(provider => provider.GetService<BackgroundTaskHost>()!);

var app = builder.Build();

app.MapGet("/task", ([FromServices] BackgroundTaskHost taskHost) =>
{
    var task = new Task(() =>
    {
        Thread.Sleep(30000);
        var error = new Random().Next(0, 100) >= 90;
        if (error)
        {
            throw new Exception("Long running task had an exception");
        }
        else
        {
            app.Logger.LogInformation("Long running task finished doing a thing");
        }
    });
    taskHost.RegisterTask(task);
    return Results.Ok($"Task {task.Id} created at {DateTime.Now:f}");
});

app.MapGet("/tasks",
    ([FromServices] BackgroundTaskHost taskHost) => Results.Ok(taskHost.BackgroundTasksStatuses()));

app.Run();