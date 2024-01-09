using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steven.QuartZDemo.QuartZJob;
using Steven.QuartZDemo.QuartZListener;

namespace Steven.QuartZDemo;

public class QuartZAop
{
    public void AopSHow()
    {
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        scheduler.Start();

        var job = JobBuilder.Create<MyJob>()
            .WithIdentity("myjob", "group")
            .Build()
            ;

        var triggerBuild = TriggerBuilder.Create()
            //.StartAt(DateTime.Now)
            .StartNow()
            .WithPriority(10)
            .ForJob(job)
            .WithIdentity("myjob", "group")
            .WithCronSchedule("* * * * * ? *")
            ;
        //下面3个都是监听器 也就是Aop 在执行之前做什么

        //触发器监听器
        scheduler.ListenerManager.AddTriggerListener(new StevenTriggerListener());

        //调度器监听器  需要实现的方法有很多
        //scheduler.ListenerManager.AddSchedulerListener(new StevenSchedulerListener());

        //任务监听器
        scheduler.ListenerManager.AddJobListener(new StevenJobListener());

        var trigger = triggerBuild.Build();
        scheduler.ScheduleJob(job, trigger);


    }
}
