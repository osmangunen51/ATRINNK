using Trinnk.Agent.WebCheck.Schedulers;
using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Trinnk.Web.Agent
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Initialize(false);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            TrinnkConfig config = EngineContext.Current.Resolve<TrinnkConfig>();
            if (config.ApplicationStartingTasksEnabled)
            {
                WebCheckMailSendScheduler.Start();
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            TrinnkConfig config = EngineContext.Current.Resolve<TrinnkConfig>();
            if (config.ApplicationStartingTasksEnabled)
            {
                WebCheckMailSendScheduler.Start();
            }
        }
    }
}
