using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly ICategoryService BrandService;
        public BrandController()
        {
            BrandService = EngineContext.Current.Resolve<ICategoryService>();
        }

        public HttpResponseMessage Get(int No)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = BrandService.GetCategoriesByCategoryType(CategoryTypeEnum.Brand).Where(x=>x.CategoryId==No);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.ActiveResultRowCount = Result.Count();
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;

                    ProcessStatus.Message.Header = "Brand Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Brand Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }

            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Brand Operations";
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
                var Result = BrandService.GetCategoriesByCategoryType(CategoryTypeEnum.Brand);
                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count;
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
                ProcessStatus.Message.Header = "Brand İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Brand İşlemleri";
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
                var Result = BrandService.GetCategoriesByCategoryType(CategoryTypeEnum.Brand).Where(x => x.CategoryName.Contains(Name));
                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count();
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;

                ProcessStatus.Message.Header = "Brand İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Brand İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;

            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

    }
}
