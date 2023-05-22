using Trinnk.Core.Infrastructure;
using Trinnk.Entities.Tables.Members;
using Trinnk.Services.Members;
using System;
using System.Security.Claims;

namespace NeoSistem.Trinnk.Web.Models.Authentication
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }

        public Member Membership
        {
            get
            {
                try
                {
                    // IAuthenticationService authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
                    IMemberService memberService = EngineContext.Current.Resolve<IMemberService>();
                    if (this.FindFirst(ClaimTypes.NameIdentifier) != null)
                    {
                        int mainPartyId = Convert.ToInt32(this.FindFirst(ClaimTypes.NameIdentifier).Value);
                        var member = memberService.GetMemberByMainPartyId(mainPartyId);
                        return member;
                    }
                    return new Member();
                }
                catch (Exception)
                {

                    return new Member();
                }
            }
        }
    }
}