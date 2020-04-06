using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Videos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class Product : BaseEntity
    {
        //test yorum satırı



        //private ICollection<ProductComplain> _productComplains;
        //private ICollection<ProductCatolog> _productCatologs;
        private ICollection<ProductComment> _productComments;
        //private ICollection<Video> _videos;
       

        public Product()
        {
            //this.Videos = new HashSet<Video>();
        }
        
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductGroupId { get; set; }
        public int? BrandId { get; set; }
        public int? SeriesId { get; set; }
        public int? ModelId { get; set; }
        public int? MainPartyId { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? LocalityId { get; set; }
        public int? TownId { get; set; }
        public int? DistrictId { get; set; }
        public byte? CurrencyId { get; set; }
        public string ProductNo { get; set; }
        public string CategoryTreeName { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductSalesType { get; set; }
        public string ProductDescription { get; set; }
        public DateTime? ProductRecordDate { get; set; }
        public bool? ProductShowcase { get; set; }
        public DateTime? ProductAdvertBeginDate { get; set; }
        public DateTime? ProductAdvertEndDate { get; set; }
        public string ProductStatu { get; set; }
        public byte? ProductActiveType { get; set; }
        public string OtherBrand { get; set; }
        public string OtherModel { get; set; }
        public int? ModelYear { get; set; }
        public long? ViewCount { get; set; }
        public DateTime? LastViewDate { get; set; }
        public string BriefDetail { get; set; }
        public bool? ProductActive { get; set; }
        public bool? MoneyCondition { get; set; }
        public DateTime? ProductLastUpdate { get; set; }
        public long? SingularViewCount { get; set; }
        public decimal? productrate { get; set; }
        public int? MenseiId { get; set; }
        public int? OrderStatus { get; set; }
        public bool? ReadyforSale { get; set; }
        public string WarrantyPeriod { get; set; }
        public string UnitType { get; set; }
        public bool Doping { get; set; }
        public int? Sort { get; set; }
        public bool HasVideo { get; set; }
        public decimal ?ProductPriceBegin { get; set; }
        public decimal? ProductPriceLast { get; set; }
        public byte? ProductPriceType { get; set; }
        public bool? ChoicedForCategoryIndex { get; set; }
        public bool? Fob { get; set; }
        public bool? Kdv { get; set; }
        public DateTime? ProductDopingBeginDate { get; set; }
        public DateTime? ProductDopingEndDate { get; set; }

        public decimal? ProductRateWithDoping { get; set; }
        public string ProductSellUrl { get; set; }
        public string Keywords { get; set; }
        public byte? ProductHomePageOrder { get; set; }
        public decimal ? ProductPriceWithDiscount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public byte? DiscountType { get; set; }
      

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Locality Locality { get; set; }
        public virtual Town Town { get; set; }
        public virtual District District { get; set; }

        public virtual Category Category { get; set; }
        public virtual Category Brand { get; set; }
        public virtual Category Model { get; set; }


        //public virtual ICollection<ProductComplain> ProductComplains
        //{
        //    get { return _productComplains ?? (_productComplains = new List<ProductComplain>()); }
        //    protected set { _productComplains = value; }
        //}

        public virtual ICollection<ProductComment> ProductComments
        {
            get { return _productComments ?? (_productComments = new List<ProductComment>()); }
            protected set { _productComments = value; }
        }

        //public virtual ICollection<ProductCatolog> ProductCatologs
        //{
        //    get { return _productCatologs ?? (_productCatologs = new List<ProductCatolog>()); }
        //    protected set { _productCatologs = value; }
        //}


        //public virtual ICollection<Video> Videos
        //{
        //    get { return _videos ?? (_videos = new List<Video>()); }
        //    protected set { _videos = value; }
        //}

        //public virtual Constant ConstantProductType { get; set; }
        //public virtual Constant ConstantProductStatu { get; set; }
        //public virtual Constant ConstantProductSalesType { get; set; }
        //public virtual Constant ConstantBriefDetail { get; set; }
        //public virtual Constant ConstantMensei { get; set; }
        //public virtual Constant ConstantOrderStatus { get; set; }

    }
}
