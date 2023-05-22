using Trinnk.Tasks.Members.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.Members.Schedulers
{
    public class MemberDescriptionOrganizerScheduler
    {
        public static async void Start()
        {

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<MemberDescriptionOrganizer>()
                .WithIdentity("MemberDescriptionOrganizer", "Members")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
             .WithDailyTimeIntervalSchedule
               (s =>
                  s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(02,00))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
