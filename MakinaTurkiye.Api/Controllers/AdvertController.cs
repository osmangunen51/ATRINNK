using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

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

       
        public AdvertController(IProductCommentService productCommentService,
            IMemberService memberService, IMemberStoreService memberStoreService,
            IProductService productService, IStoreService storeService, IPacketService packetService,
            IStoreSectorService storeSectorService)
        {
            this._productCommentService = productCommentService;
            this._memberService = memberService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._productService = productService;
            this._storeSectorService = storeSectorService;

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

        public HttpResponseMessage Advert()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    if (member.MemberType != (byte)MemberType.Enterprise)
                    {
                        processStatus.Result = "AdvertNotMemberTypeAccess";
                        processStatus.Message.Header = "Advert";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    //Commente alındı
                    {
                    //    else
                    //{
                    //        int productcount = 0;
                    //        int maxproductcount = 0;
                    //        byte isitstore = (byte)member.MemberType;
                    //        var storemember = new global::MakinaTurkiye.Entities.Tables.Members.MemberStore();
                    //        var store = new global::MakinaTurkiye.Entities.Tables.Stores.Store();
                    //        if (isitstore == 20)
                    //        {
                    //            storemember = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
                    //            store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(storemember.StoreMainPartyId));
                    //            if (store.PacketId == 12)
                    //            {
                    //                store.PacketId = 20;
                    //                _storeService.UpdateStore(store);
                    //            }
                    //            if (store.ProductCount == null)
                    //            {

                    //                var packetFeature = _packetService.GetPacketFeatureByPacketIdAndPacketFeatureTypeId(store.PacketId, 3);

                    //                if (packetFeature.FeatureContent != null)
                    //                {
                    //                    maxproductcount = 9999;
                    //                }
                    //                else if (packetFeature.FeatureActive != null)
                    //                {
                    //                    if (packetFeature.FeatureActive == false)
                    //                    {
                    //                        maxproductcount = 0;
                    //                    }
                    //                    else if (packetFeature.FeatureActive == true)
                    //                    {
                    //                        //her üründe deneme ürünü eklemek için tıklayınız diyecek.
                    //                        //if (Session["t"] == null)
                    //                        //{
                    //                        //    Session["t"] = 1;

                    //                        //    return RedirectToRoute("AdvertNotLimitedAccess");
                    //                        //}
                    //                        //maxproductcount = 3;
                    //                        //Session["t"] = null;

                    //                        //return Redirect("~/UyelikSatis/adim1?sayfa=ilanekle");

                    //                        processStatus.Result = "İlan başarıyla eklenmiştir.";
                    //                        processStatus.Message.Header = "Advert";
                    //                        processStatus.Message.Text = "Başarılı";
                    //                        processStatus.Status = true;

                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    maxproductcount = packetFeature.FeatureProcessCount.Value;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                maxproductcount = store.ProductCount.Value;
                    //            }
                    //            var products = _productService.GetProductsByMainPartyId(Convert.ToInt32(member.MainPartyId));
                    //            productcount = products.Count() - products.Where(c => c.ProductActive == false).Count() - products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Silindi).Count();
                    //            if (maxproductcount <= productcount)
                    //            {
                    //                //return RedirectToRoute("AdvertNotLimitedAccess");
                    //                processStatus.Result = "AdvertNotLimitedAccess";
                    //                processStatus.Message.Header = "Advert";
                    //                processStatus.Message.Text = "Başarısız";
                    //                processStatus.Status = false;
                    //            }
                    //        }
                    //        if (member.MemberType == (byte)MemberType.FastIndividual)
                    //        {
                    //            //return RedirectToRoute("AdvertNotLimitedAccess");
                    //            processStatus.Result = "AdvertNotLimitedAccess";
                    //            processStatus.Message.Header = "Advert";
                    //            processStatus.Message.Text = "Başarısız";
                    //            processStatus.Status = false;
                    //        }
                    //        else if (!PacketAuthenticationModel.IsAccess(PacketPage.UrunEkleme))
                    //        {
                    //            //return RedirectToRoute("AdvertNotLimitedAccess");
                    //            processStatus.Result = "AdvertNotLimitedAccess";
                    //            processStatus.Message.Header = "Advert";
                    //            processStatus.Message.Text = "Başarısız";
                    //            processStatus.Status = false;
                    //        }

                    //        ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
                    //        var sectorCategories = _storeSectorService.GetStoreSectorsByMainPartyId(store.MainPartyId);
                    //        var privateSectorCategories = new List<SelectListItem>();
                    //        if (sectorCategories.Count > 0)
                    //        {

                    //            foreach (var item in sectorCategories)
                    //            {
                    //                var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);

                    //                privateSectorCategories.Add(new SelectListItem
                    //                {
                    //                    Text = category.CategoryName,
                    //                    Value = item.CategoryId.ToString()
                    //                });
                    //            }
                    //            privateSectorCategories.Add(new SelectListItem { Value = "-1", Text = "Tüm Sektörleri Gör" });
                    //        }

                    //        model.PrivateSectorCategories = privateSectorCategories.OrderBy(x => x.Text).ToList();
                    //        return View(model);
                    //    }
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



    }

}
