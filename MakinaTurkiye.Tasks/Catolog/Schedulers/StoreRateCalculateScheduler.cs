using MakinaTurkiye.Tasks.Catolog.Tasks;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Catolog.Schedulers
{
    public class StoreRateCalculateScheduler
    {
        public static async void Start()
        {

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<StoreRateCalculate>()
                .WithIdentity("StoreRateCalculate", "Stores")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
             .WithDailyTimeIntervalSchedule
               (s =>
                  s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(14, 54))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
