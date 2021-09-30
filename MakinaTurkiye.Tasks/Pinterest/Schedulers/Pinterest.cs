using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Tasks.Members.Tasks;
using MakinaTurkiye.Tasks.Pinterest.Tasks;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Pinterest.Schedulers
{
    public class PinterestScheduler
    {
        public static async void Start()
        {
            int commonTime = 5;
            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
            if (config.PinDurum)
            {
                if (config.PinZaman != "")
                {
                    commonTime = Convert.ToInt32(config.PinZaman);
                }
                var scheduler = await new StdSchedulerFactory().GetScheduler();
                await scheduler.Start();
                IJobDetail job = JobBuilder.Create<MakinaTurkiye.Tasks.Pinterest.Tasks.Pinterest>()
                            .WithIdentity("MakinaTurkiyeTasksPinterestTasksTask", "MakinaTurkiyeTasksPinterestTasksTask").Build();
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
