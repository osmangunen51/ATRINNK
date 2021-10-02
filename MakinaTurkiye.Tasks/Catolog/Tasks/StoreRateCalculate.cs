using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Services.Stores;
using Quartz;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Catolog.Tasks
{
    public class StoreRateCalculate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();
            IApplicationLogService applicationLog = EngineContext.Current.Resolve<IApplicationLogService>();

            applicationLog.InsertApplicationLog(new Entities.Tables.Logs.ApplicationLog { Date=DateTime.Now, Thread="test", Message="Store Rate Başladı", Level="test", Exception="test", Logger="l"});

            storeService.CalculateSPStoreRate();
            applicationLog.InsertApplicationLog(new Entities.Tables.Logs.ApplicationLog { Date = DateTime.Now, Thread = "test", Message = "Store Rate Bitti", Level = "test", Exception = "test", Logger = "l" });

            return Task.CompletedTask;
        }
    }
}
