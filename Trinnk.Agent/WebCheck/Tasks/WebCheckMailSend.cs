using Trinnk.Core;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Trinnk.Agent.WebCheck.Tasks
{
    public class WebCheckMailSend : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            List<string> TelefonListesi = ConfigurationManager.AppSettings["AgentTelefonListesi"].Split(',').ToList();
            string url = "https://trinnk.com";
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                if ((response.StatusCode == HttpStatusCode.OK))
                {

                }
                else
                {
                    using (SmsHelper sms = new SmsHelper())
                    {
                        string Mesaj = "Web Sitesinde sorun var Site Açılmıyor.... | Trinnk Agent";
                        foreach (var item in TelefonListesi)
                        {
                            sms.SendPhoneMessage(item, Mesaj);
                        }                       
                    }
                }
            }
            catch(Exception Hata)
            {
                using (SmsHelper sms = new SmsHelper())
                {
                    string Mesaj = "Web Sitesinde genel sorun var kontrol edilemiyor.... | Trinnk Agent";
                    foreach (var item in TelefonListesi)
                    {
                        sms.SendPhoneMessage(item, Mesaj);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
