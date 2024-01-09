using Quartz.Impl.Calendar;
using Quartz.Impl;
using Quartz;
using Steven.QuartZDemo.QuartZJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Steven.QuartZDemo;

public class BasicQuartZ
{
    public void Show()
    {
        //创建调度器
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        //StdSchedulerFactory factory = new();
        //IScheduler scheduler = factory.GetScheduler().Result;
        //启动调度器
        scheduler.Start();
        {
            //创建具体要执行的任务  MyJob是你自己创建的类 他需要继承IJob类
            var job = JobBuilder.Create<MyJob>()
                //为任务设置唯一标识的方法
                .WithIdentity("myjob", "group")
                .Build();


            var triggerBuild = TriggerBuilder.Create()

                //: 设置触发器的开始时间，这里表示任务将在当前时间的2秒后开始执行。
                .StartAt(DateTime.Now.AddSeconds(2))

                //.StartNow() 与StartAt相对应，表示立即开始执行任务。

                //设置触发器的优先级。优先级越高，任务在等待执行时被选中的可能性就越大。在多个任务需要执行时，具有较高优先级的任务将被优先执行。
                .WithPriority(10)

                //与哪个任务相关联，这里表示与前面创建的名为 "myjob" 的任务相关联。
                .ForJob(job)

                //为触发器设置唯一标识，这里的标识符与任务的标识符相对应，确保了唯一性。
                .WithIdentity("myjob", "group")

                //设置触发器的简单调度。在这里，它表示任务将在每秒触发一次，并一直重复下去。
                .WithSimpleSchedule(opt =>
                {
                    opt.WithIntervalInSeconds(1).RepeatForever();
                });

            var trigger = triggerBuild.Build();
            Console.WriteLine(DateTime.Now);
            scheduler.ScheduleJob(job, trigger);
        }
        {
/*            var job = JobBuilder.Create<MyJob>()
                .WithIdentity("myjob", "group")
                .Build();

            var triggerBuild = TriggerBuilder.Create()
                //.StartAt(DateTime.Now)
                .StartNow()
                .WithPriority(10)
                .ForJob(job)
                .WithIdentity("myjob", "group")
                .WithSimpleSchedule(opt =>
                {
                    //这里是每秒触发一次 一共会触发4次 他本身会触发一次
                    opt.WithIntervalInSeconds(5).WithRepeatCount(3);
                })
                //下面的规则是具体到每天什么时候执行
                //表示任务将在每周的周六、周日和周四的8:00 AM到8:00 PM之间，每两秒执行一次，总共执行3次。

                //它表示每隔1小时触发一次任务
                .WithCalendarIntervalSchedule(time => time.WithIntervalInHours(1))
                //WithDailyTimeIntervalSchedule 主要用于指定每周的某几天执行，如我们想让每周的周六周日的8: 00 - 20:00，每两秒执行一次，创建触发器d如下
                .WithDailyTimeIntervalSchedule(time =>
                {
                    time.OnDaysOfTheWeek(new DayOfWeek[] { DayOfWeek.Friday, DayOfWeek.Sunday, DayOfWeek.Thursday })
                    .StartingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(8, 00, 00))//8点开始
                    .EndingDailyAt(TimeOfDay.HourMinuteAndSecondOfDay(22, 00, 00))//20点开始
                    .WithIntervalInSeconds(2)//间隔2秒即执行
                    .WithRepeatCount(3);//执行3次
                });

            var trigger = triggerBuild.Build();
            scheduler.ScheduleJob(job, trigger);*/
        }
        {
/*            var job = JobBuilder.Create<MyJob>()
                .WithIdentity("myjob", "group")
                .Build()
                ;

            var triggerBuild = TriggerBuilder.Create()
                //.StartAt(DateTime.Now)
                .StartNow()
                .WithPriority(10)
                .ForJob(job)
                .WithIdentity("myjob", "group")
                //Cron表达式 具体还有什么表达式百度
                .WithCronSchedule("* * * * * ? *");

            var trigger = triggerBuild.Build();
            scheduler.ScheduleJob(job, trigger);*/
        }
        {
/*            //这是根据反射执行你里面的事情
            Assembly assembly = Assembly.LoadFrom("QuartZJobExten.dll");
            var job = JobBuilder.Create(assembly.GetType("QuartZJobExten.CustomRefreshDataJob"))
                .WithIdentity("myjob1", "group1")
                .Build()
                ;

            var triggerBuild = TriggerBuilder.Create()
                //.StartAt(DateTime.Now)
                .StartNow()
                .WithPriority(10)
                .ForJob(job)
                .WithIdentity("myjob1", "group1")
                //Cron表达式
                .WithCronSchedule("* * * * * ? *")
                ;

            var trigger = triggerBuild.Build();
            scheduler.ScheduleJob(job, trigger);*/
        }
        {
            //添加日历格式 让他避开某个时间段后执行
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
                //第一步，   添加日历格式配置  名称是要跟下面的对应
                .ModifiedByCalendar("myCalendar");

            //第二步，  创建一个日历  配置避开某个时间段  
            DailyCalendar dailyCalendar = new DailyCalendar(DateBuilder.DateOf(09, 34, 31).DateTime,
                DateBuilder.DateOf(09, 34, 35).DateTime);

            //这句话和上面一样 都是可以当参数传过去 具体要怎么样百度
            CronCalendar cronCalendar = new CronCalendar("* * * * * ? *");

            //Quartz Scheduler 中添加一个名为 "myCalendar" 的日历
            scheduler.AddCalendar("myCalendar", dailyCalendar 
                //cronCalendar
            , true, true);



            var trigger = triggerBuild.Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
