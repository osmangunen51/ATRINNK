using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
    public static class StringHelpers
    {
        public static string ProductNameRegulator(string text)
        {
            string newText = String.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                string[] textArray = text.Split(' ');
                for (int i = 0; i < textArray.Length; i++)
                {
                    string activeText = textArray.GetValue(i).ToString();
                    if (activeText != "")
                    {
                        if (IsUpper(activeText))
                            newText += activeText + " ";
                        else
                            newText += CapitalCase(activeText) + " ";
                    }
                }
            }
            return newText;
        }

        public static string CapitalCase(string value)
        {
            bool capitalizeNext = true;
            char[] val = value.ToCharArray();
            for (int index = 0; index < val.Length; index++)
            {
                if (!Char.IsLetterOrDigit(value[index]))
                {
                    capitalizeNext = true;
                    continue;
                }

                if (capitalizeNext)
                {
                    val[index] = Char.ToUpper(value[index]);
                    capitalizeNext = false;
                }
            }
            return new string(val);
        }

        public static bool IsUpper(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLower(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static string ToUrl(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s");
                text = text.Replace("ı", "i").Replace("ö", "o").Replace("ç", "c");
                text = text.Replace(".", "");

                text = text.Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S");
                text = text.Replace("İ", "I").Replace("Ö", "O").Replace("Ç", "C").Replace(" ", "-");
                text = text.Replace("–", "").Replace("--", "-").Replace("--", "-").Replace("--", "-");
                text = Regex.Replace(text, "[" + Regex.Escape(
                  new string(Path.GetInvalidFileNameChars())) + "]", "");
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
        public static string ToPlural(string value)
        {
            //  if (!string.IsNullOrEmpty(value))
            //  {
            //      if (value.Length >= 3)
            //      {
            //          if (value.Substring(value.Length - 3) == "ler" || value.Substring(value.Length - 3) == "lar")
            //          {
            //              return value;
            //          }
            //      }
            //      string letter = value.Substring(value.Length - 2).ToString();
            //      if (letter.Contains("e") || letter.Contains("i") || letter.Contains("ö") || letter.Contains("ü"))
            //      {
            //          return value + "ler";
            //      }
            //      return value + "lar";
            //  }
            //return "";
            return value;
        }
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }


        public static bool isNumeric(this string value)
        {
            double oReturn = 0; return double.TryParse(value, out oReturn);

        }
        public static string CheckNullString(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
        public static string PriceForJson(this decimal value)
        {

            string price = "";
            var culturInfo = new CultureInfo("tr-TR");
            if (value > 0)
            {
                price = value.ToString("0.00");

                if (string.Format("{0:#,0.00}", Convert.ToDouble(price, culturInfo.NumberFormat)).EndsWith(",00"))
                {
                    price = string.Format("{0:#,0.00}", Convert.ToDouble(price, culturInfo.NumberFormat)).Replace(",00", "");
                }
                else
                {
                    price = string.Format("{0:#,0.00}", Convert.ToDouble(price));
                }
                return price;
            }

            return "";
        }
        public static string GetMoneyFormattedDecimalToString(this decimal value)
        {
            value = (value == 0) ? 1 : value;
            string price = "1";
            var culturInfo = new CultureInfo("tr-TR");
            if (value > 0)
                price = value.ToString("0.00");

            if (string.Format("{0:#,0.00}", Convert.ToDouble(price, culturInfo.NumberFormat)).EndsWith(",00"))
            {
                price = string.Format("{0:#,0.00}", Convert.ToDouble(price, culturInfo.NumberFormat)).Replace(",00", "");
            }
            else
            {
                price = string.Format("{0:#,0.00}", Convert.ToDouble(price));
            }
            return price;
        }
        public static string CheckNullCurrencySymbolString(this string value)
        {
            return string.IsNullOrEmpty(value) == false ? value : "₺";
        }

        public static string CleanProductDescriptionText(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string cleanedText = Regex.Replace(value, @"http[^\s]+", "");
                cleanedText = Regex.Replace(cleanedText, @"(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)([0-9]{9}|[0-9\-\s]{9,18})", "");
                return cleanedText;
            }
            return value;

        }

    }
}