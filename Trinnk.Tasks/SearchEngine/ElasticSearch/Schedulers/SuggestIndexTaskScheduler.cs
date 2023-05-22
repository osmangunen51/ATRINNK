using Trinnk.Tasks.SearchEngine.ElasticSearch.Tasks;
using Quartz;
using Quartz.Impl;

namespace Trinnk.Tasks.SearchEngine.ElasticSearch.Schedulers
{
    public class SuggestIndexTaskScheduler
    {
        public static async void Start()
        {
            int indexTime = 10;

            var scheduler = await new StdSchedulerFactory().GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SuggestIndexTask>()
                            .WithIdentity("SuggestIndexTask", "SearchEngine.ElasticSearch").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(indexTime)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(00, 01))
                  )
                .Build();

            var result = await scheduler.ScheduleJob(job, trigger);
        }
    }
}
