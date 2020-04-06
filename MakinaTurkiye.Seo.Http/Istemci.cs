using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;

namespace MakinaTurkiye.Seo.Http
{
    public class Istemci : IDisposable
    {
        public CookieWebClient WebClient { get; set; } = new CookieWebClient();

        public Encoding Kodlama { get; set; } = Encoding.Default;

        public Istemci()
        {
        }

        public string HttpPost(string Url, NameValueCollection values)
        {
            var responseString = "";
            try
            {
                var response = WebClient.UploadValues(Url, values);
                responseString = Kodlama.GetString(response);
            }
            catch (Exception Hata)
            {
                responseString = Hata.Message;
            }
            return responseString;
        }

        public string HttpPost(string Url, Dictionary<string, string> Dic)
        {
            NameValueCollection values = new NameValueCollection();
            foreach (var item in Dic)
            {
                values.Add(item.Key, item.Value);
            }
            return HttpPost(Url, values);
        }

        public string HttpGet(string Url)
        {
            WebClient.Encoding = Kodlama;
            var response = WebClient.DownloadString(Url);
            return response;
        }

        public void DownloadFile(string Url, string FileName)
        {
            WebClient.DownloadFile(Url, FileName);
        }

        public byte[] DownloadFile(string Url, Dictionary<string, string> Dic)
        {
            byte[] Sonuc = null;
            try
            {
                NameValueCollection values = new NameValueCollection();
                foreach (var item in Dic)
                {
                    values.Add(item.Key, item.Value);
                }
                Sonuc = WebClient.UploadValues(Url, values);
            }
            catch (Exception Hata)
            {
                Sonuc = null;
            }

            return Sonuc;
        }

        public void Dispose()
        {
        }
    }
}