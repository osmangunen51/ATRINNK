using Trinnk.Api.View;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Catalog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Trinnk.Api.Controllers
{
    public class ProductCommentController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IProductCommentService _productCommentService;

        public ProductCommentController()
        {
            _productService = EngineContext.Current.Resolve<IProductService>();
            _productCommentService = EngineContext.Current.Resolve<IProductCommentService>();
        }

        public HttpResponseMessage Get(int No)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productCommentService.GetProductCommentByProductCommentId(No);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.ActiveResultRowCount = 1;
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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
        public HttpResponseMessage GetProductCommentsByProductId(int productId, bool showHidden = false)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productCommentService.GetProductCommentsByProductId(productId, showHidden);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.ActiveResultRowCount = Result.Count();
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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
        public HttpResponseMessage GetProductCommentsForStoreByMemberMainPartyId(int storeMainPartyId)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productCommentService.GetProductCommentsForStoreByMemberMainPartyId(storeMainPartyId);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.ActiveResultRowCount = Result.Count();
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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


        public HttpResponseMessage GetProductComments(int pageSize, int pageIndex, int productId = 0, bool reported = false)
        {
            ProcessResult ProcessStatus = new ProcessResult();
            try
            {
                var Result = _productCommentService.GetProductComments(pageSize, pageIndex, productId, reported);
                if (Result != null)
                {
                    ProcessStatus.Result = Result;
                    ProcessStatus.ActiveResultRowCount = Result.Count();
                    ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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
    }
}