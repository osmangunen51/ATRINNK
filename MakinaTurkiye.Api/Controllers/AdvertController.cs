using MakinaTurkiye.Api.ExtentionsMethod;
using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Media;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class AdvertController : BaseApiController
    {
        private readonly IProductCommentService _productCommentService;
        private readonly IMemberService _memberService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly IPacketService _packetService;
        private readonly IProductService _productService;
        private readonly IStoreSectorService _storeSectorService;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;
        private readonly IVideoService _videoService;
        private readonly IProductCatologService _productCatologService;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly IFavoriteStoreService _favoriteStoreService;
        private readonly IMessageService _messageService;
        public AdvertController()
        {
            _productCommentService = EngineContext.Current.Resolve<IProductCommentService>();
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _packetService = EngineContext.Current.Resolve<IPacketService>();
            _productService = EngineContext.Current.Resolve<IProductService>();
            _storeSectorService = EngineContext.Current.Resolve<IStoreSectorService>();
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
            _videoService = EngineContext.Current.Resolve<IVideoService>();
            _productCatologService = EngineContext.Current.Resolve<IProductCatologService>();
            _favoriteProductService = EngineContext.Current.Resolve<IFavoriteProductService>();
            _favoriteStoreService = EngineContext.Current.Resolve<IFavoriteStoreService>();
            _messageService = EngineContext.Current.Resolve<IMessageService>();
        }
        public HttpResponseMessage ReportProductComment(int productCommentId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var productComment = _productCommentService.GetProductCommentByProductCommentId(productCommentId);

                    productComment.Reported = true;

                    _productCommentService.UpdateProductComment(productComment);

                    processStatus.Result = "Yorum Şikayetiniz alındı en kısa zamanda kontrol edilecek.";
                    processStatus.Message.Header = "Report Product Comment";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Login üye bulunamadı";
                    processStatus.Message.Header = "Report Product Comment";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Report Product Comment";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //public HttpResponseMessage Advert()
        //{
        //    ProcessResult processStatus = new ProcessResult();
        //    try
        //    {
        //        var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
        //        var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
        //        if (member != null)
        //        {
        //            if (member.MemberType != (byte)MemberType.Enterprise)
        //            {
        //                processStatus.Result = "AdvertNotMemberTypeAccess";
        //                processStatus.Message.Header = "Advert";
        //                processStatus.Message.Text = "Başarısız";
        //                processStatus.Status = false;
        //            }
        //            else
        //            {
        //                int productcount = 0;
        //                int maxproductcount = 0;
        //                byte isitstore = (byte)member.MemberType;
        //                var storemember = new global::MakinaTurkiye.Entities.Tables.Members.MemberStore();
        //                var store = new global::MakinaTurkiye.Entities.Tables.Stores.Store();
        //                if (isitstore == 20)
        //                {
        //                    storemember = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
        //                    store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(storemember.StoreMainPartyId));
        //                    if (store.PacketId == 12) //12:Standart Paket
        //                    {
        //                        store.PacketId = 20; //20:Sınırlı Paket
        //                        _storeService.UpdateStore(store);
        //                    }
        //                    if (store.ProductCount == null)
        //                    {
        //                        var packetFeature = _packetService.GetPacketFeatureByPacketIdAndPacketFeatureTypeId(store.PacketId, 3);

        //                        if (!string.IsNullOrEmpty(packetFeature.FeatureContent))
        //                        {
        //                            maxproductcount = 9999;
        //                        }
        //                        else if (packetFeature.FeatureActive.HasValue)
        //                        {
        //                            if (packetFeature.FeatureActive.Value == false)
        //                            {
        //                                maxproductcount = 0;
        //                            }
        //                            else if (packetFeature.FeatureActive.Value == true)
        //                            {
        //                                //her üründe deneme ürünü eklemek için tıklayınız diyecek.
        //                                //if (Session["t"] == null)
        //                                //{
        //                                //    Session["t"] = 1;

        //                                //    return RedirectToRoute("AdvertNotLimitedAccess");
        //                                //}
        //                                //maxproductcount = 3;
        //                                //Session["t"] = null;

        //                                //return Redirect("~/UyelikSatis/adim1?sayfa=ilanekle");

        //                                processStatus.Result = $"İlan ekleyebilirsiniz.({packetFeature.FeatureContent})";
        //                                processStatus.Message.Header = "Advert";
        //                                processStatus.Message.Text = "Başarılı";
        //                                processStatus.Status = true;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            maxproductcount = packetFeature.FeatureProcessCount.Value;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        maxproductcount = store.ProductCount.Value;
        //                    }
        //                    var products = _productService.GetProductsByMainPartyId(member.MainPartyId);
        //                    productcount = products.Count - products.Where(c => c.ProductActive == false).Count() - products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Silindi).Count();
        //                    if (maxproductcount <= productcount)
        //                    {
        //                        processStatus.Result = "AdvertNotLimitedAccess";
        //                        processStatus.Message.Header = "Advert";
        //                        processStatus.Message.Text = "Başarısız";
        //                        processStatus.Status = false;
        //                    }
        //                }
        //                if (member.MemberType == (byte)MemberType.FastIndividual)
        //                {
        //                    processStatus.Result = "AdvertNotLimitedAccess";
        //                    processStatus.Message.Header = "Advert";
        //                    processStatus.Message.Text = "Başarısız";
        //                    processStatus.Status = false;
        //                }
        //                //else if (!PacketAuthenticationModel.IsAccess(PacketPage.UrunEkleme))
        //                //{
        //                //    //return RedirectToRoute("AdvertNotLimitedAccess");
        //                //    processStatus.Result = "AdvertNotLimitedAccess";
        //                //    processStatus.Message.Header = "Advert";
        //                //    processStatus.Message.Text = "Başarısız";
        //                //    processStatus.Status = false;
        //                //}

        //                var sectorCategories = _storeSectorService.GetStoreSectorsByMainPartyId(store.MainPartyId);
        //                var privateSectorCategories = new List<object>();
        //                if (sectorCategories.Count > 0)
        //                {
        //                    foreach (var item in sectorCategories)
        //                    {
        //                        var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);

        //                        privateSectorCategories.Add(new
        //                        {
        //                            Text = category.CategoryName,
        //                            Value = item.CategoryId.ToString()
        //                        });
        //                    }
        //                    privateSectorCategories.Add(new { Value = "-1", Text = "Tüm Sektörleri Gör" });
        //                }

        //                processStatus.Result = privateSectorCategories;
        //                processStatus.Message.Header = "Advert";
        //                processStatus.Message.Text = "Başarılı";
        //                processStatus.Status = true;
        //            }
        //        }
        //        else
        //        {
        //            processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
        //            processStatus.Message.Header = "Advert";
        //            processStatus.Message.Text = "Başarısız";
        //            processStatus.Status = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        processStatus.Message.Header = "advert";
        //        processStatus.Message.Text = "İşlem başarısız.";
        //        processStatus.Status = false;
        //        processStatus.Result = "Hata ile karşılaşıldı!";
        //        processStatus.Error = ex;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //}

        public HttpResponseMessage GetAll(int MainPartyId, ProductActiveType ActiveType=ProductActiveType.Tumu)
        {

            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    List<View.Advert> result = new List<Advert>();
                    var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(MainPartyId).Select(x => x.MemberMainPartyId).ToList();
                    var products = _productService.GetAllProductsByMainPartyIds(mainPartyIds).OrderByDescending(x => x.ProductId).ToList();
                    if (ActiveType!= ProductActiveType.Tumu)
                    {
                        products = products.Where(x => x.ProductActiveType == (byte)ActiveType).ToList();
                    }
                    foreach (var item in products)
                    {
                        string picturePath = "";
                        var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                        if (picture != null)
                            picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;

                        var model = item.ModelId.HasValue ? _categoryService.GetCategoryByCategoryId(item.ModelId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();
                        var serie = item.SeriesId.HasValue ? _categoryService.GetCategoryByCategoryId(item.SeriesId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();
                        var brand = item.BrandId.HasValue ? _categoryService.GetCategoryByCategoryId(item.BrandId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();

                        View.Advert AdvertItm = new Advert()
                        {
                            CategoryId = (int)item.CategoryId,
                            Description = item.ProductDescription,
                            Mensei = (byte)item.MenseiId,
                            Name = item.ProductName,
                            ProductId = item.ProductId,
                            Price = item.GetFormattedPrice(),
                            TypeText = item.GetProductTypeText(),
                            Picture = picturePath,
                            Order = (item.Sort != null ? (int)item.Sort : 0),
                            SalesType = item.ProductSalesType,
                            SalesTypeText = item.GetProductSalesTypeText(),
                            Active = item.ProductActive.Value,
                            ActiveType = item.ProductActiveType.HasValue == true ? item.ProductActiveType.Value : (byte)0,
                            Statu = item.ProductStatu,
                            StatuText = item.GetProductStatuText(),
                            ModelName = model != null ? model.CategoryName : "",
                            ModelYear = item.ModelYear.HasValue == true ? item.ModelYear.Value.ToString() : DateTime.Now.Year.ToString(),
                            SeriesName = serie.CategoryName != null ? serie.CategoryName : "",
                            CurrencyId = item.CurrencyId.HasValue == true ? item.CurrencyId.Value : (byte)0,
                            CurrencyText = item.GetCurrencySymbol(),
                            Doping = item.Doping,
                            ProductTypeText = item.GetProductTypeText(),
                            ProductType = item.ProductType,
                            BrandName = brand != null ? brand.CategoryName : "",
                            BriefDetail = item.BriefDetail,
                            BriefDetailText = item.GetBriefDetailText(),
                            CategoryName = item.Category != null ? item.Category.CategoryName : "",
                            ViewCount = item.ViewCount.Value,
                            CountryId = (item.CountryId != null ? (int)item.CountryId : 0),
                            Country = (item.Country != null) ? item.Country.CountryName : "",
                            LocalityId =(item.LocalityId!=null?(int)item.LocalityId:0),
                            Locality = (item.Locality != null) ? item.Locality.LocalityName : "",
                            CityId = (item.CityId != null ? (int)item.CityId : 0),
                            City = (item.City != null) ? item.City.CityName : "",
                            TownId = (item.TownId!= null?(int)item.TownId:0),
                            Town = (item.Town != null) ? item.Town.TownName : "",
                            ProductAdvertBeginDate=item.ProductAdvertBeginDate.Value,
                            ProductAdvertEndDate=item.ProductAdvertEndDate.Value
                        };
                        result.Add(AdvertItm);
                    }
                    processStatus.Result = result;
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = " Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "advert";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //public HttpResponseMessage InsertOrUpdate(View.Advert Model)
        //{
        //    ProcessResult processStatus = new ProcessResult();
        //    try
        //    {
        //        var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
        //        var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
        //        if (member != null)
        //        {
        //        }
        //        else
        //        {
        //            processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
        //            processStatus.Message.Header = "Advert";
        //            processStatus.Message.Text = "Başarısız";
        //            processStatus.Status = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        processStatus.Message.Header = "advert";
        //        processStatus.Message.Text = "İşlem başarısız.";
        //        processStatus.Status = false;
        //        processStatus.Result = "Hata ile karşılaşıldı!";
        //        processStatus.Error = ex;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //}

        //public HttpResponseMessage GetAdvert(int AdvertId)
        //{
        //    ProcessResult processStatus = new ProcessResult();
        //    try
        //    {
        //        var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
        //        var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
        //        if (member != null)
        //        {
        //            MakinaTurkiye.Api.View.Advert result = new Advert()
        //            { };
        //            var Product = _productService.GetProductByProductId(AdvertId);
        //            //if (Product!=null)
        //            //{
        //            //    result.MainMartyId=Product.MainPartyId
        //            //}

        //            processStatus.Result = result;
        //            processStatus.Message.Header = "Advert";
        //            processStatus.Message.Text = "Başarılı";
        //            processStatus.Status = true;
        //        }
        //        else
        //        {
        //            processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
        //            processStatus.Message.Header = "Advert";
        //            processStatus.Message.Text = "Başarısız";
        //            processStatus.Status = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        processStatus.Message.Header = "advert";
        //        processStatus.Message.Text = "İşlem başarısız.";
        //        processStatus.Status = false;
        //        processStatus.Result = "Hata ile karşılaşıldı!";
        //        processStatus.Error = ex;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //}

        public void ProductCountCalc(Product curProduct, bool add)
        {
            IList<TopCategoryResult> topCategories = new List<TopCategoryResult>();
            if (curProduct.ModelId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.ModelId.Value);
            }
            else if (curProduct.SeriesId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.SeriesId.Value);
            }
            else if (curProduct.BrandId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.BrandId.Value);
            }
            else if (curProduct.CategoryId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.CategoryId.Value);
            }

            if (topCategories.Count > 0)
            {
                foreach (var item in topCategories)
                {
                    var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                    if (category != null)
                    {
                        if (add)
                        {
                            category.ProductCount = category.ProductCount + 1;
                            if (category.CategoryType == (byte)CategoryType.Sector)
                            {
                                if (curProduct.ProductStatu == "72")
                                    category.ProductCountAll = category.ProductCountAll + 1;
                                else if (curProduct.ProductStatu == "73")
                                    category.ProductCountNew = category.ProductCountNew + 1;
                                else if (curProduct.ProductStatu == "201")
                                    category.ProductCountNew = category.ProductCountNew + 1;
                            }
                        }
                        else
                        {
                            category.ProductCount = category.ProductCount - 1;
                            if (category.CategoryType == (byte)CategoryType.Sector)
                            {
                                if (curProduct.ProductStatu == "72")
                                    category.ProductCountAll = category.ProductCountAll - 1;
                                else if (curProduct.ProductStatu == "73")
                                    category.ProductCountNew = category.ProductCountNew - 1;
                                else if (curProduct.ProductStatu == "201")
                                    category.ProductCountNew = category.ProductCountNew - 1;
                            }
                        }

                        _categoryService.UpdateCategory(category);
                    }
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage ChangeActiveType(int AdvertId, ProductActiveType ActiveType)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {

                        var product = _productService.GetProductByProductId(AdvertId);
                        if (product!=null)
                        {
                            product.ProductActiveType = (byte)ActiveType;
                            product.ProductLastUpdate = DateTime.Now;
                            _productService.UpdateProduct(product);
                            Transaction.Complete();
                        }
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {

                        var product = _productService.GetProductByProductId(AdvertId);
                        if (product.ProductActiveType != (byte)ProductActiveType.CopKutusuYeni)
                        {
                            ProductCountCalc(product, false);
                        }
                        product.ProductActiveType = (byte)ProductActiveType.CopKutusuYeni;
                        product.ProductLastUpdate = DateTime.Now;
                        _productService.UpdateProduct(product);
                        Transaction.Complete();
                        processStatus.Result = true;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage ChangeStatus(int AdvertId, bool Active)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        var product = _productService.GetProductByProductId(AdvertId);
                        if (Active)
                        {
                            product.ProductActiveType = (byte)ProductActiveType.Inceleniyor;
                        }
                        else if (product.ProductActive == true && Active == false)
                        {
                            ProductCountCalc(product, false);
                        }
                         product.ProductActive = Active;
                        product.ProductLastUpdate = DateTime.Now;
                        _productService.UpdateProduct(product);
                        Transaction.Complete();
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage ChangeOrder(int AdvertId, int Order)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        var product = _productService.GetProductByProductId(AdvertId);
                        if (Order > 0)
                        {
                            product.Sort = Order;
                        }
                        else
                        {
                            product.Sort = 0;
                        }
                        product.ProductLastUpdate = DateTime.Now;
                        _productService.UpdateProduct(product);
                        Transaction.Complete();

                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage ChangePrice(int AdvertId, decimal Price)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        var product = _productService.GetProductByProductId(AdvertId);
                        product.ProductPrice = Price;
                        product.ProductLastUpdate = DateTime.Now;
                        _productService.UpdateProduct(product);
                        Transaction.Complete();

                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage DeleteVideo(int VideoId)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        List<string> deletefiles = new List<string>();
                        var video = _videoService.GetVideoByVideoId(VideoId);
                        if (video!=null)
                        {
                            deletefiles.Add(AppSettings.VideoThumbnailFolder + video.VideoPicturePath);
                            deletefiles.Add(AppSettings.VideoFolder + video.VideoPath);
                            _videoService.DeleteVideo(video);
                            _videoService.ClearAllVideoCacheWithProductId((int)video.ProductId);
                        }

                        Transaction.Complete();
                        foreach (var item in deletefiles)
                        {
                            FileHelpers.Delete(item);
                        }
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetVideos(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    List<MakinaTurkiye.Api.View.ProductVideo> list = new List<ProductVideo>();
                    var product = _productService.GetProductByProductId(AdvertId);
                    var videos = _videoService.GetVideosByProductId(AdvertId);
                    foreach (var item in videos)
                    {
                        MakinaTurkiye.Api.View.ProductVideo video = new ProductVideo()
                        {
                            Active = item.Active.HasValue ? item.Active.Value : false,
                            VideoPath = item.VideoPath,
                            ProductId = item.ProductId.HasValue ? item.ProductId.Value : 0,
                            VideoSize = null,
                            VideoPicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                            VideoId = item.VideoId,
                            VideoTitle = item.VideoTitle,
                            ProductName = product.ProductName,
                            VideoRecordDate = DateTime.Now,
                            VideoMinute = item.VideoMinute,
                            VideoSecond = item.VideoSecond,
                            VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, product.ProductName)
                        };
                        list.Add(video);
                    }
                    processStatus.Result = list;
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "advert";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage AddVideo(AddProductVideo Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        // VideoEkleme = 5 Gitmesi Gerekiyor..
                        var videos = _videoService.GetVideoByStoreMainPartyId(Model.StoreMainMartyId);
                        var store = _storeService.GetStoreByMainPartyId(Model.StoreMainMartyId);
                        var packet = _packetService.GetPacketByPacketId(store.PacketId);
                        bool access = IsAccess(member.MainPartyId, PacketPage.VideoEkleme, videos.Count() + 1);
                        if (access)
                        {
                            var product = _productService.GetProductByProductId(Model.AdvertId);
                            string Logo = Model.File;
                            string Uzanti = "";
                            Uzanti = Logo.GetUzanti();
                            bool IslemDurum = false;
                            if (Uzanti == "mp4")
                            {
                                IslemDurum = true;
                            }
                            if (IslemDurum)
                            {
                                string newFileName = !string.IsNullOrEmpty(Model.Title) ? Model.Title : $"{product.ProductName}-{Guid.NewGuid()}";
                                var file = Logo.ToFile(newFileName);
                                VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert2(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                                DateTime timesplit;
                                if (!(DateTime.TryParse(vModel.Duration, out timesplit)))
                                {
                                    timesplit = DateTime.Now.Date;
                                }
                                var video = new MakinaTurkiye.Entities.Tables.Videos.Video()
                                {
                                    Active = true,
                                    ProductId = Model.AdvertId,
                                    VideoPath = vModel.newFileName,
                                    VideoPicturePath = vModel.newFileName + ".jpg",
                                    VideoTitle = Model.Title,
                                    VideoRecordDate = DateTime.Now,
                                    VideoMinute = (byte?)timesplit.Minute,
                                    VideoSecond = (byte?)timesplit.Second,
                                    SingularViewCount = 0,
                                };
                                _videoService.InsertVideo(video);
                                Transaction.Complete();
                                processStatus.Result = null;
                                processStatus.Message.Header = "Advert";
                                processStatus.Message.Text = "Başarılı";
                                processStatus.Status = true;
                            }
                            else
                            {
                                processStatus.Result = null;
                                processStatus.Message.Header = "Advert";
                                processStatus.Message.Text = "Geçersiz Dosya Formatı";
                                processStatus.Status = false;
                            }
                        }
                        else
                        {
                            processStatus.Result = "Paket Yetersiz";
                            processStatus.Message.Header = "Advert";
                            processStatus.Message.Text = "Bu İşlem için paket satın almalısınız.";
                            processStatus.Status = false;
                        }
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetPictures(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var product = _productService.GetProductByProductId(AdvertId);
                    if (product!=null)
                    {
                        processStatus.Result = _pictureService.GetPicturesByProductId(AdvertId).Select(item =>
                            new
                            {
                                PictureId= item.PictureId,
                                LargePath = !string.IsNullOrEmpty(item.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId.Value, item.PicturePath, ProductImageSize.px500x375) : "",
                                SmallPath = !string.IsNullOrEmpty(item.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId.Value, item.PicturePath, ProductImageSize.px400x300) : "",
                                MegaPicturePath = !string.IsNullOrEmpty(item.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId.Value, item.PicturePath, ProductImageSize.px900x675) : "",
                            }
                        );
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Ürün Bulunamadı";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "advert";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage AddPicture(AddProductPicture Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        var product = _productService.GetProductByProductId((int)Model.AdvertId);
                        if (product != null)
                        {
                            var productpictures = _pictureService.GetPicturesByProductId(product.ProductId).ToList();
                            int counter=0;
                            if (productpictures!=null)
                            {
                                counter = productpictures.Count()+1;
                            }

                            bool access = IsAccess(member.MainPartyId, PacketPage.UrunResimSayisi, counter);
                            if (access)
                            {
                                List<string> thumbSizes = new List<string>();
                                thumbSizes.AddRange(AppSettings.ProductThumbSizes.Replace("*", "").Split(';'));

                                string Logo = Model.File;
                                string Uzanti = "";
                                bool IslemDurum = false;
                                Uzanti = Model.File.GetUzanti();
                                if (Uzanti == "jpg")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "png")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "heic")
                                {
                                    IslemDurum = true;
                                }
                                if (IslemDurum)
                                {

                                    int count = 0;
                                    string productPicturePath = string.Empty;
                                    if (productpictures != null)
                                    {
                                        do
                                        {
                                            count++;
                                            productPicturePath = product.ProductName.ToImageFileName(count) + ".jpg";

                                        } while (productpictures.Where(c => c.PicturePath == productPicturePath).FirstOrDefault() != null);
                                    }

                                    string filePath = string.Empty;
                                    string newMainImageFilePath = AppSettings.ProductImageFolder + product.ProductId.ToString() + "\\";
                                    string fileName = product.ProductName.ToImageFileName(count) + ".jpg";
                                    string fileserverpath = System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + fileName;
                                    
                                    if (Uzanti == "png")
                                    {
                                        System.Drawing.Image Img = Logo.ToImage();
                                        Img.Save(fileserverpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }
                                    if (Uzanti == "heic")
                                    {
                                        DataImage dtimage = DataImage.TryParse(Logo);
                                        using (ImageMagick.MagickImage magickImage=new ImageMagick.MagickImage(dtimage.RawData))
                                        {
                                            System.IO.FileInfo fileInfo = new FileInfo(fileserverpath);
                                            if (!fileInfo.Directory.Exists)
                                            {
                                                fileInfo.Directory.Create();
                                            }
                                            magickImage.Write(fileserverpath);
                                        }
                                    }
                                    if (Uzanti == "jpg")
                                    {
                                        System.Drawing.Image Img = Logo.ToImage();
                                        Img.Save(fileserverpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }

                                    bool thumbResult = ImageProcessHelper.ImageResize(System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + fileName,
                                    System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + "thumbs\\" + product.ProductName.ToImageFileName(count), thumbSizes);
                                    foreach (var thumbSize in thumbSizes)
                                    {
                                        if (thumbSize == "980x*" || thumbSize == "500x375" || thumbSize == "*x980")
                                        {
                                            var yol = System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + "thumbs\\" +
                                                      product.ProductName.ToImageFileName(count) + "-" + thumbSize.Replace("*", "") + ".jpg";
                                            ImageProcessHelper.AddWaterMarkNew(yol, thumbSize);
                                        }
                                    }

                                    int pictureOrder = 0;
                                    if (productpictures != null)
                                    {
                                        pictureOrder = productpictures.Count() + 1;
                                    }
                                    var modelpicture = new Picture()
                                    {
                                        PictureName = "",
                                        PicturePath = fileName,
                                        ProductId = product.ProductId,
                                        PictureOrder = pictureOrder
                                    };
                                    _pictureService.InsertPicture(modelpicture);
                                    Transaction.Complete();
                                    processStatus.Result = null;
                                    processStatus.Message.Header = "Advert";
                                    processStatus.Message.Text = "Başarılı";
                                    processStatus.Status = true;
                                }
                                else
                                {
                                    processStatus.Result = null;
                                    processStatus.Message.Header = "Advert";
                                    processStatus.Message.Text = "Dosya Türü Geçersiz";
                                    processStatus.Status = false;
                                }
                            }
                            else
                            {
                                processStatus.Result = "Paket Yetersiz";
                                processStatus.Message.Header = "Advert";
                                processStatus.Message.Text = "Bu İşlem için paket satın almalısınız.";
                                processStatus.Status = false;
                            }
                        }
                        else
                        {
                            processStatus.Result = null;
                            processStatus.Message.Header = "Advert";
                            processStatus.Message.Text = "Product Bulunamadı.";
                            processStatus.Status = false;
                        }
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage DeletePicture(int PictureId)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        List<string> thumbSizes = new List<string>();
                        thumbSizes.AddRange(AppSettings.ProductThumbSizes.Replace("*", "").Split(';'));
                        List<string> deletefiles = new List<string>();
                        var picture= _pictureService.GetPictureByPictureId(PictureId);
                        if (picture != null)
                        {
                            var product = _productService.GetProductByProductId((int)picture.ProductId);
                            _pictureService.DeletePicture(picture);
                            if (product != null)
                            {
                                if (picture.PictureName=="")
                                {
                                    picture.PictureName = picture.PicturePath;
                                }
                                deletefiles.Add(AppSettings.ProductImageFolder + product.ProductId + "/" + picture.PictureName);
                                foreach (string thumb in thumbSizes)
                                {
                                    string imagetype = picture.PictureName.Substring(picture.PictureName.LastIndexOf("."), picture.PictureName.Length - picture.PictureName.LastIndexOf("."));//örnek .jpeg
                                    string imagename = picture.PictureName.Remove(picture.PictureName.Length - picture.PictureName.Substring(picture.PictureName.LastIndexOf("."), picture.PictureName.Length - picture.PictureName.LastIndexOf(".")).Length);
                                    deletefiles.Add(AppSettings.ProductImageFolder + product.ProductId + "/" + "thumbs/" + imagename + "-" + thumb + imagetype);
                                }
                            }
                        }
                        Transaction.Complete();
                        foreach (var item in deletefiles)
                        {
                            FileHelpers.Delete(item);
                        }
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetCatalogs(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var product = _productService.GetProductByProductId(AdvertId);
                    if (product != null)
                    {
                        processStatus.Result = _productCatologService.GetProductCatologsByProductId(AdvertId).Select(catolog =>
                            new
                            {
                                CatalogId = catolog.ProductCatologId,
                                Url = !string.IsNullOrEmpty(catolog.FileName) ? "https:" + FileUrlHelper.GetProductCatalogUrl(catolog.FileName, product.ProductId) : "",
                            }
                        );
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Ürün Bulunamadı";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "advert";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage AddCatalog(AddProductCatalog Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        
                        var product = _productService.GetProductByProductId(Model.AdvertId);
                        if (product != null)
                        {
                            string Logo = Model.File;
                            string Uzanti = "";
                            Uzanti = Logo.GetUzanti();
                            bool IslemDurum = false;
                            if (Uzanti == "jpg")
                            {
                                IslemDurum = true;
                            }
                            if (Uzanti == "png")
                            {
                                IslemDurum = true;
                            }
                            if (Uzanti == "pdf")
                            {
                                IslemDurum = true;
                            }
                            if (IslemDurum)
                            {
                                var file= Logo.ToFile();
                                int count = 0;
                                var productCatologs = _productCatologService.GetProductCatologsByProductId(Model.AdvertId);
                                if (productCatologs!=null)
                                {
                                    count = productCatologs.Count() + 1;
                                }
                                string filePath = FileUploadHelper.UploadFile(file, AppSettings.ProductCatologFolder + "/" + product.ProductId, product.ProductName, count);
                                if (filePath != "")
                                {
                                    var productCatolog = new ProductCatolog();
                                    productCatolog.FileName = filePath;
                                    productCatolog.ProductId = product.ProductId;
                                    _productCatologService.InsertProductCatolog(productCatolog);
                                }
                                Transaction.Complete();
                                processStatus.Result = null;
                                processStatus.Message.Header = "Advert";
                                processStatus.Message.Text = "Başarılı";
                                processStatus.Status = true;
                            }
                            else
                            {
                                processStatus.Result = null;
                                processStatus.Message.Header = "Advert";
                                processStatus.Message.Text = "Geçersiz Dosya Formatı";
                                processStatus.Status = false;
                            }
                        }
                        else
                        {
                            processStatus.Result = null;
                            processStatus.Message.Header = "Advert";
                            processStatus.Message.Text = "İlan Bulunamadı";
                            processStatus.Status = false;
                        }

                        //// VideoEkleme = 5 Gitmesi Gerekiyor..
                        //var videos = _videoService.GetVideoByStoreMainPartyId(Model.StoreMainMartyId);
                        //var store = _storeService.GetStoreByMainPartyId(Model.StoreMainMartyId);
                        //var packet = _packetService.GetPacketByPacketId(store.PacketId);
                        //bool access = packet.IsStandart.GetValueOrDefault(false);
                        //if (!access)
                        //{
                        //    var packetFeatures = packet.PacketFeatures;
                        //    var packFeat = packetFeatures.FirstOrDefault(c => c.PacketFeatureTypeId == 5);
                        //    switch (packFeat.FeatureType)
                        //    {
                        //        case 1:
                        //            access = packFeat.FeatureProcessCount >= videos.Count() + 1;
                        //            break;
                        //        case 2:
                        //        case 3:
                        //            access = true;
                        //            break;
                        //        default:
                        //            break;
                        //    }
                        //}
                        //if (access)
                        //{

                        //}
                        //else
                        //{
                        //    processStatus.Result = "Paket Yetersiz";
                        //    processStatus.Message.Header = "Advert";
                        //    processStatus.Message.Text = "Bu İşlem için Paket Satın Almalısınız.";
                        //    processStatus.Status = false;
                        //}
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage DeleteCatalog(int CatalogId)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        List<string> deletefiles = new List<string>();
                        var Catalog = _productCatologService.GetProductCatologByProductCatologId(CatalogId);
                        if (Catalog!=null)
                        {
                            var filePath = FileUrlHelper.GetProductCatalogUrl(Catalog.FileName, Catalog.ProductId);
                            deletefiles.Add(AppSettings.StoreCatologFolder + "/" + Catalog.ProductId + "/" + Catalog.FileName);
                            _productCatologService.DeleteProductCatolog(Catalog);
                        }

                        Transaction.Complete();
                        foreach (var item in deletefiles)
                        {
                            FileHelpers.Delete(item);
                        }
                        processStatus.Result = null;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetDashborad(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(MainPartyId).Select(x => x.MemberMainPartyId).ToList();
                    var products = _productService.GetAllProductsByMainPartyIds(mainPartyIds).OrderByDescending(x => x.ProductId).ToList();
                    int Tumu = products.Count();
                    int Inceleniyor= products.Count(x=>x.ProductActiveType==(byte)ProductActiveType.Inceleniyor);
                    int Onaylandi = products.Count(x => x.ProductActiveType == (byte)ProductActiveType.Onaylandi);
                    int Onaylanmadi = products.Count(x => x.ProductActiveType == (byte)ProductActiveType.Onaylanmadi);
                    int Silindi = products.Count(x => x.ProductActiveType == (byte)ProductActiveType.Silindi);
                    int CopKutusunda = products.Count(x => x.ProductActiveType == (byte)ProductActiveType.CopKutusunda | x.ProductActiveType == (byte)ProductActiveType.CopKutusuYeni);

                    var productComments = _productCommentService.GetProductCommentsForStoreByMemberMainPartyId(member.MainPartyId);
                    int ProductComment = productComments.Count();
                    var favoriteProducts = _favoriteProductService.GetFavoriteProducts().Where(x => x.MainPartyId == member.MainPartyId).ToList();
                    int FavoriProduct = favoriteProducts.Count();

                    var favoriteStores = _favoriteStoreService.GetFavoriteStoresByMemberMainPartyId(member.MainPartyId).ToList();
                    int FavoriStore = favoriteProducts.Count();

                    var MessageInboxs = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageType.Inbox);
                    int MessageInbox = MessageInboxs.Count();


                    var MessageOutboxs = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageType.Outbox);
                    int MessageOutbox = MessageOutboxs.Count();

                    var MessageRecyleBins = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageType.RecyleBin);
                    int MessageRecyleBin = MessageRecyleBins.Count();



                    var chardata = products.GroupBy(x => x.ProductAdvertBeginDate.Value.Date).Select(x => new {
                        key = x.Key,
                        value = x.Count()
                    });

                    processStatus.Result = new {
                        Tumu=Tumu,
                        Inceleniyor= Inceleniyor,
                        Onaylandi= Onaylandi,
                        Onaylanmadi= Onaylanmadi,
                        Silindi =Silindi,
                        CopKutusunda= CopKutusunda,
                        FavoriProduct= FavoriProduct,
                        FavoriStore= FavoriStore,
                        ProductComment = ProductComment,
                        MessageInbox = MessageInbox,
                        MessageOutbox = MessageOutbox,
                        MessageRecyleBin = MessageRecyleBin,
                        Chart = new 
                        { 
                            Series= chardata.Select(x => x.key),
                            Values = chardata.Select(x => x.value)
                        }
                    };
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "advert";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        private bool IsAccess(int MainPartyId, PacketPage page, int value = 0)
        {
            bool access = false;
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(MainPartyId);
                if (memberStore != null)
                {
                    IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();
                    IPacketService packetService = EngineContext.Current.Resolve<IPacketService>();

                    var store = storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                    var packet = packetService.GetPacketByPacketId(store.PacketId);
                    access = packet.IsStandart.GetValueOrDefault(false);

                    if (!access)
                    {
                        var packetFeatures = packet.PacketFeatures;
                        var packFeat = packetFeatures.FirstOrDefault(c => c.PacketFeatureTypeId == (byte)page);

                        switch ((PacketFeatureType)packFeat.FeatureType)
                        {
                            case PacketFeatureType.ProcessCount:
                                access = packFeat.FeatureProcessCount >= value;
                                break;
                            case PacketFeatureType.Active:
                            case PacketFeatureType.Content:
                                access = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return access;
        }
        public HttpResponseMessage GetAdvert(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var product = _productService.GetProductByProductId(AdvertId);
                    if (product == null)
                    {
                        product = new Product();
                    }
                    string picturePath = "";
                    var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
                    if (picture != null)
                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;

                    var model = product.ModelId.HasValue ? _categoryService.GetCategoryByCategoryId(product.ModelId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();
                    var serie = product.SeriesId.HasValue ? _categoryService.GetCategoryByCategoryId(product.SeriesId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();
                    var brand = product.BrandId.HasValue ? _categoryService.GetCategoryByCategoryId(product.BrandId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();

                    View.Advert AdvertItm = new Advert()
                    {
                        CategoryId = (int)product.CategoryId,
                        Description = product.ProductDescription,
                        Mensei = (byte)product.MenseiId,
                        Name = product.ProductName,
                        ProductId = product.ProductId,
                        Price = product.GetFormattedPrice(),
                        TypeText = product.GetProductTypeText(),
                        Picture = picturePath,
                        Order = (product.Sort != null ? (int)product.Sort : 0),
                        SalesType = product.ProductSalesType,
                        SalesTypeText = product.GetProductSalesTypeText(),
                        Active = product.ProductActive.Value,
                        ActiveType = product.ProductActiveType.HasValue == true ? product.ProductActiveType.Value : (byte)0,
                        Statu = product.ProductStatu,
                        StatuText = product.GetProductStatuText(),
                        ModelName = model != null ? model.CategoryName : "",
                        ModelYear = product.ModelYear.HasValue == true ? product.ModelYear.Value.ToString() : DateTime.Now.Year.ToString(),
                        SeriesName = serie.CategoryName != null ? serie.CategoryName : "",
                        CurrencyId = product.CurrencyId.HasValue == true ? product.CurrencyId.Value : (byte)0,
                        CurrencyText = product.GetCurrencySymbol(),
                        Doping = product.Doping,
                        ProductTypeText = product.GetProductTypeText(),
                        ProductType = product.ProductType,
                        BrandName = brand != null ? brand.CategoryName : "",
                        BriefDetail = product.GetBriefDetailText(),
                        CategoryName = product.Category != null ? product.Category.CategoryName : "",
                        ViewCount = product.ViewCount.Value,
                        CountryId = (int)(product.CountryId==null?0:product.CountryId),
                        Country = (product.Country != null) ? product.Country.CountryName : "",
                        LocalityId = (int)(product.LocalityId==null?0:product.LocalityId),
                        Locality = (product.Locality != null) ? product.Locality.LocalityName : "",
                        CityId = (int)(product.CityId==null?0:product.CityId),
                        City = (product.City != null) ? product.City.CityName : "",
                        TownId = (int)(product.TownId==null?0:product.TownId),
                        Town = (product.Town != null) ? product.Town.TownName : "",
                        ProductAdvertBeginDate = product.ProductAdvertBeginDate.Value,
                        ProductAdvertEndDate = product.ProductAdvertEndDate.Value
                    };
                    processStatus.Result = AdvertItm;
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Ürün Bulunamadı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Advert";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "advert";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage AddOrUpdateAdvert(View.Advert Model)
        {
            string fileserverlogpath = System.Web.Hosting.HostingEnvironment.MapPath($"~/Content/Log/Istek{DateTime.Now.ToString().Replace(".", "_").Replace(":", "__")}.txt");
            string modeltxt = Newtonsoft.Json.JsonConvert.SerializeObject(Model,Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(fileserverlogpath, modeltxt);

            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (member != null)
                    {
                        MakinaTurkiye.Entities.Tables.Catalog.Product product = null;
                        if (Model.ProductId>0)
                        {
                            product=_productService.GetProductByProductId(Model.ProductId);
                        }                        
                        if (product == null)
                        {
                            product = new Product();
                            product.ProductAdvertBeginDate = DateTime.Now;
                            product.ProductAdvertEndDate = DateTime.Now;
                            product.ModelYear = 0;
                            product.MoneyCondition = true;
                            product.ProductActiveType = (byte)ProductActiveType.Inceleniyor;
                            product.ProductShowcase = false;
                            product.Sort = 0;
                            product.HasVideo = false;
                            product.ProductPriceBegin = 0;
                            product.ProductPriceLast = 0;
                        }
                        product.ProductGroupId = Model.ProductGroupId;
                        product.ProductPriceType =(byte)Model.ProductPriceType;
                        product.UnitType = Model.UnitType;
                        product.ProductSalesType = Model.SalesType;
                        product.WarrantyPeriod = Model.WarrantyPeriod;
                        product.OrderStatus = Model.OrderStatus;
                        product.MinumumAmount = Model.MinumumAmount.HasValue ? Model.MinumumAmount.Value : (byte)1;
                        product.ProductName = Model.Name;
                        product.ProductDescription = Model.Description;
                        product.ProductType = Model.ProductType;
                        product.ProductStatu = Model.Statu;
                        product.ProductActiveType = Model.ActiveType;
                        product.ProductLastUpdate = DateTime.Now;
                        product.MenseiId = Model.Mensei.Value;
                        product.BriefDetail = Model.BriefDetail;
                        MakinaTurkiye.Entities.Tables.Catalog.Category brand = null;
                        MakinaTurkiye.Entities.Tables.Catalog.Category model = null;
                        MakinaTurkiye.Entities.Tables.Catalog.Category category= _categoryService.GetCategoryByCategoryId(Model.CategoryId);
                        if (category != null)
                        {
                            brand = _categoryService.GetCategoryByCategoryId(Model.BrandId);
                            if (brand == null | brand.CategoryId==0)
                            {
                                if (!string.IsNullOrEmpty(Model.Brand))
                                {
                                    brand = new global::MakinaTurkiye.Entities.Tables.Catalog.Category
                                    {
                                        Active = true,
                                        RecordCreatorId = 99,
                                        RecordDate = DateTime.Now,
                                        LastUpdateDate = DateTime.Now,
                                        LastUpdaterId = 99,
                                        CategoryType = (byte)CategoryType.Brand,
                                        CategoryOrder = 0,
                                        CategoryName = Model.Brand,
                                        CategoryParentId = category.CategoryId,
                                        MainCategoryType = (byte)MainCategoryType.Ana_Kategori,
                                        CategoryContentTitle = Model.BrandName,
                                        CategoryPath = "",
                                    };
                                    _categoryService.InsertCategory(brand);
                                    brand.CategoryPathUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName, brand.CategoryId, brand.CategoryName);
                                    _categoryService.UpdateCategory(category);
                                    _categoryService.InsertCategory(brand);
                                }
                            }
                            if (brand!=null)
                            {
                                model = _categoryService.GetCategoryByCategoryId(Model.ModelId);
                                if (model.Id== 0)
                                {
                                    if (!string.IsNullOrEmpty(Model.Model))
                                    {
                                        model = new global::MakinaTurkiye.Entities.Tables.Catalog.Category
                                        {
                                            Active = true,
                                            RecordCreatorId = 99,
                                            RecordDate = DateTime.Now,
                                            LastUpdateDate = DateTime.Now,
                                            LastUpdaterId = 99,
                                            CategoryType = (byte)CategoryType.Model,
                                            CategoryOrder = 0,
                                            CategoryName = Model.Model,
                                            CategoryParentId = brand.CategoryId,
                                            MainCategoryType = (byte)MainCategoryType.Ana_Kategori,
                                            CategoryContentTitle = Model.ModelName,
                                            CategoryPath = "",
                                        };
                                        _categoryService.InsertCategory(model);
                                        model.CategoryPathUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName, model.CategoryId, model.CategoryName);
                                        _categoryService.UpdateCategory(category);
                                        _categoryService.InsertCategory(model);
                                    }
                                }
                            }
                        }
                        if (brand==null || brand.CategoryId==0)
                        {
                            brand = new Category();
                        }
                        if (model == null || model.CategoryId == 0)
                        {
                            model = new Category();
                        }
                        product.CategoryId =Model.CategoryId;
                        product.BrandId = brand.CategoryId;
                        product.ModelId = model.CategoryId;

                        DateTime dateProductEndDate = DateTime.Now;
                        switch (Model.ProductPublicationDateType)
                        {
                            case ProductPublicationDateType.Gun:
                                dateProductEndDate = dateProductEndDate.AddDays(Model.ProductPublicationDate);
                                break;

                            case ProductPublicationDateType.Ay:
                                dateProductEndDate = dateProductEndDate.AddHours(Model.ProductPublicationDate);
                                break;

                            case ProductPublicationDateType.Yil:
                                dateProductEndDate = dateProductEndDate.AddYears(Model.ProductPublicationDate);
                                break;

                            default:
                                break;
                        }
                        if (Model.ProductPublicationDateType != 0)
                        {
                            product.ProductAdvertEndDate = dateProductEndDate;
                        }

                        if (Model.CountryId > 0)
                            product.CountryId = Model.CountryId;
                        else
                            product.CountryId = null;

                        if (Model.CityId > 0)
                            product.CityId = Model.CityId;
                        else
                            product.CityId = null;

                        if (Model.LocalityId > 0)
                            product.LocalityId = Model.LocalityId;
                        else
                            product.LocalityId = null;

                        if (Model.TownId > 0)
                            product.TownId = Model.TownId;
                        else
                            product.TownId = null;

                        if (Model.CurrencyId > 0)
                            product.CurrencyId = Model.CurrencyId;
                        else
                            product.CurrencyId = null;
                        
                        var cultInfo = new CultureInfo("tr-TR");

                        string productPrice = Model.Price;
                        if (Model.ProductPriceType == ProductPriceType.Price)
                        {
                            product.Fob = false;
                            product.Kdv = false;
                            product.UnitType = Model.UnitType;

                            if (productPrice.Trim() != "")
                            {
                                if (productPrice.IndexOf('.') > 0 && productPrice.IndexOf(",") > 0)
                                {
                                    productPrice = productPrice.Replace(".", "");
                                }
                                else
                                {
                                    productPrice = productPrice.Replace(".", ",");
                                }
                            }
                            else productPrice = "0";
                            decimal price = 0;
                            if (productPrice!="Fiyat Sorunuz")
                            {
                                 price= Convert.ToDecimal(productPrice, cultInfo.NumberFormat);
                            }
                            product.ProductPrice = price;

                            product.DiscountType = Convert.ToByte(Model.DiscountType);
                            if (Model.DiscountType != 0)
                            {
                                if (Model.DiscountAmount>0)
                                {
                                    product.DiscountAmount = Model.DiscountAmount;
                                    Model.TotalPrice = Convert.ToDecimal(product.ProductPrice - ((product.ProductPrice.Value * product.DiscountAmount) / 100));
                                    product.ProductPriceWithDiscount = Model.TotalPrice;
                                }
                            }
                            else
                            {
                                product.DiscountAmount = 0;
                                product.ProductPriceWithDiscount = 0;
                            }
                        }
                        else if (Model.ProductPriceType==ProductPriceType.PriceRange)
                        {
                            product.Fob = Model.Fob;
                            product.Kdv = Model.Kdv;

                            string productPriceLast = Model.ProductPriceLast.ToString();
                            string productPriceBegin = Model.ProductPriceBegin.ToString();
                            if (productPriceLast.Trim() != "")
                            {
                                if (productPriceLast.IndexOf('.') > 0 && productPriceLast.IndexOf(",") > 0)
                                {
                                    productPriceLast = productPriceLast.Replace(".", "");
                                }
                                else
                                {
                                    productPriceLast = productPriceLast.Replace(".", ",");
                                }
                            }

                            if (productPriceBegin.Trim() != "")
                            {
                                if (productPriceBegin.IndexOf('.') > 0 && productPriceLast.IndexOf(",") > 0)
                                {
                                    productPriceBegin = productPriceBegin.Replace(".", "");
                                }
                                else
                                {
                                    productPriceBegin = productPriceBegin.Replace(".", ",");
                                }
                            }

                            product.ProductPriceBegin = Convert.ToDecimal(productPriceBegin, cultInfo.NumberFormat);
                            product.ProductPriceLast = Convert.ToDecimal(productPriceLast, cultInfo.NumberFormat);
                            productPrice = "0";
                            
                        }
                        else
                        {
                            productPrice = "0";
                            product.ProductPriceLast = 0;
                            product.ProductPriceBegin = 0;
                            product.ProductPrice = 0;
                            product.ProductPriceWithDiscount = 0;
                            product.DiscountType = 0;
                            product.DiscountAmount = 0;
                        }




                        if (product.ProductPriceWithDiscount > 0)
                            product.ProductPriceForOrder = product.ProductPriceWithDiscount;

                        if (product.ProductId == 0)
                        {
                            product.ReadyforSale = false;

                            product.ProductRecordDate = DateTime.Now;
                            product.ProductActive = true;
                            product.ViewCount = 0;
                            product.SingularViewCount = 0;
                            product.MainPartyId = member.MainPartyId;
                            _productService.InsertProduct(product);

                            product.ProductNo = "#" + product.ProductId.ToString();
                            int kalansayisi = 8 - product.ProductNo.ToString().Length;
                            if (kalansayisi < 0)
                            {
                                kalansayisi = 0;
                            }
                            for (int i = 0; i < kalansayisi; i++)
                            {
                                product.ProductNo = "0" + product.ProductNo;
                            }
                            _productService.UpdateProduct(product);
                        }
                        else
                        {
                            product.ProductNo = "#"+product.ProductId.ToString();
                            int kalansayisi = 8 - product.ProductNo.ToString().Length;
                            if (kalansayisi < 0)
                            {
                                kalansayisi = 0;
                            }
                            for (int i = 0; i < kalansayisi; i++)
                            {
                                product.ProductNo = "0" + product.ProductNo;
                            }
                            _productService.UpdateProduct(product);
                        }

                        var productpictures = _pictureService.GetPicturesByProductId(product.ProductId).ToList();
                        int PictureOrder = 0;
                        List<string> thumbSizes = new List<string>();
                        thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

                        // Main Picture Yoksa IsMain olanların ilk önce eklenmesi için Orderby Desc yapıldı

                        if (productpictures.Count==0)
                        {
                            List<AdvertItemPicture> PicturesListe = new List<AdvertItemPicture>();
                            PicturesListe.AddRange(Model.Pictures.Where(x => x.IsMain));
                            PicturesListe.AddRange(Model.Pictures.Where(x => !x.IsMain));
                            Model.Pictures = PicturesListe;
                        }

                        //int count = 0;
                        //string productPicturePath = string.Empty;
                        //if (productpictures != null)
                        //{
                        //    PictureOrder = productpictures.Count();
                        //    do
                        //    {
                        //        count++;
                        //        productPicturePath = product.ProductName.ToImageFileName(count) + ".jpg";

                        //    } while (productpictures.Where(c => c.PicturePath == productPicturePath).FirstOrDefault() != null);
                            
                        //}
                        foreach (var Picture in Model.Pictures)
                        {
                            PictureOrder++;
                            string Logo = Picture.Value;
                            string Uzanti = "";
                            Uzanti = Logo.GetUzanti();
                            bool IslemDurum = false;
                            if (Uzanti == "jpg")
                            {
                                IslemDurum = true;
                            }
                            if (Uzanti == "png")
                            {
                                IslemDurum = true;
                            }
                            if (Uzanti == "heic")
                            {
                                IslemDurum = true;
                            }
                            if (IslemDurum)
                            {

                                string filePath = string.Empty;
                                string newMainImageFilePath = AppSettings.ProductImageFolder + product.ProductId.ToString() + "\\";
                                string fileName = product.ProductName.ToImageFileName(PictureOrder) + ".jpg";
                                string fileserverpath = System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + fileName;
                                System.IO.FileInfo file = new FileInfo(fileserverpath);
                                if (!file.Directory.Exists)
                                {
                                    file.Directory.Create();
                                }

                                if (Uzanti == "png")
                                {
                                    System.Drawing.Image Img = Logo.ToImage();
                                    Img.Save(fileserverpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                if (Uzanti == "heic")
                                {
                                    DataImage dtimage = DataImage.TryParse(Logo);
                                    using (ImageMagick.MagickImage magickImage = new ImageMagick.MagickImage(dtimage.RawData))
                                    {
                                        System.IO.FileInfo fileInfo = new FileInfo(fileserverpath);
                                        if (!fileInfo.Directory.Exists)
                                        {
                                            fileInfo.Directory.Create();
                                        }
                                        magickImage.Write(fileserverpath);
                                    }
                                }
                                if (Uzanti == "jpg")
                                {
                                    System.Drawing.Image Img = Logo.ToImage();
                                    Img.Save(fileserverpath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }

                                bool thumbResult = ImageProcessHelper.ImageResize(System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + fileName,
                                System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + "thumbs\\" + product.ProductName.ToImageFileName(PictureOrder), thumbSizes);
                                if (thumbResult)
                                {
                                    foreach (var thumbSize in thumbSizes)
                                    {
                                        if (thumbSize == "900x675" || thumbSize == "500x375")
                                        {
                                            var yol = System.Web.Hosting.HostingEnvironment.MapPath(newMainImageFilePath) + "thumbs\\" +
                                                      product.ProductName.ToImageFileName(PictureOrder) + "-" + thumbSize.Replace("*", "") + ".jpg";
                                            System.IO.FileInfo fileyol = new FileInfo(yol);
                                            if (!fileyol.Directory.Exists)
                                            {
                                                fileyol.Directory.Create();
                                            }
                                            ImageProcessHelper.AddWaterMarkNew(yol, thumbSize);
                                        }
                                    }
                                }
                                else
                                {
                                    
                                }                                
                                var modelpicture = new Picture()
                                {
                                    PictureName = "",
                                    PicturePath = fileName,
                                    ProductId = product.ProductId,
                                    PictureOrder = PictureOrder,
                                };
                                _pictureService.InsertPicture(modelpicture);
                            }
                        }
                        Transaction.Complete();
                        processStatus.Result = product.ProductId;
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "advert";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Hata ile karşılaşıldı!";
                    processStatus.Error = ex;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


    }
}