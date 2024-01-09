## [QuartZ](http://www.quartz-scheduler.org/documentation/)

一个强大的、开源的、轻量级的任务调度框架

### 使用场景：

- 数据库刷盘
- 对账系统
- 定时检查
- 延迟计划

### QuartZ五大元素

1. Scheduler：调度器
2. Trigger：时间触发器
3. Job：具体要执行的任务
4. ThreadPool：线程池，专指QuartZ的线程池
5. JobStrore：RawStore内存存储，DBStore数据库存储

### 开始实操：

#### 老规矩，第一步，导包：

```C#
   <ItemGroup>
     <PackageReference Include="Quartz" Version="3.6.2" />
   </ItemGroup>
```

#### 第二步，上代码：

```C#
   IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
     //可以直接new，同理
     //StdSchedulerFactory factory = new();
     //IScheduler scheduler = factory.GetScheduler().Result;
     scheduler.Start();
     {
         //var job = JobBuilder.Create<MyJob>()
         //    .WithIdentity("myjob", "group")
         //    .Build()
         //    ;
 
         //var triggerBuild = TriggerBuilder.Create()
         //    //.StartAt(DateTime.Now)
         //    .StartNow()
         //    .WithPriority(10)
         //    .ForJob(job)
         //    .WithIdentity("myjob", "group")
         //    .WithSimpleSchedule(opt =>
         //    {
         //        opt.WithIntervalInSeconds(5).WithRepeatCount(3);
         //    })
         //    ;
 
         //var trigger = triggerBuild.Build();
         //scheduler.ScheduleJob(job, trigger);
     }
```

#### 第三步，增加继承Job的类：

```C#
 public class MyJob : IJob
 {
     public async Task Execute(IJobExecutionContext context)
     {
         Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}  Myjob excute:"+ DateTime.Now);
         await Task.CompletedTask;
     }
 }
```

#### 指定时间的操作：

```C#
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
     .WithSimpleSchedule(opt =>
     {
         opt.WithIntervalInSeconds(5).WithRepeatCount(3);
     })
     //具体到每天什么时候执行
     .WithCalendarIntervalSchedule(time => time.WithIntervalInHours(1))
     //WithDailyTimeIntervalSchedule 主要用于指定每周的某几天执行，如我们想让每周的周六周日的8: 00 - 20:00，每两秒执行一次，创建触发器d如下
     .WithDailyTimeIntervalSchedule(time =>
     {
         time.OnDaysOfTheWeek(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Wednesday })
         .StartingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(8, 00, 00))//8点开始
         .EndingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(20, 00, 00))//20点开始
         .WithIntervalInSeconds(2)//间隔2秒即执行
         .WithRepeatCount(3);//执行3次
     })
     ;
 
 var trigger = triggerBuild.Build();
 scheduler.ScheduleJob(job, trigger);
```

#### Corn表达式：

```C#
         //var job = JobBuilder.Create<MyJob>()
         //    .WithIdentity("myjob", "group")
         //    .Build()
         //    ;
 
         //var triggerBuild = TriggerBuilder.Create()
         //    //.StartAt(DateTime.Now)
         //    .StartNow()
         //    .WithPriority(10)
         //    .ForJob(job)
         //    .WithIdentity("myjob", "group")
         //    //Cron表达式
         //    .WithCronSchedule("* * * * * ? *")
         //    ;
 
         //var trigger = triggerBuild.Build();
         //scheduler.ScheduleJob(job, trigger);
```

#### 反射获取：

```C#
 Assembly assembly = Assembly.LoadFrom("QuartZJobExten.dll");
 var job = JobBuilder.Create(assembly.GetType("QuartZJobExten.CustomRefreshDataJob"))
         .WithIdentity("myjob1", "group1")
         .Build()
         ;
 
         var triggerBuild = TriggerBuilder.Create()
             .StartAt(DateTime.Now)
             .StartNow()
             .WithPriority(10)
         .ForJob(job)
         .WithIdentity("myjob1", "group1")
         //    //Cron表达式
         .WithCronSchedule("* * * * * ? *")
         ;
 
         var trigger = triggerBuild.Build();
         scheduler.ScheduleJob(job, trigger);
```

#### 排除（避开）时间段：

```C#
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
             //第一步，配置增加名称（允许配置多项）
             .ModifiedByCalendar("myCalendar")
             ;
 
         //第二步，配置避开某个时间段
         DailyCalendar dailyCalendar = new DailyCalendar(DateBuilder.DateOf(16, 07, 31).DateTime,
             DateBuilder.DateOf(16, 07, 35).DateTime);
         scheduler.AddCalendar("myCalendar", dailyCalendar, true, true);
        //支持CronCalendar
        // CronCalendar cronCalendar = new CronCalendar("* * * * * ? *");
         var trigger = triggerBuild.Build();
         scheduler.ScheduleJob(job, trigger);
 
 
 
```

