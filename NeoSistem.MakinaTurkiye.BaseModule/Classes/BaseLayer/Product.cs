namespace NeoSistem.MakinaTurkiye.Classes
{
    using EnterpriseEntity.Business;
    using EnterpriseEntity.Business.Attributes;
    using System;
    using System.Data;

    [Table("Product")]
    public partial class Product : EntityObject
    {
        [Column("ProductId", SqlDbType.Int, PrimaryKey = true, Identity = true)]
        public int ProductId { get; set; }

        [Column("ModelId", SqlDbType.Int)]
        public int? ModelId { get; set; }

        [Column("BrandId", SqlDbType.Int)]
        public int? BrandId { get; set; }

        [Column("SeriesId", SqlDbType.Int)]
        public int? SeriesId { get; set; }

        [Column("CategoryId", SqlDbType.Int)]
        public int CategoryId { get; set; }

        [Column("ProductGroupId", SqlDbType.Int)]
        public int ProductGroupId { get; set; }

        [Column("MainPartyId", SqlDbType.Int)]
        public int? MainPartyId { get; set; }

        [Column("CountryId", SqlDbType.Int)]
        public int? CountryId { get; set; }

        [Column("CityId", SqlDbType.Int)]
        public int? CityId { get; set; }

        [Column("LocalityId", SqlDbType.Int)]
        public int? LocalityId { get; set; }

        [Column("TownId", SqlDbType.Int)]
        public int? TownId { get; set; }

        [Column("DistrictId", SqlDbType.Int)]
        public int? DistrictId { get; set; }

        [Column("CurrencyId", SqlDbType.TinyInt)]
        public byte? CurrencyId { get; set; }

        [Column("ProductNo", SqlDbType.VarChar)]
        public string ProductNo { get; set; }

        [Column("CategoryTreeName", SqlDbType.NVarChar)]
        public string CategoryTreeName { get; set; }

        [Column("ProductName", SqlDbType.NVarChar)]
        public string ProductName { get; set; }

        [Column("ProductPrice", SqlDbType.Money)]
        public decimal ProductPrice { get; set; }

        [Column("ProductType", SqlDbType.VarChar)]
        public string ProductType { get; set; }

        [Column("MoneyCondition", SqlDbType.Bit)]
        public bool? MoneyCondition { get; set; }

        [Column("ProductDescription", SqlDbType.NText)]
        public string ProductDescription { get; set; }

        [Column("ProductRecordDate", SqlDbType.DateTime)]
        public DateTime ProductRecordDate { get; set; }

        [Column("ProductShowcase", SqlDbType.Bit)]
        public bool ProductShowcase { get; set; }

        [Column("ProductAdvertBeginDate", SqlDbType.Date)]
        public DateTime? ProductAdvertBeginDate { get; set; }

        [Column("ProductAdvertEndDate", SqlDbType.Date)]
        public DateTime? ProductAdvertEndDate { get; set; }

        [Column("ProductStatu", SqlDbType.VarChar)]
        public string ProductStatu { get; set; }

        [Column("ProductActiveType", SqlDbType.TinyInt)]
        public byte ProductActiveType { get; set; }

        [Column("ProductActive", SqlDbType.Bit)]
        public bool ProductActive { get; set; }

        [Column("OtherBrand", SqlDbType.VarChar)]
        public string OtherBrand { get; set; }

        [Column("OtherModel", SqlDbType.VarChar)]
        public string OtherModel { get; set; }

        [Column("ModelYear", SqlDbType.Int)]
        public int ModelYear { get; set; }

        [Column("ViewCount", SqlDbType.BigInt)]
        public long? ViewCount { get; set; }

        [Column("SingularViewCount", SqlDbType.BigInt)]
        public long? SingularViewCount { get; set; }

        [Column("LastViewDate", SqlDbType.DateTime)]
        public DateTime? LastViewDate { get; set; }

        [Column("BriefDetail", SqlDbType.VarChar)]
        public string BriefDetail { get; set; }

        [Column("ProductSalesType", SqlDbType.VarChar)]
        public string ProductSalesType { get; set; }

        [Column("ProductLastUpdate", SqlDbType.DateTime)]
        public DateTime ProductLastUpdate { get; set; }

        [Column("ReadyforSale", SqlDbType.Bit)]
        public bool ReadyforSale { get; set; }

        [Column("MenseiId", SqlDbType.Int)]
        public int? MenseiId { get; set; }

        [Column("OrderStatus", SqlDbType.Int)]
        public int? OrderStatus { get; set; }

        [Column("WarrantyPeriod", SqlDbType.NVarChar)]
        public string WarrantyPeriod { get; set; }
        [Column("UnitType", SqlDbType.NVarChar)]
        public string UnitType { get; set; }

        [Column("Doping", SqlDbType.Bit)]
        public bool Doping { get; set; }

        [Column("HasVideo", SqlDbType.Bit)]
        public bool HasVideo { get; set; }

        [Column("ProductPriceType", SqlDbType.TinyInt)]
        public byte ProductPriceType { get; set; }

        [Column("ChoicedForCategoryIndex", SqlDbType.Bit)]
        public bool? ChoicedForCategoryIndex { get; set; }
        
        [Column("ProductPriceBegin", SqlDbType.Money)]
        public decimal? ProductPriceBegin { get; set; }


        [Column("ProductPriceLast", SqlDbType.Money)]
        public decimal ?ProductPriceLast { get; set; }

         [Column("Kdv", SqlDbType.Bit)]
        public bool? Kdv { get; set; }

         [Column("Fob", SqlDbType.Bit)]
        public bool? Fob { get; set; }

        [Column("ProductDopingBeginDate", SqlDbType.DateTime)]
        public DateTime? ProductDopingBeginDate { get; set; }

        [Column("ProductDopingEndDate", SqlDbType.DateTime)]
        public DateTime? ProductDopingEndDate { get; set; }

        [Column("ProductSellUrl", SqlDbType.NVarChar)]
        public string ProductSellUrl { get; set; }

        [Column("Keywords", SqlDbType.NVarChar)]
        public string Keywords { get; set; }
        [Column("DiscountAmount", SqlDbType.Money)]
        public decimal? DiscountAmount { get; set; }
        [Column("DiscountType", SqlDbType.TinyInt)]
        public byte? DiscountType { get; set; }
        [Column("ProductPriceWithDiscount", SqlDbType.Money)]
        public decimal? ProductPriceWithDiscount { get; set; }

    }


}
