using MakinaTurkiye.Agent.WebCheck.Tasks;
using Quartz;
using Quartz.Impl;
using System.Web;

namespace MakinaTurkiye.Agent.WebCheck.Schedulers
{
    public class WebCheckMailSendScheduler
    {
        public static async void Start()
        {
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<WebCheckMailSend>()
                            .WithIdentity("WebCheckMailSend", "WebCheckMailSend")
                            .Build();

            job.JobDataMap["httpContext"] = HttpContext.Current;

            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(5)
                  )
                .Build();
            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
