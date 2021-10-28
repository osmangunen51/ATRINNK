using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Utilities.Controllers;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using System;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class ProductCreateSettingController : BaseAccountController
    {
        IStoreProductCreateSettingService _storeCreateProductSettingService;
        IMemberStoreService _memberStoreService;
        IConstantService _constantService;



        public ProductCreateSettingController(IStoreProductCreateSettingService storeCreateProductSettingService,
            IMemberStoreService memberStoreService,
            IConstantService constantService)
        {
            this._storeCreateProductSettingService = storeCreateProductSettingService;
            this._memberStoreService = memberStoreService;
            this._constantService = constantService;

        }

        public ActionResult Index()
        {
            ProductCreateSettingModel model = new ProductCreateSettingModel();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);

            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.Settings);

            var productSettingProperties = _storeCreateProductSettingService.GetStoreProductCreateProperties();
            model.Properties.Add(new SelectListItem { Text = "< Seçiniz >", Value = "0", Selected = true });
            foreach (var productSettingPropertie in productSettingProperties)
            {
                model.Properties.Add(new SelectListItem
                {
                    Text = productSettingPropertie.Title,
                    Value = productSettingPropertie.StoreProductCreatePropertieId.ToString()
                });

            }
            model.StoreMainPartyId = memberStore.StoreMainPartyId.Value;

            var productSettings = _storeCreateProductSettingService.GetStoreProductCreateSettingsByStoreMainPartyId(memberStore.StoreMainPartyId.Value);
            foreach (var productSetting in productSettings)
            {
                var productPropertie = _storeCreateProductSettingService.GetStoreProductCreatePropertieById(productSetting.StoreProductCreatePropertieId);
                var constant = _constantService.GetConstantByConstantId(Convert.ToInt16(productSetting.Value));

                ProductCreateSettingItem itemModel = new ProductCreateSettingItem();
                itemModel.CreatedDate = productSetting.CreatedDate;
                itemModel.Title = productPropertie.Title;
                itemModel.Value = constant != null ? constant.ConstantName : productSetting.Value;
                model.ProductCreateSettingItems.Add(itemModel);
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetConstants(int propertieId)
        {
            var productSettingPropertie = _storeCreateProductSettingService.GetStoreProductCreatePropertieById(propertieId);
            var constants = _constantService.GetConstantByConstantType((ConstantTypeEnum)productSettingPropertie.ConstantType);
            string text = "";
            foreach (var item in constants)
            {
                text += String.Format("<option value='{0}'>{1}</option>", item.ConstantId, item.ConstantName);
            }
            return Json(new { content = text }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(int propertieId, int storeMainPartyId, string value)
        {
            var productCreateSetting = new StoreProductCreateSetting();
            productCreateSetting.StoreMainPartyId = storeMainPartyId;
            productCreateSetting.Value = value;
            productCreateSetting.StoreProductCreatePropertieId = propertieId;
            productCreateSetting.CreatedDate = DateTime.Now;
            _storeCreateProductSettingService.InsertStoreProductCreateSetting(productCreateSetting);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}