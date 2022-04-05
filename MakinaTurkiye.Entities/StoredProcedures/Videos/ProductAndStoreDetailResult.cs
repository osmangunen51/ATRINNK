namespace MakinaTurkiye.Entities.StoredProcedures.Videos
{
    public class ProductAndStoreDetailResult
    {
        public int? StoreMainPartyId { get; set; }
        public string StoreLogo { get; set; }
        public int MainPartyId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public int ModelYear { get; set; }
        public string StoreName { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductSalesTypeText { get; set; }
        public string BriefDetailText { get; set; }
        public string StoreCityName { get; set; }
        public string MemberCityName { get; set; }
        public string StoreLocalityName { get; set; }
        public string MemberLocalityName { get; set; }
        public string StoreUrlName { get; set; }
        public bool ProductActive { get; set; }


    }
}
