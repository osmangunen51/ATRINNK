using System;
using System.Threading.Tasks;
using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(NeoSistem.MakinaTurkiye.Web.App_Start.Startup))]

namespace NeoSistem.MakinaTurkiye.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
            if (config.ApplicationTestModeEnabled)
            {
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "LoginCookie",
                    LoginPath = new PathString("/uyelik/kullanicigirisi")
                });
            }
            else
            {
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "LoginCookie",
                    LoginPath = new PathString("/uyelik/kullanicigirisi"),
                    CookieDomain = ".makinaturkiye.com"
                });
            }


        }
    }

}
