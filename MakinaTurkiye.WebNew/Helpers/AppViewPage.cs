using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using System;
using System.Security.Claims;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        protected AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}