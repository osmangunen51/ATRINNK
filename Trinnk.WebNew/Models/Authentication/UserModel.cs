namespace NeoSistem.Trinnk.Web.Models.Authentication
{
    using global::Trinnk.Entities.Tables.Members;
    using System.Security.Claims;
    using System.Web;

    public class AuthenticationUser
    {
        public static AppUser CurrentUser
        {
            get
            {
                return new AppUser(HttpContext.Current.User as ClaimsPrincipal);
            }
        }

        public static Member Membership
        {
            get
            {
                var AppUser = new AppUser(HttpContext.Current.User as ClaimsPrincipal);
                if (AppUser.Membership != null)
                {
                    return AppUser.Membership;
                }
                else
                {
                    return new Member();
                }
            }
        }

    }

}