### QuartZ里面的AOP：

#### JobAop

##### 第一步，继承IJobListener：

```C#
 
 public class StevenJobListener : IJobListener
 {
     public string Name => "StevenJobListener";
 
     public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven JobExecutionVetoed 333333333333:{DateTime.Now}");
         await Task.CompletedTask;
     }
     /// <summary>
     /// Before
     /// </summary>
     /// <param name="context"></param>
     /// <param name="cancellationToken"></param>
     /// <returns></returns>
     public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven JobToBeExecuted 11111111111:{DateTime.Now}");
         await Task.CompletedTask;
     }
     /// <summary>
     /// After
     /// </summary>
     /// <param name="context"></param>
     /// <param name="jobException"></param>
     /// <param name="cancellationToken"></param>
     /// <returns></returns>
     public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven JobWasExecuted 22222222222:{DateTime.Now}");
         await Task.CompletedTask;
     }
 }
 
```

##### 第二步，增加监听：

```C#
 //调用方增加Scheduler监听
 scheduler.ListenerManager.AddJobListener(new StevenJobListener());
```

#### TriggerAop：

##### 第一步，继承ITriggerListener：

```C#
 
 public class StevenTriggerListener : ITriggerListener
 {
     public string Name => "StevenTriggerListener";
 
     public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven TriggerComplete 333333333333:{DateTime.Now}");
         await Task.CompletedTask;
     }
 
     public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven TriggerFired 111111111111:{DateTime.Now}");
         await Task.CompletedTask;
     }
 
     /// <summary>
     /// 只有Trigger失败才会被调用，由Scheduler调用
     /// </summary>
     /// <param name="trigger"></param>
     /// <param name="cancellationToken"></param>
     /// <returns></returns>
     public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven TriggerMisfired 44444444444:{DateTime.Now}");
         await Task.CompletedTask;
     }
 
     public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
     {
         Console.WriteLine($"Steven VetoJobExecution 22222222222:{DateTime.Now}");
         //如果直接返回为True，则标识任务取消
         //return await Task.FromResult(true);
         return await Task.FromResult(cancellationToken.IsCancellationRequested);
     }
 }
```

##### 第二步，增加监听：

```C#
 //调用方增加Scheduler监听
 scheduler.ListenerManager.AddTriggerListener(new StevenTriggerListener());
```

#### SchedulerAop：

第一步，继承ITriggerListener：

```C#
 //接口中方法较多，就不详述了，操作同理
 public class StevenSchedulerListener : ISchedulerListener
```

##### 第二步，增加监听：

```C#
 //调用方增加Scheduler监听
 scheduler.ListenerManager.AddSchedulerListener(new StevenSchedulerListener());
```

---

### QuartZ持久化：

第一步，导包：

```C#
 //缺哪个导哪个，不能少：
   <ItemGroup>
     <PackageReference Include="Microsoft.Data.SqlClient" Version="5.1.1" />
     <PackageReference Include="Quartz" Version="3.6.2" />
     <PackageReference Include="Quartz.Serialization.Json" Version="3.6.2" />
   </ItemGroup>
```

第二步，配置参数：

```C#
 //（可用配置文件方式，这里使用代码为例）
 
 ///下面是SqlServer 的连接示例
    NameValueCollection pars = new NameValueCollection
         {
             //scheduler名字
             ["quartz.scheduler.instanceName"] = "MySchedulerAdvanced",
             //线程池个数
             ["quartz.threadPool.threadCount"] = "20",
             //类型为JobStoreXT,事务
             ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
             //JobDataMap中的数据都是字符串
             //["quartz.jobStore.useProperties"] = "true",
             //数据源名称
             ["quartz.jobStore.dataSource"] = "QuartzDb",
             //数据表名前缀
             ["quartz.jobStore.tablePrefix"] = "QRTZ_",
             //使用Sqlserver的Ado操作代理类
             ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
             //数据源连接字符串
             ["quartz.dataSource.QuartzDb.connectionString"] = @"Server=192.168.0.12;Database=quartzDb;Uid=sa;Pwd=qwer1234.;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True; Connection Timeout=30",
             //数据源的数据库
             ["quartz.dataSource.QuartzDb.provider"] = "SqlServer",
             //序列化类型
             ["quartz.serializer.type"] = "json",//binary
                                                 //自动生成scheduler实例ID，主要为了保证集群中的实例具有唯一标识
             ["quartz.scheduler.instanceId"] = "AUTO",
             //是否配置集群
             ["quartz.jobStore.clustered"] = "false",
         };
```

---