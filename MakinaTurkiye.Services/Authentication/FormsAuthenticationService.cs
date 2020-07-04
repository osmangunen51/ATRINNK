using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Services.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace MakinaTurkiye.Services.Authentication
{
    public class FormsAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IMemberService _memberService;
        private readonly TimeSpan _expirationTimeSpan;

        private Member _cachedMember;

        #endregion


        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="memberService">Member service</param>
        public FormsAuthenticationService(HttpContextBase httpContext,
            IMemberService memberService)
        {
            this._httpContext = httpContext;
            this._memberService = memberService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        #endregion


        #region Utilities

        /// <summary>
        /// Get authenticated member
        /// </summary>
        /// <param name="ticket">Ticket</param>
        /// <returns>Member</returns>
        protected virtual Member GetAuthenticatedMemberFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            string mainPartyIdForString = ticket.UserData;

            if (String.IsNullOrWhiteSpace(mainPartyIdForString))
                return null;

            int mainPartyId = int.Parse(mainPartyIdForString);
            var member = _memberService.GetMemberByMainPartyId(mainPartyId);

            return member;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="member">Member</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        public virtual void SignIn(Member member, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                member.MainPartyId.ToString(),
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                member.MainPartyId.ToString(),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
  
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedMember = member;
        }
        /// <summary>
        /// Sign out
        /// </summary>
        public virtual void SignOut()
        {
            
            _httpContext.User = null;
            _cachedMember = null;
            FormsAuthentication.SignOut();
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            if (!_httpContext.Request.IsLocal)
            {
                cookie1.Domain = ".makinaturkiye.com";
            }
            _httpContext.Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            _httpContext.Response.Cookies.Add(cookie2);
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Member</returns>
        public virtual Member GetAuthenticatedMember()
        {
            if (_cachedMember != null)
                return _cachedMember;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return new Member();
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var member = GetAuthenticatedMemberFromTicket(formsIdentity.Ticket);

            if (member != null && member.Active == true)
                _cachedMember = member;

            return _cachedMember;
        }

        #endregion
    }
}
