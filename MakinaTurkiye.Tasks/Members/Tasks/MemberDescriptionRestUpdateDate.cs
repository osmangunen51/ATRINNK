using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Members;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Members.Tasks
{
    public class MemberDescriptionRestUpdateDate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IMemberDescriptionService memberDescriptionService = EngineContext.Current.Resolve<IMemberDescriptionService>();
            memberDescriptionService.SP_MemberDescriptionsUpdateDateForRest();
            return Task.CompletedTask;
        }
    }
}
