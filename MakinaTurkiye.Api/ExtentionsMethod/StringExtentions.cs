using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MakinaTurkiye.Api.ExtentionsMethod
{
    public static class StringExtentions
    {
        public static Image ToImage(this string value)
        {
            Image Sonuc;
            byte[] bytes = Convert.FromBase64String(value);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Sonuc = Image.FromStream(ms);
            }
            return Sonuc;
        }

        public static string GetUzanti(this string value)
        {
            string Sonuc="";
            var data = value.Substring(0, 5);
            switch (data.ToUpper())
            {
                case "IVBOR":
                    {
                        Sonuc = "png";
                        break;
                    }
                case "/9J/4":
                    {
                        Sonuc = "jpg";
                        break;
                    }
                case "AAAAF":
                    {
                        Sonuc = "mp4";
                        break;
                    }
                case "JVBER":
                    {
                        Sonuc = "pdf";
                        break;
                    }
                case "AAABA":
                    {
                        Sonuc = "ico";
                        break;
                    }
                case "UMFYI":
                    {
                        Sonuc = "rar";
                        break;
                    }
                case "E1XYD":
                    {
                        Sonuc = "rtf";
                        break;
                    }
                case "U1PKC":
                    {
                        Sonuc = "txt";
                        break;
                    }
                case "MQOWM":
                    {
                        Sonuc = "";
                        break;
                    }
                case "77U/M":
                    {
                        Sonuc = "srt";
                        break;
                    }
                default:
                    {
                        Sonuc = string.Empty;
                        break;
                    }
            }
            return Sonuc;
        }

    }
}