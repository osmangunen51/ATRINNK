using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Tasks.Catolog.Schedulers;
using MakinaTurkiye.Tasks.Members.Schedulers;
using MakinaTurkiye.Tasks.Messages.Schedulers;
using MakinaTurkiye.Tasks.Pinterest.Schedulers;
using MakinaTurkiye.Tasks.SearchEngine.ElasticSearch.Schedulers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MakinaTurkiye.Web.Tasks
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


            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
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
                ProductRateCalculateScheduler.Start();
                SiteMapCreateScheduler.Start();
                PinterestScheduler.Start();
                StoreRateCalculateScheduler.Start();
                MemberDescriptionOrganizerScheduler.Start();
            }
        }
    }
}
