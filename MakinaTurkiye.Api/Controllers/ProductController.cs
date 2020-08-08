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
    public class ProductController : BaseApiController
    {
        private readonly IProductService ProductService;
        public ProductController()
        {
            ProductService = EngineContext.Current.Resolve<IProductService>();
        }

        public HttpResponseMessage Get(int No)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                var Result = ProductService.GetProductByProductId(No);
                if (Result != null)
                {
                    ProcessStatus.Result = new
                    {
                        Id = Result.ProductId,
                        Name = Result.ProductName,
                        Price = Result.ProductPrice
                    };
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }

            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product Operations";
                ProcessStatus.Message.Text = "Error";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetWithPageNo(int No)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                var Result = ProductService.GetProductsWithPageNo(No);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Success";
                    ProcessStatus.Status = true;
                }
                else
                {
                    ProcessStatus.Message.Header = "Product Operations";
                    ProcessStatus.Message.Text = "Entity Not Found";
                    ProcessStatus.Status = false;
                    ProcessStatus.Result = null;
                }
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product Operations";
                ProcessStatus.Message.Text = "Error";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }

        public HttpResponseMessage GetWithCategory(int No,int Page=0,int PageSize=50)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                ProcessStatus.Result = ProductService.GetSPMostViewProductsByCategoryId(No).Skip(PageSize* Page).Take(PageSize).ToList();
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
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
                ProcessStatus.Result = ProductService.GetProductsAll();
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;

            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }


        public HttpResponseMessage Search([FromBody]SearchInput Model)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                ProcessStatus.Result = ProductService.Search(Model.name,Model.companyName,Model.country,Model.town,Model.isnew,Model.isold,Model.sortByViews,Model.sortByDate,Model.minPrice,Model.maxPrice,Model.Page,50);
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarılı";
                ProcessStatus.Status = true;
            }
            catch (Exception Error)
            {
                ProcessStatus.Message.Header = "Product İşlemleri";
                ProcessStatus.Message.Text = "Başarısız";
                ProcessStatus.Status = false;
                ProcessStatus.Result = null;
                ProcessStatus.Error = Error;

            }
            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }


    }
}
