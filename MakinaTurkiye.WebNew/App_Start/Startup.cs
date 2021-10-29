using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
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
                    LoginPath = new PathString("/uyelik/kullanicigirisi"),
                    CookieSameSite = SameSiteMode.None,
                    CookieHttpOnly = true,
                    CookieSecure = CookieSecureOption.Always
                });
            }
            else
            {
                //app.UseCookieAuthentication(new CookieAuthenticationOptions
                //{
                //    AuthenticationType = "LoginCookie",
                //    LoginPath = new PathString("/uyelik/kullanicigirisi"),
                //    CookieDomain = ".makinaturkiye.com"
                //});

                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = "LoginCookie",
                    LoginPath = new PathString("/uyelik/kullanicigirisi"),
                    CookieSameSite = SameSiteMode.None,
                    CookieHttpOnly = true,
                    CookieSecure = CookieSecureOption.Always
                });
            }


        }
    }

}
