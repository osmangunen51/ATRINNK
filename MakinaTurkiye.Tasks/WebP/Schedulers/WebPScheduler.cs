using MakinaTurkiye.Tasks.Members.Tasks;
using MakinaTurkiye.Tasks.WebP.Tasks;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.WebP.Schedulers
{
    public class WebPScheduler
    {
        public static async void Start()
        {
            int commonTime = 5;
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<MakinaTurkiye.Tasks.WebP.Tasks.WebP>()
                        .WithIdentity("MakinaTurkiyeTasksWebPTasksTask", "MakinaTurkiyeTasksWebPTasksTask").Build();
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
