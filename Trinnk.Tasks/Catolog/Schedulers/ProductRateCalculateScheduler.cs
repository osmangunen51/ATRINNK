using Trinnk.Tasks.Catolog.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.Catolog.Schedulers
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
           .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(60)
                .RepeatForever())
            .Build();
            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
