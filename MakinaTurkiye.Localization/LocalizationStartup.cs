using System;
using System.Web;

namespace MakinaTurkiye.Localization
{
    internal class LocalizationStartup
    {
        /// <summary>
        /// Localization Culture (ex: tr-TR)
        /// </summary>
        /// <returns></returns>
        private string Culture { get; set; }

        /// Unique applization id (* optional)
        private string AppName { get; set; }

        /// <summary>
        /// Custom Xml folder id (* optional) (Default folder name Localization)
        /// </summary>
        /// <returns></returns>
        private string Folder { get; set; }

        public void ReadCookie()
        {
            Culture = string.Format("{0}", Language.en_US);
            var cookieLanguage = HttpContext.Current.Request.Cookies["CacheLang"];
            if (cookieLanguage != null)
            {
                Culture = string.Format("{0}", cookieLanguage.Value);
            }
            else
            {
                var httpCookie = HttpContext.Current.Response.Cookies["CacheLang"];
                if (httpCookie != null)
                    httpCookie.Value = Language.en_US.ToString();
                if (cookieLanguage != null) Culture = string.Format("{0}", cookieLanguage.Value);
            }
        }

        /// <summary>
        /// Xml localization item id usgin GetLang("itemName")
        /// </summary>
        /// <param id="id">Xml localization item id</param>
        /// <returns></returns>
        public string GetLang(string id, string _culture = "", string Deger = "")
        {
            //return Deger;
            ReadCookie();
            string uniqueApplicationName = "TMSLocalization";
            string xmlFolderName = "Localization";

            if (string.IsNullOrEmpty(AppName))
                AppName = uniqueApplicationName;

            if (string.IsNullOrEmpty(Folder))
                Folder = xmlFolderName;

            if (!string.IsNullOrEmpty(_culture))
                Culture = _culture;

            string applicationName = string.Format("{0}_{1}_", Culture, uniqueApplicationName);

            var key = applicationName + id;
            var path = HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}.xml", xmlFolderName, Culture));
            if (HttpContext.Current.Application[key] == null)
            {
                Resource.GetXmlResource(path, applicationName);
            }
            if (HttpContext.Current.Application[key] == null)
            {
                Resource.SetXmlResource(path, id, Deger);
                Resource.GetXmlResource(path, applicationName);
            }
            var valueObj = HttpContext.Current.Application[key];
            if (valueObj == null)
                return id;
            else return valueObj.ToString();
        }

        /// <summary>
        /// Xml localization item id usgin GetLang("itemName")
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string GetLang(Inline lang)
        {
            ReadCookie();

            if (Culture != null)
            {
                var propy = new GetPropertyByValue();
                var value = (string)propy.GetValue(lang, Culture);

                return value;
            }

            return lang.en_US;
        }

        // Yeni Bir G�rev Eklenecek.
        public string SetLang(Language lang)
        {
            var cookieLanguage = HttpContext.Current.Request.Cookies["CacheLang"];

            if (cookieLanguage != null)
            {
                var cookie = string.Format("{0}", cookieLanguage.Value);

                bool exists = Enum.IsDefined(typeof(Language), cookieLanguage.Value);
            }

            return null;
        }
    }
}