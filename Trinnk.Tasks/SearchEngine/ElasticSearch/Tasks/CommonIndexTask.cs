using Trinnk.Core.Infrastructure;
using Trinnk.Logging;
using Trinnk.Services.Search;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Trinnk.Tasks.SearchEngine.ElasticSearch.Tasks
{
    public class CommonIndexTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            ILogger logger = EngineContext.Current.Resolve<ILogger>();

            try
            {
                ISearchService searchService = EngineContext.Current.Resolve<ISearchService>();
                searchService.CreateAndYukleSearchGenelIndex();
            }
            catch (Exception ex)
            {
                logger.Error("Common index task error", ex);
            }

            return Task.CompletedTask;
        }
    }
}
