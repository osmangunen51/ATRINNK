using Trinnk.Tasks.Messages.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.Messages.Schedulers
{
    public class PacketReminderMailSendScheduler
    {
        public static async void Start()
        {

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<PacketReminderMailSend>()
                .WithIdentity("PacketReminderMailSend", "Messages")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
             .WithDailyTimeIntervalSchedule
               (s =>
                  s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 00))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
