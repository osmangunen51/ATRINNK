using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class SystemBaseController : Controller
    {
        public void SendMessageAsync(string message, int UserID)
        {
            //var HubSystem = EngineContext.Current.Resolve<NeoSistem.MakinaTurkiye.Management.Hubs.System>();
            //HubSystem.SendMessage(message, UserID);
        }

        public bool SendMail(MailMessage mailMessage, NetworkCredential NetworkCredential = null)
        {
            bool Status = false;
            try
            {
                if (NetworkCredential == null)
                {
                    NetworkCredential = new NetworkCredential(AppSettings.MailUserName, AppSettings.MailPassword);
                }
                using (SmtpClient sc = new SmtpClient())
                {
                    sc.Port = AppSettings.MailPort;
                    sc.Host = AppSettings.MailHost;
                    sc.EnableSsl = AppSettings.MailSsl;
                    sc.Credentials = NetworkCredential;
                    sc.Send(mailMessage);
                    Status = true;
                }
            }
            catch (Exception Hata)
            {
                Status = false;
                TempData["StoreEmailError"] = Hata.Message;
            }
            return Status;
        }
    }
}