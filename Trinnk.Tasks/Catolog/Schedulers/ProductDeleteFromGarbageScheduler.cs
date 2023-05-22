using Trinnk.Tasks.Catolog.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.Catolog.Schedulers
{
    public class ProductDeleteFromGarbageScheduler
    {
        public static async void Start()
        {

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<ProductDeleteFromGarbage>()
                .WithIdentity("ProductDeleteFromGarbage", "Products")
                .Build();


            ITrigger trigger = TriggerBuilder.Create()
             .WithDailyTimeIntervalSchedule
               (s =>
                  s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(05, 20))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
