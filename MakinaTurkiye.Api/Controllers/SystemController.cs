using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using System;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class SystemController : BaseApiController
    {
        private readonly ICategoryService CategoryService;


        public SystemController()
        {
            CategoryService = EngineContext.Current.Resolve<ICategoryService>();
        }

        public HttpResponseMessage GetKvkk()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                int CatId = 361202;
                var Category = CategoryService.GetCategoryByCategoryId(CatId);
                if (Category != null)
                {
                    processStatus.Message.Header = "System İşlemleri";
                    processStatus.Message.Text = "Başarılı.";
                    processStatus.Status = true;
                    processStatus.Result = new Kvkk()
                    {
                        Baslik = Category.CategoryName,
                        Icerik = Category.Content
                    };
                    processStatus.Error = null;
                }
                else
                {
                    processStatus.Message.Header = "Kvk Sayfa İşlemleri";
                    processStatus.Message.Text = "Kvkk sayfası bulunamadı";
                    processStatus.Status = false;
                    processStatus.Result = new Kvkk()
                    {
                        Baslik = "",
                        Icerik = ""
                    };
                    processStatus.Error = null;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kvk İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetGizlilikPolitikasi()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                int CatId = 144081;
                var Category = CategoryService.GetCategoryByCategoryId(CatId);
                if (Category != null)
                {
                    processStatus.Message.Header = "System İşlemleri";
                    processStatus.Message.Text = "Başarılı.";
                    processStatus.Status = true;
                    processStatus.Result = new Kvkk()
                    {
                        Baslik = Category.CategoryName,
                        Icerik = Category.Content
                    };
                    processStatus.Error = null;
                }
                else
                {
                    processStatus.Message.Header = "Kvk Sayfa İşlemleri";
                    processStatus.Message.Text = "Kvkk sayfası bulunamadı";
                    processStatus.Status = false;
                    processStatus.Result = new Kvkk()
                    {
                        Baslik = "",
                        Icerik = ""
                    };
                    processStatus.Error = null;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kvk İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetUyelikSozlesmesi()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                int CatId = 141649;
                var Category = CategoryService.GetCategoryByCategoryId(CatId);
                if (Category != null)
                {
                    processStatus.Message.Header = "System İşlemleri";
                    processStatus.Message.Text = "Başarılı.";
                    processStatus.Status = true;
                    processStatus.Result = new Kvkk()
                    {
                        Baslik = Category.CategoryName,
                        Icerik = Category.Content
                    };
                    processStatus.Error = null;
                }
                else
                {
                    processStatus.Message.Header = "Kvk Sayfa İşlemleri";
                    processStatus.Message.Text = "Kvkk sayfası bulunamadı";
                    processStatus.Status = false;
                    processStatus.Result = new Kvkk()
                    {
                        Baslik = "",
                        Icerik = ""
                    };
                    processStatus.Error = null;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kvk İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetBlogUrl()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                string BlogUrl = "https://blog.makinaturkiye.com/";
                processStatus.Message.Header = "System İşlemleri";
                processStatus.Message.Text = "genel bir hata oluştu.";
                processStatus.Status = true;
                processStatus.Result = BlogUrl;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kvk İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}