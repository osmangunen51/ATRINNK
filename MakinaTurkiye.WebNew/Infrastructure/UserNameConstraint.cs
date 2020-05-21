using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace NeoSistem.MakinaTurkiye.Web.Infrastructure
{

    public class UserNameConstraint : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values["username"] != null && !string.IsNullOrEmpty(values["username"].ToString()))
            {
                List<string> usedLink = new List<string>() { "Videos", "Videolar", "Sirketler", "Store", "Yardim", "Help", "KullanimKosullari", "SiteHaritasi", "SiteMap", "Error404", "HataSayfasi", "Urunler", "Blog", "testfile", "mblog", "detayli-arama", "alim-talebi","Category","Index","Home" };

                string userName = values["username"].ToString().ToLower();

                if (!usedLink.Any(x => x.ToLower() == userName))
                {
                    return true;
                }
            }
            return false;
        }
    }


}