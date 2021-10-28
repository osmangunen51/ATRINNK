using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using System;

namespace NeoSistem.MakinaTurkiye.Web.Factories
{
    public class ProductModelFactory : IProductModelFactory
    {
        IPictureService _pictureService;
        IMemberStoreService _memberStoreService;
        IStoreService _storeService;



        public ProductModelFactory(IPictureService pictureService,
            IMemberStoreService memberStoreService,
            IStoreService storeService)
        {
            this._pictureService = pictureService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
        }

        public MTCategoryProductModel PrepareProductAreaItemModel(Product product)
        {
            string picturePath = "";
            var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
            if (picture != null)
                picturePath = ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px200x150);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            var productModel = new MTCategoryProductModel
            {
                BrandId = product.BrandId,
                BrandName = product.Brand != null ? product.Brand.CategoryName : "",
                //BriefDetailText = product.BriefDetailText,
                CategoryId = product.CategoryId,

                MainPartyId = product.MainPartyId,
                ModelId = product.ModelId,
                ModelName = product.Model != null ? product.Model.CategoryName : "",
                ModelYear = product.ModelYear,
                PicturePath = picturePath,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductNo = product.ProductNo,
                //ProductSalesTypeText = product.ProductSalesTypeText,
                //ProductStatuText = product.ProductStatuText,
                //ProductTypeText = product.ProductTypeText,
                SeriesId = product.SeriesId,
                StoreName = store.StoreShortName,
                //ProductDescription = product.ProductDescription,
                CurrencyCss = product.GetCurrencyCssName(),
                Price = product.GetFormattedPrice(),
                ProductPriceType = product.ProductPriceType,
                HasVideo = product.HasVideo,
                StoreMainPartyId = memberStore.StoreMainPartyId,
                StoreUrl = UrlBuilder.GetStoreProfileUrl(Convert.ToInt32(memberStore.StoreMainPartyId), store.StoreName, store.StoreUrlName),
                StoreConnectUrl = UrlBuilder.GetStoreConnectUrl(Convert.ToInt32(memberStore.StoreMainPartyId), store.StoreName, store.StoreUrlName),
                ProductContactUrl = UrlBuilder.GetProductContactUrl(product.ProductId, store.StoreName),
                KdvOrFobText = product.GetKdvOrFobText()
            };
            return productModel;
        }
    }
}