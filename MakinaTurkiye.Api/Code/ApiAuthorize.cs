using MakinaTurkiye.Api.View;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MakinaTurkiye.Api.Code
{
    public class ApiAuthorize : AuthorizeAttribute
    {

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            if (Authorize(filterContext))
            {
                return;
            }
            HandleUnauthorizedRequest(filterContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            try
            {
                var TxtToken = actionContext.Request.Headers.GetValues("Token").First();
                var Key = actionContext.Request.Headers.GetValues("Key").First();
                string TokenSifreKey = ConfigurationManager.AppSettings["Token:Sifre-Key"].ToString();
                MakinaTurkiye.Api.View.Token Token = Newtonsoft.Json.JsonConvert.DeserializeObject<MakinaTurkiye.Api.View.Token>(TxtToken.Coz(TokenSifreKey));
                if (Token.PrivateAnahtar == Key && (DateTime.Now.Date >= Token.StartDate.Date && DateTime.Now.Date <= Token.EndDate.Date))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Hata)
            {
                return false;
            }
        }
    }
}