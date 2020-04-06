using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MMakinaTurkiye.Api.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace MakinaTurkiye.Api.Controllers
{
    public class ProductController : ApiController
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
                ProcessStatus.Result =new{
                    Id= Result.ProductId,
                    Name =Result.ProductName,
                    Price = Result.ProductPrice
                } ;
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
