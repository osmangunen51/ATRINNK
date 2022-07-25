using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(NeoSistem.MakinaTurkiye.Management.Startup))]

namespace NeoSistem.MakinaTurkiye.Management
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //app.MapHubs(new Microsoft.AspNet.SignalR.HubConfiguration()
            //{
            //    EnableDetailedErrors = true
            //});
        }
    }
}
