using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
    public class LocationHelper
    {
        public string IpAdress { get; set; }
        public LocationHelper(string newIpAdress)
        {
            this.IpAdress = newIpAdress;
        }
        public Dictionary<string, string> GetLocationFromIp()
        {
            try
            {

                using (var webClient = new System.Net.WebClient())
                {
                    string url1 = "http://ip-api.com/json/" + this.IpAdress;
                    webClient.Encoding = Encoding.UTF8;
                    var json = webClient.DownloadString(url1);
                    var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
                    return JSONObj;
                    // Now parse with JSON.Net
                }


            }
            catch (Exception)
            {
                return new Dictionary<string, string>();

            }


        }
    }
}