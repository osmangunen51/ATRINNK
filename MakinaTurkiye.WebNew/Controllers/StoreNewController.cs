using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.Mvc;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.StoreNews;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    [Compress]
    public class StoreNewController : BaseController
    {
        private readonly IStoreNewService _storeNewService;
        private readonly IPhoneService _phoneService;
        private readonly IStoreService _storeService;
        
        public StoreNewController(IStoreNewService storeNewService, IPhoneService phoneService, IStoreService storeService)
        {
            this._storeNewService = storeNewService;
            this._phoneService = phoneService;
            this._storeService = storeService;

            this._storeNewService.CachingGetOrSetOperationEnabled = true;
            _storeService.CachingGetOrSetOperationEnabled = true;
        }
        // GET: StoreNew
        public ActionResult Index(string page,byte? newType)
        {
            int p = 1;
            int pageDimension = 18;
            if (!string.IsNullOrEmpty(page)) p = Convert.ToInt32(page);
  
            if (newType == null)
                newType =1;

            var storeNews = _storeNewService.GetAllStoreNews(pageDimension,p, newType.Value);
            //if (newType == (byte)StoreNewType.Normal)
            //    SeoPageType = (byte)PageType.StoreNewHome;
            //else
            //    SeoPageType = (byte)PageType.SuccessStories;

            MTStoreNewModel model = new MTStoreNewModel();
            SearchModel<MTStoreNewItemModel> storeNewsModel = new SearchModel<MTStoreNewItemModel>();
            storeNewsModel.CurrentPage = p;
            storeNewsModel.TotalRecord = storeNews.TotalCount;
            storeNewsModel.PageDimension = pageDimension;

            var list = new List<MTStoreNewItemModel>();
            foreach (var item in storeNews)
            {

                string imagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px300x300.ToString());
                var store = _storeService.GetStoreByMainPartyId(item.StoreMainPartyId);

                list.Add(new MTStoreNewItemModel { DateString = item.RecordDate.ToString("dd MMM,yyyy", CultureInfo.InvariantCulture),
                    NewUrl = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title),
                    StoreName = store.StoreName, Title = item.Title,
                    StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName),
                    ImagePath=imagePath
                });

            }
            storeNewsModel.Source = list;
            model.MTStoreNews = storeNewsModel;
            ViewBag.Canonical = "https://haber.makinaturkiye.com";
            return View(model);
        }
      
        public ActionResult Detail(int? newId)
        {
            if (!newId.HasValue)
                return RedirectPermanent("/");

            var storeNew = _storeNewService.GetStoreNewByStoreNewId(newId.Value);

            //if (storeNew.NewType == (byte)StoreNewType.Normal)
            //    SeoPageType = (byte)PageType.StoreNewDetail;
            //else
            //    SeoPageType = (byte)PageType.SuccesstoriesDetail;

            //CreateSeoParameter("{HaberAdi}", storeNew.Title);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.FirmName, storeNew.Store.StoreName);

            var store = _storeService.GetStoreByMainPartyId(storeNew.StoreMainPartyId);

            MTNewDetailModel model = new MTNewDetailModel();
            model.Content = storeNew.Content;
            model.ImagePath = ImageHelper.GetStoreNewImagePath(storeNew.ImageName, StoreNewImageSize.px600x350.ToString());
            model.NewId = storeNew.Id;
            model.RecordDate = storeNew.RecordDate;
            model.Title = storeNew.Title;
            model.UpdateDate = storeNew.UpdateDate;

           
            MTNewStoreModel newStoreModel = new MTNewStoreModel();
            newStoreModel.StoreName = store.StoreShortName;
            newStoreModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
            newStoreModel.StoreLogoPath = ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 100);
            var phones = _phoneService.GetPhonesByMainPartyId(store.MainPartyId).Where(x => x.PhoneType == (byte)PhoneType.Gsm || x.PhoneType == (byte)PhoneType.Phone);
            foreach (var item in phones)
            {
                newStoreModel.Phones.Add(item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber);
            }

            model.NewStoreModel = newStoreModel;

            var storeNews = _storeNewService.GetStoreNewsTop(6,storeNew.NewType).Where(x => x.StoreNewId != newId);
            foreach (var item in storeNews)
            {
                var st = _storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                var imagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px100x100.ToString());
                if (string.IsNullOrEmpty(imagePath))
                    imagePath = ImageHelper.GetStoreLogoParh(st.MainPartyId, st.StoreLogo, 100);

                model.NewOthers.Add(new MTNewOtherItem
                {
                    ImagePath = imagePath,
                    NewUrl = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title),
                    Title = item.Title,
                    RecordDate = item.RecordDate
                });
            }

            ViewBag.Canonical =Request.Url.AbsolutePath.ToString();
            storeNew.ViewCount += 1;
            _storeNewService.UpdateStoreNew(storeNew);

            return View(model);
        }
    }
}