﻿using MakinaTurkiye.Tasks.Members.Tasks;
using Quartz;
using Quartz.Impl;

namespace MakinaTurkiye.Tasks.Members.Schedulers
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
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00,10))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
