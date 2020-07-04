using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Logging;
using MakinaTurkiye.Services.Search;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.SearchEngine.ElasticSearch.Tasks
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
            catch(Exception ex)
            {
                logger.Error("Suggest index task error", ex);
            }

            return Task.CompletedTask;
        }
    }
}
