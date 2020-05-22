using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services;
using MakinaTurkiye.Services.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace MakinaTurkiye.Utilities.Controllers
{
    public abstract class BaseAccountController : BaseController
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //IAuthenticationService authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            //var authenticatedMember= authenticationService.GetAuthenticatedMember();
            //if(authenticatedMember==null)
            //    requestContext.HttpContext.Response.Redirect("/");

            //Cache operations
        }

        protected void UpdateClass<TFromClass, TToClass>(TFromClass fromClass, TToClass toClass)
        {
            var toType = typeof(TToClass);
            var fromType = typeof(TFromClass);

            var toProperties = toType.GetProperties();
            var fromProperties = fromType.GetProperties();

            foreach (var item in fromProperties)
            {
                var prop = toProperties.SingleOrDefault(p => p.Name == item.Name);
                if (prop != null)
                {
                    prop.SetValue(toClass, item.GetValue(fromClass, null), null);
                }
            }
        }

        protected static string RenderPartialToString(string controlName, object viewData)
        {
            ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext() };

            viewPage.ViewData = new ViewDataDictionary(viewData);
            viewPage.Controls.Add(viewPage.LoadControl(controlName));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    viewPage.RenderControl(tw);
                }
            }

            return sb.ToString();
        }

        protected T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }

    }
}
