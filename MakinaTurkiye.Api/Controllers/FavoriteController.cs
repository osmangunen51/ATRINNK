using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using System;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class FavoriteController : BaseApiController
    {
        private readonly IProductService _productService;

        public FavoriteController()
        {
            _productService = EngineContext.Current.Resolve<IProductService>();
        }

        public HttpResponseMessage GetFavoriteProducts(int memberNo, int page, int pageDimension)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var stg2 = Request.CheckLoginUserClaims().LoginMemberEmail;

                int totalRecord = 0;

                var favoriteProducts = _productService.GetSPFavoriteProductsByMainPartyId(memberNo, page, pageDimension, out totalRecord);
                processStatus.Result = favoriteProducts;
                processStatus.ActiveResultRowCount = favoriteProducts.Count;
                processStatus.TotolRowCount = totalRecord;
                processStatus.Message.Header = "Favorite Products";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Favorite Products";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}