using Quartz;
using Quartz.Impl;
using System.Diagnostics;

namespace NeoSistem.MakinaTurkiye.Management.Jobs
{
    public class JobScheduler
    {
        public static async void Start()
        {
            try
            {

                var scheduler = await new StdSchedulerFactory().GetScheduler();
                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<PacketReminderMailSendJob>()
                    .WithIdentity("PacketReminderOne", "Message")
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
            catch (SchedulerException se)
            {
                Debug.WriteLine(se.InnerException);
            }
        }
    }
}