using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService ProductService;
        private readonly IPictureService _pictureService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;

        public ProductController()
        {
            ProductService = EngineContext.Current.Resolve<IProductService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
        }

        public HttpResponseMessage Get(int No)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                var Result = ProductService.GetProductByProductId(No);
                if (Result != null)
                {
                    MakinaTurkiye.Api.View.Result.ProductSearchResult TmpResult = new MakinaTurkiye.Api.View.Result.ProductSearchResult
                    {
                        ProductId = Result.ProductId,
                        CurrencyCodeName = "tr-TR",
                        ProductName = Result.ProductName,
                        BrandName = Result.Brand.CategoryName,
                        ModelName = Result.Model.CategoryName,
                        MainPicture = "",
                        StoreName = "",
                        MainPartyId = (int)Result.MainPartyId,
                        ProductPrice = (Result.ProductPrice.HasValue ? Result.ProductPrice.Value : 0),
                        ProductPriceType = (byte)Result.ProductPriceType,
                        ProductPriceLast = (Result.ProductPriceLast.HasValue ? Result.ProductPriceLast.Value : 0),
                        ProductPriceBegin = (Result.ProductPriceBegin.HasValue ? Result.ProductPriceBegin.Value : 0)
                    };

                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(TmpResult.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(TmpResult.ProductId, picture.PicturePath, ProductImageSize.px200x150) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(TmpResult.MainPartyId);
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    TmpResult.MainPicture = picturePath;
                    TmpResult.StoreName = store.StoreName;
                    ProcessStatus.Result = TmpResult;

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

        public HttpResponseMessage GetWithPageNo(int No)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                var Result = ProductService.GetProductsWithPageNo(No);
                if (Result != null)
                {
                    List<MakinaTurkiye.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                        new MakinaTurkiye.Api.View.Result.ProductSearchResult
                        {
                            ProductId = Snc.ProductId,
                            CurrencyCodeName = "tr-TR",
                            ProductName = Snc.ProductName,
                            BrandName = Snc.Brand.CategoryName,
                            ModelName = Snc.Model.CategoryName,
                            MainPicture = "",
                            StoreName = "",
                            MainPartyId = (int)Snc.MainPartyId,
                            ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                            ProductPriceType = (byte)Snc.ProductPriceType,
                            ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                            ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0)
                        }
                    ).ToList();

                    foreach (var item in TmpResult)
                    {
                        string picturePath = "";
                        var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                        if (picture != null)
                            picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150) : null;
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                        var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                        item.MainPicture = picturePath;
                        item.StoreName = store.StoreName;
                    }

                    ProcessStatus.Result = TmpResult;
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

        public HttpResponseMessage GetWithCategory(int No, int Page = 0, int PageSize = 50)
        {
            ProcessStatus ProcessStatus = new ProcessStatus();
            try
            {
                var Result = ProductService.GetSPMostViewProductsByCategoryId(No);
                ProcessStatus.TotolRowCount = Result.Count();
                if (ProcessStatus.TotolRowCount < PageSize)
                {
                    Page = 0;
                }
                Result = Result.Skip(PageSize * Page).Take(PageSize).ToList();

                List<MakinaTurkiye.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                        new MakinaTurkiye.Api.View.Result.ProductSearchResult
                        {
                            ProductId = Snc.ProductId,
                            CurrencyCodeName = "tr-TR",
                            ProductName = Snc.ProductName,
                            BrandName = Snc.BrandName,
                            ModelName = Snc.ModelName,
                            MainPicture = Snc.MainPicture,
                            StoreName = Snc.StoreName,
                            ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                            ProductPriceType = (byte)Snc.ProductPriceType,
                            ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                            ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0)
                        }
                    ).ToList();

                foreach (var item in TmpResult)
                {
                    item.MainPicture = !string.IsNullOrEmpty(item.MainPicture) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, item.MainPicture, ProductImageSize.px200x150) : null;
                }

                ProcessStatus.Result = TmpResult;
                ProcessStatus.Result = TmpResult;
                ProcessStatus.ActiveResultRowCount = TmpResult.Count();
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
                var Result = ProductService.GetProductsAll();
                List<MakinaTurkiye.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                         new MakinaTurkiye.Api.View.Result.ProductSearchResult
                         {
                             ProductId = Snc.ProductId,
                             CurrencyCodeName = "tr-TR",
                             ProductName = Snc.ProductName,
                             BrandName = Snc.Brand.CategoryName,
                             ModelName = Snc.Model.CategoryName,
                             MainPicture = "",
                             StoreName = "",
                             MainPartyId = (int)Snc.MainPartyId,
                             ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                             ProductPriceType = (byte)Snc.ProductPriceType,
                             ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                             ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0)
                         }
                     ).ToList();

                foreach (var item in TmpResult)
                {
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    item.MainPicture = picturePath;
                    item.StoreName = store.StoreName;
                }

                ProcessStatus.Result = TmpResult;
                ProcessStatus.ActiveResultRowCount = Result.Count();
                ProcessStatus.TotolRowCount = ProcessStatus.ActiveResultRowCount;
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
                int TotalRowCount = 0;
                var Result = ProductService.Search(out TotalRowCount, Model.name, Model.companyName, Model.country, Model.town, Model.isnew, Model.isold, Model.sortByViews, Model.sortByDate, Model.minPrice, Model.maxPrice, Model.Page, Model.PageSize);

                List<MakinaTurkiye.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
                        new MakinaTurkiye.Api.View.Result.ProductSearchResult
                        {
                            ProductId = Snc.ProductId,
                            CurrencyCodeName = "tr-TR",
                            ProductName = Snc.ProductName,
                            BrandName = Snc.Brand.CategoryName,
                            ModelName = Snc.Model.CategoryName,
                            MainPicture = "",
                            StoreName = "",
                            MainPartyId = (int)Snc.MainPartyId,
                            ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
                            ProductPriceType = (byte)Snc.ProductPriceType,
                            ProductPriceLast = (Snc.ProductPriceLast.HasValue ? Snc.ProductPriceLast.Value : 0),
                            ProductPriceBegin = (Snc.ProductPriceBegin.HasValue ? Snc.ProductPriceBegin.Value : 0)
                        }
                    ).ToList();

                foreach (var item in TmpResult)
                {
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150) : null;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
                    var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    item.MainPicture = picturePath;
                    item.StoreName = store.StoreName;
                }

                ProcessStatus.Result = TmpResult;

                ProcessStatus.Result = Result;
                ProcessStatus.ActiveResultRowCount = Result.Count();
                ProcessStatus.TotolRowCount = TotalRowCount;
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