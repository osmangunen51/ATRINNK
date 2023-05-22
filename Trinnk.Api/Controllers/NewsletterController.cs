using Trinnk.Api.View;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Newsletters;
using System;
using System.Net;
using System.Net.Http;

namespace Trinnk.Api.Controllers
{
    public class NewsletterController : BaseApiController
    {
        private readonly INewsletterService newsletterService;

        public NewsletterController()
        {
            newsletterService = EngineContext.Current.Resolve<INewsletterService>();
        }

        public HttpResponseMessage Add(string email)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var newsletter = newsletterService.GetNewsletterByNewsletterEmail(email);
                if (newsletter == null)
                {
                    try
                    {
                        newsletterService.InsertNewsletter(new Entities.Tables.Newsletter.Newsletter()
                        {
                            Active = true,
                            NewsletterDate = DateTime.Now,
                            NewsletterEmail = email
                        });
                        processStatus.Message.Header = "Newsletter İşlemleri";
                        processStatus.Message.Text = "kayıt edildi.";
                        processStatus.Status = true;
                        processStatus.Result = email;
                        processStatus.Error = null;
                    }
                    catch (Exception e)
                    {
                        processStatus.Message.Header = "Newsletter İşlemleri";
                        processStatus.Message.Text = "genel bir hata oluştu.";
                        processStatus.Status = false;
                        processStatus.Result = email;
                        processStatus.Error = e;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Newsletter İşlemleri";
                    processStatus.Message.Text = "zaten kayıtlı bir email adresi";
                    processStatus.Status = false;
                    processStatus.Result = newsletter.NewsletterEmail;
                    processStatus.Error = null;
                }

            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Newsletter İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}