using Trinnk.Core.Configuration;
using Trinnk.Core.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(NeoSistem.Trinnk.Web.App_Start.Startup))]

namespace NeoSistem.Trinnk.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            TrinnkConfig config = EngineContext.Current.Resolve<TrinnkConfig>();
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
