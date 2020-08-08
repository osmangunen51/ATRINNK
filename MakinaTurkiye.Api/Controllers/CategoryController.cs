using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService CategoryService;
        public CategoryController()
        {
            CategoryService = EngineContext.Current.Resolve<ICategoryService>();
        }

        public HttpResponseMessage Get(int No)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                var Result = CategoryService.GetCategoryByCategoryId(No);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.Message.Header = "Category Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Category Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }

            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Category Operations";
                ProcessStatus.Message.Text = "Error";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetAll()
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                ProcessStatus.Result = CategoryService.GetAllCategories();
                ProcessStatus.Message.Header = "Category İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Category İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;

            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetWithName(string Name)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                ProcessStatus.Result = CategoryService.GetCategoriesByName(Name);
                ProcessStatus.Message.Header = "Category İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Category İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;

            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetMainCategories()
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                ProcessStatus.Result = CategoryService.GetMainCategories();
                ProcessStatus.Message.Header = "Category İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Category İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;

            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }
    }
}
