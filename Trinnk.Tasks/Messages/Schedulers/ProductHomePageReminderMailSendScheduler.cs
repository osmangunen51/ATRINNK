using Trinnk.Tasks.Messages.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.Messages.Schedulers
{
    public class ProductHomePageReminderMailSendScheduler
    {
        public static async void Start()
        {

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<ProductHomePageReminderMailSend>()
                .WithIdentity("ProductHomePageReminderMailSend", "Messages")
                .Build();


            ITrigger trigger = TriggerBuilder.Create()
             .WithDailyTimeIntervalSchedule
               (s =>
                  s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(08, 30))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
