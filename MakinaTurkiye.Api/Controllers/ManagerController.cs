using MakinaTurkiye.Api.View;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MakinaTurkiye.Api.Controllers
{
    public class ManagerController : ApiController
    {
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetAccessToken([FromBody]User Model)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                if (Model.Username == "makinaturkiye" && Model.Password == "makinaturkiye")
                {
                    ProcessStatus.Status = true;
                    ProcessStatus.Message = ProcessStatus.Message = new Message()
                    {
                        Header = "Manager",
                        Text = "Başarıyla Girildi"
                    };

                    string Key = ConfigurationManager.AppSettings["Token:Sifre-Key"].ToString();
                    View.Token Token = new Token()
                    {
                        Key = "makinaturkiye",
                        PrivateAnahtar = "makinaturkiye"
                    };

                    string TxtToken = Newtonsoft.Json.JsonConvert.SerializeObject(Token, Newtonsoft.Json.Formatting.None).Sifrele(Key);
                    var Snc = new
                    {
                        KullaniciAd = "makinaturkiye",
                        Key = "makinaturkiye",
                        AdSoyad = "makinaturkiye",
                        Token = TxtToken
                    };
                    ProcessStatus.Result = Snc;
                    return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
                }
                else
                {
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = "";
                    ProcessStatus.Message = new Message()
                    {
                        Header = "Manager",
                        Text = "İşlem Başarısız."
                    };
                    return Request.CreateResponse(HttpStatusCode.NotFound, ProcessStatus);
                }

            }
            catch (Exception Error)
            {
                ProcessStatus.Status = false;
                ProcessStatus.Error = Error;
                ProcessStatus.Message = ProcessStatus.Message = new Message()
                {
                    Header = "Manager",
                    Text = string.Format("Hata Oluştu : {0}", Error.Message)
                };
                return Request.CreateResponse(HttpStatusCode.NotFound, ProcessStatus);
            }
        }
    }
}