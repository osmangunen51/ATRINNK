using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(NeoSistem.Trinnk.Management.Startup))]

namespace NeoSistem.Trinnk.Management
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
