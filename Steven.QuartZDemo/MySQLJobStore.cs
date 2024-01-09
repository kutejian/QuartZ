//using Quartz.Impl.AdoJobStore;
//using Quartz.Spi;
//using System.Collections.Specialized;
//using System.Configuration;
//using System.Data;
//using System.Data.Common;
//using MySql.Data.MySqlClient;
//using Quartz;
//using Quartz.Impl.Matchers;

//public class MySQLJobStore : StdAdoDelegate, IJobStore
//{
//    public MySQLJobStore(string dataSource, string tableNamePrefix = null)
//    {
//        var provider = new MySqlDbProvider();

//        var props = new NameValueCollection
//        {
//            { "quartz.dataSource.default.connectionString", $"Server={dataSource};Database=quartz;Uid=root;Pwd=password;" },
//            { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz" },
//            { "quartz.jobStore.tablePrefix", tableNamePrefix ?? "QRTZ_" },
//            { "quartz.jobStore.dataSource", "default" }
//        };

//        Initialize(provider, props);
//    }

//    public bool SupportsPersistence => throw new NotImplementedException();

//    public long EstimatedTimeToReleaseAndAcquireTrigger => throw new NotImplementedException();

//    public bool Clustered => throw new NotImplementedException();

//    public string InstanceId { set => throw new NotImplementedException(); }
//    public string InstanceName { set => throw new NotImplementedException(); }
//    public int ThreadPoolSize { set => throw new NotImplementedException(); }

//    public Task<IReadOnlyCollection<IOperableTrigger>> AcquireNextTriggers(DateTimeOffset noLaterThan, int maxCount, TimeSpan timeWindow, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> CalendarExists(string calName, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> CheckExists(JobKey jobKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> CheckExists(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task ClearAllSchedulingData(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> GetCalendarNames(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> GetJobGroupNames(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<JobKey>> GetJobKeys(GroupMatcher<JobKey> matcher, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<int> GetNumberOfCalendars(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<int> GetNumberOfJobs(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<int> GetNumberOfTriggers(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> GetPausedTriggerGroups(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> GetTriggerGroupNames(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<TriggerKey>> GetTriggerKeys(GroupMatcher<TriggerKey> matcher, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<IOperableTrigger>> GetTriggersForJob(JobKey jobKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<TriggerState> GetTriggerState(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task Initialize(ITypeLoadHelper loadHelper, ISchedulerSignaler signaler, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> IsJobGroupPaused(string groupName, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> IsTriggerGroupPaused(string groupName, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task PauseAll(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task PauseJob(JobKey jobKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> PauseJobs(GroupMatcher<JobKey> matcher, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task PauseTrigger(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> PauseTriggers(GroupMatcher<TriggerKey> matcher, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task ReleaseAcquiredTrigger(IOperableTrigger trigger, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> RemoveCalendar(string calName, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> RemoveJob(JobKey jobKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> RemoveJobs(IReadOnlyCollection<JobKey> jobKeys, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> RemoveTrigger(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> RemoveTriggers(IReadOnlyCollection<TriggerKey> triggerKeys, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> ReplaceTrigger(TriggerKey triggerKey, IOperableTrigger newTrigger, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task ResetTriggerFromErrorState(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task ResumeAll(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task ResumeJob(JobKey jobKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> ResumeJobs(GroupMatcher<JobKey> matcher, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task ResumeTrigger(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<string>> ResumeTriggers(GroupMatcher<TriggerKey> matcher, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<ICalendar?> RetrieveCalendar(string calName, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IJobDetail?> RetrieveJob(JobKey jobKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IOperableTrigger?> RetrieveTrigger(TriggerKey triggerKey, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task SchedulerPaused(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task SchedulerResumed(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task SchedulerStarted(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task Shutdown(CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task StoreCalendar(string name, ICalendar calendar, bool replaceExisting, bool updateTriggers, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task StoreJob(IJobDetail newJob, bool replaceExisting, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public void StoreJobAndTrigger(IJobDetail jobDetail, IOperableTrigger trigger)
//    {
//        base.StoreJobAndTrigger(jobDetail, trigger);
//    }

//    public Task StoreJobAndTrigger(IJobDetail newJob, IOperableTrigger newTrigger, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task StoreJobsAndTriggers(IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> triggersAndJobs, bool replace, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task StoreTrigger(IOperableTrigger newTrigger, bool replaceExisting, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task TriggeredJobComplete(IOperableTrigger trigger, IJobDetail jobDetail, SchedulerInstruction triggerInstCode, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<IReadOnlyCollection<TriggerFiredResult>> TriggersFired(IReadOnlyCollection<IOperableTrigger> triggers, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }
//    // 实现其他的IJobStore接口成员...
//}
