using MakinaTurkiye.Tasks.Catolog.Tasks;
using Quartz;
using Quartz.Impl;

namespace MakinaTurkiye.Tasks.Catolog.Schedulers
{
    public class ProductRateCalculateScheduler
    {
        public static async void Start()
        {

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<ProductRateCalculate>()
                .WithIdentity("ProductRateCalculate", "Products")
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
             .WithDailyTimeIntervalSchedule
               (s =>
                  s.WithIntervalInHours(24)
                 .OnEveryDay()
                 .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(22, 34))
               )
             .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
