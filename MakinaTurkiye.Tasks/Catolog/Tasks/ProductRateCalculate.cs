using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Logs;
using Quartz;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Catolog.Tasks
{
    public class ProductRateCalculate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IProductService productService = EngineContext.Current.Resolve<IProductService>();
            IApplicationLogService applicationLog = EngineContext.Current.Resolve<IApplicationLogService>();

            applicationLog.InsertApplicationLog(new Entities.Tables.Logs.ApplicationLog { Date=DateTime.Now, Thread="test", Message="Product Rate Başladı", Level="test", Exception="test", Logger="l"});

            productService.CalculateSPProductRate();
            applicationLog.InsertApplicationLog(new Entities.Tables.Logs.ApplicationLog { Date = DateTime.Now, Thread = "test", Message = "Product Rate Bitti", Level = "test", Exception = "test", Logger = "l" });

            return Task.CompletedTask;
        }
    }
}
