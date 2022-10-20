using MakinaTurkiye.Tasks.Messages.Tasks;
using Quartz;
using Quartz.Impl;
using System.Web;

namespace MakinaTurkiye.Tasks.Messages.Schedulers
{
    public class WebCheckMailSendScheduler
    {
        public static async void Start()
        {
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<WebCheckMailSend>()
                            .WithIdentity("StoreWeeklyStatisticMailSend", "Messages")
                            .Build();

            job.JobDataMap["httpContext"] = HttpContext.Current;

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                      s.WithIntervalInHours(12)
                     .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 40))
                  )
                .Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //.StartNow()
            //.WithSimpleSchedule(x => x
            //.WithIntervalInSeconds(40)
            //.RepeatForever())
            //.Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
