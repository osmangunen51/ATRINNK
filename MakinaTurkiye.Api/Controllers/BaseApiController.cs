using MakinaTurkiye.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    [MakinaTurkiye.Api.Code.ApiAuthorize]
    public class BaseApiController : ApiController
    {
        public bool SendMail(MailMessage mailMessage)
        {
            bool Status = false;
            try
            {
                using (SmtpClient sc = new SmtpClient())
                {
                    sc.Port = AppSettings.MailPort;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = AppSettings.MailHost;                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = AppSettings.MailSsl;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(AppSettings.MailUserName, AppSettings.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mailMessage);
                    Status = true;
                }
            }
            catch (Exception Hata)
            {
                Status = false;
            }
            return Status;
        }


        private string _Ip = "";

        public string Ip
        {
            get
            {
                _Ip = GetClientIp();
                return _Ip;
            }
            set
            {
                _Ip = value;
            }
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}