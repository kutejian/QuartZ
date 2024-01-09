using Quartz.Impl;
using Quartz;
using Steven.QuartZDemo.QuartZJob;
using Steven.QuartZDemo.QuartZListener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using static System.Net.WebRequestMethods;
using System.Configuration;
using System.Drawing;
namespace Steven.QuartZDemo;

public class DbStoreQuartZ
{
    public void Show()
    {
        //配置保存到数据库中去
        #region 配置固化到数据库 
        NameValueCollection pars = new NameValueCollection
        {
            ["quartz.scheduler.instanceName"] = "MySchedulerAdvanced",
            //线程池个数
            ["quartz.threadPool.threadCount"] = "20",
            //类型为JobStoreXT,事务
            ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
            //JobDataMap中的数据都是字符串
            ["quartz.jobStore.useProperties"] = "true",
            //数据源名称
            ["quartz.jobStore.dataSource"] = "QuartzDb",
            //数据表名前缀
            ["quartz.jobStore.tablePrefix"] = "QRTZ_",
            //使用Sqlserver的Ado操作代理类
            ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
            //数据源连接字符串
            ["quartz.dataSource.QuartzDb.connectionString"] = @"TrustServerCertificate=true;server=.;database=QuartzDb;uid=sa;pwd=123",
            //数据源的数据库
            ["quartz.dataSource.QuartzDb.provider"] = "SqlServer",
            //序列化类型
            ["quartz.serializer.type"] = "json",//binary
                                                //自动生成scheduler实例ID，主要为了保证集群中的实例具有唯一标识
            ["quartz.scheduler.instanceId"] = "AUTO",
            //是否配置集群
            ["quartz.jobStore.clustered"] = "false",
        };
        #endregion
        StdSchedulerFactory stdSchedulerFactory = new StdSchedulerFactory(pars);
        IScheduler scheduler = stdSchedulerFactory.GetScheduler().Result;
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

        var trigger = triggerBuild.Build();
        scheduler.ScheduleJob(job, trigger);
    }
}
