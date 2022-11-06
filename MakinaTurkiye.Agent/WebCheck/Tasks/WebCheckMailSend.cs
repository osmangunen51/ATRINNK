using MakinaTurkiye.Core;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MakinaTurkiye.Agent.WebCheck.Tasks
{
    public class WebCheckMailSend : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            string url = "https://makinaturkiye.com";
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
                        string Mesaj = "Web Sitesinde sorun var Site Açılmıyor.... | MakinaTurkiye Agent";
                        //sms.SendPhoneMessage("905326508841",Mesaj);
                        sms.SendPhoneMessage("905057916447", Mesaj);
                    }
                }
            }
            catch(Exception Hata)
            {
                using (SmsHelper sms = new SmsHelper())
                {
                    string Mesaj = "Web Sitesinde genel sorun var kontrol edilemiyor.... | MakinaTurkiye Agent";
                    //sms.SendPhoneMessage("905326508841", Mesaj);
                    sms.SendPhoneMessage("905057916447", Mesaj);
                }
            }
            return Task.CompletedTask;
        }
    }
}
