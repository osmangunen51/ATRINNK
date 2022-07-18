using MakinaTurkiye.Core.Infrastructure;
using NeoSistem.MakinaTurkiye.Management.App_Start;
using NeoSistem.MakinaTurkiye.Management.Models.Validation;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
// Note: For instructions on enabling IIS6 or IIS7 classic mode,
// visit http://go.microsoft.com/?LinkId=9394801

namespace NeoSistem.MakinaTurkiye.Management
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = Context.Request.Cookies[cookieName];
                if (authCookie == null)
                    return;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] roles = authTicket.UserData.Split(new char[] { ',' });
                FormsIdentity fi = (FormsIdentity)(Context.User.Identity);
                Context.User = new GenericPrincipal(fi, roles);
            }
        }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            //JobScheduler.Start();
            //initialize engine context
            EngineContext.Initialize(false);
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredValidationAttribute), typeof(RequiredAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(StringLengthValidationAttribute), typeof(StringLengthAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailValidationAttribute), typeof(RegularExpressionAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RangeValidationAttribute), typeof(RangeAttributeAdapter));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception exception = Server.GetLastError();
            //AppException.SaveException(exception);
        }
    }
}