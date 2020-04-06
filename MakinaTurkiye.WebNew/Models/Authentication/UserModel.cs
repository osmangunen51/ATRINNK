namespace NeoSistem.MakinaTurkiye.Web.Models.Authentication
{
  using System.Web;
    using System.Security.Claims;
    using global::MakinaTurkiye.Entities.Tables.Members;

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