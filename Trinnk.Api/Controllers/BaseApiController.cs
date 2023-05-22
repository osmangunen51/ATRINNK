using Trinnk.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Trinnk.Api.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [Trinnk.Api.Code.ApiAuthorize]
    public class BaseApiController : ApiController
    {
        protected T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }

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