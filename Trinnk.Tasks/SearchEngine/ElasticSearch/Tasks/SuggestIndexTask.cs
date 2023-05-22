using Trinnk.Core.Infrastructure;
using Trinnk.Logging;
using Trinnk.Services.Search;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Trinnk.Tasks.SearchEngine.ElasticSearch.Tasks
{
    public class SuggestIndexTask : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            ILogger logger = EngineContext.Current.Resolve<ILogger>();

            try
            {
                ISearchService searchService = EngineContext.Current.Resolve<ISearchService>();
                searchService.CreateAndYukleSuggestSearchIndex();
                searchService.CreateAndYukleSearchGenelIndex();
            }
            catch (Exception ex)
            {
                logger.Error("Suggest index task error", ex);
            }

            return Task.CompletedTask;
        }
    }
}
