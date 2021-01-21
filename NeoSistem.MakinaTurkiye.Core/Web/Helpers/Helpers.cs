namespace NeoSistem.MakinaTurkiye.Core.Web.Helpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public struct FormEnctype
    {
        public const string MULTIPART_FROM_DATA = "multipart/form-data";
        public const string APPLICATION_FORM = "application/x-www-form-urlencoded";
    }

    public static class Helpers
    {
        public static string CategoryUrl(int categoryId, string categoryname, int? brandId, string brandName)
        {
            if (brandId != null)
            {
                return "/" + ToUrl(brandName + "-" + categoryname + "-c-" + categoryId + "-" + brandId);
            }
            return "/" + ToUrl(categoryname + "-c-" + categoryId);
        }
        //public static string BrandUrl(int brandId, string brandname, string categoryname, int categoryId)
        //{
        //    return "/" + ToUrl(brandname + "-" + categoryname + "-b-" + categoryId + "-" + brandId);
        //}
        //Note:BrandUrl iptal edildi. Markalar artık CategoryUrl olarak çalışacak
        public static string SerieUrl(int seriId, string seriname, string brandname)
        {
            return "/" + ToUrl(seriname + "-" + brandname + "-s-" + seriId);
        }
        public static string ModelUrl(int modelId, string modelName, string brandname, string categoryname)
        {
            return "/" + ToUrl(modelName + "-" + brandname + "-" + categoryname + "-m-" + modelId);
        }

        public static string ProductUrl(int id, string productName)
        {

            string url = "/" + ToUrl(productName + "-p-" + id);
            #if !DEBUG
                        url = "/" + url;
            #endif
            return url;
        }

        public static string CountryUrl(int countryId, string countryName,int categoryId,string categoryName)
        {
            return "/" + ToUrl(categoryName) + "/" + categoryId + "--" + countryId + "/" + ToUrl(countryName)+"/";
        }
        public static string CityUrl(int cityId, string cityName, int countryId, int categoryId, string categoryName)
        {
            return "/" + ToUrl(categoryName) + "/" + categoryId + "--" + countryId +"-"+cityId+ "/" + ToUrl(cityName) + "/";
        }
        public static string LocalityUrl(int localityId, string localityName, int cityId, string cityName, int countryId, int categoryId, string categoryName)
        {
            return "/" + ToUrl(categoryName) + "/" + categoryId + "--" + countryId + "-" + cityId + "-" + localityId + "/" + ToUrl(localityName) + "-" + ToUrl(cityName) + "/";
        }
        public static string HtmlClear(this MvcHtmlString mhtmlString)
        {
            return Regex.Replace(mhtmlString.ToHtmlString(), @"<(.|\n)*?>", string.Empty);
        }

        public static string HtmlClear(this HtmlHelper mhtmlString, string html)
        {
            return Regex.Replace(html, @"<(.|\n)*?>", string.Empty);
        }

        public static string ToUrl(this HtmlHelper mhtmlString, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.ToLower();
                text = text.Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s");
                text = text.Replace("ı", "i").Replace("ö", "o").Replace("ç", "c");
                text = text.Replace(".", "");

                text = text.Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S");
                text = text.Replace("İ", "I").Replace("Ö", "O").Replace("Ç", "C").Replace(" ", "-");
                text = text.Replace("&", "-");
                text = text.Replace("%", "");
                //text = text.Replace("+", "-Arti-");
                text = text.Replace("+", "");
                text = Regex.Replace(text, "[" + Regex.Escape(
                        new string(Path.GetInvalidFileNameChars())) + "]", "");
                text = text.Replace("–", "").Replace("---", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("---", "-");
                text = text.Replace("---", "-");
                text = RemoveSpecialCharacters(text);
                
                return text;
            }
            return "";
        }

        public static string ToUrl(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.ToLower();
                text = text.Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s");
                text = text.Replace("ı", "i").Replace("ö", "o").Replace("ç", "c");
                text = text.Replace(".", "");
                text = text.Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S");
                text = text.Replace("İ", "I").Replace("Ö", "O").Replace("Ç", "C").Replace(" ", "-");
                text = text.Replace("&", "-");
                text = text.Replace("%", "");
                //text = text.Replace("+", "-Arti-");
                text = text.Replace("+", "");
                text = Regex.Replace(text, "[" + Regex.Escape(
                  new string(Path.GetInvalidFileNameChars())) + "]", "");
                text = text.Replace("–", "").Replace("---", "-");
                text = text.Replace("--", "-");
                text = text.Replace("--", "-");
                text = text.Replace("---", "-");
                text = text.Replace("³", "3");
                text = text.Replace("²", "2");
                text = text.Replace(" ", "");
                text = RemoveSpecialCharacters(text);

                return text;
            }
            return "";
        }
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.-]+", "", RegexOptions.Compiled);
        }
        public static string ToPlural(this HtmlHelper mhtmlString, string value)
        {
            //if (!string.IsNullOrEmpty(value))
            //{
            //    string letter = value.Substring(value.Length - 2).ToString();
            //    if (letter.Contains("e") || letter.Contains("i") || letter.Contains("ö") || letter.Contains("ü"))
            //    {
            //        return value + "ler";
            //    }
            //    return value + "lar";
            //}
            //return "";
            return value;
        }

        public static string ToPlural(string value)
        {
            //if (!string.IsNullOrEmpty(value))
            //{
            //    if (value.Length >= 3)
            //    {
            //        if (value.Substring(value.Length - 3) == "ler" || value.Substring(value.Length - 3) == "lar")
            //        {
            //            return value;
            //        }
            //    }
            //    string letter = value.Substring(value.Length - 2).ToString();
            //    if (letter.Contains("e") || letter.Contains("i") || letter.Contains("ö") || letter.Contains("ü"))
            //    {
            //        return value + "ler";
            //    }
            //    return value + "lar";
            //}
            //return "";
            return value;
        }

        public static string GetSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string ToCompress(this MvcHtmlString mhtmlString)
        {
            string mString = mhtmlString.ToHtmlString();
            mString = String.Join(" ", mString.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            return mString;
        }

        public static string ToScriptCompress(this MvcHtmlString mhtmlString)
        {
            return ToScriptString(mhtmlString);
        }

        //public static string ToUrl(this HtmlHelper helper, string value)
        //{
        //  if(String.IsNullOrEmpty(value))
        //  {
        //    return "";
        //  }

        //  value = Regex.Replace(value, "['\"-.*\\/:]", "_").Replace(" ", "+");

        //  if(value.Substring(value.Length - 1, 1) == "+") { value = value.Remove(value.Length - 1, 1); }

        //  return value;
        //}

        //public static string ToUrlString(string value)
        //{
        //  return ToUrlString(value);
        //}

        //public static string ToUrlString(this HtmlHelper helper, string value)
        //{
        //  if(String.IsNullOrEmpty(value))
        //  {
        //    return "";
        //  }
        //  value = value.Trim();
        //  value = Regex.Replace(value, "['\"-.*\\/:]", "_");
        //  value = value.Replace('ğ', 'g').Replace('ü', 'u').Replace('ş', 's').Replace('ı', 'i').Replace('ö', 'o').Replace('ç', 'c');
        //  value = value.Replace('Ğ', 'G').Replace('Ü', 'U').Replace('Ş', 'S').Replace('İ', 'I').Replace('Ö', 'O').Replace('Ç', 'C');
        //  value = value.Replace(" ", "+");

        //  return value;
        //}

        public static string ToDateString(this HtmlHelper helper, DateTime date)
        {
            TimeSpan tSpan = DateTime.Now - date;

            int value = 1;
            string valueStr = " {0} önce";
            string exValue = "";

            if (tSpan.Days >= 1)
            {
                value = tSpan.Days;
                valueStr = string.Format(valueStr, "gün");
            }
            else if (tSpan.Hours <= 24 && tSpan.Hours > 0)
            {
                value = tSpan.Hours;
                valueStr = string.Format(valueStr, "saat");
            }
            else if (tSpan.Minutes <= 60 && tSpan.Minutes > 0)
            {
                value = tSpan.Minutes;
                valueStr = string.Format(valueStr, "dakika");
            }
            else if (tSpan.Seconds <= 60)
            {
                value = tSpan.Seconds <= 0 ? 1 : tSpan.Seconds;
                valueStr = string.Format(valueStr, "saniye");
            }

            return (exValue + value + valueStr);
        }

        public static string Truncate(this HtmlHelper helper, string input, int length)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                return input.Substring(0, length) + "...";
            }
            return "";
        }

        public static string Truncate(string input, int length)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                return input.Substring(0, length) + "...";
            }
            return "";
        }
        public static string Truncatet(this HtmlHelper helper, string input, int length)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                return input.Substring(0, length) + "";
            }
            return "";
        }
        public static string Truncatet(string input, int length)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                return input.Substring(0, length) + "";
            }
            return "";
        }

        public static string ToTitleCase(string text)
        {
            return ToTitleCase(text);
        }

        public static string ToTitleCase(this HtmlHelper helper, string Text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text);
        }

        public static string FirstLetterLower(this HtmlHelper helper, string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length < 1)
                {
                    input.ToUpper();
                    return input;
                }

                else
                {
                    string firstLetter = input.Substring(0, 1).ToUpper();
                    return firstLetter + input.Substring(1, input.Length - 1).ToLower();
                }

            }
            return "";
        }

        internal static string ToScriptString(MvcHtmlString mvcString)
        {
            return String.Format("<script type=\"text/javascript\">NeoSistem.Util.ViewRender('{0}');</script>", ToJavaScriptString(mvcString, true));
        }

        internal static string ToHtmlString(MvcHtmlString mvcString, bool cdata)
        {
            string mString = mvcString.ToCompress();
            if (cdata)
            {
                mString = CdataReplace(mString);
            }
            return mString;
        }

        internal static string ToJavaScriptString(MvcHtmlString mvcString, bool cdata)
        {
            string mString = ToHtmlString(mvcString, false);
            if (cdata)
            {
                mString = mString.Replace("//<![CDATA[", "");
                mString = mString.Replace("<![CDATA[", "");
                mString = mString.Replace("//]]>", "");
                mString = mString.Replace("]]>", "");
            }

            mString = mString.Replace("'", "\\'");
            mString = mString.Replace("script>", "scr'+'ipt>");
            mString = mString.Replace("<script", "<scr'+'ipt");
            return mString;
        }

        public static string EMailProtection(string email)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < email.Length; i++)
            {
                sb.AppendFormat("&#{0};", (Int32)email[i]);
            }
            return sb.ToString();
        }

        private static string CdataReplace(string text)
        {
            text = text.Replace("<![CDATA[", "<![CDATA[\n");
            return text;
        }
    }
}