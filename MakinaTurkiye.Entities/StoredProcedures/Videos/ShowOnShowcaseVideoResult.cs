using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Videos
{
    public class ShowOnShowcaseVideoResult
    {
        public int VideoId { get; set; }
        public int ProductId { get; set; }
        public string VideoPicturePath { get; set; }
        public DateTime VideoRecordDate { get; set; }
        public long SingularViewCount { get; set; }
        public string CategoryName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int? ModelId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public byte? Minute { get; set; }
        public byte? Second { get; set; }
        public string StoreUrlName { get; set; }
 
    }
}
