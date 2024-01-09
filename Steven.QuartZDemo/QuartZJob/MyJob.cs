using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steven.QuartZDemo.QuartZJob;

public class MyJob : IJob
{
    public MyJob()
    {
        Console.WriteLine("Myjob 构造函数");
    }
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}  Myjob excute:"+ DateTime.Now);
        await Task.CompletedTask;
    }
}
