using Trinnk.Tasks.SearchEngine.ElasticSearch.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.SearchEngine.ElasticSearch.Schedulers
{
    public class CategoryProductCountUpdateScheduler
    {
        public static async void Start()
        {
            int productCountUpdate = 15;
            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CategoryProductCountUpdate>()
                            .WithIdentity("CategoryProductCountUpdate", "SearchEngine.ElasticSearch").Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithDailyTimeIntervalSchedule
            //      (s =>
            //         s.WithIntervalInMinutes(productCountUpdate)
            //        .OnEveryDay()
            //        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00, 01))
            //      )
            //    .Build();

            ITrigger trigger = TriggerBuilder.Create()
            .StartNow()
            .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(120)
            .RepeatForever())
            .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
