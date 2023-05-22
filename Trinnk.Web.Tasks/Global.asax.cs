using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Trinnk.Tasks.Catolog.Schedulers;
using Trinnk.Tasks.Members.Schedulers;
using Trinnk.Tasks.Messages.Schedulers;
using Trinnk.Tasks.Pinterest.Schedulers;
using Trinnk.Tasks.SearchEngine.ElasticSearch.Schedulers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Trinnk.Web.Tasks
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //initialize engine context
            EngineContext.Initialize(false);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            TrinnkConfig config = EngineContext.Current.Resolve<TrinnkConfig>();
            if (config.ApplicationStartingTasksEnabled)
            {
                //Messages tasks
                PacketReminderMailSendScheduler.Start();
                StoreWeeklyStatisticMailSendScheduler.Start();

                //ElasticSearch engine tasks
                CategoryProductCountUpdateScheduler.Start();
                CommonIndexTaskScheduler.Start();
                SuggestIndexTaskScheduler.Start();
                ProductDeleteFromGarbageScheduler.Start();
                ProductHomePageReminderMailSendScheduler.Start();
                MemberDescriptionRestScheduler.Start();
                SiteMapCreateScheduler.Start();
                PinterestScheduler.Start();
                StoreRateCalculateScheduler.Start();
                ProductRateCalculateScheduler.Start();
                MemberDescriptionOrganizerScheduler.Start();
            }
        }
    }
}
