using Quartz;

namespace QuartZJobExten;

public class CustomRefreshDataJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"这是一个新的任务类:{nameof(CustomRefreshDataJob)},CurrentTime:{DateTime.Now}");
        await Task.CompletedTask;
    }
}