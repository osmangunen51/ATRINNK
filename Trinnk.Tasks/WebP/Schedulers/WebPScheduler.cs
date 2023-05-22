using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.WebP.Schedulers
{
    public class WebPScheduler
    {
        public static async void Start()
        {
            int commonTime = 5;
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<Trinnk.Tasks.WebP.Tasks.WebP>()
                        .WithIdentity("TrinnkTasksWebPTasksTask", "TrinnkTasksWebPTasksTask").Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(commonTime)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00, 01))
                  )
                .Build();
            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
