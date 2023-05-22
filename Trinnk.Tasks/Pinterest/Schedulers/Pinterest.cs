using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Quartz;
using Quartz.Impl;
using System;

namespace Trinnk.Tasks.Pinterest.Schedulers
{
    public class PinterestScheduler
    {
        public static async void Start()
        {
            int commonTime = 5;
            TrinnkConfig config = EngineContext.Current.Resolve<TrinnkConfig>();
            if (config.PinDurum)
            {
                if (config.PinZaman != "")
                {
                    commonTime = Convert.ToInt32(config.PinZaman);
                }
                var scheduler = await new StdSchedulerFactory().GetScheduler();
                await scheduler.Start();
                IJobDetail job = JobBuilder.Create<Trinnk.Tasks.Pinterest.Tasks.Pinterest>()
                            .WithIdentity("TrinnkTasksPinterestTasksTask", "TrinnkTasksPinterestTasksTask").Build();
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
}
