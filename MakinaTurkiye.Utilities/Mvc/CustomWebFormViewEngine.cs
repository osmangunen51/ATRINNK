using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MakinaTurkiye.Utilities.Mvc
{
    public class CustomWebFormViewEngine : WebFormViewEngine
    {
        public CustomWebFormViewEngine() : base()

        {
            MasterLocationFormats = new[] {
            "~/Views/{1}/{0}.master",
            "~/Views/Shared/{0}.master"
         };
            AreaMasterLocationFormats = new[] {
            "~/Areas/{2}/Views/{1}/{0}.master",
            "~/Areas/{2}/Views/Shared/{0}.master",
         };

            ViewLocationFormats = new[] {
            "~/Views/{1}/{0}.aspx",
            "~/Views/{1}/{0}.ascx",
            "~/Views/Shared/{0}.aspx",
            "~/Views/Shared/{0}.ascx"
        };
            AreaViewLocationFormats = new[] {
            "~/Areas/{2}/Views/{1}/{0}.aspx",
            "~/Areas/{2}/Views/{1}/{0}.ascx",
            "~/Areas/{2}/Views/Shared/{0}.aspx",
            "~/Areas/{2}/Views/Shared/{0}.ascx",
        };
        }

    }
}
