using System;
using System.Web;
using System.Web.Security;

namespace NeoSistem.MakinaTurkiye.Management.Models.Authentication
{
    public enum TimeOfLayer
    {
        Day,
        Hour,
        Minute,
        Second,
        Tick
    }

    public class EnterpriseFormsAuthentication
    {

        public static void CreateFormsAuthenticationTicket(string Name, TimeOfLayer Expiration, long time, string userData)
        {

            DateTime date = new DateTime();
            switch (Expiration)
            {
                case TimeOfLayer.Day:
                    date = DateTime.Now.AddDays(time);
                    break;
                case TimeOfLayer.Hour:
                    date = DateTime.Now.AddHours(time);
                    break;
                case TimeOfLayer.Minute:
                    date = DateTime.Now.AddMinutes(time);
                    break;
                case TimeOfLayer.Second:
                    date = DateTime.Now.AddSeconds(time);
                    break;
                case TimeOfLayer.Tick:
                    date = DateTime.Now.AddTicks(time);
                    break;
                default:
                    break;
            }

            FormsAuthenticationTicket ticket = null;
            ticket = new FormsAuthenticationTicket(1, Name, DateTime.Now, date, true, userData, FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            if ((ticket.IsPersistent))
            {
                cookie.Expires = ticket.Expiration;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void CreateFormsAuthenticationTicket(string Name, string userData)
        {
            FormsAuthenticationTicket ticket = null;
            ticket = new FormsAuthenticationTicket(1, Name, DateTime.Now, DateTime.Now.AddMinutes(360), true, userData, FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            if ((ticket.IsPersistent))
            {
                cookie.Expires = ticket.Expiration;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void SingOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }


    }
}
