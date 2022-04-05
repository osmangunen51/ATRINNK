using MakinaTurkiye.Tasks.SearchEngine.ElasticSearch.Tasks;
using Quartz;
using Quartz.Impl;

namespace MakinaTurkiye.Tasks.SearchEngine.ElasticSearch.Schedulers
{
    public class CommonIndexTaskScheduler
    {
        public static async void Start()
        {
            int commonTime = 20;

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CommonIndexTask>()
                        .WithIdentity("CommonIndexTask", "SearchEngine.ElasticSearch").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(commonTime)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00, 01))
                  )
                .Build();

            //ITrigger trigger = TriggerBuilder.Create()
            //.StartNow()
            //.WithSimpleSchedule(x => x
            //.WithIntervalInSeconds(120)
            //.RepeatForever())
            //.Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
