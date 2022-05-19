using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

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

        

        public HttpResponseMessage GetAll(int MainPartyId)
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
                    foreach (var item in products)
                    {
                        string picturePath = "";
                        var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                        if (picture != null)
                            picturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150);

                        var model = item.ModelId.HasValue ? _categoryService.GetCategoryByCategoryId(item.ModelId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();
                        var serie = item.SeriesId.HasValue ? _categoryService.GetCategoryByCategoryId(item.SeriesId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();
                        var brand = item.BrandId.HasValue ? _categoryService.GetCategoryByCategoryId(item.BrandId.Value) : new MakinaTurkiye.Entities.Tables.Catalog.Category();

                        View.Advert AdvertItm = new Advert()
                        {
                            CategoryId=(int)item.CategoryId,
                            Description= item.ProductDescription,
                            Mensei=(byte)item.MenseiId,
                            Name=item.ProductName,
                            ProductId=item.ProductId,
                            Price=item.GetFormattedPrice(),
                            TypeText = item.GetProductTypeText(),
                            Picture= picturePath,
                            Order=(item.Sort!=null?(int)item.Sort:0),
                            SalesType=item.ProductSalesType,
                            SalesTypeText = item.GetProductSalesTypeText(),
                            Active = item.ProductActive.Value,
                            ActiveType = item.ProductActiveType.HasValue == true ? item.ProductActiveType.Value : (byte)0,
                            Statu=item.ProductStatu,
                            StatuText=item.GetProductStatuText(),
                            ModelName = model != null ? model.CategoryName : "",
                            ModelYear = item.ModelYear.HasValue == true ? item.ModelYear.Value.ToString() : DateTime.Now.Year.ToString(),
                            SeriesName = serie.CategoryName != null ? serie.CategoryName : "",
                            CurrencyId = item.CurrencyId.HasValue == true ? item.CurrencyId.Value : (byte)0,
                            Doping=item.Doping,
                            ProductTypeText = item.GetProductTypeText(),
                            ProductType= item.ProductType,
                            BrandName = brand != null ? brand.CategoryName : "",
                            BriefDetail = item.GetBriefDetailText(),
                            CategoryName = item.Category != null ? item.Category.CategoryName : "",
                            ViewCount = item.ViewCount.Value,
                            CountryId=(int)item.CountryId,
                            Country = (item.Country != null) ? item.Country.CountryName : "",
                            LocalityId =(int)item.LocalityId,
                            Locality = (item.Locality != null) ? item.Locality.LocalityName : "",
                            CityId = (int)item.CityId,
                            City = (item.City != null) ? item.City.CityName : "",
                            TownId = (int)item.TownId,
                            Town = (item.Town != null) ? item.Town.TownName : "",
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
        public HttpResponseMessage InsertOrUpdate(View.Advert Model)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {

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


        public HttpResponseMessage GetAdvert(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    MakinaTurkiye.Api.View.Advert result = new Advert()
                    { };
                    var Product = _productService.GetProductByProductId(AdvertId);
                    //if (Product!=null)
                    //{
                    //    result.MainMartyId=Product.MainPartyId
                    //}


                    processStatus.Result = result;
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
        public HttpResponseMessage Delete(int AdvertId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    
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
    }
}