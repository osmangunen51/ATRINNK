using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using System;
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

        public HttpResponseMessage GetWithCategory(int No)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                //ProcessStatus.Result = ProductService.GetCategoryProducts(No, 0, 0, 0, 1,);
                //ProcessStatus.Message.Header = "Product İşlemleri";
                //ProcessStatus.Message.Text = "Başarılı";
                //ProcessStatus.Status = true;
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
                //ProcessStatus.Result = ProductService.GetCategoryProducts(0, 0, 0, 0, 1);
                //ProcessStatus.Message.Header = "Product İşlemleri";
                //ProcessStatus.Message.Text = "Başarılı";
                //ProcessStatus.Status = true;
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
