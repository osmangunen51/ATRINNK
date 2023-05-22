using System.Web;

namespace NeoSistem.Trinnk.Management.Models.Authentication
{
    public class CurrentUserModel
    {
        public static Classes.User CurrentManagement
        {
            get
            {
                Classes.User curKullanici = HttpContext.Current.Session["CurrentManagement"] as Classes.User;
                return curKullanici ?? new Classes.User();
            }
            set
            {
                HttpContext.Current.Session["CurrentManagement"] = value;
            }
        }
    }
}