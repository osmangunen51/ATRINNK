using MakinaTurkiye.Api.View;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;

namespace MakinaTurkiye.Api.Helpers
{
    public static class CheckClaims
    {
        public static LoginInfoFromToken CheckLoginUserClaims(this HttpRequestMessage Request)
        {
            LoginInfoFromToken token = new LoginInfoFromToken();
            try
            {
                string TxtToken = Request.Headers.GetValues("Token").First();
                string Key = ConfigurationManager.AppSettings["Token:Sifre-Key"].ToString();
                TxtToken = TxtToken.Coz(Key);

                token = JsonConvert.DeserializeObject<LoginInfoFromToken>(TxtToken);
                if (token.EndDate < DateTime.Now)
                {
                    GetDefaultAccessToken();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return token;
        }

        public static string GetDefaultAccessToken()
        {
            try
            {
                string Key = ConfigurationManager.AppSettings["Token:Sifre-Key"].ToString();
                View.LoginInfoFromToken token = new LoginInfoFromToken()
                {
                    Key = "makinaturkiye",
                    PrivateAnahtar = "makinaturkiye",
                };
                string TxtToken = Newtonsoft.Json.JsonConvert.SerializeObject(token, Newtonsoft.Json.Formatting.None).Sifrele(Key);

                return TxtToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
};