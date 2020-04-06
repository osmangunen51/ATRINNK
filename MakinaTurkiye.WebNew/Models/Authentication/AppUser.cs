using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Services.Authentication;
using MakinaTurkiye.Services.Members;
using System;
using System.Linq;
using System.Security.Claims;

namespace NeoSistem.MakinaTurkiye.Web.Models.Authentication
{
    public  class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public  Member Membership
        {
            get
            {
                try
                {
                    IAuthenticationService authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
                    return authenticationService.GetAuthenticatedMember();
                }
                catch (Exception)
                {

                    return new Member();
                }
            }
        }
    }
}