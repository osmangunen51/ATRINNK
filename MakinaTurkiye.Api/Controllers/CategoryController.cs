using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Net;
using System.Net.Http;

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
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var result = CategoryService.GetCategoryByCategoryId(No);
                if (result != null)
                {
                    result.CategoryIcon = !string.IsNullOrEmpty(result.CategoryIcon) ? "https:" + ImageHelper.GetCategoryIconPath(result.CategoryIcon) : null;
                    ProcessStatus.Result = result;
                    ProcessStatus.ActiveResultRowCount = 1;
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = CategoryService.GetAllCategories();
                foreach (var item in Result)
                {
                    item.CategoryIcon = !string.IsNullOrEmpty(item.CategoryIcon) ? "https:" + ImageHelper.GetCategoryIconPath(item.CategoryIcon) : null;
                }
                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count;
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;

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
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = CategoryService.GetCategoriesByName(Name);
                foreach (var item in Result)
                {
                    item.CategoryIcon = !string.IsNullOrEmpty(item.CategoryIcon) ? "https:" + ImageHelper.GetCategoryIconPath(item.CategoryIcon) : null;
                }
                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count;
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = CategoryService.GetMainCategories();
                foreach (var item in Result)
                {
                    item.CategoryIcon = !string.IsNullOrEmpty(item.CategoryIcon) ? "https:" + ImageHelper.GetCategoryIconPath(item.CategoryIcon) : null;
                }
                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count;
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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

        public HttpResponseMessage GetSubCategoriesByParentId(int No)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = CategoryService.GetCategoriesByCategoryParentId(No);

                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count;
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
                ProcessStatus.Message.Header = "Alt Category İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Alt Category İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }
    }
}