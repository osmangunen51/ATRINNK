using NeoSistem.Trinnk.Web.Models.Catalog;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Products
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
            this.ProductComplainModel = new MTProductComplainModel();

        }


        public class MTProductComplainModel
        {

            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public string UserEmail { get; set; }
            public string UserPhone { get; set; }
            public string UserComment { get; set; }

            public List<ProductComplainTypeView> ProductComplainTypeList { get; set; }
        }


        public class ProductComplainTypeView
        {
            public int ProductComplainTypeId { get; set; }
            public string Name { get; set; }
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