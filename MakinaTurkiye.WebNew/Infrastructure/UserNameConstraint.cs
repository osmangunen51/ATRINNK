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
            if(values["username"].ToString()!="")
            {
                List<string> usedLink = new List<string>() { "Videos", "Videolar", "Sirketler", "Store", "Yardim", "Help", "KullanimKosullari", "SiteHaritasi", "SiteMap", "Error404", "HataSayfasi", "Urunler", "Blog", "testfile", "mblog", "detayli-arama","alim-talebi" };
           
                // Get the username from the url
            var username = values["username"].ToString().ToLower();
            // Check for a match (assumes case insensitive)
            string[] usernames = username.Split('-');
               bool anyNumber=false;

               foreach (var item in usernames)
               {
                   int number;
                   anyNumber = Int32.TryParse(item, out number);
               }
                //üstteki döngü kaldırılacak unutma,2 gün içerisinde 

            if(!usedLink.Any(x=>x.ToLower()==username) && usernames.Length<=3 && anyNumber==false)
            {
                if (username == null || username=="")
                    return false;
                else
                return true;
            }
            else
            {
                return false;
            }
            }
            else
            {
                return false;
            }
        }
    }
  
    
}