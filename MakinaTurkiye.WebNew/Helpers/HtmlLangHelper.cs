using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Cultures;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using static NeoSistem.MakinaTurkiye.Web.Models.EnumModel;

namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
    public static class HtmlLangHelper

    {
        public static string GetLangugeVal(string key)
        {
            ILanguageService _langService = EngineContext.Current.Resolve<ILanguageService>();
            int dilId = 1;


            if (HttpContext.Current.Session["LangCode"] != null)
            {
                if (HttpContext.Current.Session["LangCode"].ToString() == "en")
                {
                    dilId = 2;

                }

            }

            var langVal = _langService.GetStaticDefinitionByKeyAndLanguageId(key.ToLower(), dilId);
            if (langVal != null)
                return langVal.Value;
            else
                return key;

        }
        public static string GetStaticUrl(string url)
        {
            if (HttpContext.Current.Session["LangCode"] != null)
            {
                if (HttpContext.Current.Session["LangCode"].ToString() == LanguageCodeString.en.ToString())
                {
                    return AppSettings.SiteUrl + HttpContext.Current.Session["LangCode"].ToString() + "/" + url;
                }

            }
            return AppSettings.SiteUrl + url;
        }
        public static string GetBaseUrl()
        {
            if (HttpContext.Current.Session["LangCode"] != null)
            {
                if (HttpContext.Current.Session["LangCode"].ToString() == LanguageCodeString.en.ToString())
                {
                    return AppSettings.SiteUrlWithoutLastSlash + "/" + HttpContext.Current.Session["LangCode"].ToString();
                }

            }
            return AppSettings.SiteUrlWithoutLastSlash;

        }

        public static int GetLanguageId()
        {
            if (HttpContext.Current.Session["LangCode"] != null)
            {
                if (HttpContext.Current.Session["LangCode"].ToString() == LanguageCodeString.en.ToString())
                {
                    return 2;
                }

            }
            return 1;
        }
        public static string GetLanguageCode()
        {
            if (HttpContext.Current.Session["LangCode"] != null)
            {
                if (HttpContext.Current.Session["LangCode"].ToString() != LanguageCodeString.tr.ToString())
                {
                    return HttpContext.Current.Session["LangCode"].ToString();
                }

            }
            return "";
        }
    }
}