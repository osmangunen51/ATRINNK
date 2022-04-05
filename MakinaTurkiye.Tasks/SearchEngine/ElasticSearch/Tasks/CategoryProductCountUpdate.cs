using Quartz;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.SearchEngine.ElasticSearch.Tasks
{
    public class CategoryProductCountUpdate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
