using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace Job
{
    public class ScheduleService
    {
        private readonly IScheduler _scheduler;

        public ScheduleService()
        {
            var props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };

            var factory = new StdSchedulerFactory(props);
            _scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Start()
        {
            _scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();

            ScheduleJobs();
        }

        public void ScheduleJobs()
        {
            var job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3)
                    .RepeatForever())
                .Build();

            _scheduler.ScheduleJob(job, trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Stop()
        {
            _scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
