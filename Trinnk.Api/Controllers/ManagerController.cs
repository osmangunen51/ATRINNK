using Trinnk.Api.Helpers;
using Trinnk.Api.View;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Trinnk.Api.Controllers
{
    public class ManagerController : ApiController
    {
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetAccessToken([FromBody] User Model)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                if (Model.Username == "trinnk" && Model.Password == "trinnk")
                {
                    ProcessStatus.Status = true;
                    ProcessStatus.Message = ProcessStatus.Message = new Message()
                    {
                        Header = "Manager",
                        Text = "Başarıyla Girildi"
                    };
                    string TxtToken = CheckClaims.GetDefaultAccessToken();
                    var Snc = new
                    {
                        KullaniciAd = "trinnk",
                        Key = "trinnk",
                        AdSoyad = "trinnk",
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