using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
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
            productService.CalculateSPProductRate();
            return Task.CompletedTask;
        }
    }
}
