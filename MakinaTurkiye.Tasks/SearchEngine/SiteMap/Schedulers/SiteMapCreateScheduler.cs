using MakinaTurkiye.Tasks.Messages.Tasks;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MakinaTurkiye.Tasks.Messages.Schedulers
{
    public class SiteMapCreateScheduler
    {
        public static async void Start()
        {
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SiteMapCreate>()
                            .WithIdentity("SiteMapCreate", "SearchEngine.SiteMap")
                            .Build();

            job.JobDataMap["httpContext"] = HttpContext.Current;
            int Sure = 5;
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                      s.WithIntervalInMinutes(Sure)
                  )
                .Build();
            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
