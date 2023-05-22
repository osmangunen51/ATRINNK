using Trinnk.Core.Infrastructure;
using Trinnk.Services.Members;
using Trinnk.Services.Stores;
using Quartz;
using System.Threading.Tasks;

namespace Trinnk.Tasks.Members.Tasks
{
    public class MemberDescriptionRestUpdateDate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IMemberDescriptionService memberDescriptionService = EngineContext.Current.Resolve<IMemberDescriptionService>();
            memberDescriptionService.SP_MemberDescriptionsUpdateDateForRest();
            IStoreSeoNotificationService storeSeoNotificationService = EngineContext.Current.Resolve<IStoreSeoNotificationService>();
            storeSeoNotificationService.SP_StoreSeoNotificationChangeDateForRest();
            return Task.CompletedTask;
        }
    }
}
