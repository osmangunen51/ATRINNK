using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductDetailViewModel
    {
        public MTProductDetailViewModel()
        {
            this.ProductDetailModel = new MTProductDetailModel();
            this.ProductPictureModels = new List<MTProductPictureModel>();
            this.ProductTabModel = new MTProductTabModel();
            this.SimilarProductModel = new MTSimilarProductModel();
            this.StoreOtherProductModel = new MTStoreOtherProductModel();
            this.ProductStoreModel = new MTProductStoreModel();
            this.ProductPageHeaderModel = new MTProductPageHeaderModel();
            this.ProductStoreMessageModel = new MTProductStoreMessageModel();
            this.MtJsonLdModel = new MTJsonLdModel();
            this.ProductContanctModel = new ProductContactModel();


        }

        public MTProductDetailModel ProductDetailModel { get; set; }
        public List<MTProductPictureModel> ProductPictureModels { get; set; }
        public MTProductTabModel ProductTabModel { get; set; }
        public MTSimilarProductModel SimilarProductModel { get; set; }
        public MTStoreOtherProductModel StoreOtherProductModel { get; set; }
        public MTProductStoreModel ProductStoreModel { get; set; }
        public MTProductPageHeaderModel ProductPageHeaderModel { get; set; }
        public MTProductStoreMessageModel ProductStoreMessageModel { get; set; }
        public MTProductComplainModel ProductComplainModel { get; set; }
        public ProductContactModel ProductContanctModel { get; set; }
        public MTJsonLdModel MtJsonLdModel { get; set; }

        public bool OnlyStoreSee { get; set; }
        public bool ProductActive { get; set; }
    }
}